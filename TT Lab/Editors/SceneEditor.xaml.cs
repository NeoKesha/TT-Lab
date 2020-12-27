using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Rendering;
using TT_Lab.ViewModels;

namespace TT_Lab.Editors
{
    /// <summary>
    /// Interaction logic for SceneEditor.xaml
    /// </summary>
    public partial class SceneEditor : BaseEditor
    {
        private List<AssetViewModel> chunkTree = new List<AssetViewModel>();
        private List<Keys> pressedKeys = new List<Keys>();
        private Scene scene;
        private bool isDefault;
        private GLControl glcontrol;

        public SceneEditor() : this(null)
        {
        }

        public SceneEditor(ChunkFolder chunk)
        {
            foreach (var item in ((FolderData)chunk.GetData()).Children)
            {
                chunkTree.Add(new AssetViewModel(item));
            }
            DataContext = new { Items = chunkTree };
            isDefault = chunk.Name.ToLower() == "default";
            InitializeComponent();
            SizeChanged += SceneEditor_SizeChanged;
            glcontrol = new GLControl();
            glcontrol.Load += Glcontrol_Init;
            glcontrol.Paint += Glcontrol_Paint;
            glcontrol.MouseMove += Glcontrol_MouseMove;
            glcontrol.KeyDown += Glcontrol_KeyDown;
            glcontrol.KeyUp += Glcontrol_KeyUp;
            glcontrol.Dock = DockStyle.Fill;

            if (!isDefault)
            {
                Task.Factory.StartNew(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        glcontrol.MakeCurrent();
                        var colData = (CollisionData)chunkTree.Find(avm => avm.Asset.Type == "CollisionData").Asset.GetData();
                        scene = new Scene(colData, (float)GLHost.ActualWidth, (float)GLHost.ActualHeight);
                    });
                });
            }

            GLHost.Child = glcontrol;

            // Start render loop
            Timer timer = new Timer
            {
                Interval = (int)TimeSpan.FromSeconds(1 / 60.0).TotalMilliseconds
            };
            timer.Tick += OnRender;
            timer.Start();
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

        private System.Drawing.Point mousePos;
        private void Glcontrol_MouseMove(Object sender, MouseEventArgs e)
        {
            var curMousePos = e.Location;
            if (e.Button == MouseButtons.Middle)
            {
                scene?.RotateView(new Vector3(mousePos.X - curMousePos.X, curMousePos.Y - mousePos.Y, 0));
            }
            mousePos = curMousePos;
        }

        private void Glcontrol_Init(Object sender, EventArgs e)
        {
            glcontrol.MakeCurrent();
            GL.ClearColor(System.Drawing.Color.LightGray);
        }

        private void Glcontrol_Paint(Object sender, System.Windows.Forms.PaintEventArgs e)
        {
            glcontrol.MakeCurrent();
            GL.Viewport(glcontrol.Location, glcontrol.Size);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            scene?.PreRender();
            scene?.Move(pressedKeys);
            scene?.Render();
            scene?.PostRender();

            glcontrol.SwapBuffers();
        }

        private void OnRender(Object sender, EventArgs e)
        {
            glcontrol.Invalidate();
        }

        private void SceneEditor_SizeChanged(Object sender, System.Windows.SizeChangedEventArgs e)
        {
            scene?.SetResolution((float)GLHost.ActualWidth, (float)GLHost.ActualHeight);
        }
    }
}
