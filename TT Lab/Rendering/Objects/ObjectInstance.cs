using GlmSharp;
using System.Collections.Generic;
using System.Linq;
using org.ogre;
using TT_Lab.AssetData.Code;
using TT_Lab.AssetData.Graphics;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Extensions;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;

namespace TT_Lab.Rendering.Objects;

public sealed class ObjectInstance : EditableObject
{
    private readonly List<ModelBuffer> modelBuffers = new();

    public ObjectInstance(OgreWindow window, string name, ObjectInstanceData instance) : base(window, name)
    {
        var objURI = instance.ObjectId;
        var sceneManager = window.GetSceneManager();
        SetupModelBuffer(sceneManager, objURI);

        Pos = new vec3(-instance.Position.X, instance.Position.Y, instance.Position.Z);
        Rot = new vec3(instance.RotationX.GetRotation(), -instance.RotationY.GetRotation(), -instance.RotationZ.GetRotation());
        AmbientColor = new vec3(0.5f, 0.5f, 0.5f);
        UpdatePositionAndRotation();
    }

    public override void Select()
    {
        foreach (var nodeMaterial in modelBuffers.SelectMany(model => model.MeshNodes))
        {
            var entity = nodeMaterial.MeshNode.getAttachedObject(0).castEntity();
            entity.setMaterial(nodeMaterial.Materials[(int)ModelBuffer.MaterialType.Transparent]);
            var subEntity = entity.getSubEntity(0);
            subEntity.setCustomParameter(0, new Vector4(AmbientColor.x, AmbientColor.y, AmbientColor.z, 0.5f));
        }
            
        base.Select();
    }

    public override void Deselect()
    {
        foreach (var nodeMaterial in modelBuffers.SelectMany(model => model.MeshNodes))
        {
            var entity = nodeMaterial.MeshNode.getAttachedObject(0).castEntity();
            entity.setMaterial(nodeMaterial.Materials[(int)ModelBuffer.MaterialType.Opaque]);
            var subEntity = entity.getSubEntity(0);
            subEntity.setCustomParameter(0, new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
        }
            
        base.Deselect();
    }

    private void SetupModelBuffer(SceneManager sceneManager, LabURI uri)
    {
        var assetManager = AssetManager.Get();
        var objData = assetManager.GetAssetData<GameObjectData>(uri);
        if (objData.OGISlots.All(ogiUri => ogiUri == LabURI.Empty))
        {
            return;
        }

        var ogiURI = objData.OGISlots.First(ogiUri => ogiUri != LabURI.Empty);
        var ogiData = assetManager.GetAssetData<OGIData>(ogiURI);
        
        var skeletonMap = TwinSkeletonManager.CreateSceneNodeSkeleton(SceneNode, ogiURI);
        var jointIndex = 0;
        foreach (var rigidModel in ogiData.RigidModelIds)
        {
            var sceneNode = skeletonMap[ogiData.JointIndices[jointIndex++]];
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
        }
        if (ogiData.BlendSkin != LabURI.Empty)
        {
            var blendSkin = assetManager.GetAssetData<BlendSkinData>(ogiData.BlendSkin);
            modelBuffers.Add(new ModelBufferBlendSkin(sceneManager, SceneNode, ogiData.BlendSkin, blendSkin));
        }
    }
}