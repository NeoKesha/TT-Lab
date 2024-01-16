using System;
using System.Collections.Generic;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;

namespace TT_Lab.Rendering.Renderers
{
    public class BasicRenderer : IRenderer
    {
        public Scene Scene { get; set; }

        public ShaderProgram RenderProgram => renderShader;

        private ShaderProgram renderShader;

        public BasicRenderer(ShaderProgram.LibShader lib)
        {
            renderShader = new(ManifestResourceLoader.LoadTextFile("Shaders\\ModelRender.vert"), ManifestResourceLoader.LoadTextFile("Shaders\\ModelTextured.frag"), lib);
        }

        public void Delete()
        {
            RenderProgram.Delete();
        }

        public void ReallocateFramebuffer(Int32 width, Int32 height)
        {
        }

        public void Render(List<IRenderable> objects)
        {
            Scene.SetProjectViewShaderUniforms(RenderProgram);
            foreach (var @object in objects)
            {
                @object.Render();
            }
        }

        public void RenderOpaque(List<IRenderable> objects)
        {
        }
    }
}
