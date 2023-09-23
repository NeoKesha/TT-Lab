using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;

namespace TT_Lab.AssetData
{
    public class FolderData : AbstractAssetData
    {
        [JsonProperty(Required = Required.AllowNull)]
        public LabURI? Parent { get; set; }

        [JsonProperty(Required = Required.Always)]
        public List<LabURI> Children { get; private set; } = new();

        // The Folder is a special case data since we do need it loaded at all times due to its heavy relationship to Project Tree
        // so it doesn't get disposed
        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import(String package, String subpackage, String? variant)
        {
        }
    }
}
