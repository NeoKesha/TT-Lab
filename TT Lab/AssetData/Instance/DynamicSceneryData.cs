using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.AssetData.Instance.DynamicScenery;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;

namespace TT_Lab.AssetData.Instance
{
    public class DynamicSceneryData : AbstractAssetData
    {
        public DynamicSceneryData()
        {
            DynamicModels = new();
        }

        public DynamicSceneryData(ITwinDynamicScenery dynamicScenery) : this()
        {
            SetTwinItem(dynamicScenery);
        }

        [JsonProperty(Required = Required.Always)]
        public Int32 UnkInt { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<DynamicSceneryModelData> DynamicModels { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            DynamicModels.Clear();
        }

        public override void Import(LabURI package, String? variant)
        {
            ITwinDynamicScenery dynamicScenery = GetTwinItem<ITwinDynamicScenery>();
            UnkInt = dynamicScenery.UnkInt;
            DynamicModels.Clear();
            foreach (var model in dynamicScenery.DynamicModels)
            {
                DynamicModels.Add(new DynamicSceneryModelData(package, variant, model));
            }
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            writer.Write(UnkInt);
            writer.Write((Int16)DynamicModels.Count);
            foreach (var model in DynamicModels)
            {
                model.Write(writer);
            }

            ms.Position = 0;
            return factory.GenerateDynamicScenery(ms);
        }
    }
}
