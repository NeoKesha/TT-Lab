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
        private mat4 cachedWorldTransform;
        private mat4 localTransform;
        private IRenderable? parent;

        public Scene Root { get; set; }
        public IRenderable? Parent
        {
            get => parent;
            set
            {
                parent = value;
            }
        }
        public List<IRenderable> Children { get; set; } = new();
        public Single Opacity { get; set; } = 1.0f;
        public mat4 WorldTransform
        {
            get => cachedWorldTransform;
        }
        public mat4 LocalTransform
        {
            get => localTransform;
            set
            {
                localTransform = value;
                UpdateTransform();
            }
        }

        public BaseRenderable(Scene root)
        {
            Root = root;
            parent = root;
            localTransform = mat4.Identity;
            UpdateTransform();
        }

        public void UpdateTransform()
        {
            cachedWorldTransform = (parent != null) ? parent.WorldTransform * localTransform : localTransform;
            foreach (var child in Children)
            {
                child.UpdateTransform();
            }
        }

        public abstract void Render();

        public virtual void AddChild(IRenderable child)
        {
            Children.Add(child);
            child.Parent = this;
            child.UpdateTransform();
        }

        public virtual void RemoveChild(IRenderable child)
        {
            Children.Remove(child);
            child.Parent = Root;
            child.UpdateTransform();
        }

        public virtual void SetUniforms(ShaderProgram shader)
        {
            shader.SetUniformMatrix4("Model", WorldTransform.Values1D);
        }
    }
}
