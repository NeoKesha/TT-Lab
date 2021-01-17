using GlmNet;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Project;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;

namespace TT_Lab.Rendering.Objects
{
    public class Plane : IRenderable
    {
        public Scene? Parent { get; set; }

        IndexedBufferArray planeBuffer;
        TextureBuffer? texture;
        ShaderProgram shader;

        public Plane() : this(new vec3())
        {
        }

        public Plane(vec3 position)
        {
            planeBuffer = BufferGeneration.GetModelBuffer(
                new List<Twinsanity.TwinsanityInterchange.Common.Vector3>
                {
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(1 + position.x, 1 + position.y, -1 + position.z),
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(-1 + position.x, 1 + position.y, -1 + position.z),
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(-1 + position.x, -1 + position.y, -1 + position.z),
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(1 + position.x, -1 + position.y, -1 + position.z)
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

            shader = new ShaderProgram(ManifestResourceLoader.LoadTextFile("Shaders\\TexturePass.vert"),
                ManifestResourceLoader.LoadTextFile("Shaders\\TexturePass.frag"));
        }

        public Plane(MaterialData material) : this()
        {
            if (material.Shaders[0].TxtMapping == Twinsanity.TwinsanityInterchange.Common.TwinShader.TextureMapping.ON)
            {
                var tex = (TextureData)ProjectManagerSingleton.PM.OpenedProject.GetAsset(material.Shaders[0].TextureId).GetData();
                texture = new TextureBuffer(tex.Bitmap.Width, tex.Bitmap.Height, tex.Bitmap);
            }
        }

        public Plane(TextureData tex) : this(tex.Bitmap)
        {
        }

        public Plane(Bitmap texImage) : this()
        {
            texture = new TextureBuffer(texImage.Width, texImage.Height, texImage);
        }

        public void Bind()
        {
            texture?.Bind();
            planeBuffer.Bind();
            shader.Bind();
        }

        public void Delete()
        {
            shader.Delete();
            texture?.Delete();
            planeBuffer.Delete();
        }

        public void Render()
        {
            Bind();
            Parent?.SetPVMNShaderUniforms(shader);
            GL.DrawElements(PrimitiveType.Triangles, planeBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
            Unbind();
        }

        public void Unbind()
        {
            shader.Unbind();
            texture?.Unbind();
            planeBuffer.Unbind();
        }
    }
}
