using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Editors.Graphics;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Graphics;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.Assets.Graphics
{
    public class Texture : SerializableAsset
    {
        protected override String DataExt => ".png";
        public override UInt32 Section => Constants.GRAPHICS_TEXTURES_SECTION;

        public Texture(LabURI package, String? variant, UInt32 id, String name, ITwinTexture texture) : base(id, name, package, variant)
        {
            assetData = new TextureData(texture);
            Raw = false;
            Parameters.Add("texture_function", texture.TexFun);
            Parameters.Add("pixel_storage_format", texture.TextureFormat);
        }

        public Texture()
        {
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
            return typeof(TextureEditor);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new TextureData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }

        public override AssetViewModel GetViewModel(AssetViewModel? parent = null)
        {
            viewModel ??= new TextureViewModel(URI, parent);
            return viewModel;
        }
    }
}
