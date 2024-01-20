using System;
using TT_Lab.Rendering;
using TT_Lab.Rendering.Objects;
using TT_Lab.ViewModels;

namespace TT_Lab.Editors.Graphics
{
    /// <summary>
    /// Interaction logic for ModelEditor.xaml
    /// </summary>
    public partial class ModelEditor : BaseEditor
    {
        public ModelEditor()
        {
            InitializeComponent();
        }

        public ModelEditor(AssetViewModel model) : base(model)
        {
            InitializeComponent();
            SceneRenderer_RendererInit();
        }

        public override void CloseEditor(Object? sender, EventArgs e)
        {
            SceneRenderer.CloseEditor();

            base.CloseEditor(sender, e);
        }

        private void SceneRenderer_RendererInit()
        {
            SceneRenderer.Scene = new Scene((float)SceneRenderer.Glcontrol.ActualWidth, (float)SceneRenderer.Glcontrol.ActualHeight, Rendering.Shaders.ShaderStorage.LibraryFragmentShaders.Light);
            SceneRenderer.Scene.SetCameraSpeed(0.2f);
            Model m = new(SceneRenderer.Scene, GetAssetData<AssetData.Graphics.ModelData>());
            SceneRenderer.Scene.AddRender(m);
        }
    }
}
