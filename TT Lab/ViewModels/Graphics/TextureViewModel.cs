using System;
using System.Drawing;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Assets.Graphics;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.ViewModels.Graphics
{
    public class TextureViewModel : AssetViewModel
    {
        private Bitmap? _texture;
        private ITwinTexture.TextureFunction _texFun;
        private ITwinTexture.TexturePixelFormat _pixelFormat;
        private Boolean _generateMipmaps;

        public TextureViewModel(LabURI asset) : base(asset)
        {
        }

        public TextureViewModel(LabURI asset, AssetViewModel parent) : base(asset, parent)
        {
            _pixelFormat = GetAsset<Texture>().PixelFormat;
            _texFun = GetAsset<Texture>().TextureFunction;
            _generateMipmaps = GetAsset<Texture>().GenerateMipmaps;
        }

        public override void Save(object? o)
        {
            var data = Asset.GetData<TextureData>();
            data.Bitmap = Texture;
            GetAsset<Texture>().PixelFormat = PixelStorageFormat;
            GetAsset<Texture>().TextureFunction = TextureFunction;
            GetAsset<Texture>().GenerateMipmaps = GenerateMipmaps;
            base.Save(o);
        }

        protected override void UnloadData()
        {
            _texture?.Dispose();
            _texture = null;
            base.UnloadData();
        }

        public Bitmap Texture
        {
            get
            {
                _texture ??= (Bitmap)(_asset.GetData<TextureData>()).Bitmap.Clone();
                return _texture;
            }
            set
            {
                //_texture?.Dispose();
                _texture = (Bitmap)value.Clone();
                IsDirty = true;
            }
        }

        public ITwinTexture.TextureFunction TextureFunction
        {
            get
            {
                return _texFun;
            }
            set
            {
                if (value != _texFun)
                {
                    _texFun = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }

        public ITwinTexture.TexturePixelFormat PixelStorageFormat
        {
            get
            {
                return _pixelFormat;
            }
            set
            {
                if (value != _pixelFormat)
                {
                    _pixelFormat = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }

        public Boolean GenerateMipmaps
        {
            get
            {
                return _generateMipmaps;
            }

            set
            {
                if (value != _generateMipmaps)
                {
                    _generateMipmaps = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
    }
}
