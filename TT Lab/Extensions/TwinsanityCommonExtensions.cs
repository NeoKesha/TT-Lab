namespace TT_Lab.Extensions
{
    public static class TwinsanityCommonExtensions
    {
        public static OpenTK.Mathematics.Vector4 ToGL(this Twinsanity.TwinsanityInterchange.Common.Vector4 twinVec)
        {
            return new OpenTK.Mathematics.Vector4(twinVec.X, twinVec.Y, twinVec.Z, twinVec.W);
        }

        public static System.Numerics.Vector4 ToSystem(this Twinsanity.TwinsanityInterchange.Common.Vector4 twinVec)
        {
            return new System.Numerics.Vector4(twinVec.X, twinVec.Y, twinVec.Z, twinVec.W);
        }

        public static OpenTK.Mathematics.Vector3 ToGL(this Twinsanity.TwinsanityInterchange.Common.Vector3 twinVec)
        {
            return new OpenTK.Mathematics.Vector3(twinVec.X, twinVec.Y, twinVec.Z);
        }

        public static System.Numerics.Vector3 ToSystem(this Twinsanity.TwinsanityInterchange.Common.Vector3 twinVec)
        {
            return new System.Numerics.Vector3(twinVec.X, twinVec.Y, twinVec.Z);
        }
    }
}
