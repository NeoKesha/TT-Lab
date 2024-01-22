using TT_Lab.AssetData.Graphics;
using TT_Lab.Rendering.Buffers;

namespace TT_Lab.Rendering.Objects
{
    public class Skin : BaseRenderable
    {
        ModelBuffer model;

        public Skin(Scene root, SkinData skin) : base(root)
        {
            model = new ModelBuffer(root, skin);
            AddChild(model);
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
