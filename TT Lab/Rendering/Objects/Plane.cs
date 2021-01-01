using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Project;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;

namespace TT_Lab.Rendering.Objects
{
    public class Plane : IRenderable
    {
        IndexedBufferArray planeBuffer;
        TextureBuffer texture;

        public Plane(MaterialData material)
        {
            planeBuffer = BufferGeneration.GetModelBuffer(
                new List<Twinsanity.TwinsanityInterchange.Common.Vector3>
                {
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(-1, -1, 1),
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(1, -1, 1),
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(1, 1, 1),
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(-1, 1, 1)
                },
                new List<AssetData.Graphics.SubModels.IndexedFace>
                {
                    new AssetData.Graphics.SubModels.IndexedFace(new int[] { 0, 1, 2 }),
                    new AssetData.Graphics.SubModels.IndexedFace(new int[] { 2, 3, 0 })
                },
                new List<System.Drawing.Color> { System.Drawing.Color.White },
                new List<Twinsanity.TwinsanityInterchange.Common.Vector3>
                {
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(0, 0, 0),
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(1, 0, 0),
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(1, 1, 0),
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(0, 1, 0)
                });
            if (material.Shaders[0].TxtMapping == Twinsanity.TwinsanityInterchange.Common.TwinShader.TextureMapping.ON)
            {
                var tex = (TextureData)ProjectManagerSingleton.PM.OpenedProject.GetAsset(material.Shaders[0].TextureId).GetData();
                //tex.Bitmap.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                texture = new TextureBuffer(tex.Bitmap.Width, tex.Bitmap.Height, tex.Bitmap);
            }
        }

        public void Bind()
        {
            texture?.Bind();
            planeBuffer.Bind();
        }

        public void Delete()
        {
            texture?.Delete();
            planeBuffer.Delete();
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
            GL.DrawElements(PrimitiveType.Triangles, planeBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
            Unbind();
        }

        public void Unbind()
        {
            texture?.Unbind();
            planeBuffer.Unbind();
        }
    }
}
