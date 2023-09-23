using OpenTK.Graphics.OpenGL;

namespace TT_Lab.Rendering.Buffers
{
    public class DisplayList : IGLObject
    {
        private int listBuffer;
        private ListMode listMode;

        public DisplayList(ListMode mode)
        {
            listBuffer = GL.GenLists(1);
            listMode = mode;
        }

        public void Bind()
        {
            GL.NewList(listBuffer, listMode);
        }

        public void Delete()
        {
            GL.DeleteLists(listBuffer, 1);
        }

        public void Unbind()
        {
            GL.EndList();
        }

        public int Buffer
        {
            get => listBuffer;
        }
    }
}
