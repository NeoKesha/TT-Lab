using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using TT_Lab.AssetData;
using TT_Lab.Assets;
using TT_Lab.Rendering;
using TT_Lab.Rendering.Shaders;
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
            SceneRenderer.CloseEditor();

            base.CloseEditor(sender, e);
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
    }
}
