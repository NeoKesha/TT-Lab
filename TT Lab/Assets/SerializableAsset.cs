﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.AssetData;
using TT_Lab.ViewModels;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.Assets
{
    public abstract class SerializableAsset : IAsset
    {
        protected virtual String SavePath => Type.Name;
        protected virtual String DataExt => ".data";
        protected virtual String TwinDataExt => "bin";
        public abstract UInt32 Section { get; }

        protected AbstractAssetData assetData;
        protected AssetViewModel viewModel;

        public Type Type { get; set; }

        public String Name { get; set; }
        public Boolean Raw { get; set; }
        public String Data { get; set; }
        public UInt32 ID { get; set; }
        public String Alias { get; set; }
        public String Chunk { get; set; }
        public Int32? LayoutID { get; set; }
        public Boolean IsLoaded { get; protected set; }
        public UInt32 Order { get; set; }
        public Boolean SkipExport { get; set; } = false;

        public Dictionary<String, Object?> Parameters { get; set; } = new();
        public LabURI URI { get; set; }
        public LabURI Package { get; set; }
        public String Variation { get; set; }

        private bool resolveTraversed = false;

        public SerializableAsset()
        {
            IsLoaded = false;
        }

        private SerializableAsset(UInt32 id, String name)
        {
            ID = id;
            Name = name;
            Alias = Name;
            Raw = true;
            IsLoaded = true;
            Type = GetType();
        }

        public SerializableAsset(UInt32 id, String name, LabURI package, Boolean needVariant, String variant) : this(id, name)
        {
            Package = package;
            Variation = variant;
            var variantPath = needVariant ? Variation.Replace("\\", "_").Replace("/", "_") : "";
            Data = $"{Name.Replace("/", "_").Replace("\\", "_")}_{variantPath}{DataExt}";
            RegenerateURI(needVariant);
        }

        public void RegenerateURI(Boolean needVariant)
        {
            var variantAddition = needVariant ? $"/{Variation}" : "";
            var layoutId = LayoutID == null ? "" : $"/{LayoutID}";
            URI = new LabURI($"{Package}/{Type.Name}/{ID}{variantAddition}{layoutId}");
        }

        public virtual void Serialize()
        {
            var path = SavePath;
            Directory.CreateDirectory(path);
            var variantPath = Variation == null ? "" : Variation.Replace("\\", "_").Replace("/", "_");
            var name = Name.Replace("/", "_").Replace("\\", "_");
            using FileStream fs = new(Path.Combine(path, $"{name}_{variantPath}.json"), FileMode.Create, FileAccess.Write);
            using BinaryWriter writer = new(fs);
            writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented).ToCharArray());

            // Created or loaded data needs to be saved on disk but then disposed of since we are not gonna need it unless user wishes to edit the exact asset
            if (assetData != null)
            {
                assetData.Save(Path.Combine(path, Data));
                assetData.Dispose();
            }
        }

        public virtual void Deserialize(String json)
        {
            JsonConvert.PopulateObject(json, this);
        }

        public virtual void PostDeserialize() { }

        public abstract void ToRaw(Byte[] data);
        public abstract Byte[] ToFormat();
        public abstract Type GetEditorType();
        public abstract AbstractAssetData GetData();

        public virtual void Import()
        {
            assetData.Import(Package, Variation, LayoutID);
            assetData.NullifyReference();
        }

        public virtual ITwinItem Export(Factory.ITwinItemFactory factory)
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = GetData();
            }
            var item = assetData.Export(factory);
            assetData.Dispose();
            return item;
        }

        public void ExportToFile(Factory.ITwinItemFactory factory)
        {
            PreResolveResources();
            var item = Export(factory);
            using var itemFile = new FileStream($"{Name}.{TwinDataExt}", FileMode.Create, FileAccess.Write);
            using var binaryWriter = new BinaryWriter(itemFile);
            item.Write(binaryWriter);
            binaryWriter.Flush();
            binaryWriter.Close();
        }

        public virtual void PreResolveResources()
        {

        }

        public virtual void PostResolveResources(Factory.ITwinItemFactory factory, ITwinSection section, ITwinItem? item)
        {
            item?.SetID(ID);
            item?.Compile();
        }

        private void DisposeData()
        {
            assetData.Dispose();
            IsLoaded = false;
        }

        public virtual void ResolveChunkResources(Factory.ITwinItemFactory factory, ITwinSection section)
        {
            if (resolveTraversed) return;

            resolveTraversed = true;
            assetData = GetData();
            PreResolveResources();
            var item = assetData.ResolveChunkResouces(factory, section, ID, LayoutID);
            PostResolveResources(factory, section, item);

            DisposeData();
            resolveTraversed = false;
        }

        public virtual AssetViewModel GetViewModel(AssetViewModel? parent = null)
        {
            viewModel ??= new AssetViewModel(URI, parent);
            return viewModel;
        }
    }
}
