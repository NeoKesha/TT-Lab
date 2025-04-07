using System.Collections.Generic;
using System.Linq;
using GlmSharp;
using org.ogre;
using TT_Lab.AssetData.Code;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Extensions;
using TT_Lab.Rendering.Buffers;

namespace TT_Lab.Rendering.Objects;

public class OGI : ManualObject
{
    private readonly List<ModelBuffer> modelBuffers = new();
    private ModelBuffer? skinBuffer = null;
    private ModelBuffer? blendSkinBuffer = null;
    private TwinSkeleton defaultSkeleton = new();
    private SceneNode SceneNode;
    
    public OGI(string name, SceneManager sceneManager, OGIData ogiData) : base(name)
    {
        SceneNode = sceneManager.createSceneNode();
        SceneNode.attachObject(this);
        
        BuildSkeleton(sceneManager, ogiData);
    }

    public new void Dispose()
    {
        defaultSkeleton.Dispose();
        
        base.Dispose();
    }

    public void ApplyTransformToJoint(int jointIndex, vec3 position, vec3 scale, quat rotation)
    {
        var jointNode = defaultSkeleton.Bones[jointIndex].ResidingSceneNode;
        jointNode.resetToInitialState();
        jointNode.translate(position.x, position.y, position.z, Node.TransformSpace.TS_LOCAL);
        jointNode.rotate(OgreExtensions.FromGlm(rotation), Node.TransformSpace.TS_LOCAL);
        jointNode.setScale(scale.x, scale.y, scale.z);

        if (skinBuffer != null)
        {
            var resultMat = defaultSkeleton.Bones[jointIndex].GetBoneMatrix();
            foreach (var meshNode in skinBuffer.MeshNodes)
            {
                var vertexShader = meshNode.Materials[0].Material.getTechnique(0).getPass(0).getVertexProgramParameters();
                vertexShader.setNamedConstant($"boneMatrices[{jointIndex}]", resultMat);
            }
        }

        if (blendSkinBuffer != null)
        {
            var resultMat = defaultSkeleton.Bones[jointIndex].GetBoneMatrix();
            foreach (var meshNode in blendSkinBuffer.MeshNodes)
            {
                var vertexShader = meshNode.Materials[0].Material.getTechnique(0).getPass(0).getVertexProgramParameters();
                vertexShader.setNamedConstant($"boneMatrices[{jointIndex}]", resultMat);
            }
        }
    }

    private void BuildSkeleton(SceneManager sceneManager, OGIData ogiData)
    {
        var assetManager = AssetManager.Get();
        
        defaultSkeleton = TwinSkeletonManager.CreateSceneNodeSkeleton(SceneNode, ogiData);
        var jointIndex = 0;
        foreach (var rigidModel in ogiData.RigidModelIds)
        {
            var sceneNode = defaultSkeleton.Bones[ogiData.JointIndices[jointIndex++]].ResidingSceneNode;
            if (rigidModel == LabURI.Empty)
            {
                continue;
            }

            var rigidModelData = assetManager.GetAssetData<RigidModelData>(rigidModel);
            modelBuffers.Add(new ModelBuffer(sceneManager, sceneNode, rigidModel, rigidModelData));
        }
        
        if (ogiData.Skin != LabURI.Empty)
        {
            var skin = assetManager.GetAssetData<SkinData>(ogiData.Skin);
            modelBuffers.Add(new ModelBuffer(sceneManager, SceneNode, ogiData.Skin, skin));
            skinBuffer = modelBuffers[^1];
        }
        if (ogiData.BlendSkin != LabURI.Empty)
        {
            var blendSkin = assetManager.GetAssetData<BlendSkinData>(ogiData.BlendSkin);
            modelBuffers.Add(new ModelBufferBlendSkin(sceneManager, SceneNode, ogiData.BlendSkin, blendSkin));
            blendSkinBuffer = modelBuffers[^1];
        }
    }
    
}