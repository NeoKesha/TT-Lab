using System.Collections.Generic;
using System.Linq;
using org.ogre;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Rendering.Buffers;

namespace TT_Lab.Rendering.Objects;

public class Skydome : ManualObject
{
    public Skydome(string name, SceneManager sceneManager, SkydomeData skydomeData) : base(name)
    {
        var assetManager = AssetManager.Get();
        var skydomeNode = sceneManager.getRootSceneNode().createChildSceneNode("skydome");
        
        foreach (var meshUri in skydomeData.Meshes)
        {
            var mesh = assetManager.GetAssetData<RigidModelData>(meshUri);
            var meshNode = new ModelBuffer(sceneManager, skydomeNode, meshUri, mesh);
            foreach (var meshNodeMaterial in meshNode.MeshNodes)
            {
                meshNodeMaterial.MeshNode.getAttachedObject(0).setRenderQueueGroup((byte)RenderQueueGroupID.RENDER_QUEUE_SKIES_EARLY);
            }
        }

        var skydomeScale = 50;
        skydomeNode.scale(skydomeScale, skydomeScale, skydomeScale);
        skydomeNode.attachObject(this);
    }
}