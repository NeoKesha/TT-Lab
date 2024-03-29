﻿using TT_Lab.AssetData.Graphics;
using TT_Lab.Rendering.Buffers;

namespace TT_Lab.Rendering.Objects
{
    public class Model : BaseRenderable
    {
        ModelBuffer model;

        public Model(Scene root, ModelData model) : base(root)
        {
            this.model = new ModelBuffer(root, model);
            AddChild(this.model);
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
