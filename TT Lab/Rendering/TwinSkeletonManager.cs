using System;
using System.Collections.Generic;
using System.Linq;
using GlmSharp;
using org.ogre;
using TT_Lab.AssetData.Code;
using TT_Lab.Assets;
using TT_Lab.Extensions;
using TT_Lab.Util;

namespace TT_Lab.Rendering;

public sealed class TwinBone : IDisposable
{
    public readonly SceneNode ResidingSceneNode;
    private Matrix4 inverseBindMatrix;
    private Matrix4 bindingMatrix;
    private Matrix4 globalTransform;
    
    public TwinBone(SceneNode node)
    {
        ResidingSceneNode = node;
        globalTransform = Matrix4.IDENTITY;
        bindingMatrix = Matrix4.IDENTITY;
        inverseBindMatrix = Matrix4.IDENTITY;
    }

    public void SetBindingAndInverseMatrix(mat4 mat)
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
        
        globalTransform.makeTransform(globalTranslation, globalScale, globalRotation);

        return globalTransform.__mul__(inverseBindMatrix);
    }

    public void Dispose()
    {
        inverseBindMatrix.Dispose();
        bindingMatrix.Dispose();
        globalTransform.Dispose();
    }
}

public sealed class TwinSkeleton : IDisposable
{
    public Dictionary<int, TwinBone> Bones = new();

    public void Dispose()
    {
        foreach (var (_, bone) in Bones)
        {
            bone.Dispose();
        }
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
        rootBone.SetBindingAndInverseMatrix(globalTransform);
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
            sceneNode.setDebugDisplayEnabled(true);
            sceneNode.translate(position, Node.TransformSpace.TS_LOCAL);
            sceneNode.rotate(OgreExtensions.FromGlm(quat), Node.TransformSpace.TS_LOCAL);
            var bone = new TwinBone(sceneNode);
            bone.SetBindingAndInverseMatrix(globalTransform);
            boneMap.TryAdd(joint.Index, bone);
        }
        skeleton.Bones = boneMap;

        return skeleton;
    }
}