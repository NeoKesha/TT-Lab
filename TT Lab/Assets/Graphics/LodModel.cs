using Newtonsoft.Json;
using System;
using System.Windows.Controls;
using TT_Lab.AssetData.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Assets.Graphics
{
    public class LodModel : SerializableAsset<LodModelData>
    {
        public override String Type => "LodModel";

        public LodModel(UInt32 id, String name, PS2AnyLOD lod) : base(id, name)
        {
            assetData = new LodModelData(lod);
        }

        public LodModel()
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
