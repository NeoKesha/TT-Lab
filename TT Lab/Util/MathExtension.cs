using GlmNet;
using OpenTK.Mathematics;
using System;
using TT_Lab.AssetData.Graphics;

namespace TT_Lab.Util
{
    public static class MathExtension
    {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            return val;
        }

        public static float[] Matrix4ToArray(Matrix4 matrix)
        {
            return new float[] { matrix.M11, matrix.M12, matrix.M13, matrix.M14,
                                 matrix.M21, matrix.M22, matrix.M23, matrix.M24,
                                 matrix.M31, matrix.M32, matrix.M33, matrix.M34,
                                 matrix.M41, matrix.M42, matrix.M43, matrix.M44,};
        }

        //https://gamedev.stackexchange.com/questions/96459/fast-ray-sphere-collision-code
        public static bool IntersectRaySphere(vec3 origin, vec3 direction, vec3 spherePosition, float radius, ref float distance, ref vec3 hit)
        {
            vec3 dirToSphere = origin - spherePosition;
            float angleDifference = glm.dot(dirToSphere, direction);
            float distanceDifference = glm.dot(dirToSphere, dirToSphere) - radius * radius;

            if (distanceDifference > 0.0f && angleDifference > 0.0f)
            {
                return false;
            }
            float D = angleDifference * angleDifference - distanceDifference;

            if (D < 0.0f)
            {
                return false;
            }

            distance = (float)Math.Max(-angleDifference - (float)Math.Sqrt(D), 0.0);
            hit = origin + distance * direction;

            return true;
        }
    }
}
