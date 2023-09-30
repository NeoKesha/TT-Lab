using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Editors.Graphics;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.Assets.Graphics
{
    public class Skin : SerializableAsset
    {
        protected override String DataExt => ".dae";
        public Skin(LabURI package, String? variant, UInt32 id, String name, ITwinSkin skin) : base(id, name, package, variant)
        {
            assetData = new SkinData(skin);
            Raw = false;
            Materials = new();
        }

        [JsonProperty(Required = Required.Always)]
        public List<LabURI> Materials { get; set; }

        public Skin()
        {
            Materials = new();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override Type GetEditorType()
        {
            return typeof(SkinModelEditor);
        }

        public override void Import()
        {
            var skinData = (SkinData)assetData;
            var skin = skinData.GetRef();
            foreach (var e in skin.SubSkins)
            {
                Materials.Add(AssetManager.Get().GetUri(Package, typeof(Material).Name, Variation, e.Material));
            }
            base.Import();
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new SkinData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
