namespace TT_Lab.Rendering
{
    public interface IGLObject
    {
        SharpGL.OpenGL GL { get; }
        void Bind() { }
        void Unbind() { }
        void Delete() { }
    }
}
