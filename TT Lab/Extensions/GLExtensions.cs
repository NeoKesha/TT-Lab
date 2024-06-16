using Microsoft.VisualBasic.Logging;
using SharpGL;
using System;
using System.Runtime.InteropServices;
using System.Text;
using TT_Lab.Util;

namespace TT_Lab.Extensions
{
    public static class GLExtensions
    {
        public static string GetProgramInfoLog(this OpenGL GL, uint program)
        {
            // Get the info log length.
            int[] infoLength = new int[] { 0 };
            GL.GetProgram(program, OpenGL.GL_INFO_LOG_LENGTH, infoLength);
            int bufSize = infoLength[0];

            //  Get the compile info.
            StringBuilder il = new();
            GL.GetProgramInfoLog(program, bufSize, bufSize, il);

            return il.ToString();
        }

        public static string GetShaderInfoLog(this OpenGL GL, uint shader)
        {
            //  Get the info log length.
            int[] infoLength = new int[] { 0 };
            GL.GetShader(shader, OpenGL.GL_INFO_LOG_LENGTH, infoLength);
            int bufSize = infoLength[0];

            //  Get the compile info.
            var il = new StringBuilder();
            GL.GetShaderInfoLog(shader, bufSize, bufSize, il);

            return il.ToString();
        }

        public static void MemoryBarrier(this OpenGL gl, uint barriers)
        {
            ((OpenGLExtension)gl).MemoryBarrier(barriers);
        }

        public static void ClearColor(this OpenGL gl, System.Drawing.Color color)
        {
            var colors = color.ToArray();
            gl.ClearColor(colors[0], colors[1], colors[2], colors[3]);
        }
    }

    public class OpenGLExtension : OpenGL
    {
        // Extensions importing code from https://github.com/dwmkerr/sharpgl/blob/9a18e2624f488635ee87d4ee5a75aa2e6df09cdd/source/SharpGL/Core/SharpGL/OpenGLExtensions.cs
        private delegate void glMemoryBarrier(uint barrierMask);

        public const uint GL_FRAMEBUFFER_BARRIER_BIT = 0x400;
        public const uint GL_TEXTURE_FETCH_BARRIER_BIT = 0x100;

        static class GlExtensionFunction<T> where T : class
        {
            private static T m_delegate = null;
            public static T Delegate
            {
                get
                {
                    if (m_delegate == null)
                    {
                        Delegate del = null;
                        Type delegateType = typeof(T);
                        //  Get the name of the extension function.
                        string name = delegateType.Name;
                        IntPtr proc = Win32.wglGetProcAddress(name);
                        if (proc == IntPtr.Zero)
                            throw new Exception("Extension function " + name + " not supported");

                        //  Get the delegate for the function pointer.
                        del = Marshal.GetDelegateForFunctionPointer(proc, delegateType);
                        if (del == null)
                            throw new Exception("Extension function " + name + " marshalled incorrectly");

                        m_delegate = del as T;
                    }
                    return m_delegate;
                }
            }
        }

        private T GetDelegateFor<T>() where T : class
        {
            return GlExtensionFunction<T>.Delegate;
        }

        public void MemoryBarrier(uint barrierMask)
        {
            PreGLCall();
            GetDelegateFor<glMemoryBarrier>()(barrierMask);
            PostGLCall();
        }
    }
}
