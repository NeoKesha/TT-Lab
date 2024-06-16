using SharpGL;
using System.Drawing;

namespace TT_Lab.Rendering.Buffers
{
    public class TextureBuffer : IGLObject
    {
        public OpenGL GL { get; private set; }

        private uint textureBuffer = 0;
        private TextureTarget textureTarget;

        public enum TextureTarget : uint
        {
            Texture1D = SharpGL.Enumerations.TextureTarget.Texture1D,
            Texture2D = SharpGL.Enumerations.TextureTarget.Texture2D,
            Texture3D = SharpGL.Enumerations.TextureTarget.Texture3D,
            Texture2DMultisample = OpenGL.GL_TEXTURE_2D_MULTISAMPLE,
        }

        public enum PixelFormat : uint
        {
            Alpha = OpenGL.GL_ALPHA,
            DepthComponent = OpenGL.GL_DEPTH_COMPONENT,
            Luminance = OpenGL.GL_LUMINANCE,
            LuminanceAlpha = OpenGL.GL_LUMINANCE_ALPHA,
            Rgb = OpenGL.GL_RGB,
            Rgba = OpenGL.GL_RGBA,
            Bgra = OpenGL.GL_BGRA,
        }

        public enum PixelInternalFormat : uint
        {
            Rgba = OpenGL.GL_RGBA,
            Rgb = OpenGL.GL_RGB,
            Rgba16f = OpenGL.GL_RGBA16F,
            Rgb16f = OpenGL.GL_RGB16F,
            Rgba8SNorm = OpenGL.GL_RGBA8_SNORM
        }

        public enum PixelType : uint
        {
            Byte = OpenGL.GL_BYTE,
            Short = OpenGL.GL_SHORT,
            UnsignedByte = OpenGL.GL_UNSIGNED_BYTE,
        }

        public TextureBuffer(OpenGL gl, TextureTarget target, int width, int height, Bitmap data, bool generateMipmaps, System.Drawing.Imaging.PixelFormat textureFormat, PixelFormat format, PixelInternalFormat pixelInternalStorageFormat, PixelType pixelType)
        {
            GL = gl;

            textureTarget = target;

            var bitmapBits = data.LockBits(new Rectangle(0, 0, data.Width, data.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadOnly, textureFormat);

            var buffer = new uint[1];
            GL.GenTextures(1, buffer);
            textureBuffer = buffer[0];
            Bind();
            GL.TexParameter((uint)textureTarget, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
            GL.TexParameter((uint)textureTarget, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);
            GL.TexParameter((uint)textureTarget, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
            GL.TexParameter((uint)textureTarget, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
            GL.TexImage2D((uint)textureTarget, 0, (uint)pixelInternalStorageFormat, width, height, 0, (uint)format, (uint)pixelType, bitmapBits.Scan0);
            if (generateMipmaps)
            {
                GL.GenerateMipmapEXT((uint)textureTarget);
            }
            data.UnlockBits(bitmapBits);
            Unbind();
        }

        public TextureBuffer(OpenGL gl, TextureTarget target, int width, int height, Bitmap data)
            : this(gl, target, width, height, data, true, System.Drawing.Imaging.PixelFormat.Format32bppArgb, PixelFormat.Bgra, PixelInternalFormat.Rgba, PixelType.UnsignedByte)
        {
        }

        public TextureBuffer(OpenGL gl, int width, int height, Bitmap data) : this(gl, TextureTarget.Texture2D, width, height, data)
        {
        }

        public TextureBuffer(OpenGL gl, TextureTarget target)
        {
            GL = gl;

            textureTarget = target;
            var buffer = new uint[1];
            GL.GenTextures(1, buffer);
            textureBuffer = buffer[0];
        }

        public TextureBuffer(OpenGL gl) : this(gl, TextureTarget.Texture2D)
        {
        }

        public void Bind()
        {
            GL.BindTexture((uint)textureTarget, textureBuffer);
        }

        public void Delete()
        {
            if (textureBuffer != 0)
            {
                GL.DeleteTextures(1, new uint[] { textureBuffer });
                textureBuffer = 0;
            }
        }

        public void Unbind()
        {
            GL.BindTexture((uint)textureTarget, 0);
        }

        public uint Buffer
        {
            get => textureBuffer;
        }
    }
}
