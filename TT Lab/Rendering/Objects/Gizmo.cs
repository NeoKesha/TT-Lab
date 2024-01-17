using GlmSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Rendering.Buffers;

namespace TT_Lab.Rendering.Objects
{
    internal class Gizmo : BaseRenderable
    {
        private IRenderable? anchor;
        public Gizmo(Scene root, IRenderable? anchor = null) : base(root)
        {
            this.anchor = anchor;
        }

        public void Bind() 
        { 

        }

        public void Delete()
        {

        }

        protected override void RenderSelf()
        {
            Root.DrawSimpleAxis(WorldTransform);
        }

        public void Unbind()
        {

        }

    }
}
