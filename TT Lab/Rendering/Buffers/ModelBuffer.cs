using System;
using System.Collections.Generic;
using org.ogre;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Vector4 = org.ogre.Vector4;

namespace TT_Lab.Rendering.Buffers;

public class ModelBuffer
{
    //protected List<IndexedBufferArray> modelBuffers = new();

    public enum MaterialType
    {
        Opaque,
        Transparent,
    }

    // These are all the materials that model buffer (exception being Blend Skin) will clone and set needed parameters for its needs which then later can be obtained
    // to swap the current material for a different one, for example to enable/disable transparency of a model
    private readonly List<string> _baseMaterials = new()
    {
        "DiffuseTexture",
        "DiffuseTextureTransparent"
    };

    public List<MeshNodeMaterial> MeshNodes { get; private set; } = new();

    public ModelBuffer(SceneManager sceneManager, string name, ModelData model, TwinMaterialGenerator.ShaderSettings shaderSettings = default) : this(sceneManager, sceneManager.getRootSceneNode(), name, model, shaderSettings)
    {
            
    }

    public ModelBuffer(SceneManager sceneManager, SceneNode parent, string name, ModelData model, TwinMaterialGenerator.ShaderSettings shaderSettings = default)
    {
        for (var i = 0; i < model.Vertexes.Count; ++i)
        {
            var meshPtr = BufferGeneration.GetModelBuffer($"{name}{i}", model.Vertexes[i], model.Faces[i]);
            var node = parent.createChildSceneNode();
            var entity = sceneManager.createEntity(meshPtr);
            var material = new TwinMaterialGenerator.GeneratedMaterial
            {
                Material = MaterialManager.GetDefault()
            };
            entity.setMaterial(material.Material);
            node.attachObject(entity);
            MeshNodes.Add(new MeshNodeMaterial { MeshNode = node, Materials = new List<TwinMaterialGenerator.GeneratedMaterial> { material } });
        }
    }

    public ModelBuffer(SceneManager sceneManager, string name, RigidModelData rigid, TwinMaterialGenerator.ShaderSettings shaderSettings = default) : this(sceneManager, sceneManager.getRootSceneNode(), name, rigid, shaderSettings)
    {
            
    }

