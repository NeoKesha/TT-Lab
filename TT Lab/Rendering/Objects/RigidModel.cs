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

        public override void Render()
        {
            model.Render();
        }
    }
}
