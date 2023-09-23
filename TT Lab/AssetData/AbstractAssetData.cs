﻿using Newtonsoft.Json;
using System;
using System.Diagnostics;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.AssetData
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public abstract class AbstractAssetData : IDisposable
    {
        protected Boolean disposedValue;

        public virtual Boolean Disposed => disposedValue;

        ITwinItem? twinRef = null;

        public virtual void Load(string dataPath, JsonSerializerSettings? settings = null)
        {
            using System.IO.FileStream fs = new(dataPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            using System.IO.StreamReader reader = new(fs);
            JsonConvert.PopulateObject(value: reader.ReadToEnd(), target: this, settings);
            disposedValue = false;
        }

        public virtual void Save(string dataPath, JsonSerializerSettings? settings = null)
        {
            using System.IO.FileStream fs = new(dataPath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            using System.IO.BinaryWriter writer = new(fs);
            writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented, settings).ToCharArray());
        }

        protected abstract void Dispose(Boolean disposing);

        public void Dispose()
        {
            // DO NOT change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            disposedValue = true;
            GC.SuppressFinalize(this);
        }

        protected void SetTwinItem(ITwinItem item)
        {
            twinRef = item;
        }

        protected T GetTwinItem<T>() where T : ITwinItem
        {
            Debug.Assert(twinRef != null && twinRef.GetType().IsAssignableTo(typeof(T)), $"Twin ref must be set to a valid item that's assignable to {typeof(T).Name}");
            return (T)twinRef;
        }

        public abstract void Import(String package, String subpackage, String? variant);

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
