using System.Linq;
using GlmSharp;
using org.ogre;
using TT_Lab.AssetData.Graphics;
using TT_Lab.AssetData.Instance;
using TT_Lab.AssetData.Instance.Scenery;
using TT_Lab.Assets;
using TT_Lab.Extensions;
using TT_Lab.Rendering.Buffers;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;

namespace TT_Lab.Rendering.Objects;

public class Scenery : ManualObject
{
    public Scenery(string name, SceneManager sceneManager, SceneryData sceneryData) : base(name)
    {
        var sceneryNode = sceneManager.getRootSceneNode().createChildSceneNode("scenery");
        BuildSceneryRenderTree(sceneryNode, sceneManager, sceneryData);
        sceneryNode.attachObject(this);
    }

    private void BuildSceneryRenderTree(SceneNode sceneNode, SceneManager sceneManager, SceneryData sceneryData)
    {
        var root = (SceneryRootData)sceneryData.Sceneries[0];
        sceneryData.Sceneries = sceneryData.Sceneries.Skip(1).ToList();
        BuildSceneryRenderTreeForNode(sceneNode, sceneManager, root, ref sceneryData);
    }

    private void BuildSceneryRenderTreeForNode(SceneNode sceneNode, SceneManager sceneManager,
        SceneryNodeData sceneryNode, ref SceneryData sceneryData)
    {
        foreach (var sceneryType in sceneryNode.SceneryTypes)
        {
            if (sceneryType == ITwinScenery.SceneryType.Node)
            {
                var childNode = sceneNode.createChildSceneNode();
                var data = (SceneryNodeData)sceneryData.Sceneries[0];
                sceneryData.Sceneries = sceneryData.Sceneries.Skip(1).ToList();
                CreateSceneryNodes(childNode, sceneManager, data);
                BuildSceneryRenderTreeForNode(childNode, sceneManager, data, ref sceneryData);
            }
            else if (sceneryType == ITwinScenery.SceneryType.Leaf)
            {
                var data = sceneryData.Sceneries[0];
                sceneryData.Sceneries = sceneryData.Sceneries.Skip(1).ToList();
                CreateSceneryNodes(sceneNode, sceneManager, data);
            }
        }
    }

    private void CreateSceneryNodes(SceneNode sceneNode, SceneManager sceneManager, SceneryBaseData dataScenery)
    {
        var assetManager = AssetManager.Get();
        var index = 0;
        foreach (var meshId in dataScenery.MeshIDs)
        {
            var mesh = assetManager.GetAssetData<MeshData>(meshId);
            var meshNode = new ModelBuffer(sceneManager, sceneNode, meshId, mesh, new TwinMaterialGenerator.ShaderSettings { MirrorX = true });
            var meshMatrix = dataScenery.MeshModelMatrices[index];
            SetupMeshNode(meshMatrix, meshNode);
            index++;
        }

        index = 0;
        foreach (var lodId in dataScenery.LodIDs)
        {
            var lod = assetManager.GetAssetData<LodModelData>(lodId);
            var mesh = assetManager.GetAssetData<MeshData>(lod.Meshes[0]);
            var meshNode = new ModelBuffer(sceneManager, sceneNode, lod.Meshes[0], mesh, new TwinMaterialGenerator.ShaderSettings { MirrorX = true });
            var meshMatrix = dataScenery.LodModelMatrices[index];
            SetupMeshNode(meshMatrix, meshNode);
            index++;
        }
    }
    
    private void SetupMeshNode(Twinsanity.TwinsanityInterchange.Common.Matrix4 twinMat, ModelBuffer meshNode)
    {
        var glmMatrix = new mat4(twinMat[0].ToGlm(), twinMat[1].ToGlm(), twinMat[2].ToGlm(), twinMat[3].ToGlm());
        glmMatrix = glmMatrix.Transposed;
        var ogreMatrix = new Matrix4(glmMatrix.m00, glmMatrix.m01, glmMatrix.m02, glmMatrix.m03, glmMatrix.m10, glmMatrix.m11, glmMatrix.m12, glmMatrix.m13,
            glmMatrix.m20, glmMatrix.m21, glmMatrix.m22, glmMatrix.m23, glmMatrix.m30, glmMatrix.m31, glmMatrix.m32, glmMatrix.m33);
        var affine = new Affine3(ogreMatrix);
        var position = new Vector3();
        var scale = new Vector3();
        var orientation = new Quaternion();
        affine.decomposition(position, scale, orientation);
        foreach (var meshNodeMaterial in meshNode.MeshNodes)
        {
            meshNodeMaterial.MeshNode.setPosition(position);
            meshNodeMaterial.MeshNode.setScale(scale);
            meshNodeMaterial.MeshNode.setOrientation(orientation);
        }
    }
}