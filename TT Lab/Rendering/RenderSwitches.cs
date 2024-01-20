namespace TT_Lab.Rendering
{
    /// <summary>
    /// Contained different enums on how to render the scene
    /// </summary>
    public static class RenderSwitches
    {
        public enum TranslucencyMethod
        {
            /// <summary>
            /// Weighted blended
            /// </summary>
            WBOIT,
            /// <summary>
            /// Basic renderer with no translucency support
            /// </summary>
            BASIC
        }
    }
}
