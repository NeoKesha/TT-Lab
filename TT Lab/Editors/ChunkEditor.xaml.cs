﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TT_Lab.AssetData;
using TT_Lab.Assets;
using TT_Lab.Controls;
using TT_Lab.Project;
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
            var chunk = (ChunkFolder)chunkAss.Asset;
            foreach (var item in ((FolderData)chunk.GetData()).Children)
            {
                chunkTree.Add(ProjectManagerSingleton.PM.OpenedProject.GetAsset(item).GetViewModel());
            }
            DataContext = new { Items = chunkTree };
            isDefault = chunk.Name.ToLower() == "default";
            InitializeComponent();
            if (!isDefault)
            {
                Task.Factory.StartNew(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        SceneRenderer.Glcontrol.MakeCurrent();
                        try
                        {
                            SceneRenderer.Scene = new Scene(chunkTree, (float)SceneRenderer.GLHost.ActualWidth, (float)SceneRenderer.GLHost.ActualHeight);
                        }
                        catch (ShaderCompilationException ex)
                        {
                            Log.WriteLine($"Error creating scene: {ex.Message}\n{ex.CompilerOutput}");
                        }
                    });
                });
            }
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
