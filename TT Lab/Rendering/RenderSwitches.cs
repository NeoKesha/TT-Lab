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
            /// Dual Depth Peeling
            /// </summary>
            DDP
        }
    }
}
