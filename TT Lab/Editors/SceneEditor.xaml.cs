﻿using OpenTK;
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
using TT_Lab.Rendering;
using TT_Lab.Rendering.Shaders;
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

        public SceneEditor()
        {
            InitializeComponent();

            // Design viewer crashes if it attempts to use GLControl so prevent that :)
#if DEBUG
            if (DesignerProperties.GetIsInDesignMode(this)) return;
#endif
            SizeChanged += SceneEditor_SizeChanged;

            Glcontrol = new GLControl();
            Glcontrol.Load += Glcontrol_Init;
            Glcontrol.Paint += Glcontrol_Paint;
            Glcontrol.MouseMove += Glcontrol_MouseMove;
            Glcontrol.KeyDown += Glcontrol_KeyDown;
            Glcontrol.KeyUp += Glcontrol_KeyUp;
            Glcontrol.MouseWheel += Glcontrol_MouseWheel;
            Glcontrol.Dock = DockStyle.Fill;

            GLHost.Child = Glcontrol;

            // Start render loop
            Timer timer = new Timer
            {
                Interval = (int)TimeSpan.FromSeconds(1 / 60.0).TotalMilliseconds
            };
            timer.Tick += OnRender;
            timer.Start();
        }

        private void Glcontrol_MouseWheel(Object sender, MouseEventArgs e)
        {
            Scene?.ZoomView(e.Delta);
        }

        private void Glcontrol_KeyUp(Object sender, KeyEventArgs e)
        {
            if (pressedKeys.Contains(e.KeyCode))
            {
                pressedKeys.Remove(e.KeyCode);
            }
        }

        private void Glcontrol_KeyDown(Object sender, KeyEventArgs e)
        {
            if (!pressedKeys.Contains(e.KeyCode))
            {
                pressedKeys.Add(e.KeyCode);
            }
        }

        
        private void Glcontrol_MouseMove(Object sender, MouseEventArgs e)
        {
            var curMousePos = e.Location;
            if (e.Button == MouseButtons.Middle)
            {
                Scene?.RotateView(new Vector3(mousePos.X - curMousePos.X, curMousePos.Y - mousePos.Y, 0));
            }
            mousePos = curMousePos;
        }

        private void Glcontrol_Init(Object sender, EventArgs e)
        {
            Glcontrol.MakeCurrent();
            GL.ClearColor(System.Drawing.Color.LightGray);
            RendererInit?.Invoke(sender, e);
        }

        private void Glcontrol_Paint(Object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Glcontrol.MakeCurrent();
            GL.Viewport(Glcontrol.Location, Glcontrol.Size);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            Scene?.PreRender();
            Scene?.Move(pressedKeys);
            Scene?.Render();
            Scene?.PostRender();

            Glcontrol.SwapBuffers();
        }

        private void OnRender(Object sender, EventArgs e)
        {
            Glcontrol.Invalidate();
        }

        private void SceneEditor_SizeChanged(Object sender, System.Windows.SizeChangedEventArgs e)
        {
            Scene?.SetResolution((float)GLHost.ActualWidth, (float)GLHost.ActualHeight);
        }

        private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SceneEditor control = d as SceneEditor;
            control.SceneHeader.Header = e.NewValue;
        }
    }
}
