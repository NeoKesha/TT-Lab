using Newtonsoft.Json;
using System;

namespace TT_Lab.Assets
{
    public abstract class SerializableAsset : IAsset
    {
        public Guid UUID { get; }

        public virtual String Type => "Asset";

        public String Name { get; set; }
        public Boolean Raw { get; set; }
        public String Data { get; set; }
        public UInt32 ID { get; set; }

        public SerializableAsset(UInt32 id, String name)
        {
            UUID = Guid.NewGuid();
            ID = id;
            Name = name;
            Raw = true;
            Data = null; // Indicates that game's original data is preserved
        }

        public void Serialize()
        {
            var path = System.IO.Path.Combine(Type, UUID.ToString());
            System.IO.Directory.CreateDirectory(path);
            using (System.IO.FileStream fs = new System.IO.FileStream(System.IO.Path.Combine(path, Name + ".json"), System.IO.FileMode.Create, System.IO.FileAccess.Write))
            using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(fs))
            {
                writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented).ToCharArray());
            }
        }

        public abstract void ToRaw(Byte[] data);
        public abstract Byte[] ToFormat();
    }
}
