using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Rendering.Buffers
{
    public class TextureBuffer : IGLObject
    {

        private uint textureBuffer = 0;
        private TextureTarget textureTarget;

        public TextureBuffer(TextureTarget target, int width, int height, Bitmap data)
        {
            textureTarget = target;

            var bitmapBits = data.LockBits(new Rectangle(0, 0, data.Width, data.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            textureBuffer = (uint)GL.GenTexture();
            Bind();
            GL.TexParameter(textureTarget, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(textureTarget, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(textureTarget, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(textureTarget, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexImage2D(textureTarget, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, bitmapBits.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            data.UnlockBits(bitmapBits);
            Unbind();
        }

        public TextureBuffer(int width, int height, Bitmap data) : this (TextureTarget.Texture2D, width, height, data)
        {
        }

        public TextureBuffer(TextureTarget target)
        {
            textureTarget = target;
            textureBuffer = (uint)GL.GenTexture();
        }

        public TextureBuffer() : this (TextureTarget.Texture2D)
        {
        }

        public void Bind()
        {
            GL.BindTexture(textureTarget, textureBuffer);
        }

        public void Delete()
        {
            if (textureBuffer != 0)
            {
                GL.DeleteTexture(textureBuffer);
                textureBuffer = 0;
            }
        }

        public void Unbind()
        {
            GL.BindTexture(textureTarget, 0);
        }

        public uint Buffer
        {
            get => textureBuffer;
        }
    }
}
