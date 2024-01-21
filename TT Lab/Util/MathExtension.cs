﻿using GlmSharp;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common.Input;
using System;
using TT_Lab.AssetData.Graphics;
using TT_Lab.AssetData.Graphics.SubModels;

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

        //https://en.wikipedia.org/wiki/M%C3%B6ller%E2%80%93Trumbore_intersection_algorithm

        public static bool IntersectRayTriangle(vec3 origin, vec3 direction, vec3 p1, vec3 p2, vec3 p3, ref float distance, ref vec3 hit)
        {
            vec3 edge1 = p2 - p1;
            vec3 edge2 = p3 - p1;
            vec3 ray_cross_e2 = glm.Cross(direction, edge2);
            float det = glm.Dot(edge1, ray_cross_e2);

            if (Math.Abs(det) < 0.001f)
            {
                return false;
            }

            float inv_det = 1.0f/ det;
            vec3 s = origin - p1;
            float u = inv_det * glm.Dot(s, ray_cross_e2);

            if (u < 0 || u > 1)
            {
                return false;
            }

            vec3 s_cross_e1 = glm.Cross(s, edge1);
            float v = inv_det * glm.Dot(direction, s_cross_e1);

            if (v < 0 || u + v > 1)
            {
                return false;
            }

            distance = inv_det * glm.Dot(edge2, s_cross_e1);

            if (distance >= 0.001f)
            {
                hit = origin + direction * distance;
                return true;
            }
            
            return false;
        }
    }
}
