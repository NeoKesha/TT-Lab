using System;
using TT_Lab.AssetData.Graphics;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.Assets.Graphics
{
    public class Mesh : RigidModel
    {
        public override UInt32 Section => Constants.GRAPHICS_MESHES_SECTION;

        public Mesh() { }

        public Mesh(LabURI package, String? variant, UInt32 id, String Name, ITwinMesh mesh) : base(package, variant, id, Name, mesh)
        {
            assetData = new MeshData(mesh);
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }
    }
}
