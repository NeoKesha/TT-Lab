using System;
using GlmSharp;

namespace TT_Lab.Extensions;

public static class GlmExtensions
{
    public static quat SLerpSafe(quat q1, quat q2, float t)
    {
        if (q1.LengthSqr == 0.0)
            return q2.LengthSqr == 0.0 ? quat.Identity : q2;
        if (q2.LengthSqr == 0.0)
            return q1;
        float d = q1.w * q2.w + vec3.Dot(new vec3(q1.x, q1.y, q1.z), new vec3(q2.x, q2.y, q2.z));
        if (d >= 1.0 || d <= -1.0)
            return q1;
        if (d < 0.0)
        {
            q2 = q2.Conjugate;
            q2.w = -q2.w;
            d = -d;
        }
        float num1;
        float num2;
        if (d < 0.9900000095367432)
        {
            float a = (float) Math.Acos(d);
            float num3 = 1f / (float) Math.Sin(a);
            num1 = (float) Math.Sin(a * (1.0 - t)) * num3;
            num2 = (float) Math.Sin(a * t) * num3;
        }
        else
        {
            num1 = 1f - t;
            num2 = t;
        }
        quat q = new quat(num1 * new vec3(q1.x, q1.y, q1.z) + num2 * new vec3(q2.x, q2.y, q2.z), num1 * q1.w + num2 * q2.w);
        return q.LengthSqr > 0.0 ? q.Normalized : quat.Identity;
    }

    public static quat Multiply(this quat left, quat right)
    {
        var leftXyz = new vec3(left.x, left.y, left.z);
        var rightXyz = new vec3(right.x, right.y, right.z);
        return new quat(right.w * leftXyz + left.w * rightXyz + vec3.Cross(leftXyz, rightXyz), left.w * right.w - vec3.Dot(leftXyz, rightXyz));
    }
}