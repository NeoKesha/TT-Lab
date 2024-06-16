using SharpGL;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Shaders;

namespace TT_Lab.Rendering.Objects
{
    public class Skin : BaseRenderable
    {
        ModelBuffer model;

        public Skin(OpenGL gl, GLWindow window, Scene root, SkinData skin) : base(gl, window, root)
        {
            model = new ModelBuffer(GL, window, root, skin);
            AddChild(model);
        }

        public void Delete()
        {
            model.Delete();
        }

        protected override void RenderSelf(ShaderProgram shader)
        {
        }
    }
}
