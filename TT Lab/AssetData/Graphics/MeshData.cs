using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.AssetData.Graphics
{
    public class MeshData : RigidModelData
    {
        public MeshData() : base()
        {
        }

        public MeshData(ITwinMesh mesh) : base(mesh)
        {
        }
    }
}
