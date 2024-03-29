﻿using TT_Lab.AssetData.Graphics;
using TT_Lab.Rendering.Buffers;

namespace TT_Lab.Rendering.Objects
{
    public class RigidModel : BaseRenderable
    {
        ModelBuffer model;

        public RigidModel(Scene root, RigidModelData rigid) : base(root)
        {
            model = new ModelBuffer(root, rigid);
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
