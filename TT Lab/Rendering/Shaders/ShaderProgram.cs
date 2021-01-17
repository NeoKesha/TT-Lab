using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private static readonly Shader weightLibShader;
        private Action? uniformSetAction;

        /// <summary>
        /// Creates the shader program.
        /// </summary>
        /// <param name="vertexShaderSource">The vertex shader source.</param>
        /// <param name="fragmentShaderSource">The fragment shader source.</param>
        /// <exception cref="ShaderCompilationException"></exception>
        public ShaderProgram(string vertexShaderSource, string fragmentShaderSource)
        {
            //  Create the shaders.
            vertexShader = new Shader(ShaderType.VertexShader, vertexShaderSource);
            fragmentShader = new Shader(ShaderType.FragmentShader, fragmentShaderSource);

            //  Create the program, attach the shaders.
            shaderProgramObject = (uint)GL.CreateProgram();
            GL.AttachShader((int)shaderProgramObject, vertexShader.ShaderObject);
            GL.AttachShader((int)shaderProgramObject, fragmentShader.ShaderObject);
            GL.AttachShader((int)shaderProgramObject, weightLibShader.ShaderObject);

            //  Now we can link the program.
            GL.LinkProgram(shaderProgramObject);

            //  Now that we've compiled and linked the shader, check it's link status. If it's not linked properly, we're
            //  going to throw an exception.
            if (GetLinkStatus() == false)
            {
                throw new ShaderCompilationException(string.Format("Failed to link shader program with ID {0}.", shaderProgramObject), GetInfoLog());
            }
        }

        static ShaderProgram()
        {
            weightLibShader = new Shader(ShaderType.FragmentShader, Util.ManifestResourceLoader.LoadTextFile("Shaders\\WeightCalc.frag"));
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
            vertexShader.Delete();
            fragmentShader.Delete();
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
            if (vertexShader.GetCompileStatus() == false)
                throw new Exception(vertexShader.GetInfoLog());
            if (fragmentShader.GetCompileStatus() == false)
                throw new Exception(fragmentShader.GetInfoLog());
            if (GetLinkStatus() == false)
                throw new Exception(GetInfoLog());
        }

        public void SetUniform1(string uniformName, float v1)
        {
            GL.Uniform1(GetUniformLocation(uniformName), v1);
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

        public int GetUniformLocation(string uniformName)
        {
            //  If we don't have the uniform name in the dictionary, get it's 
            //  location and add it.
            if (uniformNamesToLocations.ContainsKey(uniformName) == false)
            {
                uniformNamesToLocations[uniformName] = GL.GetUniformLocation(shaderProgramObject, uniformName);
                //  TODO: if it's not found, we should probably throw an exception.
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
        /// A mapping of uniform names to locations. This allows us to very easily specify 
        /// uniform data by name, quickly looking up the location first if needed.
        /// </summary>
        private readonly Dictionary<string, int> uniformNamesToLocations = new Dictionary<string, int>();
    }
}
