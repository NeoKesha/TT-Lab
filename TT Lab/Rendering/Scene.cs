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
        private vec3 cameraDirection = new vec3(0, 0, -1);
        private vec3 cameraUp = new vec3(0, 1, 0);
        private const float cameraSpeed = 1.0f;

        // Scene rendering
        private ShaderProgram shader;
        private List<IRenderable> objects = new List<IRenderable>();

        /// <summary>
        /// Constructor to setup a simple rendering scene
        /// </summary>
        /// <param name="width">Viewport render width</param>
        /// <param name="height">Viewport render height</param>
        public Scene(float width, float height)
        {
            var passVerShader = ManifestResourceLoader.LoadTextFile(@"Shaders\Light.vert");
            var passFragShader = ManifestResourceLoader.LoadTextFile(@"Shaders\Light.frag");
            shader = new ShaderProgram(passVerShader, passFragShader, new Dictionary<uint, string> {
                { 0, "in_Position" },
                { 1, "in_Normal" }
            });

            projectionMat = glm.infinitePerspective(glm.radians(90.0f), width / height, 0.1f);
            viewMat = glm.lookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
            modelMat = glm.scale(new mat4(1.0f), new vec3(1.0f));
            var modelView = viewMat * modelMat;
            normalMat = modelView.to_mat3();
        }

        /// <summary>
        /// Constructor to perform a full chunk render
        /// </summary>
        /// <param name="sceneTree">Tree of chunk resources collision data, positions, cameras, etc.</param>
        /// <param name="width">Viewport render width</param>
        /// <param name="height">Viewport render height</param>
        public Scene(List<AssetViewModel> sceneTree, float width, float height) : this(width, height)
        {
            // Collision renderer
            var colData = (CollisionData)sceneTree.Find(avm => avm.Asset.Type == "CollisionData").Asset.GetData();
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

        public void SetResolution(float width, float height)
        {
            projectionMat = glm.infinitePerspective(glm.radians(90.0f), width / height, 0.1f);
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

        public void RotateView(Vector3 rot)
        {
            rot.Normalize();
            vec3 cross = glm.normalize(glm.cross(cameraUp, cameraDirection));
            mat4 rotMat = glm.rotate(new mat4(1.0f), glm.radians(rot.Y), cross);
            rotMat = glm.rotate(rotMat, glm.radians(rot.X), cameraUp);
            cameraDirection = glm.normalize(new vec3(rotMat * new vec4(cameraDirection, 0.0f)));
            UpdateMatrices();
        }

        public void Move(List<Keys> keysPressed)
        {
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
            viewMat = glm.lookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
            var modelView = viewMat * modelMat;
            normalMat = modelView.to_mat3();
        }
    }
}
