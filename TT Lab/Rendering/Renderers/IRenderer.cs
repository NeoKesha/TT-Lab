using System.Collections.Generic;
using TT_Lab.Rendering.Shaders;

namespace TT_Lab.Rendering.Renderers
{
    public interface IRenderer
    {
        Scene Scene { get; set; }
        ShaderProgram RenderProgram { get; }
        void Render(List<IRenderable> objects);
        void RenderOpaque(List<IRenderable> objects);
        void ReallocateFramebuffer(int width, int height);
        void Delete();
    }
}
