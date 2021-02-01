using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets.Graphics;
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
            twinRef = ogi;
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
        public List<Guid> RigidModelIds { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Matrix4> Type1RelatedMatrix { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 SkinID { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 BlendSkinID { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<BoundingBoxBuilder> BoundingBoxBuilders { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Byte> BuilderMatrixIndex { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            Type1List.Clear();
            Type2List.Clear();
            RigidRelatedList.Clear();
            RigidModelIds.Clear();
            Type1RelatedMatrix.Clear();
            BoundingBoxBuilders.Clear();
            BuilderMatrixIndex.Clear();
        }

        public override void Import()
        {
            PS2AnyOGI ogi = (PS2AnyOGI)twinRef!;
            BoundingBox = CloneUtils.CloneArray(ogi.BoundingBox);
            RigidRelatedList = CloneUtils.CloneList(ogi.RigidRelatedList);
            RigidModelIds = new List<Guid>();
            foreach (var model in ogi.RigidModelIds)
            {
                RigidModelIds.Add(GuidManager.GetGuidByTwinId(model, typeof(RigidModel)));
            }
            BuilderMatrixIndex = CloneUtils.CloneList(ogi.Type3RelatedList);
            Type1List = CloneUtils.DeepClone(ogi.Type1List);
            Type2List = CloneUtils.DeepClone(ogi.Type2List);
            BoundingBoxBuilders = CloneUtils.DeepClone(ogi.Type3List);
            Type1RelatedMatrix = CloneUtils.CloneListUnsafe(ogi.Type1RelatedMatrix);
        }
    }
}
