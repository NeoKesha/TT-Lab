using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Assets
{
    public class Folder : SerializableAsset
    {

        [JsonProperty(Required = Required.AllowNull)]
        public Guid? Parent { get; set; }

        [JsonProperty(Required = Required.Always)]
        public List<Guid> Children { get; } = new List<Guid>();

        public override String Type => "Folder";

        public Folder() { }

        public Folder(String name) : this(name, null)
        {
        }

        public Folder(String name, Folder parent) : this((UInt32)Guid.NewGuid().ToByteArray().Sum(b => b), name)
        {
            Parent = parent?.UUID;
            if (Parent != null)
            {
                parent.Children.Add(UUID);
            }
        }

        private Folder(UInt32 id, String name) : base(id, name)
        {
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
