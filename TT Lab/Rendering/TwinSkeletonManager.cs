using System.Collections.Generic;
using System.Linq;
using GlmSharp;
using org.ogre;
using TT_Lab.AssetData.Code;
using TT_Lab.Assets;
using TT_Lab.Extensions;

namespace TT_Lab.Rendering;

public static class TwinSkeletonManager
{
    public static Dictionary<int, SceneNode> CreateSceneNodeSkeleton(SceneNode parentNode, LabURI ogiData)
    {
        var skeletonData = AssetManager.Get().GetAssetData<OGIData>(ogiData);
        var skeletonMap = new Dictionary<int, SceneNode>();
        var rootSceneNode = parentNode.createChildSceneNode();
        skeletonMap.Add(skeletonData.Joints[0].Index, rootSceneNode);
        foreach (var joint in skeletonData.Joints.Skip(1))
        {
            var parentSceneNode = skeletonMap[joint.ParentIndex];
            var position = new Vector3(-joint.LocalTranslation.X, joint.LocalTranslation.Y, joint.LocalTranslation.Z);
            var quat = new quat(-joint.LocalRotation.X, joint.LocalRotation.Y, joint.LocalRotation.Z, -joint.LocalRotation.W);
            var sceneNode = parentSceneNode.createChildSceneNode();
            sceneNode.translate(position, Node.TransformSpace.TS_PARENT);
            sceneNode.rotate(OgreExtensions.FromGlm(quat));
            skeletonMap.TryAdd(joint.Index, sceneNode);
        }

        return skeletonMap;
    }
}