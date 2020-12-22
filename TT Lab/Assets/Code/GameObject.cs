using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Code;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class GameObject : SerializableAsset<GameObjectData>
    {
        public override String Type => "GameObject";

        public GameObject() { }

        public GameObject(UInt32 id, String name, PS2AnyObject @object) : base(id, name)
        {
            assetData = new GameObjectData(@object);
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
