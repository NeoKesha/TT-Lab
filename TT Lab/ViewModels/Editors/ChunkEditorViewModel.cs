using Caliburn.Micro;
using GlmSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using org.ogre;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Graphics;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Assets.Instance;
using TT_Lab.Extensions;
using TT_Lab.Project.Messages;
using TT_Lab.Rendering;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Objects;
using TT_Lab.Rendering.Objects.SceneInstances;
using TT_Lab.Util;
using TT_Lab.ViewModels.Editors.Instance;
using TT_Lab.ViewModels.Interfaces;
using TT_Lab.ViewModels.ResourceTree;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Camera = TT_Lab.Rendering.Objects.Camera;
using Collision = TT_Lab.Rendering.Objects.Collision;
using Position = TT_Lab.Rendering.Objects.Position;
using Scenery = TT_Lab.Rendering.Objects.Scenery;
using Trigger = TT_Lab.Rendering.Objects.Trigger;

namespace TT_Lab.ViewModels.Editors
{
    public class ChunkEditorViewModel :
        Conductor<IScreen>.Collection.AllActive,
        IEditorViewModel,
        IHandle<ChangeRenderCameraPositionMessage>,
        IInputListener,
        IDirtyMarker
    {
        private readonly BindableCollection<ResourceTreeElementViewModel> _chunkTree = new();
        private bool _isDefault;
        private bool _isDirty = false;
        private DirtyTracker _dirtyTracker;

        private EditingContext _editingContext;
        private readonly List<SceneInstance> _sceneInstances = new();
        private CollisionData? _colData;
        private SceneEditorViewModel _sceneEditor = IoC.Get<SceneEditorViewModel>();
        private readonly InputController _inputController;
        private Collision _collisionRender;
        private Scenery _sceneryRender;
        private Skydome _skydomeRender;
        private SceneNode _instancesNode;
        private DrawFilter _drawFilter = DrawFilter.Scenery | DrawFilter.Triggers | DrawFilter.Positions | DrawFilter.Instances | DrawFilter.Cameras | DrawFilter.Skybox;

        [Flags]
        private enum DrawFilter
        {
            Disabled = 0,
            Scenery = 1 << 0,
            Collision = 1 << 1,
            Instances = 1 << 2,
            Positions = 1 << 3,
            Triggers = 1 << 4,
            Cameras = 1 << 5,
            Skybox = 1 << 6,
            Paths = 1 << 7,
            AiPositions = 1 << 8,
            AiPaths = 1 << 9,
            DynamicScenery = 1 << 10,
            Lighting = 1 << 11,
        }

        public ChunkEditorViewModel(IEventAggregator eventAggregator)
        {
            _sceneEditor.SceneHeaderModel = "Chunk Viewer";
            _inputController = new InputController(_sceneEditor);
            eventAggregator.SubscribeOnUIThread(this);
            _dirtyTracker = new DirtyTracker(this);
            InitScene();
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _sceneEditor.AddInputListener(this);
            
            return base.OnActivateAsync(cancellationToken);
        }

        protected override Task OnDeactivateAsync(Boolean close, CancellationToken cancellationToken)
        {
            _sceneEditor.RemoveInputListener(this);
            
            foreach (var item in Items.Select(s => s).ToArray())
            {
                DeactivateItemAsync(item, close, cancellationToken);
            }

            return base.OnDeactivateAsync(close, cancellationToken);
        }

        public SceneInstance NewSceneInstance(ObjectInstanceData instData)
        {
            Debug.Assert(_sceneEditor.RenderControl?.GetRenderWindow() != null, "Invalid editor state!");

            var sceneInstance = new ObjectSceneInstance(_sceneEditor.RenderControl.GetRenderWindow(), instData);
            _sceneInstances.Add(sceneInstance);
            _instancesNode.addChild(sceneInstance.GetRenderable().getParentSceneNode());

            //TODO: actually add instance
            return sceneInstance;
        }

        public async void InstanceEditorChanged(RoutedPropertyChangedEventArgs<Object> e)
        {
            if (e.NewValue == null) return;
            var asset = (ResourceTreeElementViewModel)e.NewValue;
            if (asset.Asset.Type == typeof(Folder)) return;

            try
            {
                if (CurrentInstanceEditor != null)
                {
                    await DeactivateItemAsync(CurrentInstanceEditor, true);
                }

                CurrentInstanceEditor = (InstanceSectionResourceEditorViewModel)IoC.GetInstance(asset.Asset.GetEditorType(), null);
                CurrentInstanceEditor.EditableResource = asset.Asset.URI;
                CurrentInstanceEditor.ParentEditor = this;
                await ActivateItemAsync(CurrentInstanceEditor);
                NotifyOfPropertyChange(nameof(CurrentInstanceEditor));
            }
            catch (Exception ex)
            {
                Log.WriteLine($"Failed to create editor: {ex.Message}");
            }
        }

        public BindableCollection<ResourceTreeElementViewModel> ChunkTree
        {
            get => _chunkTree;
        }

        public LabURI EditableResource { get; set; } = LabURI.Empty;
        public SceneEditorViewModel SceneEditor { get => _sceneEditor; set => _sceneEditor = value; }
        public InstanceSectionResourceEditorViewModel? CurrentInstanceEditor { get; set; }

        public bool IsDirty
        {
            get => _isDirty;
            set
            {
                if (_isDirty != value)
                {
                    _isDirty = value;
                    NotifyOfPropertyChange();
                }
            }
        }
        
        public void ResetDirty()
        {
            _dirtyTracker.ResetDirty();
            IsDirty = false;
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            ActivateItemAsync(_sceneEditor, cancellationToken);

            return base.OnInitializeAsync(cancellationToken);
        }

        public void SaveChanges()
        {
            // TODO: Implement saving the scene/chunk
            return;
        }

        public bool MouseMove(Object? sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed || !_editingContext.IsInstanceSelected())
            {
                return false;
            }
            
            var pos = e.GetPosition(_sceneEditor.RenderControl);
            _editingContext.UpdateTransform((float)pos.X, (float)pos.Y);
            return true;
        }

