﻿using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;
using TT_Lab.ViewModels.Instance;

namespace TT_Lab.Rendering.Objects
{
    public class Trigger : BaseRenderable
    {

        private uint id;
        private int layid;
        private Vector4 pos;
        private Vector4 scale;
        private Vector4 rotation;
        private IndexedBufferArray triggerBuffer;

        public Trigger(Scene root, TriggerViewModel tvm) : base(root)
        {
            Opacity = 0.4f;
            id = tvm.Asset.ID;
            layid = (int)tvm.LayoutID;
            pos = new Vector4(tvm.Position.X, tvm.Position.Y, tvm.Position.Z, tvm.Position.W);
            scale = new Vector4(tvm.Scale.X, tvm.Scale.Y, tvm.Scale.Z, tvm.Scale.W);
            rotation = new Vector4(tvm.Rotation.X, tvm.Rotation.Y, tvm.Rotation.Z, tvm.Rotation.W);
            var trgColor = System.Drawing.Color.DarkOrange;
            trgColor = System.Drawing.Color.FromArgb(100, trgColor.R >> 1, trgColor.G >> 1, trgColor.B >> 1);
            triggerBuffer = BufferGeneration.GetCubeBuffer(pos.Xyz, scale.Xyz, new Quaternion(rotation.Xyz, rotation.W), new List<System.Drawing.Color>
            {
                trgColor
            });
            tvm.PropertyChanged += Tvm_PropertyChanged;
        }

        private void Tvm_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.PropertyName) && e.PropertyName == "IsSelected") return;
            var tvm = (TriggerViewModel)sender!;
            triggerBuffer.Delete();
            layid = (int)tvm.LayoutID;
            pos = new Vector4(tvm.Position.X, tvm.Position.Y, tvm.Position.Z, tvm.Position.W);
            scale = new Vector4(tvm.Scale.X, tvm.Scale.Y, tvm.Scale.Z, tvm.Scale.W);
            rotation = new Vector4(tvm.Rotation.X, tvm.Rotation.Y, tvm.Rotation.Z, tvm.Rotation.W);
            var trgColor = System.Drawing.Color.Orange;
            trgColor = System.Drawing.Color.FromArgb(100, trgColor.R, trgColor.G, trgColor.B);
            triggerBuffer = BufferGeneration.GetCubeBuffer(pos.Xyz, scale.Xyz, new Quaternion(rotation.Xyz, rotation.W), new List<System.Drawing.Color>
            {
                trgColor
            });
        }

        public void Bind()
        {
            Root?.Renderer.RenderProgram.SetUniform1("Alpha", Opacity);
            triggerBuffer.Bind();
        }

        public void Delete()
        {
            triggerBuffer.Delete();
        }

        public override void Render()
        {
            Bind();
            GL.DrawElements(PrimitiveType.Triangles, triggerBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
            GL.DrawElements(PrimitiveType.Lines, triggerBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
            Unbind();
        }

        public void Unbind()
        {
            triggerBuffer.Unbind();
        }
    }
}
