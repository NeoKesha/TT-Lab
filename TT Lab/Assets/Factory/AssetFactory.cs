﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace TT_Lab.Assets.Factory
{
    public static class AssetFactory
    {
        public static async Task<Dictionary<LabURI, IAsset>> GetAssets(string[] jsonAssets)
        {
            var assets = new Dictionary<LabURI, IAsset>();
            var assetMut = new Mutex();
            var tasks = new Task[jsonAssets.Length];
            var index = 0;
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            foreach (var str in jsonAssets)
            {
                tasks[index++] = Task.Factory.StartNew(() =>
                {
                    IAsset newAsset;
                    using System.IO.FileStream fs = new(str, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    using System.IO.StreamReader reader = new(fs);
                    var json = reader.ReadToEnd();
                    var baseAss = JsonConvert.DeserializeObject<BaseAsset>(json)!;
                    newAsset = (IAsset)Activator.CreateInstance(baseAss.Type)!;
                    newAsset.Deserialize(json);
                    assetMut.WaitOne();
                    assets.Add(newAsset.URI, newAsset);
                    assetMut.ReleaseMutex();
                });
            }
            await Task.WhenAll(tasks);
            return assets;
        }

        [JsonObject]
        private class BaseAsset
        {
            public Type Type { get; set; }
        }
    }

}
