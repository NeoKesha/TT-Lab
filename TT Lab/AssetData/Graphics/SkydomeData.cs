using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Assets.Graphics;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.AssetData.Graphics
{
    public class SkydomeData : AbstractAssetData
    {
        public SkydomeData()
        {
            Meshes = new();
        }

        public SkydomeData(ITwinSkydome skydome) : this()
        {
            SetTwinItem(skydome);
        }

        [JsonProperty(Required = Required.Always)]
        public List<LabURI> Meshes { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            Meshes.Clear();
        }

        public override void Import(LabURI package, String? variant, Int32? layoutId)
        {
            ITwinSkydome skydome = GetTwinItem<ITwinSkydome>();
            Meshes = new List<LabURI>();
            foreach (var mesh in skydome.Meshes)
            {
                Meshes.Add(AssetManager.Get().GetUri(package, typeof(Mesh).Name, variant, mesh));
            }
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            var assetManager = AssetManager.Get();
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            writer.Write(20480); // Unused header
            writer.Write(Meshes.Count);
            foreach (var mesh in Meshes)
            {
                writer.Write(assetManager.GetAsset(mesh).ID);
            }

            writer.Flush();
            ms.Position = 0;
            return factory.GenerateMesh(ms);
        }

        public override ITwinItem? ResolveChunkResouces(ITwinItemFactory factory, ITwinSection section, UInt32 id)
        {
            var assetManager = AssetManager.Get();
            var root = section.GetRoot();
            var graphicsSection = root.GetItem<ITwinSection>(Constants.SCENERY_GRAPHICS_SECTION);
            var meshSection = graphicsSection.GetItem<ITwinSection>(Constants.GRAPHICS_MESHES_SECTION);
            foreach (var mesh in Meshes)
            {
                assetManager.GetAsset(mesh).ResolveChunkResources(factory, meshSection);
            }

            section = section.GetItem<ITwinSection>(Constants.GRAPHICS_SKYDOMES_SECTION);
            return base.ResolveChunkResouces(factory, section, id);
        }
    }
}
