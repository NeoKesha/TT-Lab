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

        public Gizmo(Scene root, IRenderable? parent = null) : base(root)
        {

        }

        public void Bind() 
        { 

        }

        public void Delete()
        {

        }

        public override void Render()
        {
            Root.DrawSimpleAxis(WorldTransform);
        }

        public void Unbind()
        {

        }

    }
}
