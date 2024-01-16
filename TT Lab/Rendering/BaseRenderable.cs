using GlmSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Rendering.Shaders;

namespace TT_Lab.Rendering
{
    public abstract class BaseRenderable : IRenderable
    {
        private mat4 globalTransform;
        private mat4 localTransform;
        private IRenderable parent;

        public Scene Root { get; set; }
        public IRenderable Parent
        {
            get => parent;
            set
            {
                parent = value;
                UpdateChildrenTransform();
            }
        }
        public List<IRenderable> Children { get; set; } = new();
        public Single Opacity { get; set; } = 1.0f;
        public mat4 GlobalTransform
        {
            get => globalTransform;
            set
            {
                globalTransform = value;
                UpdateChildrenTransform();
            }
        }
        public mat4 LocalTransform
        {
            get => localTransform;
            set
            {
                localTransform = value;
                GlobalTransform = LocalTransform * Parent.GlobalTransform;
            }
        }

        public BaseRenderable(Scene? root)
        {
            Root = root;
            parent = Root;
            globalTransform = mat4.Identity;
            localTransform = mat4.Identity;
        }

        public abstract void Render();

        public virtual void AddChild(IRenderable child)
        {
            Children.Add(child);
            child.Parent = this;
            UpdateChildrenTransform();
        }

        public virtual void RemoveChild(IRenderable child)
        {
            Children.Remove(child);
            child.Parent = Root;
            UpdateChildrenTransform();
        }

        private void UpdateChildrenTransform()
        {
            foreach (var child in Children)
            {
                child.GlobalTransform = child.LocalTransform * GlobalTransform;
            }
        }

        public virtual void SetUniforms(ShaderProgram shader)
        {
            shader.SetUniformMatrix4("Model", GlobalTransform.Values1D);
        }
    }
}
