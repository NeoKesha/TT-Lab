using GlmNet;
using OpenTK;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static Vector2 ToGL(this Twinsanity.TwinsanityInterchange.Common.Vector2 v)
        {
            return new Vector2(v.X, v.Y);
        }

        /// <summary>
        /// Transforms Twinsanity Vector into an OpenGL compatible one
        /// </summary>
        /// <param name="v">Twinsanity Vector</param>
        /// <returns>OpenGL vector</returns>
        public static Vector3 ToGL(this Twinsanity.TwinsanityInterchange.Common.Vector3 v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        /// <summary>
        /// Transforms Twinsanity Vector into an OpenGL compatible one
        /// </summary>
        /// <param name="v">Twinsanity Vector</param>
        /// <returns>OpenGL vector</returns>
        public static Vector4 ToGL(this Twinsanity.TwinsanityInterchange.Common.Vector4 v)
        {
            return new Vector4(v.X, v.Y, v.Z, v.W);
        }

        public static float[] ToArray(this Color color)
        {
            return new float[] { color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f };
        }

        
    }
}
