using System.Runtime.CompilerServices;

namespace NvTriStripDotNet
{
    /// <summary>
    /// Misc. utility class.
    /// </summary>
    internal static class Utils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(ref T first, ref T second)
        {
            (second, first) = (first, second);
        }
    }
}