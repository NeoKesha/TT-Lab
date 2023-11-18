﻿using Newtonsoft.Json;
using System;
using System.Diagnostics;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.AssetData
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public abstract class AbstractAssetData : IDisposable
    {
        protected Boolean disposedValue;

        public virtual Boolean Disposed => disposedValue;

        ITwinItem? twinRef = null;

        public void Load(String dataPath, JsonSerializerSettings? settings = null)
        {
            var workingDirectory = System.IO.Directory.GetCurrentDirectory();
            System.IO.Directory.SetCurrentDirectory(Project.ProjectManagerSingleton.PM.OpenedProject!.ProjectPath);
            LoadInternal(dataPath, settings);
            System.IO.Directory.SetCurrentDirectory(workingDirectory);
        }

        protected virtual void LoadInternal(String dataPath, JsonSerializerSettings? settings = null)
        {
            using System.IO.FileStream fs = new(dataPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            using System.IO.StreamReader reader = new(fs);
            if (dataPath.Contains("d7eb31b3-b93b-4d34-a122-107209036d94"))
            {
                Console.WriteLine("DUMBASS");
            }
            JsonConvert.PopulateObject(value: reader.ReadToEnd(), target: this, settings);
            disposedValue = false;
        }

        public virtual void Save(String dataPath, JsonSerializerSettings? settings = null)
        {
            using System.IO.FileStream fs = new(dataPath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            using System.IO.BinaryWriter writer = new(fs);
            writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented, settings).ToCharArray());
        }

        protected abstract void Dispose(Boolean disposing);

        public void Dispose()
        {
            // DO NOT change this code. Put cleanup code in 'Dispose(bool disposing)' method
            if (disposedValue) return;

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

        /// <summary>
        /// Casts the data to a specific data type
        /// </summary>
        /// <typeparam name="T">Type of desired data</typeparam>
        /// <returns>Asset data of a specific type</returns>
        public T To<T>() where T : AbstractAssetData
        {
            Debug.Assert(GetType().IsAssignableFrom(typeof(T)), $"Attempted to cast to an illegal type {typeof(T).Name}");
            return (T)this;
        }

        public abstract void Import(LabURI package, String? variant);

        public abstract ITwinItem Export(ITwinItemFactory factory);

        public void NullifyReference()
        {
            twinRef = null;
        }
    }
}
