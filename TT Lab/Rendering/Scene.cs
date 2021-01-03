using GlmNet;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TT_Lab.AssetData.Instance;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;
using TT_Lab.ViewModels;

namespace TT_Lab.Rendering
{
    /// <summary>
    /// A 3D scene to render to
    /// </summary>
    public class Scene : IRenderable
    {
        // Rendering matrices
        private mat4 projectionMat;
        private mat4 viewMat;
        private mat4 modelMat;
        private mat3 normalMat;
        private vec3 cameraPosition = new vec3(0.0f, 0.0f, 0.0f);
        private vec3 cameraDirection = new vec3(0, 0, 1);
        private vec3 cameraUp = new vec3(0, 1, 0);
        private vec2 resolution = new vec2(0, 0);
        private float cameraSpeed = 1.0f;
        private float cameraZoom = 90.0f;
        private bool canManipulateCamera = true;

        // Scene rendering
        private ShaderProgram shader;
        private List<IRenderable> objects = new List<IRenderable>();

        /// <summary>
        /// Constructor to setup the matrices
        /// </summary>
        /// <param name="width">Viewport render width</param>
        /// <param name="height">Viewport render height</param>
        private Scene(float width, float height)
        {
            resolution.x = width;
            resolution.y = height;
            projectionMat = glm.infinitePerspective(glm.radians(cameraZoom), resolution.x / resolution.y, 0.1f);
            viewMat = glm.lookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
            modelMat = glm.scale(new mat4(1.0f), new vec3(1.0f));
            var modelView = viewMat * modelMat;
            normalMat = modelView.to_mat3();
        }

        /// <summary>
        /// Constructor to setup a simple rendering scene with a custom shader
        /// </summary>
        /// <param name="width">Viewport render width</param>
        /// <param name="height">Viewport render height</param>
        /// <param name="shaderName">Shader file name for both .vert and .frag programs</param>
        /// <param name="shdSetUni">Callback when setting shader's uniforms</param>
        /// <param name="attribPositions">Attribute positions in shader for the Vertex program</param>
        public Scene(float width, float height, string shaderName = "Light",
            Action<ShaderProgram, Scene> shdSetUni = null, Dictionary<uint, string> attribPositions = null)
            : this(width, height)
        {
            var passVerShader = ManifestResourceLoader.LoadTextFile($"Shaders\\{shaderName}.vert");
            var passFragShader = ManifestResourceLoader.LoadTextFile($"Shaders\\{shaderName}.frag");
            if (shdSetUni == null && attribPositions == null)
            {
                shader = new ShaderProgram(passVerShader, passFragShader, new Dictionary<uint, string> {
                    { 0, "in_Position" },
                    { 1, "in_Color" },
                    { 2, "in_Normal" }
                });
                shader.SetUniforms(() =>
                {
                    DefaultShaderUniforms();
                });
            }
            else
            {
                shader = new ShaderProgram(passVerShader, passFragShader, attribPositions);
                shader.SetUniforms(() => shdSetUni(shader, this));
            }
        }

        /// <summary>
        /// Constructor to perform a full chunk render
        /// </summary>
        /// <param name="sceneTree">Tree of chunk resources collision data, positions, cameras, etc.</param>
        /// <param name="width">Viewport render width</param>
        /// <param name="height">Viewport render height</param>
        public Scene(List<AssetViewModel> sceneTree, float width, float height) : this(width, height, "Light")
        {
            // Collision renderer
            var colData = (CollisionData)sceneTree.Find((avm) =>
            {
                return avm.Asset.Type == typeof(Assets.Instance.Collision);
            }).Asset.GetData();
            var colRender = new Objects.Collision(colData);
            objects.Add(colRender);

            // Positions renderer
            var positions = sceneTree.Find(avm => avm.Alias == "Positions");
            foreach (var pos in positions.Children)
            {
                var pRend = new Objects.Position((Assets.Instance.Position)pos.Asset);
                objects.Add(pRend);
            }
        }

