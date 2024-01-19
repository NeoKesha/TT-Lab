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
            if (anchor == null)
            {
                return;
            }
            TransformSpace transformSpace = Root.editingContext.transformSpace;
            TransformMode transformMode = Root.editingContext.transformMode;
            switch (transformSpace)
            {
                case TransformSpace.LOCAL:
                    LocalTransform = mat4.Identity;
                    break;
                case TransformSpace.WORLD:
                    var quat = anchor.LocalTransform.ToQuaternion;
                    LocalTransform = quat.ToMat4.Inverse;
                    break;
            }
            switch (transformMode)
            {
                case TransformMode.SELECTION:
                    LocalTransform *= mat4.Scale(0.25f);
                    Root.DrawBox(WorldTransform, new vec4(1.0f, 0.0f, 0.0f, 1.0f));
                    break;
                case TransformMode.TRANSLATE:
                    Root.DrawSimpleAxis(WorldTransform);
                    if (Root.editingContext.transformAxis == TransformAxis.X)
                    {
                        LocalTransform *= mat4.Translate(0.5f, 0.0f, 0.0f);
                        LocalTransform *= mat4.Scale(0.5f,0.025f, 0.025f);
                        Root.DrawBox(WorldTransform, new vec4(1.0f, 0.0f, 0.0f, 1.0f));
                    }
                    if (Root.editingContext.transformAxis == TransformAxis.Y)
                    {
                        LocalTransform *= mat4.Translate(0.0f, 0.5f, 0.0f);
                        LocalTransform *= mat4.Scale(0.025f, 0.5f, 0.025f);
                        Root.DrawBox(WorldTransform, new vec4(0.0f, 1.0f, 0.0f, 1.0f));
                    }
                    if (Root.editingContext.transformAxis == TransformAxis.Z)
                    {
                        LocalTransform *= mat4.Translate(0.0f, 0.0f, 0.5f);
                        LocalTransform *= mat4.Scale(0.025f, 0.025f, 0.5f);
                        Root.DrawBox(WorldTransform, new vec4(0.0f, 0.0f, 1.0f, 1.0f));
                    }
                    break;
                case TransformMode.SCALE:
                    {
                        Root.DrawSimpleAxis(WorldTransform);
                        var localTransformCopy = LocalTransform;
                        LocalTransform = localTransformCopy * mat4.Translate(1.0f, 0.0f, 0.0f) * mat4.Scale(0.1f);
                        Root.DrawBox(WorldTransform, new vec4(1.0f, 0.0f, 0.0f, 1.0f));
                        LocalTransform = localTransformCopy * mat4.Translate(0.0f, 1.0f, 0.0f) * mat4.Scale(0.1f);
                        Root.DrawBox(WorldTransform, new vec4(0.0f, 1.0f, 0.0f, 1.0f));
                        LocalTransform = localTransformCopy * mat4.Translate(0.0f, 0.0f, 1.0f) * mat4.Scale(0.1f);
                        Root.DrawBox(WorldTransform, new vec4(0.0f, 0.0f, 1.0f, 1.0f));
                        break;
                    }
                case TransformMode.ROTATE:
                    {
                        var localTransformCopy = LocalTransform;
                        if (Root.editingContext.transformAxis == TransformAxis.Y)
                        {
                            LocalTransform *= mat4.Scale(1.25f);
                        }
                        Root.DrawCircle(WorldTransform, new vec4(0.0f, 1.0f, 0.0f, 1.0f));
                        LocalTransform = localTransformCopy * mat4.RotateX(3.14f / 2.0f);
                        if (Root.editingContext.transformAxis == TransformAxis.Z)
                        {
                            LocalTransform *= mat4.Scale(1.25f);
                        }
                        Root.DrawCircle(WorldTransform, new vec4(0.0f, 0.0f, 1.0f, 1.0f));

                        LocalTransform = localTransformCopy * mat4.RotateZ(3.14f / 2.0f);
                        if (Root.editingContext.transformAxis == TransformAxis.X)
                        {
                            LocalTransform *= mat4.Scale(1.25f);
                        }
                        Root.DrawCircle(WorldTransform, new vec4(1.0f, 0.0f, 0.0f, 1.0f));
                        break;
                    }
                    
            }
            
        }

        public void Unbind()
        {

        }

    }
}
