using GlmSharp;
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
            float angleDifference = glm.Dot(dirToSphere, direction);
            float distanceDifference = glm.Dot(dirToSphere, dirToSphere) - radius * radius;

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

        public static bool IntersectRayBox(vec3 origin, vec3 direction, vec3 boxPosition, vec3 originOffset, vec3 boxSize, mat4 localTransform, ref float distance, ref vec3 hit)
        {
            mat4 invWorld = localTransform.Inverse;
            vec4 localOrigin = invWorld * new vec4(origin, 0);
            vec4 localDirection = invWorld * new vec4(direction, 0);
            vec4 localBoxPosition = invWorld * new vec4(boxPosition, 0) + new vec4(originOffset, 0);
            return IntersectRayAABB(localOrigin.xyz, localDirection.xyz, localBoxPosition.xyz, boxSize, ref distance, ref hit);
        }

        //https://gamedev.stackexchange.com/questions/18436/most-efficient-aabb-vs-ray-collision-algorithms
        public static bool IntersectRayAABB(vec3 origin, vec3 direction, vec3 boxPosition, vec3 boxSize, ref float distance, ref vec3 hit)
        {
            vec3 dirfrac;
            // r.dir is unit direction vector of ray
            dirfrac.x = 1.0f / direction.x;
            dirfrac.y = 1.0f / direction.y;
            dirfrac.z = 1.0f / direction.z;
            // lb is the corner of AABB with minimal coordinates - left bottom, rt is maximal corner
            // r.org is origin of ray
            vec3 boxTopCorner = boxPosition + boxSize;
            float t1 = (boxPosition.x - origin.x) * dirfrac.x;
            float t2 = (boxTopCorner.x - origin.x) * dirfrac.x;
            float t3 = (boxPosition.y - origin.y) * dirfrac.y;
            float t4 = (boxTopCorner.y - origin.y) * dirfrac.y;
            float t5 = (boxPosition.z - origin.z) * dirfrac.z;
            float t6 = (boxTopCorner.z - origin.z) * dirfrac.z;

            float tmin = Math.Max(Math.Max(Math.Min(t1, t2), Math.Min(t3, t4)), Math.Min(t5, t6));
            float tmax = Math.Min(Math.Min(Math.Max(t1, t2), Math.Max(t3, t4)), Math.Max(t5, t6));

            // if tmax < 0, ray (line) is intersecting AABB, but the whole AABB is behind us
            if (tmax < 0)
            {
                return false;
            }

            // if tmin > tmax, ray doesn't intersect AABB
            if (tmin > tmax)
            {
                return false;
            }

            distance = tmin;
            hit = origin + direction * distance;
            return true;
        }
    }
}
