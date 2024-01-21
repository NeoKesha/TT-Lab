using GlmSharp;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;

namespace TT_Lab.Rendering
{
    internal class PrimitiveRenderer
    {
        public void Init(Scene scene)
        {
            this.scene = scene;
            boxBuffer = BufferGeneration.GetCubeBuffer(vec3.Zero, vec3.Ones, new quat(vec3.Zero, 1.0f), new List<System.Drawing.Color>
            {
                System.Drawing.Color.White
            });
            ringBuffer = new IndexedBufferArray[RING_SEGMENT_RESOLUTION];
            for (int i = 0; i < RING_SEGMENT_RESOLUTION; ++i)
            {
                ringBuffer[i] = BufferGeneration.GetCircleBuffer(System.Drawing.Color.White, i / (float)(RING_SEGMENT_RESOLUTION - 1));
            }
            lineBuffer = BufferGeneration.GetLineBuffer(System.Drawing.Color.White);
            simpleAxisBuffer = BufferGeneration.GetSimpleAxisBuffer();
        }

        public void Terminate()
        {
            boxBuffer?.Delete();
            boxBuffer = null;
            scene = null;
        }

        public void DrawBox(mat4 transform, vec4 color)
        {
            if (scene == null)
            {
                return;
            }
            scene.Renderer.RenderProgram.SetUniform1("Alpha", color.w);
            scene.Renderer.RenderProgram.SetUniform3("AmbientMaterial", color.x, color.y, color.z);
            scene.Renderer.RenderProgram.SetUniform3("LightPosition", scene.CameraPosition.x, scene.CameraPosition.y, scene.CameraPosition.z);
            scene.Renderer.RenderProgram.SetUniform3("LightDirection", -scene.CameraDirection.x, scene.CameraDirection.y, scene.CameraDirection.z);
            scene.Renderer.RenderProgram.SetUniformMatrix4("Model", transform.Values1D);
            if (boxBuffer != null)
            {
                boxBuffer.Bind();
                GL.DrawElements(PrimitiveType.Triangles, boxBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
                boxBuffer.Unbind();
            }
        }

        public void DrawCircle(mat4 transform, vec4 color, float segment = 1.0f)
        {
            if (scene == null)
            {
                return;
            }
            scene.Renderer.RenderProgram.SetUniform1("Alpha", color.w);
            scene.Renderer.RenderProgram.SetUniform3("AmbientMaterial", color.x, color.y, color.z);
            scene.Renderer.RenderProgram.SetUniform3("LightPosition", scene.CameraPosition.x, scene.CameraPosition.y, scene.CameraPosition.z);
            scene.Renderer.RenderProgram.SetUniform3("LightDirection", -scene.CameraDirection.x, scene.CameraDirection.y, scene.CameraDirection.z);
            scene.Renderer.RenderProgram.SetUniformMatrix4("Model", transform.Values1D);
            if (ringBuffer != null)
            {
                var idx = (int)Math.Ceiling(segment * (RING_SEGMENT_RESOLUTION - 1));
                ringBuffer[idx].Bind();
                GL.DrawElements(PrimitiveType.Triangles, ringBuffer[idx].Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
                ringBuffer[idx].Unbind();
            }
        }

        public void DrawLine(mat4 transform, vec4 color)
        {
            if (scene == null)
            {
                return;
            }
            scene.Renderer.RenderProgram.SetUniform1("Alpha", color.w);
            scene.Renderer.RenderProgram.SetUniform3("AmbientMaterial", color.x, color.y, color.z);
            scene.Renderer.RenderProgram.SetUniform3("LightPosition", scene.CameraPosition.x, scene.CameraPosition.y, scene.CameraPosition.z);
            scene.Renderer.RenderProgram.SetUniform3("LightDirection", -scene.CameraDirection.x, scene.CameraDirection.y, scene.CameraDirection.z);
            scene.Renderer.RenderProgram.SetUniformMatrix4("Model", transform.Values1D);
            if (lineBuffer != null)
            {
                lineBuffer.Bind();
                GL.DrawElements(PrimitiveType.Lines, lineBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
                lineBuffer.Unbind();
            }
        }

        public void DrawSimpleAxis(mat4 transform)
        {
            if (scene == null)
            {
                return;
            }
            scene.Renderer.RenderProgram.SetUniform1("Alpha", 1.0f);
            scene.Renderer.RenderProgram.SetUniform3("AmbientMaterial", 1.0f, 1.0f, 1.0f);
            scene.Renderer.RenderProgram.SetUniform3("LightPosition", scene.CameraPosition.x, scene.CameraPosition.y, scene.CameraPosition.z);
            scene.Renderer.RenderProgram.SetUniform3("LightDirection", -scene.CameraDirection.x, scene.CameraDirection.y, scene.CameraDirection.z);
            scene.Renderer.RenderProgram.SetUniformMatrix4("Model", transform.Values1D);
            if (simpleAxisBuffer != null)
            {
                simpleAxisBuffer.Bind();
                GL.DrawElements(PrimitiveType.Lines, simpleAxisBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
                simpleAxisBuffer.Unbind();
            }
        }

        const int RING_SEGMENT_RESOLUTION = 16;
        private IndexedBufferArray? boxBuffer;
        private IndexedBufferArray? lineBuffer;
        private IndexedBufferArray? simpleAxisBuffer;
        private IndexedBufferArray[]? ringBuffer;
        private Scene? scene;
    }
}
