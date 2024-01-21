using GlmSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Rendering;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;
using TT_Lab.ViewModels;

namespace TT_Lab.Editors
{
    /// <summary>
    /// Interaction logic for ChunkEditor.xaml
    /// </summary>
    public partial class ChunkEditor : BaseEditor
    {
        private List<AssetViewModel> chunkTree = new List<AssetViewModel>();
        private bool isDefault;

        private EditingContext editingContext;
        private List<SceneInstance> sceneInstances = new List<SceneInstance>();
        private CollisionData colData;
        //Control keys handling
        private List<Key> pressedKeys = new();
        private bool leftShift = false;
        private bool rightShift = false;
        private bool leftCtrl = false;
        private bool rightCtrl = false;
        private bool leftAlt = false;
        private bool rightAlt = false;

        private bool Shift { get => leftShift | rightShift; }
        private bool Ctrl { get => leftCtrl | rightCtrl; }
        private bool Alt { get => leftAlt | rightAlt; }

        public ChunkEditor() : this(null)
        {
            InitializeComponent();
        }

        public ChunkEditor(AssetViewModel chunkAss) : base(chunkAss)
        {
            var chunk = chunkAss.GetAsset<ChunkFolder>();
            foreach (var item in chunk.GetData().To<FolderData>().Children)
            {
                chunkTree.Add(AssetManager.Get().GetAsset(item).GetViewModel());
            }
            DataContext = new ChunkViewModel(chunkTree);
            isDefault = chunk.Name.ToLower() == "default";
            InitializeComponent();
            if (!isDefault)
            {
                Task.Factory.StartNew(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        try
                        {
                            SceneRenderer.Scene = new Scene(chunkTree, (float)SceneRenderer.Glcontrol.ActualWidth, (float)SceneRenderer.Glcontrol.ActualHeight);
                            SceneRenderer.Glcontrol.MouseMove += MouseMove;
                            SceneRenderer.Glcontrol.MouseUp += MouseUp;
                            SceneRenderer.Glcontrol.MouseDown += MouseDown;
                            SceneRenderer.Glcontrol.KeyDown += KeyUp;
                            SceneRenderer.Glcontrol.KeyUp += KeyDown;
                            editingContext = new EditingContext(SceneRenderer.Scene, this);
                            colData = chunkTree.Find((avm) =>
                            {
                                return avm.Asset.Type == typeof(Assets.Instance.Collision);
                            })!.Asset.GetData<CollisionData>();
                            var manager = AssetManager.Get();
                            var instances = chunkTree.Find(avm => avm.Alias == "Instances");
                            foreach (var instance in instances!.Children)
                            {
                                var instData = instance.Asset.GetData<ObjectInstanceData>();
                                sceneInstances.Add(SceneRenderer.Scene.AddObjectInstance(instData));
                            }
                        }
                        catch (ShaderCompilationException ex)
                        {
                            Log.WriteLine($"Error creating scene: {ex.Message}\n{ex.CompilerOutput}");
                        }
                        catch (Exception ex)
                        {
                            Log.WriteLine($"Error creating scene: {ex.Message}");
                        }
                    });
                });
            }
        }

        public override void CloseEditor(Object? sender, EventArgs e)
        {
            SceneRenderer.Glcontrol.MouseMove -= MouseMove;
            SceneRenderer.Glcontrol.MouseUp -= MouseUp;
            SceneRenderer.Glcontrol.MouseDown -= MouseDown;
            SceneRenderer.Glcontrol.KeyDown -= KeyUp;
            SceneRenderer.Glcontrol.KeyUp -= KeyDown;
            SceneRenderer.CloseEditor();

            base.CloseEditor(sender, e);
        }

        public SceneInstance NewSceneInstance(ObjectInstanceData instData)
        {
            var sceneInstance = SceneRenderer.Scene.AddObjectInstance(instData);
            var pRend = sceneInstance.GetRenderable();
        
            SceneRenderer.Scene.AddRender(pRend);
            sceneInstances.Add(sceneInstance);

            //TODO: actually add instance
            return sceneInstance;
        }

        private void ChunkTree_SelectedItemChanged(Object sender, RoutedPropertyChangedEventArgs<Object> e)
        {
            if (e.NewValue == null) return;
            var asset = (AssetViewModel)e.NewValue;
            if (asset.Asset.Type == typeof(Folder)) return;

            try
            {
                var editor = asset.GetEditor(CommandManager);
                ((BaseEditor)editor).ParentEditor = this;
                if (EditorScroll.Content != null)
                {
                    EditorScroll.Content = null;
                }
                EditorScroll.Content = editor;
            }
            catch (Exception ex)
            {
                Log.WriteLine($"Failed to create editor: {ex.Message}");
            }
        }

        public List<AssetViewModel> ChunkTree
        {
            get => chunkTree;
        }

        private void MouseMove(Object? sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }
            var pos = e.GetPosition(SceneRenderer.Glcontrol);
            editingContext.UpdateTransform((float)pos.X, (float)pos.Y);
        }

        private void MouseDown(Object? sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }
            var pos = e.GetPosition(SceneRenderer.Glcontrol);
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
            var pos = e.GetPosition(SceneRenderer.Glcontrol);
            editingContext.EndTransform((float)pos.X, (float)pos.Y);
        }

        private void KeyDown(Object? sender, KeyEventArgs e)
        {
            var key = e.Key;
            if (pressedKeys.Contains(key))
            {
                return;
            }

            pressedKeys.Add(key);
            if (key == Key.LeftAlt) { leftAlt = true; return; }
            if (key == Key.RightAlt) { rightAlt = true; return; }
            if (key == Key.LeftCtrl) { leftCtrl = true; return; }
            if (key == Key.RightCtrl) { rightCtrl = true; return; }
            if (key == Key.LeftShift) { leftShift = true; return; }
            if (key == Key.RightShift) { rightShift = true; return; }
            switch (key)
            {
                case Key.M:
                    editingContext.ToggleSpace();
                    break;
                case Key.T:
                    editingContext.ToggleTranslate();
                    break;
                case Key.R:
                    editingContext.ToggleRotate();
                    break;
                case Key.X:
                    editingContext.SetTransformAxis(TransformAxis.X);
                    break;
                case Key.Y:
                    editingContext.SetTransformAxis(TransformAxis.Y);
                    break;
                case Key.Z:
                    editingContext.SetTransformAxis(TransformAxis.Z);
                    break;
                case Key.K:
                    editingContext.SetPalette(editingContext.selectedInstance);
                    break;
                case Key.P:
                    editingContext.SpawnAtCursor();
                    break;
                    //case Key.None:
                    //    editingContext.transformMode = TransformMode.SCALE;
                    //    break;
            }
        }

        private void KeyUp(Object? sender, KeyEventArgs e)
        {
            var key = e.Key;
            if (!pressedKeys.Contains(key))
            {
                return;
            }

            pressedKeys.Remove(key);

            if (key == Key.LeftAlt) { leftAlt = false; return; }
            if (key == Key.RightAlt) { rightAlt = false; return; }
            if (key == Key.LeftCtrl) { leftCtrl = false; return; }
            if (key == Key.RightCtrl) { rightCtrl = false; return; }
            if (key == Key.LeftShift) { leftShift = false; return; }
            if (key == Key.RightShift) { rightShift = false; return; }
        }

        private void MouseSelect(float x, float y)
        {
            var rayDirection = SceneRenderer.Scene.GetRayFromViewport(x, y);
            var rayOrigin = SceneRenderer.Scene.GetCameraPosition();

            editingContext.Deselect();
            SceneInstance result = null;
            if (!Ctrl)
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

            if (result == null)
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
                        if (Ctrl)
                        {
                            editingContext.SpawnAtCursor();
                        }
                        break;
                    }
                }
            }
        }
    }
}
