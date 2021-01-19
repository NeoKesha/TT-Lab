using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Controls;
using TT_Lab.Rendering;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;
using TT_Lab.ViewModels;

namespace TT_Lab.Editors
{
    /// <summary>
    /// Interaction logic for SceneEditor.xaml
    /// </summary>
    public partial class SceneEditor : System.Windows.Controls.UserControl
    {
        private List<Keys> pressedKeys = new List<Keys>();
        private System.Drawing.Point mousePos;

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

        public Scene Scene;
        public GLControl Glcontrol;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SceneEditor()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            InitializeComponent();

            // Design viewer crashes if it attempts to use GLControl so prevent that :)
#if DEBUG
            if (DesignerProperties.GetIsInDesignMode(this)) return;
#endif
            SizeChanged += SceneEditor_SizeChanged;

            Glcontrol = new GLControl
            {
                Enabled = true,
                AllowDrop = true,
                Visible = true,
                VSync = true
            };
            Glcontrol.DragEnter += Glcontrol_DragEnter;
            Glcontrol.DragDrop += Glcontrol_DragDrop;
            Glcontrol.Load += Glcontrol_Init;
            Glcontrol.Paint += Glcontrol_Paint;
            Glcontrol.MouseMove += Glcontrol_MouseMove;
            Glcontrol.KeyDown += Glcontrol_KeyDown;
            Glcontrol.KeyUp += Glcontrol_KeyUp;
            Glcontrol.MouseWheel += Glcontrol_MouseWheel;
            Glcontrol.MouseClick += Glcontrol_MouseClick;
            Glcontrol.Dock = DockStyle.Fill;

            GLHost.Child = Glcontrol;

            ContextMenu = new System.Windows.Controls.ContextMenu();

            // Start render loop
            Timer timer = new Timer
            {
                Interval = (int)TimeSpan.FromSeconds(1 / 60.0).TotalMilliseconds
            };
            timer.Tick += OnRender;
            timer.Start();
        }

        private void Glcontrol_DragDrop(Object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop);
                FileDrop?.Invoke(this, new FileDropEventArgs { File = files[0] });
            }
            else if (e.Data.GetDataPresent(typeof(DraggedData)))
            {
                var data = e.Data.GetDragDropData() as DraggedData;
                FileDrop?.Invoke(this, new FileDropEventArgs { Data = data });
            }
        }

        private void Glcontrol_DragEnter(Object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (AllowDrop)
            {
                e.Effect = System.Windows.Forms.DragDropEffects.All;
            }
        }

        private void Glcontrol_MouseWheel(Object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            Scene?.ZoomView(e.Delta);
        }

        private void Glcontrol_KeyUp(Object? sender, KeyEventArgs e)
        {
            if (pressedKeys.Contains(e.KeyCode))
            {
                pressedKeys.Remove(e.KeyCode);
            }
        }

        private void Glcontrol_KeyDown(Object? sender, KeyEventArgs e)
        {
            if (!pressedKeys.Contains(e.KeyCode))
            {
                pressedKeys.Add(e.KeyCode);
            }
        }

        private void Glcontrol_MouseClick(Object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && ContextMenu.Items.Count != 0)
            {
                ContextMenu.IsOpen = true;
            }
        }

        private void Glcontrol_MouseMove(Object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            var curMousePos = e.Location;
            if (e.Button == MouseButtons.Middle)
            {
                Scene?.RotateView(new Vector2(curMousePos.X - mousePos.X, mousePos.Y - curMousePos.Y));
            }
            mousePos = curMousePos;
        }

        private void Glcontrol_Init(Object? sender, EventArgs e)
        {
            Glcontrol.MakeCurrent();
            //GL.ClearColor(System.Drawing.Color.LightGray);
            RendererInit?.Invoke(sender, e);
        }

        private void Glcontrol_Paint(Object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Glcontrol.MakeCurrent();
            GL.Viewport(Glcontrol.Location, Glcontrol.Size);
            GL.ClearColor(System.Drawing.Color.LightGray);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            Scene?.PreRender();
            Scene?.Move(pressedKeys);
            Scene?.Render();
            Scene?.PostRender();

            Glcontrol.SwapBuffers();
        }

        private void OnRender(Object? sender, EventArgs e)
        {
            Glcontrol.Invalidate();
        }

        private void SceneEditor_SizeChanged(Object sender, System.Windows.SizeChangedEventArgs e)
        {
            Scene?.SetResolution((float)GLHost.ActualWidth, (float)GLHost.ActualHeight);
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
