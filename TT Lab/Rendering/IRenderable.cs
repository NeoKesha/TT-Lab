using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Rendering
{
    public interface IRenderable : IGLObject
    {
        void PreRender();
        void Render();
        void PostRender();
    }
}
