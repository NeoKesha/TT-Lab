using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2;

namespace TT_Lab.Assets.Instance
{
    public class Scenery : SerializableInstance
    {
        [JsonIgnore]
        private static readonly string subSceneryTypes = "SubSceneryTypes";

        public Scenery()
        {
            Parameters = new Dictionary<string, object?>();
            Parameters[subSceneryTypes] = new List<Type?>();
        }

        public Scenery(UInt32 id, String name, String chunk, PS2AnyScenery scenery) : base(id, name, chunk, null)
        {
            assetData = new SceneryData(scenery);
            Parameters = new Dictionary<string, object?>();
            Parameters[subSceneryTypes] = new List<Type?>();
        }

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }

        public override void Serialize()
        {
            if (assetData != null)
            {
                var scData = (SceneryData)assetData;
                var scTypeList = (List<Type?>)Parameters[subSceneryTypes]!;
                foreach (var scenery in scData.Sceneries)
                {
                    scTypeList.Add(scenery.GetType());
                }
            }
            base.Serialize();
        }

        public override void Deserialize(String json)
        {
            base.Deserialize(json);
            var jarr = (JArray)Parameters[subSceneryTypes]!;
            var typeList = jarr.Select(t => t.ToObject<Type>()).ToList();
            Parameters[subSceneryTypes] = typeList;
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new SceneryData((List<Type?>?)Parameters[subSceneryTypes]);
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
