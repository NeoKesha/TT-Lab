using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace TT_Lab.AssetData.Global
{
    public class SaveIconData : AbstractAssetData
    {
        public SaveIconData()
        {
            IconData = Array.Empty<Byte>();
        }

        public SaveIconData(Byte[] iconData)
        {
            IconData = CloneUtils.CloneArray(iconData);
        }

        public Byte[] IconData { get; set; }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }

        public override void Import(LabURI package, String? variant)
        {
            return;
        }

        public override void Save(String dataPath, JsonSerializerSettings? settings = null)
        {
            using var fs = new FileStream(dataPath, FileMode.Create, FileAccess.Write);
            using var writer = new BinaryWriter(fs);
            writer.Write(IconData);
        }

        protected override void LoadInternal(String dataPath, JsonSerializerSettings? settings = null)
        {
            using var fs = new FileStream(dataPath, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(fs);
            IconData = reader.ReadBytes((Int32)fs.Length);
        }


        protected override void Dispose(Boolean disposing)
        {
            IconData = Array.Empty<Byte>();
        }
    }
}
