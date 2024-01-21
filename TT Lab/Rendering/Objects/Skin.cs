using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;

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
