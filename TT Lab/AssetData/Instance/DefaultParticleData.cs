﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common.Particles;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM;

namespace TT_Lab.AssetData.Instance
{
    public class DefaultParticleData : ParticleData
    {

        public DefaultParticleData()
        {
            UnkData = new Byte[4];
            UnkBlob = new Byte[0x420];
            UnkInts = new Int32[10];
        }

        public DefaultParticleData(ITwinDefaultParticle particle) : base(particle) { }

        [JsonProperty(Required = Required.Always)]
        public List<LabURI> TextureIDs { get; set; } = new();
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> MaterialIDs { get; set; } = new();
        [JsonProperty(Required = Required.Always)]
        public LabURI DecalTextureID { get; set; } = LabURI.Empty;
        [JsonProperty(Required = Required.Always)]
        public LabURI DecalMaterialID { get; set; } = LabURI.Empty;
        [JsonProperty(Required = Required.Always)]
        public Byte[] UnkData { get; private set; }
        [JsonProperty(Required = Required.Always)]
        public Byte[] UnkBlob { get; private set; }
        [JsonProperty(Required = Required.Always)]
        public Int32[] UnkInts { get; private set; }
        [JsonProperty(Required = Required.Always)]
        public List<Byte[]> UnkBlobs { get; private set; } = new();

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            var assetManager = AssetManager.Get();
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);

            for (Int32 i = 0; i < 3; i++)
            {
                writer.Write(assetManager.GetAsset(TextureIDs[i]).ID);
                writer.Write(assetManager.GetAsset(MaterialIDs[i]).ID);
            }

            WriteExport(writer);
            ms.Position -= 4;

            writer.Write(assetManager.GetAsset(DecalTextureID).ID);
            writer.Write(assetManager.GetAsset(DecalMaterialID).ID);

            writer.Write(UnkData);
            writer.Write(UnkBlob);
            foreach (var @int in UnkInts)
            {
                writer.Write(@int);
            }

            foreach (var blob in UnkBlobs)
            {
                writer.Write(blob);
            }

            ms.Position = 0;
            return factory.GenerateDefaultParticle(ms);
        }

        public override void Import(LabURI package, String? variant)
        {
            base.Import(package, variant);

            var assetManager = AssetManager.Get();
            ITwinDefaultParticle particle = GetTwinItem<ITwinDefaultParticle>();

            foreach (var texture in particle.TextureIDs)
            {
                TextureIDs.Add(assetManager.GetUri(package, typeof(Texture).Name, null, texture));
            }

            foreach (var material in particle.MaterialIDs)
            {
                MaterialIDs.Add(assetManager.GetUri(package, typeof(Material).Name, null, material));
            }

            DecalTextureID = assetManager.GetUri(package, typeof(Texture).Name, null, particle.DecalTextureID);
            DecalMaterialID = assetManager.GetUri(package, typeof(Material).Name, null, particle.DecalMaterialID);

            UnkData = CloneUtils.CloneArray(particle.UnkData);
            UnkBlob = CloneUtils.CloneArray(particle.UnkBlob);
            UnkInts = CloneUtils.CloneArray(particle.UnkInts);

            foreach (var blob in particle.UnkBlobs)
            {
                UnkBlobs.Add(CloneUtils.CloneArray(blob));
            }
        }

        protected override void Dispose(Boolean disposing)
        {
            TextureIDs.Clear();
            MaterialIDs.Clear();
            UnkBlobs.Clear();

            base.Dispose(disposing);
        }
    }
}
