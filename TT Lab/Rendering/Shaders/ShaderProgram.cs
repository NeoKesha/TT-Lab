using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using Twinsanity.Libraries;
// Credits to https://github.com/dwmkerr/sharpgl
namespace TT_Lab.Rendering.Shaders
{
    /// <summary>
    /// A simple wrapper for Vertex/Fragment shader pairs
    /// </summary>
    public class ShaderProgram : IGLObject
    {
        private readonly Shader vertexShader;
        private readonly Shader fragmentShader;
        private readonly Shader shadeLibShaderVert;
        private readonly Shader shadeLibShaderFrag;
        private Action? uniformSetAction;

        /// <summary>
        /// Creates the shader program from the precompiled shaders
        /// </summary>
        /// <param name="vertex">Precompiled vertex shader</param>
        /// <param name="fragment">Precompiled fragment shader</param>
        /// <param name="vertexLibrary">Precompiled vertex library shader for additional vertex shading</param>
        /// <param name="fragmentLibrary">Precompiled fragment library shader for additional pixel shading</param>
        /// <exception cref="ShaderCompilationException"></exception>
        public ShaderProgram(Shader vertex, Shader fragment, Shader vertexLibrary, Shader fragmentLibrary)
        {
            vertexShader = vertex;
            fragmentShader = fragment;
            shadeLibShaderVert = vertexLibrary;
            shadeLibShaderFrag = fragmentLibrary;

            shaderProgramObject = (uint)GL.CreateProgram();
            GL.AttachShader((int)shaderProgramObject, vertexShader.ShaderObject);
            GL.AttachShader((int)shaderProgramObject, fragmentShader.ShaderObject);
            GL.AttachShader((int)shaderProgramObject, shadeLibShaderVert.ShaderObject);
            GL.AttachShader((int)shaderProgramObject, shadeLibShaderFrag.ShaderObject);

            GL.LinkProgram(shaderProgramObject);

            if (!GetLinkStatus())
            {
                throw new ShaderCompilationException(string.Format("Failed to link shader program with ID {0}.", shaderProgramObject), GetInfoLog());
            }
        }

        public void SetUniforms()
        {
            uniformSetAction?.Invoke();
        }

        public void SetUniformsAction(Action uniformSetAction)
        {
            this.uniformSetAction = uniformSetAction;
        }

        public void Delete()
        {
            GL.DetachShader((int)shaderProgramObject, vertexShader.ShaderObject);
            GL.DetachShader((int)shaderProgramObject, fragmentShader.ShaderObject);
            GL.DetachShader((int)shaderProgramObject, shadeLibShaderFrag.ShaderObject);
            GL.DetachShader((int)shaderProgramObject, shadeLibShaderVert.ShaderObject);
            vertexShader.Delete();
            fragmentShader.Delete();
            shadeLibShaderFrag.Delete();
            shadeLibShaderVert.Delete();
            GL.DeleteProgram(shaderProgramObject);
            shaderProgramObject = 0;
        }

        public int GetAttributeLocation(string attributeName)
        {
            return GL.GetAttribLocation(shaderProgramObject, attributeName);
        }

        public void Bind()
        {
            GL.UseProgram(shaderProgramObject);
        }

        public void Unbind()
        {
            GL.UseProgram(0);
        }

        public bool GetLinkStatus()
        {
            int[] parameters = new int[] { 0 };
            GL.GetProgram(shaderProgramObject, GetProgramParameterName.LinkStatus, parameters);
            return parameters[0] == 1;
        }

        public string GetInfoLog()
        {
            //  Get the info log length.
            int[] infoLength = new int[] { 0 };
            GL.GetProgram(shaderProgramObject, GetProgramParameterName.InfoLogLength, infoLength);
            int bufSize = infoLength[0];

            //  Get the compile info.
            GL.GetProgramInfoLog((int)shaderProgramObject, bufSize, out infoLength[0], out string il);

            return il;
        }

        public void AssertValid()
        {
            if (!vertexShader.GetCompileStatus())
                throw new Exception(vertexShader.GetInfoLog());
            if (!fragmentShader.GetCompileStatus())
                throw new Exception(fragmentShader.GetInfoLog());
            if (!GetLinkStatus())
                throw new Exception(GetInfoLog());
        }

        public void SetUniform1(string uniformName, float v1)
        {
            GL.Uniform1(GetUniformLocation(uniformName), v1);
        }

        public void SetUniform1(string uniformName, int v1)
        {
            GL.Uniform1(GetUniformLocation(uniformName), v1);
        }

        public void SetUniform2(string uniformName, float v1, float v2)
        {
            GL.Uniform2(GetUniformLocation(uniformName), v1, v2);
        }

        public void SetUniform3(string uniformName, float v1, float v2, float v3)
        {
            GL.Uniform3(GetUniformLocation(uniformName), v1, v2, v3);
        }

        public void SetUniformMatrix3(string uniformName, float[] m)
        {
            GL.UniformMatrix3(GetUniformLocation(uniformName), 1, false, m);
        }

        public void SetUniformMatrix4(string uniformName, float[] m)
        {
            GL.UniformMatrix4(GetUniformLocation(uniformName), 1, false, m);
        }

        public void SetTextureUniform(string uniformName, TextureTarget target, uint texId, uint texUnit)
        {
            GL.ActiveTexture((TextureUnit)((int)TextureUnit.Texture0 + texUnit));
            GL.BindTexture(target, texId);
            GL.Uniform1(GetUniformLocation(uniformName), (int)texUnit);
            GL.ActiveTexture(TextureUnit.Texture0);
        }

        public int GetUniformLocation(string uniformName)
        {
            //  If we don't have the uniform Name in the dictionary, get it's 
            //  location and add it.
            if (!uniformNamesToLocations.ContainsKey(uniformName))
            {
                uniformNamesToLocations[uniformName] = GL.GetUniformLocation(shaderProgramObject, uniformName);
            }

            //  Return the uniform location.
            return uniformNamesToLocations[uniformName];
        }

        /// <summary>
        /// Gets the shader program object.
        /// </summary>
        /// <value>
        /// The shader program object.
        /// </value>
        public uint ShaderProgramObject
        {
            get { return shaderProgramObject; }
        }

        private uint shaderProgramObject;

        /// <summary>
        /// A mapping of uniform Names to locations. This allows us to very easily specify 
        /// uniform data by Name, quickly looking up the location first if needed.
        /// </summary>
        private readonly Dictionary<string, int> uniformNamesToLocations = new Dictionary<string, int>();
    }
}
