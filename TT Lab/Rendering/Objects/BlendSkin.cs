using SharpGL;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Shaders;

namespace TT_Lab.Rendering.Objects
{
    public class BlendSkin : BaseRenderable
    {
        readonly ModelBufferBlendSkin model;

        public BlendSkin(OpenGL gl, GLWindow window, Scene root, BlendSkinData blendSkin) : base(gl, window, root)
        {
            model = new ModelBufferBlendSkin(gl, window, root, blendSkin);
            AddChild(model);
        }

        public void SetBlendShapeValue(int index, float value)
        {
            model.BlendShapesValues[index] = value;
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
