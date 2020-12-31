using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Rendering.Buffers
{
    public class TextureBuffer : IGLObject
    {

        private uint textureBuffer;

        public TextureBuffer(int width, int height, IntPtr data)
        {
            uint[] ids = new uint[1];
            GL.GenTextures(1, ids);
            textureBuffer = ids[0];
            Bind();
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);
            //GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            Unbind();
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, textureBuffer);
        }

        public void Delete()
        {
            GL.DeleteTexture(textureBuffer);
        }

        public void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}
