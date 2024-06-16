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
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Project.Messages;
using TT_Lab.Rendering;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;
using TT_Lab.ViewModels.Editors.Instance;
using TT_Lab.ViewModels.Interfaces;

namespace TT_Lab.ViewModels.Editors
{
    public class ChunkEditorViewModel :
        Conductor<IScreen>.Collection.AllActive,
        IEditorViewModel,
        IHandle<RendererInitializedMessage>,
        IHandle<ChangeRenderCameraPositionMessage>
    {
        private BindableCollection<ResourceTreeElementViewModel> chunkTree = new();
        private bool isDefault;

        private EditingContext editingContext;
        private List<SceneInstance> sceneInstances = new();
        private CollisionData? colData;
        private SceneEditorViewModel sceneEditor = IoC.Get<SceneEditorViewModel>();

        public ChunkEditorViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.SubscribeOnUIThread(this);
        }

        protected override Task OnDeactivateAsync(Boolean close, CancellationToken cancellationToken)
        {
            foreach (var item in Items)
            {
                DeactivateItemAsync(item, close, cancellationToken);
            }

            return base.OnDeactivateAsync(close, cancellationToken);
        }

        public SceneInstance NewSceneInstance(ObjectInstanceData instData)
        {
            Debug.Assert(sceneEditor.Scene != null, "Invalid editor state!");

            var sceneInstance = sceneEditor.Scene.AddObjectInstance(instData);
            var pRend = sceneInstance.GetRenderable();

            sceneEditor.Scene.AddChild(pRend);
            sceneInstances.Add(sceneInstance);

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
                CurrentInstanceEditor.ParentEditor = this;
                await ActivateItemAsync(CurrentInstanceEditor);
            }
            catch (Exception ex)
            {
                Log.WriteLine($"Failed to create editor: {ex.Message}");
            }
        }

        public BindableCollection<ResourceTreeElementViewModel> ChunkTree
        {
            get => chunkTree;
        }

        public LabURI EditableResource { get; set; } = LabURI.Empty;
        public SceneEditorViewModel SceneEditor { get => sceneEditor; set => sceneEditor = value; }
        public InstanceSectionResourceEditorViewModel? CurrentInstanceEditor { get; set; }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            ActivateItemAsync(sceneEditor, cancellationToken);

            return base.OnInitializeAsync(cancellationToken);
        }

        public void SaveChanges()
        {
            // TODO: Implement saving the scene/chunk
            return;
        }

        private void MouseMove(Object? sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }
            var pos = e.GetPosition(sceneEditor.RenderControl);
            editingContext.UpdateTransform((float)pos.X, (float)pos.Y);
        }

        private void MouseDown(Object? sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }
            var pos = e.GetPosition(sceneEditor.RenderControl);
            if (editingContext.transformMode == TransformMode.SELECTION || editingContext.transformAxis == TransformAxis.NONE)
            {
                MouseSelect((float)pos.X, (float)pos.Y);
            }
            else
            {
                editingContext.StartTransform((float)pos.X, (float)pos.Y);
            }
        }

        private void MouseUp(Object? sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(sceneEditor.RenderControl);
            editingContext.EndTransform((float)pos.X, (float)pos.Y);
        }

        private void KeyPressed(Object sender, KeyEventArgs arg)
        {
            var key = arg.Key;
            if (key == Key.M)
            {
                editingContext.ToggleSpace();
            }
            else if (key == Key.T)
            {
                editingContext.ToggleTranslate();
            }
            else if (key == Key.R)
            {
                editingContext.ToggleRotate();
            }
            else if (key == Key.X)
            {
                editingContext.SetTransformAxis(TransformAxis.X);
            }
            else if (key == Key.Y)
            {
                editingContext.SetTransformAxis(TransformAxis.Y);
            }
            else if (key == Key.Z)
            {
                editingContext.SetTransformAxis(TransformAxis.Z);
            }
            else if (key == Key.Left)
            {
                editingContext.MoveCursorGrid(-vec3.UnitX);
            }
            else if (key == Key.Right)
            {
                editingContext.MoveCursorGrid(vec3.UnitX);
            }
            else if (key == Key.Up)
            {
                editingContext.MoveCursorGrid(vec3.UnitZ);
            }
            else if (key == Key.Down)
            {
                editingContext.MoveCursorGrid(-vec3.UnitZ);
            }
            else if (key == Key.PageUp)
            {
                editingContext.MoveCursorGrid(vec3.UnitY);
            }
            else if (key == Key.PageDown)
            {
                editingContext.MoveCursorGrid(-vec3.UnitY);
            }
            else if (key == Key.K)
            {
                editingContext.SetPalette(editingContext.selectedInstance);
            }
            else if (key == Key.P)
            {
                editingContext.SpawnAtCursor();
            }
            else if (key == Key.G)
            {
                editingContext.SetGrid();
            }
        }

        private void MouseSelect(float x, float y)
        {
            if (sceneEditor.Scene == null)
            {
                return;
            }

            var inputController = IoC.Get<InputController>();
            var rayDirection = sceneEditor.Scene.GetRayFromViewport(x, y);
            var rayOrigin = sceneEditor.Scene.GetCameraPosition();

            editingContext.Deselect();
            SceneInstance result = null;
            if (!inputController.Ctrl)
            {
                foreach (var instance in sceneInstances)
                {
                    vec3 hit = new vec3();
                    float distance = 0.0f;
                    var worldPosition = instance.GetTransform() * new vec4(0, 0, 0, 1);
                    if (MathExtension.IntersectRayBox(rayOrigin, rayDirection, worldPosition.xyz, instance.GetOffset(), instance.GetSize(), instance.GetTransform(), ref distance, ref hit))
                    {
                        result = instance;
                        break;
                    }
                }
                editingContext.Select(result);
            }

            if (result == null && colData != null)
            {
                vec3 hit = new vec3();
                float distance = 0.0f;
                foreach (var triangle in colData.Triangles)
                {
                    var p1 = colData.Vectors[triangle.Face.Indexes[0]];
                    var p2 = colData.Vectors[triangle.Face.Indexes[1]];
                    var p3 = colData.Vectors[triangle.Face.Indexes[2]];
                    if (MathExtension.IntersectRayTriangle(rayOrigin, rayDirection, new vec3(-p1.X, p1.Y, p1.Z), new vec3(-p2.X, p2.Y, p2.Z), new vec3(-p3.X, p3.Y, p3.Z), ref distance, ref hit))
                    {
                        editingContext.SetCursorCoordinates(hit);
                        if (inputController.Ctrl)
                        {
                            editingContext.SpawnAtCursor();
                        }
                        break;
                    }
                }
            }
        }

        public Task HandleAsync(RendererInitializedMessage message, CancellationToken cancellationToken)
        {
            var assetManager = AssetManager.Get();
            var chunkAss = assetManager.GetAsset(EditableResource).GetResourceTreeElement();
            var chunk = chunkAss.GetAsset<ChunkFolder>();
            foreach (var item in chunk.GetData().To<FolderData>().Children)
            {
                chunkTree.Add(assetManager.GetAsset(item).GetResourceTreeElement());
            }
            isDefault = chunk.Name.ToLower() == "default";
            if (!isDefault)
            {
                try
                {
                    sceneEditor.SceneCreator = (GLWindow glControl) =>
                    {
                        var scene = new Scene(glControl.RenderContext, glControl, chunkTree, (float)glControl.RenderControl.Width, (float)glControl.RenderControl.Height);

                        editingContext = new EditingContext(glControl.RenderContext, glControl, scene, this);
                        colData = chunkTree.First((avm) =>
                        {
                            return avm.Asset.Type == typeof(Assets.Instance.Collision);
                        })!.Asset.GetData<CollisionData>();
                        var manager = AssetManager.Get();
                        var instances = chunkTree.First(avm => avm.Alias == "Instances");
                        foreach (var instance in instances!.Children)
                        {
                            var instData = instance.Asset.GetData<ObjectInstanceData>();
                            sceneInstances.Add(scene.AddObjectInstance(instData));
                        }

                        return scene;
                    };
                }
                catch (ShaderCompilationException ex)
                {
                    Log.WriteLine($"Error creating scene: {ex.Message}\n{ex.CompilerOutput}");
                }
                catch (Exception ex)
                {
                    Log.WriteLine($"Error creating scene: {ex.Message}");
                }
            }

            return Task.FromResult(true);
        }

        public Task HandleAsync(ChangeRenderCameraPositionMessage message, CancellationToken cancellationToken)
        {
            if (sceneEditor.Scene == null)
            {
                return Task.FromResult(false);
            }

            var renderWindow = sceneEditor.RenderControl?.GetRenderWindow();
            renderWindow?.DeferToRender(() =>
            {
                sceneEditor.Scene.SetCameraPosition(message.NewCameraPosition);
            });

            return Task.FromResult(true);
        }
    }
}
