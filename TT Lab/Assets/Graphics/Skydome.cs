using Newtonsoft.Json;
using System;
using System.Windows.Controls;
using TT_Lab.AssetData.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Assets.Graphics
{
    public class Skydome : SerializableAsset<SkydomeData>
    {
        public override String Type => "Skydome";

        public Skydome(UInt32 id, String name, PS2AnySkydome skydome) : base(id, name)
        {
            assetData = new SkydomeData(skydome);
        }

        public Skydome()
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
