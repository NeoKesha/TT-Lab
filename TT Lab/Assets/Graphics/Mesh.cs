using System;
using TT_Lab.AssetData.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Assets.Graphics
{
    public class Mesh : RigidModel
    {

        public Mesh() { }

        public Mesh(UInt32 id, String name, PS2AnyMesh mesh) : base(id, name, mesh)
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
