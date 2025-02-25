﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.AssetData.Code;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Assets.Factory;
using TT_Lab.Attributes;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.AssetData.Global
{
    [ReferencesAssets]
    public class UiSoundLibraryData : AbstractAssetData
    {

        public UiSoundLibraryData() { }

        public UiSoundLibraryData(ITwinSection section) : this()
        {
            SetTwinItem(section);
        }

        [JsonProperty(Required = Required.Always)]
        public List<LabURI> UiSounds { get; set; } = new();

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            var assetManager = AssetManager.Get();
            var sounds = new List<ITwinSound>();
            foreach (var sound in UiSounds)
            {
                sounds.Add((ITwinSound)(assetManager.GetAssetData<SoundEffectData>(sound).Export(factory)));
            }

            return factory.GenerateFrontend(sounds);
        }

        public override void Import(LabURI package, String? variant, Int32? layoutId)
        {
            var frontend = GetTwinItem<ITwinSection>();
            for (Int32 i = 0; i < frontend.GetItemsAmount(); i++)
            {
                var sound = frontend.GetItem<ITwinSound>(frontend.GetItem(i).GetID());
                var soundImport = new SoundEffect(package, true, $"{sound.GetName()}_ui_sfx_{i}", sound.GetID(), $"{sound.GetName()}_ui_sfx_{i}", sound);
                AssetManager.Get().AddAssetToImport(soundImport);
                UiSounds.Add(soundImport.URI);
            }
        }

        protected override void Dispose(Boolean disposing)
        {
            UiSounds.Clear();
        }
    }
}
