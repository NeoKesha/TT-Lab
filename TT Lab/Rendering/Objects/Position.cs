﻿using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;
using TT_Lab.ViewModels.Instance;

namespace TT_Lab.Rendering.Objects
{
    public class Position : IRenderable
    {
        public Scene? Parent { get; set; }
        public float Opacity { get; set; } = 1.0f;

        private uint id;
        private int layid;
        private Vector4 pos;
        private IndexedBufferArray positionBuffer;

        public Position(PositionViewModel pos)
        {
            id = pos.Asset.ID;
            layid = (int)pos.LayoutID;
            this.pos = new Vector4(pos.Position.X, pos.Position.Y, pos.Position.Z, pos.Position.W);
            positionBuffer = BufferGeneration.GetCubeBuffer(this.pos.Xyz, 0.3f, new List<System.Drawing.Color> { System.Drawing.Color.FromArgb(layid * 255 / 7, 100, 200) });
            pos.PropertyChanged += Pos_PropertyChanged;
        }

        private void Pos_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.PropertyName) && (e.PropertyName == "IsSelected" || e.PropertyName == "IsDirty")) return;
            var vm = (PositionViewModel)sender!;
            positionBuffer.Delete();
            layid = (int)vm.LayoutID;
            pos = new Vector4(vm.Position.X, vm.Position.Y, vm.Position.Z, vm.Position.W);
            positionBuffer = BufferGeneration.GetCubeBuffer(pos.Xyz, 0.3f, new List<System.Drawing.Color> { System.Drawing.Color.FromArgb(layid * 255 / 7, 100, 200) });
        }

        public void Bind()
        {
            Parent?.Renderer.RenderProgram.SetUniform1("Alpha", Opacity);
            positionBuffer.Bind();
        }

        public void Delete()
        {
            positionBuffer.Delete();
        }

        public void Render()
        {
            Bind();
            GL.DrawElements(PrimitiveType.Triangles, positionBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
            Unbind();
        }

        public void Unbind()
        {
            positionBuffer.Unbind();
        }
    }
}
