using GlmSharp;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;

namespace TT_Lab.Rendering
{
    internal class PrimitiveRenderer
    {
        public void Init(Scene scene)
        {
            this.scene = scene;
            boxBuffer = BufferGeneration.GetCubeBuffer(new Vector3(0,0,0), new Vector3(1,1,1), new Quaternion(new Vector3(0,0,0), 1.0f), new List<System.Drawing.Color>
            {
                System.Drawing.Color.White
            });
            ringBuffer = new IndexedBufferArray[RING_SEGMENT_RESOLUTION];
            for (int i = 0; i < RING_SEGMENT_RESOLUTION; ++i)
            {
                ringBuffer[i] = BufferGeneration.GetCircleBuffer(System.Drawing.Color.White, i / (float)(RING_SEGMENT_RESOLUTION - 1));
            }
            lineBuffer = BufferGeneration.GetLineBuffer(System.Drawing.Color.White);
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
            if (boxBuffer != null) {
                boxBuffer.Bind();
                GL.DrawElements(PrimitiveType.Triangles, boxBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
                GL.DrawElements(PrimitiveType.Lines, boxBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
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

        const int RING_SEGMENT_RESOLUTION = 16;
        private IndexedBufferArray? boxBuffer;
        private IndexedBufferArray? lineBuffer;
        private IndexedBufferArray[]? ringBuffer;
        private Scene? scene;
    }
}
