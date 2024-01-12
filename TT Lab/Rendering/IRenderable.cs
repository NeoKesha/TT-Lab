
using System.Collections.Generic;

namespace TT_Lab.Rendering
{
    public interface IRenderable : IGLObject
    {
        Scene Root { get; set; }
        List<IRenderable> Children { get; set; }
        float Opacity { get; set; }
        void PreRender() { }
        void Render();
        void PostRender() { }
        void AddChild(IRenderable child);
        void RemoveChild(IRenderable child);
    }
}
