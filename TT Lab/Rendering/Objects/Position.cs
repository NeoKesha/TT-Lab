using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;
using TT_Lab.ViewModels.Instance;

namespace TT_Lab.Rendering.Objects
{
    public class Position : BaseRenderable
    {
        private uint id;
        private int layid;
        private Vector4 pos;
        private IndexedBufferArray positionBuffer;

        public Position(Scene root, PositionViewModel pos) : base(root)
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
            Root?.Renderer.RenderProgram.SetUniform1("Opacity", Opacity);
            positionBuffer.Bind();
        }

        public void Delete()
        {
            positionBuffer.Delete();
        }

        public override void Render()
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
