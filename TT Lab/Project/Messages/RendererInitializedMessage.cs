using TT_Lab.Rendering;
using TT_Lab.ViewModels.Editors;

namespace TT_Lab.Project.Messages
{
    public class RendererInitializedMessage
    {
        public RendererInitializedMessage(SceneEditorViewModel renderer, GLWindow glControl)
        {
            GlControl = glControl;
            Renderer = renderer;
        }

        public SceneEditorViewModel Renderer { get; set; }
        public GLWindow GlControl { get; set; }
    }
}
