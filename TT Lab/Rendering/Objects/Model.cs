using SharpGL;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Shaders;

namespace TT_Lab.Rendering.Objects
{
    public class Model : BaseRenderable
    {
        ModelBuffer model;

        public Model(OpenGL gl, GLWindow window, Scene root, ModelData model) : base(gl, window, root)
        {
            this.model = new ModelBuffer(GL, window, root, model);
            AddChild(this.model);
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
