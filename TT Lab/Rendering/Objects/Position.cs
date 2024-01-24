using GlmSharp;
using System;
using TT_Lab.Rendering.Shaders;
using TT_Lab.ViewModels.Instance;

namespace TT_Lab.Rendering.Objects
{
    public class Position : BaseRenderable
    {
        private uint id;
        private int layid;
        private vec3 position;
        private vec4 color;

        public Position(Scene root, PositionViewModel pos) : base(root)
        {
            id = pos.Asset.ID;
            layid = (int)pos.LayoutID;
            position = new vec3(-pos.Position.X, pos.Position.Y, pos.Position.Z);
            pos.PropertyChanged += Pos_PropertyChanged;
            color = new vec4();
            System.Drawing.Color tmp = System.Drawing.Color.FromArgb(layid * 255 / 7, 100, 200);
            color.x = tmp.R / 255.0f;
            color.y = tmp.G / 255.0f;
            color.z = tmp.B / 255.0f;
            color.w = tmp.A / 255.0f;
            LocalTransform = mat4.Translate(position);
            LocalTransform *= mat4.Scale(0.25f);
        }

        private void Pos_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.PropertyName) && (e.PropertyName == "IsSelected" || e.PropertyName == "IsDirty")) return;
            var vm = (PositionViewModel)sender!;
            layid = (int)vm.LayoutID;
            position = new vec3(-vm.Position.X, vm.Position.Y, vm.Position.Z);
            LocalTransform = mat4.Translate(position);
            LocalTransform *= mat4.Scale(0.25f);
        }

        public void Bind()
        {

        }

        public void Delete()
        {
        }

        protected override void RenderSelf(ShaderProgram shader)
        {
            Root.DrawBox(WorldTransform, color);
        }

        public void Unbind()
        {
        }
    }
}
