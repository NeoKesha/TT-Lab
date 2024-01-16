using GlmSharp;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using TT_Lab.Assets.Instance;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;
using TT_Lab.ViewModels.Instance;

namespace TT_Lab.Rendering.Objects
{
    public class Trigger : BaseRenderable
    {

        private uint id;
        private int layid;
        private vec3 pos;
        private vec3 scale;
        private vec3 rotation;
        private vec4 color;

        private TriggerViewModel viewModel;

        public Trigger(Scene root, TriggerViewModel tvm) : base(root)
        {
            Opacity = 0.4f;
            id = tvm.Asset.ID;
            layid = (int)tvm.LayoutID;
            viewModel = tvm;
            viewModel.PropertyChanged += Tvm_PropertyChanged;
            Update();
        }

        private void Tvm_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.PropertyName) && e.PropertyName == "IsSelected") return;
            Update();
        }

        private void Update()
        {
            pos = new vec3(-viewModel.Position.X, viewModel.Position.Y, viewModel.Position.Z);
            scale = new vec3(viewModel.Scale.X, viewModel.Scale.Y, viewModel.Scale.Z);

            var q = new Quaternion(viewModel.Rotation.X, viewModel.Rotation.Y, viewModel.Rotation.Z, viewModel.Rotation.W);
            var tmp = q.ToEulerAngles();
            rotation = new vec3(tmp.X, tmp.Y, tmp.Z);

            var trgColor = System.Drawing.Color.DarkOrange;
            trgColor = System.Drawing.Color.FromArgb(100, trgColor.R >> 1, trgColor.G >> 1, trgColor.B >> 1);
            color = new vec4(trgColor.R / 255.0f, trgColor.G / 255.0f, trgColor.B / 255.0f, Opacity);

            mat4 matrixPosition = mat4.Translate(pos.x, pos.y, pos.z);
            mat4 matrixRotationX, matrixRotationY, matrixRotationZ;
            matrixRotationX = mat4.RotateX(rotation.x);
            matrixRotationY = mat4.RotateY(rotation.y);
            matrixRotationZ = mat4.RotateZ(rotation.z);
            mat4 matrixScale = mat4.Scale(scale);
            LocalTransform = mat4.Identity;

            LocalTransform *= matrixPosition;
            LocalTransform *= matrixRotationZ * matrixRotationY * matrixRotationX;
            LocalTransform *= matrixScale;
        }

        public void Bind()
        {
        }

        public void Delete()
        {
            viewModel.PropertyChanged -= Tvm_PropertyChanged;
        }

        public override void Render()
        {
            Root.DrawBox(GlobalTransform, color);
        }

        public void Unbind()
        {
        }
    }
}
