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

    public void ApplyTransformToJoint(int jointIndex, vec3 position, vec3 scale, quat rotation)
    {
        var jointNode = defaultSkeleton.Bones[jointIndex].ResidingSceneNode;
        jointNode.resetToInitialState();
        jointNode.translate(position.x, position.y, position.z, Node.TransformSpace.TS_LOCAL);
        jointNode.rotate(OgreExtensions.FromGlm(rotation), Node.TransformSpace.TS_LOCAL);
        jointNode.setScale(scale.x, scale.y, scale.z);

        if (skinBuffer != null)
        {
            var mat = new Matrix4();
            // var inverseBind = defaultSkeleton.Bones[jointIndex].GetInverseBindMatrix();
            // mat.makeTransform(jointNode.convertLocalToWorldPosition(jointNode.getPosition()), jointNode.getScale(), jointNode.convertLocalToWorldOrientation(jointNode.getOrientation()));
            var resultMat = defaultSkeleton.Bones[jointIndex].GetBoneMatrix(); //mat.__mul__(inverseBind);
            foreach (var meshNode in skinBuffer.MeshNodes)
            {
                var vertexShader = meshNode.Materials[0].Material.getTechnique(0).getPass(0).getVertexProgramParameters();
                vertexShader.setNamedConstant($"boneMatrices[{jointIndex}]", resultMat);
            }
        }

        if (blendSkinBuffer != null)
        {
            var mat = new Matrix4();
            // var inverseBind = defaultSkeleton.Bones[jointIndex].GetInverseBindMatrix();
            // mat.makeTransform(jointNode.convertLocalToWorldPosition(jointNode.getPosition()), jointNode.getScale(), jointNode.convertLocalToWorldOrientation(jointNode.getOrientation()));
            var resultMat = defaultSkeleton.Bones[jointIndex].GetBoneMatrix(); //mat.__mul__(inverseBind);
            foreach (var meshNode in blendSkinBuffer.MeshNodes)
            {
                var vertexShader = meshNode.Materials[0].Material.getTechnique(0).getPass(0).getVertexProgramParameters();
                vertexShader.setNamedConstant($"boneMatrices[{jointIndex}]", resultMat);
            }
        }
        
        // if (skinBuffer != null)
        // {
        //     skinSkeleton.Value.BoneMap[jointIndex].resetToInitialState();
        //     skinSkeleton.Value.BoneMap[jointIndex].translate(position.x, position.y, position.z, Node.TransformSpace.TS_PARENT);
        //     skinSkeleton.Value.BoneMap[jointIndex].rotate(OgreExtensions.FromGlm(rotation), Node.TransformSpace.TS_PARENT);
        //     skinSkeleton.Value.BoneMap[jointIndex].setScale(scale.x, scale.y, scale.z);
        // }

        // if (skinBuffer != null)
        // {
        //     var bonePos = OgreExtensions.FromOgre(skeletonMap[jointIndex].getPosition());
        //     var boneRot = skeletonMap[jointIndex].getOrientation();
        //     var boneScale = OgreExtensions.FromOgre(skeletonMap[jointIndex].getScale());
        //     var angle = new Degree();
        //     var axes = new Vector3();
        //     boneRot.ToAngleAxis(angle, axes);
        //     
        //     var transform = mat4.Translate(bonePos);
        //     transform *= mat4.Rotate(angle.valueRadians(), OgreExtensions.FromOgre(axes));
        //     transform *= mat4.Scale(boneScale.x, boneScale.y, boneScale.z);
        //
        //     var material = skinBuffer.MeshNodes[jointIndex].Materials[0];
        //     var vertexBuffer = material.SkinningBonesBuffer![0];
        //     var offset = (uint)jointIndex * sizeof(float) * 16;
        //     var length = 16U * sizeof(float);
        //     var data = transform.Values1D;
        //     HardwareBufferLockGuard bufferLock = new HardwareBufferLockGuard(vertexBuffer.__deref__(), offset, length, HardwareBuffer.LockOptions.HBL_WRITE_ONLY);
        //     unsafe
        //     {
        //         fixed (float* buffer = data)
        //         {
        //             bufferLock.pBuf.writeData(0, 16 * sizeof(float), new nint(buffer));
        //         }
        //     }
        // }
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
            // skinSkeleton = TwinSkeletonManager.CreateSkeleton(getName(), ogiData);
            var skin = assetManager.GetAssetData<SkinData>(ogiData.Skin);
            modelBuffers.Add(new ModelBuffer(sceneManager, SceneNode, ogiData.Skin, skin));
            skinBuffer = modelBuffers[^1];
            // foreach (var meshNode in skinBuffer.MeshNodes)
            // {
            //     var mesh = meshNode.MeshNode.getAttachedObject(0).castEntity().getMesh();
            //     mesh.setSkeletonName(getName());
            // }
        }
        if (ogiData.BlendSkin != LabURI.Empty)
        {
            var blendSkin = assetManager.GetAssetData<BlendSkinData>(ogiData.BlendSkin);
            modelBuffers.Add(new ModelBufferBlendSkin(sceneManager, SceneNode, ogiData.BlendSkin, blendSkin));
            blendSkinBuffer = modelBuffers[^1];
        }
    }
    
}