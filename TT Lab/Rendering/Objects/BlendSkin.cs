using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;

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
