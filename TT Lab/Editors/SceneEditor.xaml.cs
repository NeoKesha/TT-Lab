using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TT_Lab.Controls;
using TT_Lab.Rendering;

namespace TT_Lab.Editors
{
    /// <summary>
    /// Interaction logic for SceneEditor.xaml
    /// </summary>
    public partial class SceneEditor : System.Windows.Controls.UserControl
    {
        private List<Key> pressedKeys = new();
        private Point mousePos;

        public event EventHandler RendererInit;
        public event EventHandler<FileDropEventArgs> FileDrop;

        [Description("Scene viewer's header"), Category("Common Properties")]
        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(SceneEditor),
                new FrameworkPropertyMetadata("Scene viewer", FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnHeaderChanged)));

        public Scene? Scene
        {
            get => _scene;
            set
            {
                if (_scene != value)
                {
                    _scene = value;
                }
                if (_scene != null)
                {
                    _scene.RenderFramebuffer = Glcontrol.Framebuffer;
                }
            }
        }

        private Scene? _scene;

        public SceneEditor()
        {
            InitializeComponent();

            SizeChanged += SceneEditor_SizeChanged;

            Glcontrol.DragEnter += Glcontrol_DragEnter;
            Glcontrol.DragOver += Glcontrol_DragDrop;
            Glcontrol.MouseMove += Glcontrol_MouseMove;
            Glcontrol.KeyDown += Glcontrol_KeyDown;
            Glcontrol.KeyUp += Glcontrol_KeyUp;
            Glcontrol.MouseWheel += Glcontrol_MouseWheel;
            Glcontrol.MouseDown += Glcontrol_MouseClick;

            ContextMenu = new System.Windows.Controls.ContextMenu();
            var settings = new GLWpfControlSettings
            {
                MajorVersion = 4,
                MinorVersion = 3,
                GraphicsProfile = OpenTK.Windowing.Common.ContextProfile.Compatability,
                GraphicsContextFlags = OpenTK.Windowing.Common.ContextFlags.Debug
            };
            Glcontrol.Start(settings);
        }

        public void CloseEditor()
        {
            SizeChanged -= SceneEditor_SizeChanged;

            Glcontrol.DragEnter -= Glcontrol_DragEnter;
            Glcontrol.DragOver -= Glcontrol_DragDrop;
            Glcontrol.MouseMove -= Glcontrol_MouseMove;
            Glcontrol.KeyDown -= Glcontrol_KeyDown;
            Glcontrol.KeyUp -= Glcontrol_KeyUp;
            Glcontrol.MouseWheel -= Glcontrol_MouseWheel;
            Glcontrol.MouseDown -= Glcontrol_MouseClick;
            Glcontrol.Initialized -= Glcontrol_Init;
            Glcontrol.Render -= Glcontrol_Paint;

            Scene?.Delete();
        }

        private void Glcontrol_DragDrop(Object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                FileDrop?.Invoke(this, new FileDropEventArgs { File = files[0] });
            }
            else if (e.Data.GetDataPresent(typeof(DraggedData)))
            {
                var data = e.Data.GetData(typeof(DraggedData)) as DraggedData;
                FileDrop?.Invoke(this, new FileDropEventArgs { Data = data });
            }
        }

        private void Glcontrol_DragEnter(Object sender, DragEventArgs e)
        {
            if (AllowDrop)
            {
                e.Effects = DragDropEffects.All;
            }
        }

        private void Glcontrol_MouseWheel(Object? sender, MouseWheelEventArgs e)
        {
            Scene?.ZoomView(e.Delta);
        }

        private void Glcontrol_KeyUp(Object? sender, KeyEventArgs e)
        {
            if (pressedKeys.Contains(e.Key))
            {
                pressedKeys.Remove(e.Key);
            }
        }

        private void Glcontrol_KeyDown(Object? sender, KeyEventArgs e)
        {
            if (!pressedKeys.Contains(e.Key))
            {
                pressedKeys.Add(e.Key);
            }
        }

        private void Glcontrol_MouseClick(Object? sender, MouseEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed && ContextMenu.Items.Count != 0)
            {
                ContextMenu.IsOpen = true;
            }
        }

        private void Glcontrol_MouseMove(Object? sender, MouseEventArgs e)
        {
            var curMousePos = e.GetPosition(Glcontrol);
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                Scene?.RotateView(new Vector2((float)(curMousePos.X - mousePos.X), (float)(mousePos.Y - curMousePos.Y)));
            }
            mousePos = curMousePos;
        }

        private void Glcontrol_Init(Object sender, EventArgs e)
        {
            RendererInit?.Invoke(sender, e);
        }

        private void Glcontrol_Paint(TimeSpan delta)
        {
            if (Scene != null)
            {
                Scene.RenderFramebuffer = Glcontrol.Framebuffer;
            }
            GL.ClearColor(Color4.LightGray);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            Scene?.PreRender();
            Scene?.Move(pressedKeys);
            Scene?.HandleInputs(pressedKeys);
            Scene?.Render();
            Scene?.PostRender();

            GL.Finish();
        }

        private void SceneEditor_SizeChanged(Object sender, SizeChangedEventArgs e)
        {
            Scene?.SetResolution((float)Glcontrol.ActualWidth, (float)Glcontrol.ActualHeight);
        }

        private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SceneEditor control)
            {
                control.SceneHeader.Header = e.NewValue;
            }
        }
    }
}
