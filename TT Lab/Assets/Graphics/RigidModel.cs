using Newtonsoft.Json;
using System;
using System.Windows.Controls;
using TT_Lab.AssetData.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Assets.Graphics
{
    public class RigidModel : SerializableAsset<RigidModelData>
    {
        public override String Type => "RigidModel";

        public RigidModel(UInt32 id, String name, PS2AnyRigidModel rigidModel) : base(id, name)
        {
            assetData = new RigidModelData(rigidModel);
        }

        public RigidModel()
        {
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override UserControl GetEditor()
        {
            throw new NotImplementedException();
        }
    }
}
