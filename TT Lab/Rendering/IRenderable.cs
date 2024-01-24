
using GlmSharp;
using System.Collections.Generic;
using TT_Lab.Rendering.Shaders;

namespace TT_Lab.Rendering
{
    public interface IRenderable : IGLObject
    {
        Scene Root { get; set; }
        IRenderable? Parent { get; set; }
        bool Enabled { get; set; }
        List<IRenderable> Children { get; set; }
        float Opacity { get; set; }
        mat4 WorldTransform { get; }
        mat4 LocalTransform { get; set; }
        void PreRender() { }
        void Render(ShaderProgram shader, bool transparent);
        void PostRender() { }
        void AddChild(IRenderable child);
        void RemoveChild(IRenderable child);
        void EnableAlphaBlending();
        void DisableAlphaBlending();
        bool HasAlphaBlending();
        void Enable();
        void Disable();
        void UpdateTransform();
    }
}
