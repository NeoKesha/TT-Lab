using GlmSharp;
using SharpGL;
using TT_Lab.Rendering.Shaders;

namespace TT_Lab.Rendering.Objects
{
    internal class EditorCursor : BaseRenderable
    {
        vec3? pos = null;

        public EditorCursor(OpenGL gl, GLWindow window, Scene root) : base(gl, window, root)
        {

        }

        public void Bind()
        {

        }

        public void Delete()
        {

        }

        public void SetPosition(vec3 newPos)
        {
            pos = newPos;
            LocalTransform = mat4.Translate(pos.Value) * mat4.Scale(0.1f);
        }

        public vec3 GetPosition()
        {
            return pos.Value;
        }

        protected override void RenderSelf(ShaderProgram shader)
        {
            if (pos == null)
            {
                return;
            }

            Window.DrawBox(WorldTransform, new vec4(0.5f, 0.0f, 1.0f, 1.0f));
        }

        public void Unbind()
        {

        }

    }
}
