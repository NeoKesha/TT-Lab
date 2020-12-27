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
        private ushort[] indices;
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
            var indices = new List<ushort>();
            
            /*foreach (var v in collisionData.Vectors)
            {
                var vec = new vec3(-v.X, v.Y, v.Z);
                var nor = glm.normalize(vec);
                var col = new vec4(nor.x, nor.y, nor.z, 1.0f);
                vertices.AddRange(vec.to_array());
                colors.AddRange(col.to_array());
            }*/
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
                indices.Add((ushort)(vert3s.Count - 3));
                indices.Add((ushort)(vert3s.Count - 2));
                indices.Add((ushort)(vert3s.Count - 1));
                vertices.AddRange(vec1.ToArray());
                vertices.AddRange(vec2.ToArray());
                vertices.AddRange(vec3.ToArray());
                
                vec1.Normalize();
                vec2.Normalize();
                vec3.Normalize();
                var col = new vec4(vec1.X, vec1.Y, vec1.Z, 1.0f);
                colors.AddRange(col.to_array());
                col = new vec4(vec2.X, vec2.Y, vec2.Z, 1.0f);
                colors.AddRange(col.to_array());
                col = new vec4(vec3.X, vec3.Y, vec3.Z, 1.0f);
                colors.AddRange(col.to_array());
            }

            var normals = new Vector3[colData.Triangles.Count * 3];
            for (var i = 0; i < indices.Count; i += 3)
            {
                var vec1 = vert3s[indices[i]];
                var vec2 = vert3s[indices[i + 1]];
                var vec3 = vert3s[indices[i + 2]];

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

            // Draw collision
            collisionBuffer.Bind();
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedShort, IntPtr.Zero);
            collisionBuffer.Unbind();

            Unbind();
        }

        public void Bind()
        {
            shader.Bind();
            // Fragment program uniforms
            shader.SetUniform3("AmbientMaterial", 0.04f, 0.04f, 0.04f);
            shader.SetUniform3("SpecularMaterial", 0.5f, 0.5f, 0.5f);
            shader.SetUniform3("LightPosition", 0.25f, 0.25f, 1f);
            shader.SetUniform1("Shininess", 50f);

            // Vertex program uniforms
            shader.SetUniformMatrix4("Projection", projectionMat.to_array());
            shader.SetUniformMatrix4("View", viewMat.to_array());
            shader.SetUniformMatrix4("Model", modelMat.to_array());
            shader.SetUniformMatrix3("NormalMatrix", normalMat.to_array());
            shader.SetUniform3("DiffuseMaterial", 0f, 0.75f, 0.75f);

            collisionBuffer.Bind();
        }

        public void RotateView(Vector3 rot)
        {
            rot.Normalize();
            vec3 cross = glm.normalize(glm.cross(cameraUp, cameraDirection));
            mat4 rotMat = glm.rotate(new mat4(1.0f), glm.radians(rot.Y), cross);
            rotMat = glm.rotate(rotMat, glm.radians(rot.X), cameraUp);
            cameraDirection = glm.normalize(new vec3(rotMat * new vec4(cameraDirection, 0.0f)));
            viewMat = glm.lookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
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
            viewMat = glm.lookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
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
    }
}
