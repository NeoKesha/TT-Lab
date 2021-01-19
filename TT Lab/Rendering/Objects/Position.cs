using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;

namespace TT_Lab.Rendering.Objects
{
    public class Position : IRenderable
    {
        public Scene? Parent { get; set; }
        public float Opacity { get; set; } = 1.0f;

        private uint id;
        private int layid;
        private Vector4 pos;

        public Position(Assets.Instance.Position pos)
        {
            id = pos.ID;
            layid = (int)pos.LayoutID!;
            var posData = (PositionData)pos.GetData();
            this.pos = new Vector4(posData.Coords.X, posData.Coords.Y, posData.Coords.Z, posData.Coords.W);
        }

        public void Bind()
        {
        }

        public void Delete()
        {
        }

        public void PostRender()
        {
        }

        public void PreRender()
        {
        }

        public void Render()
        {
            /*GL.Begin(PrimitiveType.LineLoop);
            GL.LineWidth(20.0f);
            GL.Color3(255, 0, 0);
            GL.Vertex4(pos.X, pos.Y, pos.Z - 1, pos.W);
            GL.Vertex4(pos.X + 1, pos.Y, pos.Z, pos.W);
            GL.Vertex4(pos.X, pos.Y + 1, pos.Z, pos.W);
            GL.Vertex4(pos.X - 1, pos.Y, pos.Z + 1, pos.W);
            GL.End();*/
        }

        public void Unbind()
        {
        }
    }
}
