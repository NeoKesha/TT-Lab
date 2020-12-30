using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.AssetData
{
    public class FolderData : AbstractAssetData
    {
        [JsonProperty(Required = Required.AllowNull)]
        public Guid? Parent { get; set; }

        [JsonProperty(Required = Required.Always)]
        public List<Guid> Children { get; private set; } = new List<Guid>();

        // The Folder is a special case data since we do need it loaded at all times due to its heavy relationship to Project Tree
        // so it doesn't get disposed
        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import()
        {
        }
    }
}
