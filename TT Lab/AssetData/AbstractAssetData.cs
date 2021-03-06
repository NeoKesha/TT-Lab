﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.ViewModels;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.AssetData
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public abstract class AbstractAssetData : IDisposable
    {
        protected Boolean disposedValue;

        public virtual Boolean Disposed => disposedValue;

        protected ITwinItem? twinRef = null;

        public virtual void Load(string dataPath, JsonSerializerSettings? settings = null)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(dataPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            using (System.IO.StreamReader reader = new System.IO.StreamReader(fs))
            {
                JsonConvert.PopulateObject(value: reader.ReadToEnd(), target: this, settings);
                disposedValue = false;
            }
        }

        public virtual void Save(string dataPath, JsonSerializerSettings? settings = null)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(dataPath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(fs))
            {
                writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented, settings).ToCharArray());
            }
        }

        protected abstract void Dispose(Boolean disposing);

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            disposedValue = true;
            GC.SuppressFinalize(this);
        }

        public abstract void Import();

        public virtual ITwinItem Export()
        {
            return null;
        }

        public void NullifyReference()
        {
            twinRef = null;
        }
    }
}
