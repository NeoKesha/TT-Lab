using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace TT_Lab.Rendering.Buffers
{
    public class TextureBuffer : IGLObject
    {

        private uint textureBuffer = 0;
        private TextureTarget textureTarget;

        public TextureBuffer(TextureTarget target, int width, int height, Bitmap data, bool generateMipmaps, System.Drawing.Imaging.PixelFormat textureFormat, PixelFormat format, PixelInternalFormat storageFormat, PixelType pixelType)
        {
            textureTarget = target;

            var bitmapBits = data.LockBits(new Rectangle(0, 0, data.Width, data.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadOnly, textureFormat);

            textureBuffer = (uint)GL.GenTexture();
            Bind();
            GL.TexParameter(textureTarget, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(textureTarget, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(textureTarget, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(textureTarget, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexImage2D(textureTarget, 0, storageFormat, width, height, 0, format, pixelType, bitmapBits.Scan0);
            if (generateMipmaps)
            {
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }
            data.UnlockBits(bitmapBits);
            Unbind();
        }

        public TextureBuffer(TextureTarget target, int width, int height, Bitmap data)
            : this(target, width, height, data, true, System.Drawing.Imaging.PixelFormat.Format32bppArgb, PixelFormat.Bgra, PixelInternalFormat.Rgba, PixelType.UnsignedByte)
        {
        }

        public TextureBuffer(int width, int height, Bitmap data) : this(TextureTarget.Texture2D, width, height, data)
        {
        }

        public TextureBuffer(TextureTarget target)
        {
            textureTarget = target;
            textureBuffer = (uint)GL.GenTexture();
        }

        public TextureBuffer() : this(TextureTarget.Texture2D)
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
