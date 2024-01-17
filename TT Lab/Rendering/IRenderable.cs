
using GlmSharp;
using System.Collections.Generic;
using TT_Lab.Rendering.Shaders;

namespace TT_Lab.Rendering
{
    public interface IRenderable : IGLObject
    {
        Scene Root { get; set; }
        IRenderable? Parent { get; set; }
        List<IRenderable> Children { get; set; }
        float Opacity { get; set; }
        mat4 WorldTransform { get; }
        mat4 LocalTransform { get; set; }
        void PreRender() { }
        void SetUniforms(ShaderProgram shader);
        void Render();
        void PostRender() { }
        void AddChild(IRenderable child);
        void RemoveChild(IRenderable child);
        public void UpdateTransform();
    }
}
