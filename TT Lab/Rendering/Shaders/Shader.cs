using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Credits to https://github.com/dwmkerr/sharpgl
namespace TT_Lab.Rendering.Shaders
{
    public class Shader
    {
        public Shader(ShaderType shaderType, string source)
        {
            //  Create the OpenGL shader object.
            shaderObject = GL.CreateShader(shaderType);

            //  Set the shader source.
            GL.ShaderSource(shaderObject, source);

            //  Compile the shader object.
            GL.CompileShader(shaderObject);

            //  Now that we've compiled the shader, check it's compilation status. If it's not compiled properly, we're
            //  going to throw an exception.
            if (GetCompileStatus() == false)
            {
                throw new ShaderCompilationException(string.Format("Failed to compile shader with ID {0}.", shaderObject), GetInfoLog());
            }
        }

        public void Delete()
        {
            GL.DeleteShader(shaderObject);
            shaderObject = 0;
        }

        public bool GetCompileStatus()
        {
            int[] parameters = new int[] { 0 };
            GL.GetShader(shaderObject, ShaderParameter.CompileStatus, parameters);
            return parameters[0] == 1;
        }

        public string GetInfoLog()
        {
            //  Get the info log length.
            int[] infoLength = new int[] { 0 };
            GL.GetShader(ShaderObject,
                ShaderParameter.InfoLogLength, infoLength);
            int bufSize = infoLength[0];

            //  Get the compile info.
            GL.GetShaderInfoLog(shaderObject, bufSize, out infoLength[0], out string il);

            return il;
        }

        /// <summary>
        /// The OpenGL shader object.
        /// </summary>
        private int shaderObject;

        /// <summary>
        /// Gets the shader object.
        /// </summary>
        public int ShaderObject
        {
            get { return shaderObject; }
        }
    }
}
