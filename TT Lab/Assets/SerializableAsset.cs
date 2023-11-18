using Newtonsoft.Json;
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
        public Guid UUID { get; private set; }

        protected virtual String SavePath => Type.Name;
        protected virtual String DataExt => ".data";
        protected virtual String TwinDataExt => "bin";

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
        /// <summary>
        /// If asset shouldn't be exported during game's build stage
        /// </summary>
        public Boolean SkipExport { get; set; } = false;

        public Dictionary<String, Object?> Parameters { get; set; } = new();
        public LabURI URI { get; set; }
        public LabURI Package { get; set; }
        public String? Variation { get; set; }

        public SerializableAsset()
        {
            IsLoaded = false;
        }

        private SerializableAsset(UInt32 id, String name)
        {
            UUID = Guid.NewGuid();
            ID = id;
            Name = name;
            Alias = Name;
            Raw = true;
            Data = UUID.ToString() + DataExt;
            IsLoaded = true;
            Type = GetType();
        }

        public SerializableAsset(UInt32 id, String name, LabURI package, String? variant) : this(id, name)
        {
            Package = package;
            Variation = variant;
            RegenerateURI();
        }

        public void RegenerateURI()
        {
            var variantAddition = Variation == null ? "" : $"/{Variation}";
            URI = new LabURI($"{Package}/{Type.Name}/{ID}{variantAddition}");
        }

        public virtual void Serialize()
        {
            var path = SavePath;
            System.IO.Directory.CreateDirectory(path);
            using (System.IO.FileStream fs = new(System.IO.Path.Combine(path, UUID + ".json"), System.IO.FileMode.Create, System.IO.FileAccess.Write))
            using (System.IO.BinaryWriter writer = new(fs))
            {
                writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented).ToCharArray());
            }
            // Created or loaded data needs to be saved on disk but then disposed of since we are not gonna need it unless user wishes to edit the exact asset
            if (assetData != null)
            {
                assetData.Save(System.IO.Path.Combine(path, Data));
                assetData.Dispose();
            }
        }

        public virtual void Deserialize(String json)
        {
            JsonConvert.PopulateObject(json, this);
        }

        public abstract void ToRaw(Byte[] data);
        public abstract Byte[] ToFormat();
        public abstract Type GetEditorType();

        public abstract AbstractAssetData GetData();
        public virtual void Import()
        {
            assetData.Import(Package, Variation);
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
            var item = Export(factory);
            using var mcdonaldsFile = new FileStream($"{Name}.{TwinDataExt}", FileMode.Create, FileAccess.Write);
            using var binaryWriter = new BinaryWriter(mcdonaldsFile);
            item.Write(binaryWriter);
            binaryWriter.Flush();
            binaryWriter.Close();
        }

        public virtual AssetViewModel GetViewModel(AssetViewModel? parent = null)
        {
            viewModel ??= new AssetViewModel(URI, parent);
            return viewModel;
        }
    }
}
