using SharpGL;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Shaders;

namespace TT_Lab.Rendering.Objects
{
    public class RigidModel : BaseRenderable
    {
        ModelBuffer model;

        public RigidModel(OpenGL gl, GLWindow window, Scene root, RigidModelData rigid) : base(gl, window, root)
        {
            model = new ModelBuffer(GL, window, root, rigid);
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
