using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.AssetData.Graphics
{
    public class LodModelData : AbstractAssetData
    {
        public LodModelData()
        {
        }

        public LodModelData(PS2AnyLOD lod) : this()
        {
            twinRef = lod;
        }

        [JsonProperty(Required = Required.Always)]
        public Int32 Type { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int32 UnkInt1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int32 UnkInt2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int32[] UnkInts { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte[] UnkData { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Guid> Meshes { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import()
        {
            PS2AnyLOD lod = (PS2AnyLOD)twinRef;
            Type = lod.Type;
            UnkInt1 = lod.UnkInt1;
            UnkInt2 = lod.UnkInt2;
            UnkInts = CloneUtils.CloneArray(lod.UnkInts);
            UnkData = CloneUtils.CloneArray(lod.UnkData);
            Meshes = new List<Guid>();
            foreach (var mesh in lod.Meshes)
            {
                Meshes.Add(GuidManager.GetGuidByTwinId(mesh, typeof(Mesh)));
            }
        }

        public override ITwinItem Export()
        {
            PS2AnyLOD lod = new PS2AnyLOD();
            lod.Type = Type;
            lod.UnkInt1 = UnkInt1;
            lod.UnkInt2 = UnkInt2;
            lod.UnkInts = CloneUtils.CloneArray(UnkInts);
            lod.UnkData = CloneUtils.CloneArray(UnkData);
            foreach (var mesh in Meshes)
            {
                lod.Meshes.Add(GuidManager.GetTwinIdByGuid(mesh));
            }
            return lod;
        }
    }
}
