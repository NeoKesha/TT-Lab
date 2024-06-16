using SharpGL;
using System;
using System.Collections.Generic;
using TT_Lab.Extensions;
using Twinsanity.Libraries;
// Credits to https://github.com/dwmkerr/sharpgl
namespace TT_Lab.Rendering.Shaders
{
    public class Shader
    {
        private OpenGL GL;

        public enum ShaderType : uint
        {
            VertexShader = OpenGL.GL_VERTEX_SHADER,
            FragmentShader = OpenGL.GL_FRAGMENT_SHADER,
        }

        public Shader(OpenGL gl, ShaderType shaderType, string source)
        {
            GL = gl;

            //  Create the OpenGL shader object.
            shaderObject = GL.CreateShader((uint)shaderType);

            // Remove all the comments
            ProcessComments(ref source);

            // Process all the include directives
            ProcessIncludes(ref source);

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
            GL.GetShader(shaderObject, OpenGL.GL_COMPILE_STATUS, parameters);
            return parameters[0] == 1;
        }

        public string GetInfoLog()
        {
            return GL.GetShaderInfoLog(shaderObject);
        }

        private static void ProcessIncludes(ref string shaderText)
        {
            var encounteredIncludes = new List<String>();
            while (StringUtils.GetStringInBetween(shaderText, "#include \"", "\"") != String.Empty)
            {
                var includePath = StringUtils.GetStringInBetween(shaderText, "#include \"", "\"");
                var includeLoadPath = $"Shaders\\{includePath}";
                // Detect cyclic includes
                if (encounteredIncludes.Contains(includePath))
                {
                    shaderText = shaderText.Replace($"#include \"{includePath}\"", String.Empty);
                    continue;
                }

                encounteredIncludes.Add(includePath);
                var includeText = Util.ManifestResourceLoader.LoadTextFile(includeLoadPath);
                shaderText = shaderText.Replace($"#include \"{includePath}\"", includeText);
                ProcessComments(ref shaderText);
            }
        }

        private static void ProcessComments(ref string shaderText)
        {
            while (StringUtils.GetStringInBetween(shaderText, "//", "\n") != String.Empty)
            {
                var startCommentIndex = StringUtils.GetIndexBefore(shaderText, "//");
                if (startCommentIndex == -1)
                {
                    break;
                }

                var endCommentIndex = StringUtils.GetIndexAfter(shaderText, "\n", startCommentIndex);
                if (endCommentIndex == -1)
                {
                    break;
                }
                shaderText = shaderText.Replace(shaderText[startCommentIndex..endCommentIndex], String.Empty);
            }
        }

        /// <summary>
        /// The OpenGL shader object.
        /// </summary>
        private uint shaderObject;

        /// <summary>
        /// Gets the shader object.
        /// </summary>
        public uint ShaderObject
        {
            get { return shaderObject; }
        }
    }
}
