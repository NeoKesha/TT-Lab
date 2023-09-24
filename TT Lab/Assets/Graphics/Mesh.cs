using System;
using TT_Lab.AssetData.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Assets.Graphics
{
    public class Mesh : RigidModel
    {

        public Mesh() { }

        public Mesh(String package, String subpackage, String? variant, UInt32 id, String Name, PS2AnyMesh mesh) : base(package, subpackage, variant, id, Name, mesh)
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
