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
    private OGI skeleton;

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
        skeleton.ChangeMaterialParameter(0, new Vector4(AmbientColor.x, AmbientColor.y, AmbientColor.z, 0.5f));
            
        base.Select();
    }

    public override void Deselect()
    {
        skeleton.ChangeMaterialParameter(0, new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
            
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
        skeleton = new OGI(getName() + "_ogi_skeleton", sceneManager, ogiData);
        SceneNode.addChild(skeleton.GetSceneNode());
    }
}