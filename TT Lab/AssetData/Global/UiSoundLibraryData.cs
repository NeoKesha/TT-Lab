using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Code;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.AssetData.Global
{
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
            throw new NotImplementedException();
        }

        public override void Import(LabURI package, String? variant)
        {
            var frontend = GetTwinItem<ITwinSection>();
            for (Int32 i = 0; i < frontend.GetItemsAmount(); i++)
            {
                var sound = frontend.GetItem<ITwinSound>(frontend.GetItem(i).GetID());
                var soundImport = new SoundEffect(package, $"{sound.GetName()}_ui_sfx_{i}", sound.GetID(), $"{sound.GetName()}_ui_sfx_{i}", sound);
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
