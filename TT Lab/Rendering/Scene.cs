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
        private vec3 cameraPosition = new vec3(0.0f, 15.0f, 0.0f);
        private vec3 cameraDirection = new vec3(0, 0, -1);
        private vec3 cameraUp = new vec3(0, 1, 0);
        private const float cameraSpeed = 1.0f;

        // Scene rendering
        private CollisionData colData;
        private VertexBufferArray collisionBuffer;
        private uint[] indices;
        private ShaderProgram shader;
        private List<IRenderable> objects = new List<IRenderable>();

        public Scene(CollisionData collisionData, float width, float height)
        {
            colData = collisionData;

            var passVerShader = ManifestResourceLoader.LoadTextFile(@"Shaders\Light.vert");
            var passFragShader = ManifestResourceLoader.LoadTextFile(@"Shaders\Light.frag");
            shader = new ShaderProgram(passVerShader, passFragShader, new Dictionary<uint, string> {
                { 0, "in_Position" },
                { 1, "in_Color" },
                { 2, "in_Normal" }
            });

            projectionMat = glm.infinitePerspective(glm.radians(90.0f), width / height, 0.1f);
            viewMat = glm.lookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
            modelMat = glm.scale(new mat4(1.0f), new vec3(1.0f));
            var modelView = viewMat * modelMat;
            normalMat = modelView.to_mat3();

            // Init collision buffer
            var vertices = new List<float>();
            var vert3s = new List<Vector3>();
            var colors = new List<float>();
            var indices = new List<uint>();

            var surfaceColors = new System.Drawing.Color[]
            {
                System.Drawing.Color.White,
                System.Drawing.Color.Red,
                System.Drawing.Color.Brown,
                System.Drawing.Color.Cyan,
                System.Drawing.Color.Blue,
                System.Drawing.Color.Pink,
                System.Drawing.Color.Yellow,
                System.Drawing.Color.Gray,
                System.Drawing.Color.DarkGray,
                System.Drawing.Color.Green,
                System.Drawing.Color.Gold,
                System.Drawing.Color.Aqua,
                System.Drawing.Color.SkyBlue,
                System.Drawing.Color.AntiqueWhite,
                System.Drawing.Color.Bisque,
                System.Drawing.Color.Chocolate,
                System.Drawing.Color.DarkSeaGreen,
                System.Drawing.Color.Azure,
                System.Drawing.Color.HotPink,
                System.Drawing.Color.Honeydew,
                System.Drawing.Color.Lime,
                System.Drawing.Color.Magenta,
                System.Drawing.Color.White,
                System.Drawing.Color.White,
                System.Drawing.Color.White,
                System.Drawing.Color.White,
                System.Drawing.Color.White,
                System.Drawing.Color.White,
                System.Drawing.Color.White,
                System.Drawing.Color.White,
                System.Drawing.Color.White,
            };
            foreach (var tri in collisionData.Triangles)
            {
                var v1 = collisionData.Vectors[tri.Vector1Index];
                var v2 = collisionData.Vectors[tri.Vector2Index];
                var v3 = collisionData.Vectors[tri.Vector3Index];
                var vec1 = new Vector3(-v1.X, v1.Y, v1.Z);
                var vec2 = new Vector3(-v2.X, v2.Y, v2.Z);
                var vec3 = new Vector3(-v3.X, v3.Y, v3.Z);
                vert3s.Add(vec1);
                vert3s.Add(vec2);
                vert3s.Add(vec3);
                indices.Add((uint)(vert3s.Count - 1));
                indices.Add((uint)(vert3s.Count - 2));
                indices.Add((uint)(vert3s.Count - 3));
                vertices.AddRange(vec1.ToArray());
                vertices.AddRange(vec2.ToArray());
                vertices.AddRange(vec3.ToArray());

                var vecCol = surfaceColors[tri.SurfaceIndex];
                var col = new vec4(vecCol.R / 255f, vecCol.G / 255f, vecCol.B / 255f, 1.0f);
                colors.AddRange(col.to_array());
                colors.AddRange(col.to_array());
                colors.AddRange(col.to_array());
            }

            var normals = new Vector3[colData.Triangles.Count * 3];
            for (var i = 0; i < indices.Count; i += 3)
            {
                var vec1 = vert3s[(int)indices[i]];
                var vec2 = vert3s[(int)indices[i + 1]];
                var vec3 = vert3s[(int)indices[i + 2]];

                normals[indices[i]] += Vector3.Cross(vec2 - vec1, vec3 - vec1);
                normals[indices[i + 1]] += Vector3.Cross(vec2 - vec1, vec3 - vec1);
                normals[indices[i + 2]] += Vector3.Cross(vec2 - vec1, vec3 - vec1);
            }

            for (var i = 0; i < normals.Length; ++i)
            {
                var n = normals[i];
                n.Normalize();
                normals[i] = n;
            }

            collisionBuffer = new VertexBufferArray();
            collisionBuffer.Bind();

            var indexBuffer = new IndexBuffer();
            indexBuffer.Bind();
            indexBuffer.SetData(indices.ToArray());
            this.indices = indices.ToArray();

            var vertexBuffer = new VertexBuffer();
            vertexBuffer.Bind();
            vertexBuffer.SetData(0, vertices.ToArray(), false, 3);

            var colorBuffer = new VertexBuffer();
            colorBuffer.Bind();
            colorBuffer.SetData(1, colors.ToArray(), false, 4);

            var normalBuffer = new VertexBuffer();
            normalBuffer.Bind();
            normalBuffer.SetData(2, normals.SelectMany(v => v.ToArray()).ToArray(), false, 3);

            collisionBuffer.Unbind();
        }

        public void SetResolution(float width, float height)
        {
            projectionMat = glm.infinitePerspective(glm.radians(90.0f), width / height, 0.1f);
        }

        public void Render()
        {
            foreach (var @object in objects)
            {
                @object.Render();
            }

            Bind();

            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.Enable(EnableCap.DepthTest);
            // Draw collision
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
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

            collisionBuffer.Bind();
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
            collisionBuffer.Unbind();
            shader.Unbind();
        }

        public void Delete()
        {
            shader.Delete();
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
