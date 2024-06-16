﻿using GlmSharp;
using SharpGL;
using TT_Lab.Rendering.Shaders;

namespace TT_Lab.Rendering.Objects
{
    internal class Gizmo : BaseRenderable
    {
        private EditingContext editingContext;
        public Gizmo(OpenGL gl, GLWindow window, Scene root, EditingContext editingContext) : base(gl, window, root)
        {
            this.editingContext = editingContext;
            Enabled = true;
        }

        public void Bind()
        {

        }

        public void Delete()
        {

        }

        protected override void RenderSelf(ShaderProgram shader)
        {
            if (editingContext.selectedInstance == null)
            {
                return;
            }
            var selectedRenderable = editingContext.selectedRenderable;
            TransformSpace transformSpace = editingContext.transformSpace;
            TransformMode transformMode = editingContext.transformMode;
            switch (transformSpace)
            {
                case TransformSpace.LOCAL:
                    LocalTransform = mat4.Identity;
                    break;
                case TransformSpace.WORLD:
                    var quat = selectedRenderable.LocalTransform.ToQuaternion;
                    LocalTransform = quat.ToMat4.Inverse;
                    break;
            }
            switch (transformMode)
            {
                case TransformMode.SELECTION:
                    LocalTransform *= mat4.Scale(0.25f);
                    Window.DrawBox(WorldTransform, new vec4(1.0f, 0.0f, 0.0f, 1.0f));
                    break;
                case TransformMode.TRANSLATE:
                    Window.DrawSimpleAxis(WorldTransform);
                    if (editingContext.transformAxis == TransformAxis.X)
                    {
                        LocalTransform *= mat4.Translate(0.5f, 0.0f, 0.0f);
                        LocalTransform *= mat4.Scale(0.5f, 0.025f, 0.025f);
                        Window.DrawBox(WorldTransform, new vec4(1.0f, 0.0f, 0.0f, 1.0f));
                    }
                    if (editingContext.transformAxis == TransformAxis.Y)
                    {
                        LocalTransform *= mat4.Translate(0.0f, 0.5f, 0.0f);
                        LocalTransform *= mat4.Scale(0.025f, 0.5f, 0.025f);
                        Window.DrawBox(WorldTransform, new vec4(0.0f, 1.0f, 0.0f, 1.0f));
                    }
                    if (editingContext.transformAxis == TransformAxis.Z)
                    {
                        LocalTransform *= mat4.Translate(0.0f, 0.0f, 0.5f);
                        LocalTransform *= mat4.Scale(0.025f, 0.025f, 0.5f);
                        Window.DrawBox(WorldTransform, new vec4(0.0f, 0.0f, 1.0f, 1.0f));
                    }
                    break;
                case TransformMode.SCALE:
                    {
                        Window.DrawSimpleAxis(WorldTransform);
                        var localTransformCopy = LocalTransform;
                        LocalTransform = localTransformCopy * mat4.Translate(1.0f, 0.0f, 0.0f) * mat4.Scale(0.1f);
                        Window.DrawBox(WorldTransform, new vec4(1.0f, 0.0f, 0.0f, 1.0f));
                        LocalTransform = localTransformCopy * mat4.Translate(0.0f, 1.0f, 0.0f) * mat4.Scale(0.1f);
                        Window.DrawBox(WorldTransform, new vec4(0.0f, 1.0f, 0.0f, 1.0f));
                        LocalTransform = localTransformCopy * mat4.Translate(0.0f, 0.0f, 1.0f) * mat4.Scale(0.1f);
                        Window.DrawBox(WorldTransform, new vec4(0.0f, 0.0f, 1.0f, 1.0f));
                        break;
                    }
                case TransformMode.ROTATE:
                    {
                        var localTransformCopy = LocalTransform;
                        if (editingContext.transformAxis == TransformAxis.Y)
                        {
                            LocalTransform *= mat4.Scale(1.25f);
                        }
                        Window.DrawCircle(WorldTransform, new vec4(0.0f, 1.0f, 0.0f, 1.0f));
                        LocalTransform = localTransformCopy * mat4.RotateX(3.14f / 2.0f);
                        if (editingContext.transformAxis == TransformAxis.Z)
                        {
                            LocalTransform *= mat4.Scale(1.25f);
                        }
                        Window.DrawCircle(WorldTransform, new vec4(0.0f, 0.0f, 1.0f, 1.0f));

                        LocalTransform = localTransformCopy * mat4.RotateZ(3.14f / 2.0f);
                        if (editingContext.transformAxis == TransformAxis.X)
                        {
                            LocalTransform *= mat4.Scale(1.25f);
                        }
                        Window.DrawCircle(WorldTransform, new vec4(1.0f, 0.0f, 0.0f, 1.0f));
                        break;
                    }

            }

        }

        public void Unbind()
        {

        }

    }
}
