using OpenTK.Wpf;
using TT_Lab.ViewModels.Editors;

namespace TT_Lab.Project.Messages
{
    public class RendererInitializedMessage
    {
        public RendererInitializedMessage(SceneEditorViewModel renderer, GLWpfControl glControl)
        {
            GlControl = glControl;
            Renderer = renderer;
        }

        public SceneEditorViewModel Renderer { get; set; }
        public GLWpfControl GlControl { get; set; }
    }
}
