using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Assets
{
    public static class AssetFactory
    {
        public static Dictionary<Guid, IAsset> GetAssets(Dictionary<string, Type> strToT, string[] jsonAssets)
        {
            var assets = new Dictionary<Guid, IAsset>();
            foreach (var str in jsonAssets)
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(str, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                using (System.IO.StreamReader reader = new System.IO.StreamReader(fs))
                {
                    var json = reader.ReadToEnd();
                    var baseAss = JsonConvert.DeserializeObject<BaseAsset>(json);
                    var newAsset = (IAsset)Activator.CreateInstance(strToT[baseAss.Type]);
                    newAsset.Deserialize(json);
                    assets.Add(newAsset.UUID, newAsset);
                }
            }
            return assets;
        }

        [JsonObject]
        private class BaseAsset
        {
            public string Type { get; set; }
        }
    }
    
}
