using SharpGL;
using System;
using System.Collections.Generic;
using TT_Lab.Rendering.Shaders;

namespace TT_Lab.Rendering.Renderers
{
    public class BasicRenderer : IRenderer
    {
        public OpenGL GL { get; private set; }
        public ShaderProgram RenderProgram => renderShader;

        private ShaderProgram renderShader;

        public BasicRenderer(OpenGL gl, ShaderStorage storage, ShaderStorage.LibraryFragmentShaders fragmentLib, ShaderStorage.LibraryVertexShaders vertexLib)
        {
            GL = gl;
            renderShader = storage.BuildShaderProgram(GL, ShaderStorage.StoredVertexShaders.ModelRender, ShaderStorage.StoredFragmentShaders.ModelTextured, vertexLib, fragmentLib);
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
