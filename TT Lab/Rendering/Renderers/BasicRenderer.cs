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

        public BasicRenderer(ShaderStorage.LibraryFragmentShaders fragmentLib, ShaderStorage.LibraryVertexShaders vertexLib)
        {
            renderShader = ShaderStorage.BuildShaderProgram(ShaderStorage.StoredVertexShaders.ModelRender, ShaderStorage.StoredFragmentShaders.ModelTextured, vertexLib, fragmentLib);
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
            Scene.SetGlobalUniforms(RenderProgram);
            foreach (var @object in objects)
            {
                @object.Render(RenderProgram, false);
            }
        }

        public void PostProcess()
        {
        }
    }
}
