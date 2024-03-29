﻿using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using TT_Lab.Rendering.Objects;
using TT_Lab.Util;
using TT_Lab.ViewModels.Graphics;

namespace TT_Lab.Editors.Graphics
{
    /// <summary>
    /// Interaction logic for TextureEditor.xaml
    /// </summary>
    public partial class TextureEditor : BaseEditor
    {

        private static RoutedCommand ReplaceCommand = new RoutedCommand();

        public TextureEditor()
        {
            InitializeComponent();
        }

        public TextureEditor(TextureViewModel texture) : base(texture)
        {
            InitializeComponent();
            TextureViewer_RendererInit();
            TextureViewer.FileDrop += TextureViewer_FileDrop;
            var repBinding = new CommandBinding(ReplaceCommand, ReplaceExecuted);
            CommandBindings.Add(repBinding);
            ReplaceButton.Command = repBinding.Command;
        }

        public override void CloseEditor(Object? sender, EventArgs e)
        {
            TextureViewer.CloseEditor();

            base.CloseEditor(sender, e);
        }

        private void ReplaceExecuted(Object sender, ExecutedRoutedEventArgs e)
        {
            var file = MiscUtils.GetFileFromDialogue("Image file|*.jpg;*.png;*.bmp");
            TextureViewer_FileDrop(this, new Controls.FileDropEventArgs { File = file });
        }

        private void TextureViewer_RendererInit()
        {
            ResetViewer();
        }

        private void ResetViewer()
        {
            TextureViewer.Scene = new Rendering.Scene((float)TextureViewer.Glcontrol.ActualWidth, (float)TextureViewer.Glcontrol.ActualHeight,
                new Rendering.Shaders.ShaderProgram.LibShader { Type = OpenTK.Graphics.OpenGL.ShaderType.FragmentShader, Path = "Shaders\\TexturePass.frag" });
            TextureViewer.Scene.SetCameraSpeed(0);
            TextureViewer.Scene.DisableCameraManipulation();
            var texPlane = new Plane(TextureViewer.Scene, ((TextureViewModel)viewModel).Texture);
            TextureViewer.Scene.AddRender(texPlane);
        }

        private void TextureViewer_Drop(Object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var file = (string[])e.Data.GetData(DataFormats.FileDrop);
                TextureViewer_FileDrop(sender, new Controls.FileDropEventArgs { File = file[0] });
            }
            else if (e.Data.GetDataPresent(typeof(Controls.DraggedData)))
            {
                var data = (Controls.DraggedData)e.Data.GetData(typeof(Controls.DraggedData));
                TextureViewer_FileDrop(sender, new Controls.FileDropEventArgs { Data = data });
            }
            else
            {
                Log.WriteLine("Format not compatible!");
                e.Effects = DragDropEffects.None;
            }
        }

        private static bool IsPowerOfTwo(long x)
        {
            return (x != 0) && ((x & (x - 1)) == 0);
        }

        private void TextureViewer_FileDrop(Object? sender, Controls.FileDropEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.File))
            {
                Bitmap image = new Bitmap(e.File);
                if (image.Width > 256 || image.Height > 256 || !IsPowerOfTwo(image.Width) || !IsPowerOfTwo(image.Height)
                    || image.Width < 8 || image.Height < 8)
                {
                    Log.WriteLine(@"Image is not compatible.
                * Width and height can't exceed 256 pixels
                * Width and height have to be a power of 2
                * Width and height can't be less than 8 pixels");
                    image.Dispose();
                    return;
                }
                SetData("Texture", image);
            }
            else if (e.Data != null)
            {
                try
                {
                    var texViewModel = (TextureViewModel)e.Data.Data;
                    SetData("Texture", texViewModel.Texture);
                    Log.WriteLine($"Replacing with texture: {texViewModel.Alias}");
                }
                catch (Exception)
                {
                    Log.WriteLine($"Unsupported texture");
                }
            }
            ResetViewer();
        }

        protected override void Control_UndoPerformed(Object sender, EventArgs e)
        {
            base.Control_UndoPerformed(sender, e);
            ResetViewer();
        }

        protected override void Control_RedoPerformed(Object sender, EventArgs e)
        {
            base.Control_RedoPerformed(sender, e);
            ResetViewer();
        }
    }
}
