using System.Linq;
using GlmSharp;
using TT_Lab.AssetData.Code;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Editors.Instance;
using TT_Lab.ViewModels.ResourceTree;

namespace TT_Lab.Rendering.Objects.SceneInstances;

public sealed class ObjectSceneInstance : SceneInstance
{
    public ObjectSceneInstance(OgreWindow window, EditingContext editingContext, ObjectInstanceData instanceData, ResourceTreeElementViewModel viewModel) : base(editingContext, viewModel)
    {
        Position = new vec3(-instanceData.Position.X, instanceData.Position.Y, instanceData.Position.Z);
        Rotation = new vec3(instanceData.RotationX.GetRotation(), -instanceData.RotationY.GetRotation(), -instanceData.RotationZ.GetRotation());
        
        var assetManager = AssetManager.Get();
        var objData = assetManager.GetAssetData<GameObjectData>(instanceData.ObjectId);
        AttachedEditableObject = new ObjectInstance(window, $"{GetHashCode()} Instance of {objData.Name}", instanceData);
        foreach (var ogiData in from uri in objData.OGISlots where uri != LabURI.Empty select assetManager.GetAssetData<OGIData>(uri))
        {
            Size = new vec3
            {
                x = ogiData.BoundingBox[1].X - ogiData.BoundingBox[0].X,
                y = ogiData.BoundingBox[1].Y - ogiData.BoundingBox[0].Y,
                z = ogiData.BoundingBox[1].Z - ogiData.BoundingBox[0].Z
            };
            Offset = new vec3(ogiData.BoundingBox[0].X, ogiData.BoundingBox[0].Y, ogiData.BoundingBox[0].Z);
            break;
        }
    }
}