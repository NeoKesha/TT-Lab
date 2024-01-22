using TT_Lab.AssetData.Graphics;
using TT_Lab.Rendering.Buffers;

namespace TT_Lab.Rendering.Objects
{
    public class BlendSkin : BaseRenderable
    {
        readonly ModelBufferBlendSkin model;

        public BlendSkin(Scene root, BlendSkinData blendSkin) : base(root)
        {
            model = new ModelBufferBlendSkin(root, blendSkin);
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

        protected override void RenderSelf()
        {
            model.Render();
        }
    }
}