        public void AddRender(IRenderable renderObj)
        {
            objects.Add(renderObj);
        }

        public void DefaultShaderUniforms()
        {
            // Fragment program uniforms
            shader.SetUniform3("AmbientMaterial", 0.15f, 0.15f, 0.15f);
            shader.SetUniform3("SpecularMaterial", 0.5f, 0.5f, 0.5f);
            shader.SetUniform3("LightPosition", cameraPosition.x, cameraPosition.y, cameraPosition.z);
            shader.SetUniform3("LightDirection", cameraDirection.x, cameraDirection.y, cameraDirection.z);

            // Vertex program uniforms
            shader.SetUniformMatrix4("Projection", projectionMat.to_array());
            shader.SetUniformMatrix4("View", viewMat.to_array());
            shader.SetUniformMatrix4("Model", modelMat.to_array());
            shader.SetUniformMatrix3("NormalMatrix", normalMat.to_array());
            shader.SetUniform3("DiffuseMaterial", 0.75f, 0.75f, 0.75f);
        }

        public void SetResolution(float width, float height)
        {
            resolution.x = width;
            resolution.y = height;
            UpdateMatrices();
        }

        public void Render()
        {
            Bind();
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.Enable(EnableCap.DepthTest);
            foreach (var @object in objects)
            {
                @object.Render();
            }
            GL.CullFace(CullFaceMode.Back);
            GL.Disable(EnableCap.DepthTest);
            Unbind();
        }

        public void Bind()
        {
            shader.Bind();
            shader.SetUniforms();
        }

        public void SetCameraSpeed(float s)
        {
            cameraSpeed = s;
        }

        public void DisableCameraManipulation()
        {
            canManipulateCamera = false;
        }

        public void RotateView(Vector3 rot)
        {
            if (!canManipulateCamera) return;

            rot.Normalize();
            vec3 cross = glm.normalize(glm.cross(cameraUp, cameraDirection));
            mat4 rotMat = glm.rotate(new mat4(1.0f), glm.radians(rot.Y), cross);
            rotMat = glm.rotate(rotMat, glm.radians(rot.X), cameraUp);
            cameraDirection = glm.normalize(new vec3(rotMat * new vec4(cameraDirection, 0.0f)));
            UpdateMatrices();
        }

        public void ZoomView(float z)
        {
            if (!canManipulateCamera) return;

            z *= -0.01f;
            cameraZoom = (z + cameraZoom).Clamp(10.0f, 100.0f);
            UpdateMatrices();
        }

        public void Move(List<Keys> keysPressed)
        {
            if (!canManipulateCamera) return;

            foreach (var keyPressed in keysPressed)
            {
                switch (keyPressed)
                {
                    case Keys.W:
                        cameraPosition += cameraSpeed * cameraDirection;
                        break;
                    case Keys.S:
                        cameraPosition -= cameraSpeed * cameraDirection;
                        break;
                    case Keys.A:
                        cameraPosition -= cameraSpeed * glm.cross(cameraDirection, cameraUp);
                        break;
                    case Keys.D:
                        cameraPosition += cameraSpeed * glm.cross(cameraDirection, cameraUp);
                        break;
                }
            }
            UpdateMatrices();
        }

        public void Unbind()
        {
            shader.Unbind();
        }

        public void Delete()
        {
            shader.Delete();
            foreach (var @object in objects)
            {
                @object.Delete();
            }
            objects.Clear();
        }

        public void PreRender()
        {
            foreach (var @object in objects)
            {
                @object.PreRender();
            }
        }

        public void PostRender()
        {
            foreach (var @object in objects)
            {
                @object.PostRender();
            }
        }

        private void UpdateMatrices()
        {
            projectionMat = glm.infinitePerspective(glm.radians(cameraZoom), resolution.x / resolution.y, 0.1f);
            viewMat = glm.lookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
            var modelView = viewMat * modelMat;
            normalMat = modelView.to_mat3();
        }
    }
}
