using GlmSharp;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.Extensions
{
    public static class TwinsanityCommonExtensions
    {
        public static System.Numerics.Vector4 ToSystem(this Twinsanity.TwinsanityInterchange.Common.Vector4 twinVec)
        {
            return new System.Numerics.Vector4(twinVec.X, twinVec.Y, twinVec.Z, twinVec.W);
        }

        public static GlmSharp.vec4 ToGlm(this Twinsanity.TwinsanityInterchange.Common.Vector4 twinVec)
        {
            return new GlmSharp.vec4(twinVec.X, twinVec.Y, twinVec.Z, twinVec.W);
        }

        public static System.Numerics.Vector3 ToSystem(this Twinsanity.TwinsanityInterchange.Common.Vector3 twinVec)
        {
            return new System.Numerics.Vector3(twinVec.X, twinVec.Y, twinVec.Z);
        }

        public static GlmSharp.vec3 ToGlm(this Twinsanity.TwinsanityInterchange.Common.Vector3 twinVec)
        {
            return new GlmSharp.vec3(twinVec.X, twinVec.Y, twinVec.Z);
        }

        public static Vector4 ToTwin(this vec4 vec)
        {
            return new Vector4(vec.x, vec.y, vec.z, vec.w);
        }

        public static Matrix4 ToTwin(this mat4 mat)
        {
            var twinMat = new Matrix4
            {
                Column1 = mat.Column0.ToTwin(),
                Column2 = mat.Column1.ToTwin(),
                Column3 = mat.Column2.ToTwin(),
                Column4 = mat.Column3.ToTwin()
            };
            return twinMat;
        }
    }
}
