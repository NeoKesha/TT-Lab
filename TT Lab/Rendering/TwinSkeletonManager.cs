using System.Collections.Generic;
using System.Linq;
using GlmSharp;
using org.ogre;
using TT_Lab.AssetData.Code;
using TT_Lab.Assets;
using TT_Lab.Extensions;
using TT_Lab.Util;

namespace TT_Lab.Rendering;

public class TwinBone
{
    public SceneNode ResidingSceneNode;
    private Matrix4 inverseBindMatrix;
    private Matrix4 bindingMatrix;
    
    public TwinBone(SceneNode node)
    {
        ResidingSceneNode = node;
        bindingMatrix = Matrix4.IDENTITY;
        inverseBindMatrix = Matrix4.IDENTITY;
    }

    public void SetInverseBindingMatrix(mat4 mat)
    {
        bindingMatrix = OgreExtensions.FromGlm(mat);
        inverseBindMatrix = OgreExtensions.FromGlm(mat.Inverse);
    }

    public Matrix4 GetBindingMatrix()
    {
        return bindingMatrix;
    }

    public Matrix4 GetBoneMatrix()
    {
        Vector3 globalScale = ResidingSceneNode._getDerivedScale();
        Quaternion globalRotation = ResidingSceneNode._getDerivedOrientation();
        Vector3 globalTranslation = ResidingSceneNode._getDerivedPosition();
        
        var m = new Matrix4();
        m.makeTransform(globalTranslation, globalScale, globalRotation);

        return m.__mul__(inverseBindMatrix);
    }
}

public struct TwinSkeleton
{
    public Dictionary<int, TwinBone> Bones = new();

    public TwinSkeleton()
    {
    }
}

public static class TwinSkeletonManager
{
    public static TwinSkeleton CreateSceneNodeSkeleton(SceneNode parentNode, LabURI ogiData)
    {
        var skeletonData = AssetManager.Get().GetAssetData<OGIData>(ogiData);
        return CreateSceneNodeSkeleton(parentNode, skeletonData);
    }
    
    public static TwinSkeleton CreateSceneNodeSkeleton(SceneNode parentNode, OGIData ogiData)
    {
        var skeleton = new TwinSkeleton();
        var skeletonData = ogiData;
        var boneMap = new Dictionary<int, TwinBone>();
        var rootSceneNode = parentNode.createChildSceneNode();
        var rootBone = new TwinBone(rootSceneNode);
        boneMap.Add(skeletonData.Joints[0].Index, rootBone);
        var globalTransform = mat4.Identity;
        rootBone.SetInverseBindingMatrix(globalTransform);
        var allOtherJoints = skeletonData.Joints.Skip(1);
        foreach (var joint in allOtherJoints)
        {
            var parentBone = boneMap[joint.ParentIndex];
            globalTransform = OgreExtensions.FromOgre(parentBone.GetBindingMatrix());
            var position = new Vector3(-joint.LocalTranslation.X, joint.LocalTranslation.Y, joint.LocalTranslation.Z);
            var quat = new quat(-joint.LocalRotation.X, joint.LocalRotation.Y, joint.LocalRotation.Z, -joint.LocalRotation.W);
            var localTransform = mat4.Translate(OgreExtensions.FromOgre(position)) * glm.ToMat4(quat);
            globalTransform *= localTransform;
            var sceneNode = parentBone.ResidingSceneNode.createChildSceneNode();
            sceneNode.setInheritScale(false);
            sceneNode.translate(position, Node.TransformSpace.TS_LOCAL);
            sceneNode.rotate(OgreExtensions.FromGlm(quat), Node.TransformSpace.TS_LOCAL);
            var bone = new TwinBone(sceneNode);
            bone.SetInverseBindingMatrix(globalTransform);
            boneMap.TryAdd(joint.Index, bone);
        }
        skeleton.Bones = boneMap;

        return skeleton;
    }
}