﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.AssetData.Instance.Scenery;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common.Lights;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;

namespace TT_Lab.AssetData.Instance
{
    public class SceneryData : AbstractAssetData
    {
        private readonly static Dictionary<ITwinScenery.SceneryType, Type> scIndexToType = new();

        static SceneryData()
        {
            scIndexToType.Add(ITwinScenery.SceneryType.Root, typeof(SceneryRootData));
            scIndexToType.Add(ITwinScenery.SceneryType.Leaf, typeof(SceneryLeafData));
            scIndexToType.Add(ITwinScenery.SceneryType.Node, typeof(SceneryNodeData));
        }

        public SceneryData()
        {
            ChunkPath = "levels\\earth\\hub\\beach";
            SkydomeID = LabURI.Empty;
            HasLighting = false;
            UnkLightFlags = new Boolean[6];
            AmbientLights = new();
            DirectionalLights = new();
            PointLights = new();
            NegativeLights = new();
            Sceneries = new();
        }

        public SceneryData(ITwinScenery scenery) : this()
        {
            SetTwinItem(scenery);
        }

        [JsonProperty(Required = Required.Always)]
        public String ChunkPath { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkUInt { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte UnkByte { get; set; }
        [JsonProperty(Required = Required.Always)]
        public LabURI SkydomeID { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Boolean HasLighting { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public Boolean[] UnkLightFlags { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public List<AmbientLight> AmbientLights { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public List<DirectionalLight> DirectionalLights { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public List<PointLight> PointLights { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public List<NegativeLight> NegativeLights { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public List<SceneryBaseData> Sceneries { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            AmbientLights.Clear();
            DirectionalLights.Clear();
            PointLights.Clear();
            NegativeLights.Clear();
            Sceneries.Clear();
        }

        public override void Save(String dataPath, JsonSerializerSettings? settings = null)
        {
            settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            base.Save(dataPath, settings);
        }

        protected override void LoadInternal(String dataPath, JsonSerializerSettings? settings = null)
        {
            settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            base.LoadInternal(dataPath, settings);
        }

        public override void Import(LabURI package, String? variant, Int32? layoutId)
        {
            ITwinScenery scenery = GetTwinItem<ITwinScenery>();
            ChunkPath = scenery.Name[..];
            UnkUInt = scenery.UnkUInt;
            UnkByte = scenery.UnkByte;
            if (scenery.SkydomeID != 0)
            {
                SkydomeID = AssetManager.Get().GetUri(package, typeof(Skydome).Name, variant, scenery.SkydomeID);
            }
            HasLighting = scenery.HasLighting;
            if (HasLighting)
            {
                UnkLightFlags = CloneUtils.CloneArray(scenery.UnkLightFlags);
                AmbientLights = CloneUtils.DeepClone(scenery.AmbientLights);
                DirectionalLights = CloneUtils.DeepClone(scenery.DirectionalLights);
                PointLights = CloneUtils.DeepClone(scenery.PointLights);
                NegativeLights = CloneUtils.DeepClone(scenery.NegativeLights);
            }
            Sceneries = new List<SceneryBaseData>();
            foreach (var sc in scenery.Sceneries)
            {
                Sceneries.Add((SceneryBaseData)Activator.CreateInstance(scIndexToType[sc.GetObjectIndex()], package, variant, sc)!);
            }
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            var assetManager = AssetManager.Get();
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            writer.Write(ChunkPath);
            writer.Write(UnkUInt);
            writer.Write(UnkByte);
            writer.Write(SkydomeID == LabURI.Empty ? 0 : assetManager.GetAsset(SkydomeID).ID);
            writer.Write(HasLighting);
            if (HasLighting)
            {
                for (var i = 0; i < UnkLightFlags.Length; ++i)
                {
                    writer.Write(UnkLightFlags[i]);
                }

                writer.Write(AmbientLights.Count);
                foreach (var ambient in AmbientLights)
                {
                    ambient.Write(writer);
                }

                writer.Write(DirectionalLights.Count);
                foreach (var directional in DirectionalLights)
                {
                    directional.Write(writer);
                }

                writer.Write(PointLights.Count);
                foreach (var point in PointLights)
                {
                    point.Write(writer);
                }

                writer.Write(NegativeLights.Count);
                foreach (var negative in NegativeLights)
                {
                    negative.Write(writer);
                }
            }
            writer.Write(Sceneries.Count);
            foreach (var scenery in Sceneries)
            {
                writer.Write((Int32)scenery.GetSceneryType());
                scenery.Write(writer);
            }

            ms.Position = 0;
            return factory.GenerateScenery(ms);
        }

        public override ITwinItem? ResolveChunkResouces(ITwinItemFactory factory, ITwinSection section, UInt32 id)
        {
            var assetManager = AssetManager.Get();
            var graphicsSection = section.GetItem<ITwinSection>(Constants.SCENERY_GRAPHICS_SECTION);
            var skydomeSection = graphicsSection.GetItem<ITwinSection>(Constants.GRAPHICS_SKYDOMES_SECTION);
            if (SkydomeID != LabURI.Empty)
            {
                assetManager.GetAsset(SkydomeID).ResolveChunkResources(factory, skydomeSection);
            }

            foreach (var scenery in Sceneries)
            {
                scenery.ResolveChunkResouces(factory, graphicsSection);
            }

            return base.ResolveChunkResouces(factory, section, id);
        }
    }
}
