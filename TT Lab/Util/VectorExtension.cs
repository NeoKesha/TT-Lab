using OpenTK.Mathematics;
using System.Drawing;

namespace TT_Lab.Util
{
    public static class VectorExtension
    {
        public static Vector3 Normal(this Vector3 v1, Vector3 v2, Vector3 v3)
        {
            return Vector3.Cross(v2 - v1, v3 - v1);
        }

        public static float[] ToArray(this Vector2 v)
        {
            return new float[] { v.X, v.Y };
        }

        public static float[] ToArray(this Vector3 v)
        {
            return new float[] { v.X, v.Y, v.Z };
        }

        public static float[] ToArray(this Color color)
        {
            return new float[] { color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f };
        }

        public static Twinsanity.TwinsanityInterchange.Common.Vector4 ToTwin(this System.Numerics.Vector4 v)
        {
            return new Twinsanity.TwinsanityInterchange.Common.Vector4(v.X, v.Y, v.Z, v.W);
        }

        public static Twinsanity.TwinsanityInterchange.Common.Vector4 ToTwin(this System.Numerics.Vector3 v)
        {
            return new Twinsanity.TwinsanityInterchange.Common.Vector4(v.X, v.Y, v.Z, 1.0f);
        }

        public static Twinsanity.TwinsanityInterchange.Common.Vector4 ToTwin(this System.Numerics.Vector2 v)
        {
            return new Twinsanity.TwinsanityInterchange.Common.Vector4(v.X, v.Y, 1.0f, 0.0f);
        }

    }
}
