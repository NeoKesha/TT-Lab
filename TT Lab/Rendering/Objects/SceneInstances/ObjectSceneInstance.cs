using System.Linq;
using GlmSharp;
using TT_Lab.AssetData.Code;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;

namespace TT_Lab.Rendering.Objects.SceneInstances;

public class ObjectSceneInstance : SceneInstance
{
    public ObjectSceneInstance(OgreWindow window, ObjectInstanceData instanceData) : base(window, instanceData)
    {
        Position = new vec3(-instanceData.Position.X, instanceData.Position.Y, instanceData.Position.Z);
        Rotation = new vec3(instanceData.RotationX.GetRotation(), -instanceData.RotationY.GetRotation(), -instanceData.RotationZ.GetRotation());
        
        var assetManager = AssetManager.Get();
        var objData = assetManager.GetAssetData<GameObjectData>(instanceData.ObjectId);
        InstanceRender = new ObjectInstance(window, $"{GetHashCode()} Instance of {objData.Name}", instanceData);
        foreach (OGIData? ogiData in from uri in objData.OGISlots where uri != LabURI.Empty select assetManager.GetAssetData<OGIData>(uri))
        {
            Size = new vec3();
            Size.x = ogiData.BoundingBox[1].X - ogiData.BoundingBox[0].X;
            Size.y = ogiData.BoundingBox[1].Y - ogiData.BoundingBox[0].Y;
            Size.z = ogiData.BoundingBox[1].Z - ogiData.BoundingBox[0].Z;
            Offset = new vec3(ogiData.BoundingBox[0].X, ogiData.BoundingBox[0].Y, ogiData.BoundingBox[0].Z);
            break;
        }
    }

    public override void SetPositionRotation(vec3 position, dvec3 rotation)
    {
        var data = GetData<ObjectInstanceData>();
        data.Position.X = -position.x;
        data.Position.Y = position.y;
        data.Position.Z = position.z;
        data.RotationX.SetRotation((float)rotation.x);
        data.RotationY.SetRotation((float)-rotation.y);
        data.RotationZ.SetRotation((float)-rotation.z);
    }
}