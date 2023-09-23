namespace TT_Lab.Rendering
{
    public interface IRenderable : IGLObject
    {
        Scene? Parent { get; set; }
        float Opacity { get; set; }
        void PreRender() { }
        void Render();
        void PostRender() { }
    }
}
