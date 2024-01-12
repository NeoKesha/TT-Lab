using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Rendering
{
    public abstract class BaseRenderable : IRenderable
    {
        public Scene Root { get; set; }
        public List<IRenderable> Children { get; set; } = new();
        public Single Opacity { get; set; } = 1.0f;

        public BaseRenderable(Scene? root)
        {
            Root = root;
        }

        public abstract void Render();

        public virtual void AddChild(IRenderable child)
        {
            Children.Add(child);
        }

        public virtual void RemoveChild(IRenderable child)
        {
            Children.Remove(child);
        }
    }
}
