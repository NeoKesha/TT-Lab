using GlmNet;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;

namespace TT_Lab.Rendering.Objects
{
    public class Collision : IRenderable
    {

        IndexedBufferArray collisionBuffer;

        public Collision(CollisionData colData)
        {
            collisionBuffer = BufferGeneration.GetModelBuffer(
                colData.Vectors.Select(v => new Twinsanity.TwinsanityInterchange.Common.Vector3(v.X, v.Y, v.Z)).ToList(),
                colData.Triangles.Select(t => t.Face).ToList());
        }

        public void Bind()
        {
            collisionBuffer.Bind();
        }

        public void Delete()
        {
            collisionBuffer.Delete();
        }

        public void PostRender()
        {
        }

        public void PreRender()
        {
        }

        public void Render()
        {
            Bind();
            // Draw collision
            GL.DrawElements(PrimitiveType.Triangles, collisionBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
            Unbind();
        }

        public void Unbind()
        {
            collisionBuffer.Unbind();
        }
    }
}
