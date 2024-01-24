using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using Twinsanity.Libraries;
// Credits to https://github.com/dwmkerr/sharpgl
namespace TT_Lab.Rendering.Shaders
{
    public class Shader
    {
        public Shader(ShaderType shaderType, string source)
        {
            //  Create the OpenGL shader object.
            shaderObject = GL.CreateShader(shaderType);

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