        public bool MouseDown(Object? sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return false;
            }
            
            var pos = e.GetPosition(_sceneEditor.RenderControl);
            if ((_editingContext.TransformMode == TransformMode.SELECTION || _editingContext.TransformAxis == TransformAxis.NONE) && !_editingContext.IsInstanceSelected())
            {
                MouseSelect((float)pos.X, (float)pos.Y);
            }
            else if (_editingContext.IsInstanceSelected())
            {
                _editingContext.StartTransform((float)pos.X, (float)pos.Y);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool MouseUp(Object? sender, MouseButtonEventArgs e)
        {
            if (!_editingContext.IsInstanceSelected())
            {
                return false;
            }
            
            var pos = e.GetPosition(_sceneEditor.RenderControl);
            _editingContext.EndTransform((float)pos.X, (float)pos.Y);
            return true;
        }

        public bool KeyPressed(Object sender, KeyEventArgs arg)
        {
            if (arg.IsRepeat || arg.IsUp || !_editingContext.IsInstanceSelected())
            {
                return false;
            }
            
            var key = arg.Key;
            if (key == Key.T)
            {
                _editingContext.ToggleTranslate();
            }
            else if (key == Key.R)
            {
                _editingContext.ToggleRotate();
            }
            else if (key == Key.X)
            {
                _editingContext.SetTransformAxis(TransformAxis.X);
            }
            else if (key == Key.Y)
            {
                _editingContext.SetTransformAxis(TransformAxis.Y);
            }
            else if (key == Key.Z)
            {
                _editingContext.SetTransformAxis(TransformAxis.Z);
            }
            else if (key == Key.Left)
            {
                _editingContext.MoveCursorGrid(-vec3.UnitX);
            }
            else if (key == Key.Right)
            {
                _editingContext.MoveCursorGrid(vec3.UnitX);
            }
            else if (key == Key.Up)
            {
                _editingContext.MoveCursorGrid(vec3.UnitZ);
            }
            else if (key == Key.Down)
            {
                _editingContext.MoveCursorGrid(-vec3.UnitZ);
            }
            else if (key == Key.PageUp)
            {
                _editingContext.MoveCursorGrid(vec3.UnitY);
            }
            else if (key == Key.PageDown)
            {
                _editingContext.MoveCursorGrid(-vec3.UnitY);
            }
            else if (key == Key.K)
            {
                _editingContext.SetPalette(_editingContext.SelectedInstance);
            }
            else if (key == Key.P)
            {
                _editingContext.SpawnAtCursor();
            }
            else if (key == Key.G)
            {
                _editingContext.SetGrid();
            }
            else if (key == Key.U)
            {
                _editingContext.Deselect();
            }

            return true;
        }

        private void MouseSelect(float x, float y)
        {
            if (_sceneEditor.RenderControl?.GetRenderWindow() == null)
            {
                return;
            }

            var ray = _sceneEditor.RenderControl.GetRenderWindow().GetRayFromViewport(x, y);
            var rayOrigin = ray.getOrigin();
            var rayDirection = ray.getDirection();

            _editingContext.Deselect();
            SceneInstance? result = null;
            if (!_inputController.Ctrl)
            {
                foreach (var instance in _sceneInstances)
                {
                    if (!instance.GetRenderable().isVisible())
                    {
                        continue;
                    }
                    
                    var hit = new vec3();
                    var distance = 0.0f;
                    var worldPosition = instance.GetTransform() * new vec4(0, 0, 0, 1);
                    if (!MathExtension.IntersectRayBox(OgreExtensions.FromOgre(rayOrigin),
                            OgreExtensions.FromOgre(rayDirection), worldPosition.xyz, instance.GetOffset(),
                            instance.GetSize(), instance.GetTransform(), ref distance, ref hit))
                    {
                        continue;
                    }
                    
                    result = instance;
                    break;
                }

                if (result != null)
                {
                    _editingContext.Select(result);
                }
            }

            if (result == null && _colData != null)
            {
                var hit = new vec3();
                var distance = 0.0f;
                foreach (var triangle in _colData.Triangles)
                {
                    var p1 = _colData.Vectors[triangle.Face.Indexes[0]];
                    var p2 = _colData.Vectors[triangle.Face.Indexes[1]];
                    var p3 = _colData.Vectors[triangle.Face.Indexes[2]];
                    if (!MathExtension.IntersectRayTriangle(OgreExtensions.FromOgre(rayOrigin),
                            OgreExtensions.FromOgre(rayDirection), new vec3(-p1.X, p1.Y, p1.Z),
                            new vec3(-p2.X, p2.Y, p2.Z), new vec3(-p3.X, p3.Y, p3.Z), ref distance, ref hit))
                    {
                        continue;
                    }
                    
                    _editingContext.SetCursorCoordinates(hit);
                    if (_inputController.Ctrl)
                    {
                        _editingContext.SpawnAtCursor();
                    }
                    break;
                }
            }
        }

        private bool IsDrawFilterEnabled(DrawFilter filter)
        {
            return (_drawFilter & filter) == filter;
        }

        private void EnableDrawFilter(DrawFilter filter)
        {
            _drawFilter |= filter;
        }

        private void DisableDrawFilter(DrawFilter filter)
        {
            _drawFilter &= ~filter;
        }

        private void InitScene()
        {
            _sceneEditor.SceneCreator = glControl =>
            {
                var assetManager = AssetManager.Get();
                var task = assetManager.GetAsset(EditableResource).GetResourceTreeElement();
                task.Wait();
                var chunkAss = task.Result;
                var chunk = chunkAss.GetAsset<ChunkFolder>();
                foreach (var item in chunk.GetData().To<FolderData>().Children)
                {
                    task = assetManager.GetAsset(item).GetResourceTreeElement();
                    task.Wait();
                    _chunkTree.Add(task.Result);
                }
                _isDefault = chunk.Name.ToLower() == "default";
                if (_isDefault)
                {
                    return;
                }

                var sceneManager = glControl.GetSceneManager();
                glControl.EnableImgui(true);
                glControl.SetCameraStyle(CameraStyle.CS_FREELOOK);
                glControl.SetCameraSpeed(50.0f);
                glControl.SetCameraPosition(vec3.Zero);

                _editingContext = new EditingContext(glControl, sceneManager, this);
                
                // Currently all stuff is created as is from the data without linking it to view models
                // TODO: Link all that together so that changes in the editor reflect on the end data
                _colData = _chunkTree.First((avm) =>
                {
                    return avm.Asset.Type == typeof(Assets.Instance.Collision);
                })!.Asset.GetData<CollisionData>();

                _collisionRender = new Collision("CollisionData", sceneManager, _colData);
                
                var instances = _chunkTree.First(avm => avm.Alias == "Instances");
                _instancesNode = sceneManager.getRootSceneNode().createChildSceneNode();
                var instanceRenderObject = sceneManager.createManualObject();
                _instancesNode.attachObject(instanceRenderObject);
                foreach (var instance in instances!.Children)
                {
                    var instData = instance.Asset.GetData<ObjectInstanceData>();
                    var objSceneInstance = new ObjectSceneInstance(glControl, instData);
                    _sceneInstances.Add(objSceneInstance);
                    _instancesNode.addChild(objSceneInstance.GetRenderable().getParentSceneNode());
                }
                
                var scenery = _chunkTree.First(avm => avm.Asset.Section == Constants.SCENERY_SECENERY_ITEM).Asset.GetData<SceneryData>();
                if (scenery.SkydomeID != LabURI.Empty)
                {
                    _skydomeRender = new Skydome("SkydomeRender", sceneManager, assetManager.GetAssetData<SkydomeData>(scenery.SkydomeID));
                }

                _sceneryRender = new Scenery("SceneryRender", sceneManager, scenery);
                
                var triggers = _chunkTree.First(avm => avm.Alias == "Triggers");
                var triggersNode = sceneManager.getRootSceneNode().createChildSceneNode();
                triggersNode.attachObject(_editingContext.GetTriggersBillboards());
                foreach (var trigger in triggers!.Children)
                {
                    var billboard = _editingContext.CreateTriggerBillboard();
                    var trg = new Trigger(trigger.Alias, triggersNode, sceneManager, billboard, trigger.Asset.GetData<TriggerData>());
                    triggersNode.attachObject(trg);
                }
                
                var positions = _chunkTree.First(avm => avm.Alias == "Positions");
                var positionsNode = sceneManager.getRootSceneNode().createChildSceneNode();
                positionsNode.attachObject(_editingContext.GetPositionBillboards());
                foreach (var position in positions!.Children)
                {
                    var billboard = _editingContext.CreatePositionBillboard();
                    var pos = new Position(position.Alias, sceneManager, billboard, position.Asset.LayoutID!.Value, position.Asset.GetData<PositionData>());
                    positionsNode.attachObject(pos);
                }
                
                var cameras = _chunkTree.First(avm => avm.Alias == "Cameras");
                var camerasNode = sceneManager.getRootSceneNode().createChildSceneNode();
                camerasNode.attachObject(_editingContext.GetCamerasBillboards());
                foreach (var camera in cameras!.Children)
                {
                    var billboard = _editingContext.CreateCameraBillboard();
                    var cam = new Camera($"{camera.Alias}_{camera.Asset.URI}", camerasNode, sceneManager, billboard, camera.Asset.GetData<CameraData>());
                    camerasNode.attachObject(cam);
                }

                glControl.OnRender += (sender, args) =>
                {
                    ImGui.Begin("Chunk Render Settings");
                    ImGui.SetWindowPos(new ImVec2(glControl.GetViewportWidth() - 300, 5));
                    ImGui.SetWindowSize(new ImVec2(295, 200));
                    ImguiRenderFilterCheckbox("Render Collision", _collisionRender, DrawFilter.Collision);
                    ImguiRenderFilterCheckbox("Render Scenery", _sceneryRender, DrawFilter.Scenery);
                    ImguiRenderFilterCheckbox("Render Skydome", _skydomeRender, DrawFilter.Skybox);
                    ImguiRenderFilterCheckbox("Render Positions", _editingContext.GetPositionBillboards(), DrawFilter.Positions);
                    ImguiRenderFilterCheckbox("Render Triggers", _editingContext.GetTriggersBillboards(), DrawFilter.Triggers);
                    ImguiRenderFilterCheckbox("Render Cameras", _editingContext.GetCamerasBillboards(), DrawFilter.Cameras);
                    ImguiRenderFilterCheckbox("Render Instances", instanceRenderObject, DrawFilter.Instances);
                    ImGui.End();

                    if (_editingContext.IsInstanceSelected())
                    {
                        ImguiRenderControls(glControl);
                    }
                };
            };
        }

        private void ImguiRenderControls(OgreWindow glControl)
        {
            ImGui.Begin("Editor Controls");
            ImGui.SetWindowPos(new ImVec2(5, glControl.GetViewportHeight() - 400));
            ImGui.SetWindowSize(new ImVec2(300, 395));
            ImGui.Text("U - Unselect instance");
            ImGui.Text("T - Toggle translate");
            ImGui.Text("R - Toggle rotate");
            ImGui.Text("X - Edit on X axis");
            ImGui.Text("Y - Edit on Y axis");
            ImGui.Text("Z - Edit on Z axis");
            ImGui.Text("G - Move edit cursor on a grid");
            ImGui.Text("P - Create instance at cursor's position");
            ImGui.Text("K - Add current instance to palette");
            ImGui.End();
        }

        private void ImguiRenderFilterCheckbox(string label, MovableObject renderObject, DrawFilter filter)
        {
            var renderEnabled = IsDrawFilterEnabled(filter);
            if (ImGui.Checkbox(label, ref renderEnabled) && !renderObject.isVisible())
            {
                renderObject.getParentSceneNode().setVisible(true, true);
                EnableDrawFilter(filter);
            }
            else if (!renderEnabled && renderObject.isVisible())
            {
                renderObject.getParentSceneNode().setVisible(false, true);
                DisableDrawFilter(filter);
            }
        }

        public Task HandleAsync(ChangeRenderCameraPositionMessage message, CancellationToken cancellationToken)
        {
            if (_sceneEditor.RenderControl?.GetRenderWindow() == null)
            {
                return Task.FromResult(false);
            }

            var renderWindow = _sceneEditor.RenderControl?.GetRenderWindow()!;
            renderWindow.SetCameraPosition(message.NewCameraPosition);

            return Task.FromResult(true);
        }
    }
}
