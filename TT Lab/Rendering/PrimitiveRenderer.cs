using GlmSharp;
using SharpGL;
using System;
using System.Collections.Generic;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;

namespace TT_Lab.Rendering
{
    public class PrimitiveRenderer
    {
        public void Init(OpenGL gl, GLWindow window)
        {
            GL = gl;
            this.window = window;
            boxBuffer = BufferGeneration.GetCubeBuffer(gl, vec3.Zero, vec3.Ones, new quat(vec3.Zero, 1.0f), new List<System.Drawing.Color>
            {
                System.Drawing.Color.White
            });
            ringBuffer = new IndexedBufferArray[RING_SEGMENT_RESOLUTION];
            for (int i = 0; i < RING_SEGMENT_RESOLUTION; ++i)
            {
                ringBuffer[i] = BufferGeneration.GetCircleBuffer(gl, System.Drawing.Color.White, i / (float)(RING_SEGMENT_RESOLUTION - 1));
            }
            lineBuffer = BufferGeneration.GetLineBuffer(gl, System.Drawing.Color.White);
            simpleAxisBuffer = BufferGeneration.GetSimpleAxisBuffer(gl);
        }

        public void Delete()
        {
            boxBuffer?.Delete();
            boxBuffer = null;
            window = null;
        }

        public void DrawBox(mat4 transform, vec4 color)
        {
            if (window == null || window.Renderer == null)
            {
                return;
            }

            window.Renderer.RenderProgram.SetUniform1("Opacity", color.w);
            window.Renderer.RenderProgram.SetUniform3("AmbientMaterial", color.x, color.y, color.z);
            window.Renderer.RenderProgram.SetUniformMatrix4("StartModel", transform.Values1D);
            if (boxBuffer != null)
            {
                boxBuffer.Bind();
                GL.DrawElements(OpenGL.GL_TRIANGLES, boxBuffer.Indices.Length, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
                boxBuffer.Unbind();
            }
        }

        public void DrawCircle(mat4 transform, vec4 color, float segment = 1.0f)
        {
            if (window == null || window.Renderer == null)
            {
                return;
            }

            window.Renderer.RenderProgram.SetUniform1("Opacity", color.w);
            window.Renderer.RenderProgram.SetUniform3("AmbientMaterial", color.x, color.y, color.z);
            window.Renderer.RenderProgram.SetUniformMatrix4("StartModel", transform.Values1D);
            if (ringBuffer != null)
            {
                var idx = (int)Math.Ceiling(segment * (RING_SEGMENT_RESOLUTION - 1));
                ringBuffer[idx].Bind();
                unsafe
                {
                    GL.DrawElements(OpenGL.GL_TRIANGLES, ringBuffer[idx].Indices.Length, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
                }
                ringBuffer[idx].Unbind();
            }
        }

        public void DrawLine(mat4 transform, vec4 color)
        {
            if (window == null || window.Renderer == null)
            {
                return;
            }

            window.Renderer.RenderProgram.SetUniform1("Opacity", color.w);
            window.Renderer.RenderProgram.SetUniform3("AmbientMaterial", color.x, color.y, color.z);
            window.Renderer.RenderProgram.SetUniformMatrix4("StartModel", transform.Values1D);
            if (lineBuffer != null)
            {
                lineBuffer.Bind();
                unsafe
                {
                    GL.DrawElements(OpenGL.GL_TRIANGLES, lineBuffer.Indices.Length, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
                }
                lineBuffer.Unbind();
            }
        }

        public void DrawSimpleAxis(mat4 transform)
        {
            if (window == null || window.Renderer == null)
            {
                return;
            }

            window.Renderer.RenderProgram.SetUniform1("Opacity", 1.0f);
            window.Renderer.RenderProgram.SetUniform3("AmbientMaterial", 1.0f, 1.0f, 1.0f);
            window.Renderer.RenderProgram.SetUniformMatrix4("StartModel", transform.Values1D);
            if (simpleAxisBuffer != null)
            {
                simpleAxisBuffer.Bind();
                unsafe
                {
                    GL.DrawElements(OpenGL.GL_TRIANGLES, simpleAxisBuffer.Indices.Length, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
                }
                simpleAxisBuffer.Unbind();
            }
        }

        const int RING_SEGMENT_RESOLUTION = 16;
        private IndexedBufferArray? boxBuffer;
        private IndexedBufferArray? lineBuffer;
        private IndexedBufferArray? simpleAxisBuffer;
        private IndexedBufferArray[]? ringBuffer;
        private GLWindow? window;
        private OpenGL? GL;
    }
}
