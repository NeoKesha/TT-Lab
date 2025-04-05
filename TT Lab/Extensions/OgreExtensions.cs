using GlmSharp;
using org.ogre;

namespace TT_Lab.Extensions;

public class OgreExtensions
{
    public static Vector3 FromGlm(vec3 glmVec)
    {
        return new Vector3(glmVec.x, glmVec.y, glmVec.z);
    }

    public static Quaternion FromGlm(quat glmQuat)
    {
        return new Quaternion(glmQuat.w, glmQuat.x, glmQuat.y, glmQuat.z);
    }

    public static Matrix4 FromGlm(mat4 glmMat)
    {
        return new Matrix4(glmMat.m00, glmMat.m10, glmMat.m20, glmMat.m30,
            glmMat.m01, glmMat.m11, glmMat.m21, glmMat.m31,
            glmMat.m02, glmMat.m12, glmMat.m22, glmMat.m32,
            glmMat.m03, glmMat.m13, glmMat.m23, glmMat.m33);
    }

    public static mat4 FromOgre(Matrix4 mat)
    { 
        return new mat4(mat.__getitem__(0, 0), mat.__getitem__(1, 0), mat.__getitem__(2, 0), mat.__getitem__(3, 0),
            mat.__getitem__(0, 1), mat.__getitem__(1, 1), mat.__getitem__(2, 1), mat.__getitem__(3, 1),
            mat.__getitem__(0, 2), mat.__getitem__(1, 2), mat.__getitem__(2, 2), mat.__getitem__(3, 2),
            mat.__getitem__(0, 3), mat.__getitem__(1, 3), mat.__getitem__(2, 3), mat.__getitem__(3, 3));
    }

    public static Matrix4 FromTwin(Twinsanity.TwinsanityInterchange.Common.Matrix4 mat)
    {
        return new Matrix4(mat[0].X, mat[0].Y, mat[0].Z, mat[0].W,
            mat[1].X, mat[1].Y, mat[1].Z, mat[1].W,
            mat[2].X, mat[2].Y, mat[2].Z, mat[2].W,
            mat[3].X, mat[3].Y, mat[3].Z, mat[3].W);
    }

    public static vec3 FromOgre(Vector3 ogreVec)
    {
        return new vec3(ogreVec.x, ogreVec.y, ogreVec.z);
    }

    public static quat FromOgre(Quaternion ogreQuat)
    {
        return new quat(ogreQuat.x, ogreQuat.y, ogreQuat.z, ogreQuat.w);
    }

    public static Quaternion FromGlmVec(vec3 eulerAngles)
    {
        return FromGlm(new quat(eulerAngles));
    }
}