﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.AssetData.Instance.Scenery;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common.Lights;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;

namespace TT_Lab.AssetData.Instance
{
    public class SceneryData : AbstractAssetData
    {
        private readonly static Dictionary<Int32, Type> scIndexToType = new();

        static SceneryData()
        {
            scIndexToType.Add(0x160A, typeof(SceneryRootData));
            scIndexToType.Add(0x1605, typeof(SceneryLeafData));
            scIndexToType.Add(0x1600, typeof(SceneryNodeData));
        }

        public SceneryData()
        {
        }

        public SceneryData(ITwinScenery scenery) : this()
        {
            SetTwinItem(scenery);
        }

        [JsonProperty(Required = Required.Always)]
        public String SceneryName { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkUInt { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte UnkByte { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public LabURI SkydomeID { get; set; }
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

        public override void Load(String dataPath, JsonSerializerSettings? settings = null)
        {
            settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            base.Load(dataPath, settings);
        }

        public override void Import(LabURI package, String? variant)
        {
            ITwinScenery scenery = GetTwinItem<ITwinScenery>();
            SceneryName = scenery.Name[..];
            UnkUInt = scenery.UnkUInt;
            UnkByte = scenery.UnkByte;
            if (scenery.SkydomeID != 0)
            {
                SkydomeID = AssetManager.Get().GetUri(package, typeof(Skydome).Name, variant, scenery.SkydomeID);
            }
            UnkLightFlags = CloneUtils.CloneArray(scenery.UnkLightFlags);
            AmbientLights = CloneUtils.DeepClone(scenery.AmbientLights);
            DirectionalLights = CloneUtils.DeepClone(scenery.DirectionalLights);
            PointLights = CloneUtils.DeepClone(scenery.PointLights);
            NegativeLights = CloneUtils.DeepClone(scenery.NegativeLights);
            Sceneries = new List<SceneryBaseData>();
            foreach (var sc in scenery.Sceneries)
            {
                Sceneries.Add((SceneryBaseData)Activator.CreateInstance(scIndexToType[sc.GetObjectIndex()], package, variant, sc)!);
            }
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }
    }
}
