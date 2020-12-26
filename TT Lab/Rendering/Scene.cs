using GlmNet;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
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

        private CollisionData colData;
        private ShaderProgram shader;

        public Scene(CollisionData collisionData, float width, float height)
        {
            colData = collisionData;

            var passVerShader = ManifestResourceLoader.LoadTextFile(@"Shaders\Pass.vert");
            var passFragShader = ManifestResourceLoader.LoadTextFile(@"Shaders\Pass.frag");
            shader = new ShaderProgram(passVerShader, passFragShader, new Dictionary<uint, string> {
                { 0, "in_Position" },
                { 1, "in_Color" }
            });

            const float rads = (60.0f / 360.0f) * (float)Math.PI * 2.0f;
            projectionMat = glm.infinitePerspective(rads, width / height, 0.1f);
            viewMat = glm.translate(new mat4(1.0f), new vec3(0.0f, 0.0f, -5.0f));
            modelMat = glm.scale(new mat4(1.0f), new vec3(1.0f));

            // Init collision buffer
            var vertices = new List<float>();
            var colors = new List<float>();
            foreach (var v in collisionData.Vectors)
            {
                var vec = new vec3(-v.X, v.Y, v.Z);
                var nor = glm.normalize(vec);
                var col = new vec4(nor.x, nor.y, nor.z, 1.0f);
                vertices.AddRange(vec.to_array());
                colors.AddRange(col.to_array());
            }
        }

        public void SetResolution(float width, float height)
        {
            const float rads = (60.0f / 360.0f) * (float)Math.PI * 2.0f;
            projectionMat = glm.infinitePerspective(rads, width / height, 0.1f);
        }

        public void Render()
        {
            Bind();

            /*collisionBuffer.Bind(gl);

            gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, colData.Vectors.Count);

            collisionBuffer.Unbind(gl);*/
            Unbind();
        }

        public void Bind()
        {
            shader.Bind();
            shader.SetUniformMatrix4("projectionMatrix", projectionMat.to_array());
            shader.SetUniformMatrix4("viewMatrix", viewMat.to_array());
            shader.SetUniformMatrix4("modelMatrix", modelMat.to_array());
        }

        public void Unbind()
        {
            shader.Unbind();
        }

        public void Delete()
        {
            shader.Delete();
        }

        public void PreRender()
        {
            viewMat = glm.rotate(viewMat, glm.radians(5), new vec3(0, 1, 0));
        }

        public void PostRender()
        {
        }
    }
}
