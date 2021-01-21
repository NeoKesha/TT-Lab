using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class CameraData : AbstractAssetData
    {
        public CameraData()
        {
        }

        public CameraData(PS2AnyCamera camera) : this()
        {
            twinRef = camera;
        }

        [JsonProperty(Required = Required.Always)]
        public TriggerData Trigger { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 CameraHeader { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 UnkShort { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 UnkVector1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 UnkVector2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat3 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt3 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt4 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt5 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt6 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat4 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat5 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat6 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat7 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt7 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt8 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt9 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat8 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 TypeIndex1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 TypeIndex2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte UnkByte { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public CameraSubBase MainCamera1 { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public CameraSubBase MainCamera2 { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
        public override void Import()
        {
            PS2AnyCamera camera = (PS2AnyCamera)twinRef!;
            Trigger = new TriggerData(camera.CamTrigger);
            CameraHeader = camera.CameraHeader;
            UnkShort = camera.UnkShort;
            UnkFloat1 = camera.UnkFloat1;
            UnkVector1 = CloneUtils.Clone(camera.UnkVector1);
            UnkVector2 = CloneUtils.Clone(camera.UnkVector2);
            UnkFloat2 = camera.UnkFloat2;
            UnkFloat3 = camera.UnkFloat3;
            UnkInt1 = camera.UnkInt1;
            UnkInt2 = camera.UnkInt2;
            UnkInt3 = camera.UnkInt3;
            UnkInt4 = camera.UnkInt4;
            UnkInt5 = camera.UnkInt5;
            UnkInt6 = camera.UnkInt6;
            UnkFloat4 = camera.UnkFloat4;
            UnkFloat5 = camera.UnkFloat5;
            UnkFloat6 = camera.UnkFloat6;
            UnkFloat7 = camera.UnkFloat7;
            UnkInt7 = camera.UnkInt7;
            UnkInt8 = camera.UnkInt8;
            UnkInt9 = camera.UnkInt9;
            UnkFloat8 = camera.UnkFloat8;
            TypeIndex1 = camera.TypeIndex1;
            TypeIndex2 = camera.TypeIndex2;
            UnkByte = camera.UnkByte;
            MainCamera1 = CloneUtils.DeepClone(camera.MainCamera1);
            MainCamera2 = CloneUtils.DeepClone(camera.MainCamera2);
        }
    }
}
