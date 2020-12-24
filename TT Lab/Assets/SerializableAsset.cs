using Newtonsoft.Json;
using System;
using System.Windows.Controls;
using TT_Lab.AssetData;

namespace TT_Lab.Assets
{
    public abstract class SerializableAsset : IAsset
    {
        public Guid UUID { get; private set; }

        protected virtual String SavePath => Type;

        protected AbstractAssetData assetData;

        public virtual String Type => "Asset";

        public String Name { get; set; }
        public Boolean Raw { get; set; }
        public String Data { get; set; }
        public UInt32 ID { get; set; }
        public String Alias { get; set; }
        public String Chunk { get; set; }
        public Int32? LayoutID { get; set; }
        public Boolean IsLoaded { get; protected set; }
        public UInt32 Order { get; set; }

        public SerializableAsset()
        {
            IsLoaded = false;
        }

        public SerializableAsset(UInt32 id, String name)
        {
            UUID = Guid.NewGuid();
            ID = id;
            Name = name;
            Alias = Name;
            Raw = true;
            Data = UUID.ToString() + ".data";
            IsLoaded = true;
        }

        public virtual void Serialize()
        {
            var path = SavePath;
            System.IO.Directory.CreateDirectory(path);
            using (System.IO.FileStream fs = new System.IO.FileStream(System.IO.Path.Combine(path, UUID + ".json"), System.IO.FileMode.Create, System.IO.FileAccess.Write))
            using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(fs))
            {
                writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented).ToCharArray());
            }
            // Created data needs to be saved on disk but then disposed of since we are not gonna need it unless user wishes to edit the exact asset
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
        public abstract UserControl GetEditor();

        public abstract AbstractAssetData GetData();
    }
}