    public ModelBuffer(SceneManager sceneManager, SceneNode parent, string name, RigidModelData rigid, TwinMaterialGenerator.ShaderSettings shaderSettings = default)
    {
        var model = AssetManager.Get().GetAssetData<ModelData>(rigid.Model);
        var materials = rigid.Materials;
        for (var i = 0; i < model.Vertexes.Count; ++i)
        {
            var materialName = AssetManager.Get().GetAsset<Assets.Graphics.Material>(materials[i]).Name;
            var matData = AssetManager.Get().GetAssetData<MaterialData>(materials[i]);
            // var texturedShaderIndex = matData.Shaders.FindIndex(0, s => s.TxtMapping == TwinShader.TextureMapping.ON);
            // var hasTexture = texturedShaderIndex != -1;
            var meshPtr = BufferGeneration.GetModelBuffer($"{name}{i}", model.Vertexes[i], model.Faces[i]);
            var node = parent.createChildSceneNode();
            var entity = sceneManager.createEntity(meshPtr);
            var materialList = new List<TwinMaterialGenerator.GeneratedMaterial>();
            ushort renderPriority = 0;
            foreach (var baseMaterial in _baseMaterials)
            {
                // var needsSetup = MaterialManager.CreateOrGetMaterial(materialName, out var material, baseMaterial);
                // if (hasTexture && needsSetup)
                // {
                //     MaterialManager.SetupMaterial(material, matData.Shaders[texturedShaderIndex]);
                // }
                var material = TwinMaterialGenerator.GenerateMaterialFromTwinMaterial(matData, shaderSettings);
                materialList.Add(material);
                renderPriority = material.RenderPriority;
            }
            entity.setMaterial(materialList[(int)MaterialType.Opaque].Material);
            entity.setRenderQueueGroupAndPriority((byte)RenderQueueGroupID.RENDER_QUEUE_MAIN, renderPriority);
            node.attachObject(entity);
            entity.getSubEntity(0).setCustomParameter(0, new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
            MeshNodes.Add(new MeshNodeMaterial { MeshNode = node, Materials = materialList });
        }
    }

    public ModelBuffer(SceneManager sceneManager, string name, SkinData skin, TwinMaterialGenerator.ShaderSettings shaderSettings = default) : this(sceneManager, sceneManager.getRootSceneNode(), name, skin, shaderSettings)
    {
    }

    public ModelBuffer(SceneManager sceneManager, SceneNode parent, string name, SkinData skin, TwinMaterialGenerator.ShaderSettings shaderSettings = default)
    {
        var index = 0;
        shaderSettings.UseSkinning = true;
        foreach (var subSkin in skin.SubSkins)
        {
            var materialName = AssetManager.Get().GetAsset<Assets.Graphics.Material>(subSkin.Material).Name;
            var matData = AssetManager.Get().GetAssetData<MaterialData>(subSkin.Material);
            var texturedShaderIndex = matData.Shaders.FindIndex(0, s => s.TxtMapping == TwinShader.TextureMapping.ON);
            var hasTexture = texturedShaderIndex != -1;
            var meshPtr = BufferGeneration.GetModelBuffer($"{name}{index++}", subSkin.Vertexes, subSkin.Faces, RenderOperation.OperationType.OT_TRIANGLE_LIST, true, true);
            var node = parent.createChildSceneNode();
            var entity = sceneManager.createEntity(meshPtr);
            var materialList = new List<TwinMaterialGenerator.GeneratedMaterial>();
            ushort renderPriority = 0;
            foreach (var baseMaterial in _baseMaterials)
            {
                // var needsSetup = MaterialManager.CreateOrGetMaterial(materialName, out var material, baseMaterial);
                // if (hasTexture && needsSetup)
                // {
                //     MaterialManager.SetupMaterial(material, matData.Shaders[texturedShaderIndex]);
                // }
                var material = TwinMaterialGenerator.GenerateMaterialFromTwinMaterial(matData, shaderSettings);
                materialList.Add(material);
                renderPriority = material.RenderPriority;
            }
            entity.setMaterial(materialList[(int)MaterialType.Opaque].Material);
            entity.setRenderQueueGroupAndPriority((byte)RenderQueueGroupID.RENDER_QUEUE_MAIN, renderPriority);
            node.attachObject(entity);
            entity.getSubEntity(0).setCustomParameter(0, new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
            MeshNodes.Add(new MeshNodeMaterial { MeshNode = node, Materials = materialList });
        }
    }

    public struct MeshNodeMaterial
    {
        public SceneNode MeshNode;
        public List<TwinMaterialGenerator.GeneratedMaterial> Materials;
    }

    protected ModelBuffer(SceneManager sceneManager, SceneNode parent, string name, BlendSkinData blendSkin, TwinMaterialGenerator.ShaderSettings shaderSettings = default)
    {
        var index = 0;
        shaderSettings.UseSkinning = true;
        foreach (var blend in blendSkin.Blends)
        {
            var materialName = AssetManager.Get().GetAsset<Assets.Graphics.Material>(blend.Material).Name;
            var matData = AssetManager.Get().GetAssetData<MaterialData>(blend.Material);
            var texturedShaderIndex = matData.Shaders.FindIndex(0, s => s.TxtMapping == TwinShader.TextureMapping.ON);
            var hasTexture = texturedShaderIndex != -1;
            foreach (var model in blend.Models)
            {
                var meshPtr = BufferGeneration.GetModelBuffer($"{name}_{index}", model.Vertexes, model.Faces, RenderOperation.OperationType.OT_TRIANGLE_LIST, true, true);
                var node = parent.createChildSceneNode();
                var entity = sceneManager.createEntity(meshPtr);
                var material = TwinMaterialGenerator.GenerateMaterialFromTwinMaterial(matData, shaderSettings);
                entity.setMaterial(material.Material);
                entity.setRenderQueueGroupAndPriority((byte)RenderQueueGroupID.RENDER_QUEUE_MAIN, material.RenderPriority);
                node.attachObject(entity);
                entity.getSubEntity(0).setCustomParameter(0, new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
                MeshNodes.Add(new MeshNodeMaterial { MeshNode = node, Materials = new List<TwinMaterialGenerator.GeneratedMaterial> { material, material }});
                index++;
            }
        }
    }
}