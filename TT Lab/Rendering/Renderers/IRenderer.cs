using System.Collections.Generic;
using TT_Lab.Rendering.Shaders;

namespace TT_Lab.Rendering.Renderers
{
    public interface IRenderer
    {
        SharpGL.OpenGL GL { get; }
        ShaderProgram RenderProgram { get; }
        void Render(List<IRenderable> objects);
        void PostProcess();
        void ReallocateFramebuffer(int width, int height);
        void Delete();
    }
}
