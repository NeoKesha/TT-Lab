using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Interfaces;
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

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            var assetManager = AssetManager.Get();
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            writer.Write(260); // Unused header
            writer.Write(Materials.Count);
            foreach (var mat in Materials)
            {
                writer.Write(assetManager.GetAsset(mat).ID);
            }
            writer.Write(assetManager.GetAsset(Model).ID);

            ms.Position = 0;
            return factory.GenerateMesh(ms);
        }
    }
}
