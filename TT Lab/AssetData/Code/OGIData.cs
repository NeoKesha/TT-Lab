using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.AssetData.Code
{
    public class OGIData : AbstractAssetData
    {
        public OGIData()
        {
        }

        public OGIData(PS2AnyOGI ogi) : this()
        {
            BoundingBox = CloneUtils.CloneArray(ogi.BoundingBox);
            RigidRelatedList = CloneUtils.CloneList(ogi.RigidRelatedList);
            RigidModelIds = CloneUtils.CloneList(ogi.RigidModelIds);
            Type3RelatedList = CloneUtils.CloneList(ogi.Type3RelatedList);
            Type1List = CloneUtils.DeepClone(ogi.Type1List);
            Type2List = CloneUtils.DeepClone(ogi.Type2List);
            Type3List = CloneUtils.DeepClone(ogi.Type3List);
            Type1RelatedMatrix = CloneUtils.CloneListUnsafe(ogi.Type1RelatedMatrix);
        }

        [JsonProperty(Required = Required.Always)]
        public Vector4[] BoundingBox { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<OGIType1> Type1List { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<OGIType2> Type2List { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Byte> RigidRelatedList { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt32> RigidModelIds { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Matrix4> Type1RelatedMatrix { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 SkinID { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 BlendSkinID { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<OGIType3> Type3List { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Byte> Type3RelatedList { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
