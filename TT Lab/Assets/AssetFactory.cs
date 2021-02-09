using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.Assets.Instance;

namespace TT_Lab.Assets
{
    public static class AssetFactory
    {
        public static Dictionary<Guid, IAsset> GetAssets(string[] jsonAssets)
        {
            var assets = new Dictionary<Guid, IAsset>();
            var assetMut = new Mutex();
            var tasks = new Task[jsonAssets.Length];
            var index = 0;
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            foreach (var str in jsonAssets)
            {
                tasks[index++] = Task.Factory.StartNew(() =>
                {
                    IAsset newAsset;
                    using (System.IO.FileStream fs = new System.IO.FileStream(str, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(fs))
                    {
                        var json = reader.ReadToEnd();
                        var baseAss = JsonConvert.DeserializeObject<BaseAsset>(json);
                        newAsset = (IAsset)Activator.CreateInstance(baseAss.Type);
                        newAsset.Deserialize(json);
                    }
                    assetMut.WaitOne();
                    assets.Add(newAsset.UUID, newAsset);
                    assetMut.ReleaseMutex();
                });
            }
            Task.WaitAll(tasks);
            return assets;
        }

        [JsonObject]
        private class BaseAsset
        {
            public Type Type { get; set; }
        }
    }
    
}
