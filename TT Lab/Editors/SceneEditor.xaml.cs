using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            glcontrol.Dock = System.Windows.Forms.DockStyle.Fill;

            GLHost.Child = glcontrol;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1 / 60.0);
            timer.Tick += OnRender;
            timer.Start();
        }

        private void Glcontrol_Init(Object sender, EventArgs e)
        {
            glcontrol.MakeCurrent();
            GL.ClearColor(Color.LightGray);
        }

        private void Glcontrol_Paint(Object sender, System.Windows.Forms.PaintEventArgs e)
        {
            glcontrol.MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            glcontrol.SwapBuffers();
        }

        private void OnRender(Object sender, EventArgs e)
        {
            glcontrol.Invalidate();
        }

        private void SceneEditor_SizeChanged(Object sender, System.Windows.SizeChangedEventArgs e)
        {
            if (scene != null)
            {
                scene.SetResolution((float)GLHost.ActualWidth, (float)GLHost.ActualHeight);
            }
        }
    }
}
