using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
