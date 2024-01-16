using GlmSharp;
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
        private vec4 color;

        public Position(Scene root, PositionViewModel pos) : base(root)
        {
            id = pos.Asset.ID;
            layid = (int)pos.LayoutID;
            this.pos = new Vector4(pos.Position.X, pos.Position.Y, pos.Position.Z, pos.Position.W);
            positionBuffer = BufferGeneration.GetCubeBuffer(this.pos.Xyz, 0.3f, new List<System.Drawing.Color> { System.Drawing.Color.FromArgb(layid * 255 / 7, 100, 200) });
            pos.PropertyChanged += Pos_PropertyChanged;
            color = new vec4();
            System.Drawing.Color tmp = System.Drawing.Color.FromArgb(layid * 255 / 7, 100, 200);
            color.x = tmp.R / 255.0f;
            color.y = tmp.G / 255.0f;
            color.z = tmp.B / 255.0f;
            color.w = tmp.A / 255.0f;
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
            Root?.Renderer.RenderProgram.SetUniform1("Alpha", Opacity);
            positionBuffer.Bind();
        }

        public void Delete()
        {
            positionBuffer.Delete();
        }

        public override void Render()
        {
            Root.DrawCircle(new vec3(-pos.X, pos.Y, pos.Z),new vec3(0,0,0) ,vec3.Ones, color);
            Root.DrawCircle(new vec3(-pos.X, pos.Y, pos.Z),new vec3(90,0,0) ,vec3.Ones, color);
            Root.DrawCircle(new vec3(-pos.X, pos.Y, pos.Z),new vec3(0,0,90) ,vec3.Ones, color);
            Root.DrawLine(new vec3(-pos.X, pos.Y, pos.Z), new vec3(-pos.X - 1, pos.Y, pos.Z), new vec4(1.0f, 0.0f, 0.0f, 1.0f));
            Root.DrawLine(new vec3(-pos.X, pos.Y, pos.Z), new vec3(-pos.X, pos.Y + 1, pos.Z), new vec4(0.0f, 1.0f, 0.0f, 1.0f));
            Root.DrawLine(new vec3(-pos.X, pos.Y, pos.Z), new vec3(-pos.X, pos.Y, pos.Z + 1), new vec4(0.0f, 0.0f, 1.0f, 1.0f));
        }

        public void Unbind()
        {
            positionBuffer.Unbind();
        }
    }
}
