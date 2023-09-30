using System.Diagnostics;
using System.Drawing;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.ViewModels.Graphics
{
    public class TextureViewModel : AssetViewModel
    {
        private Bitmap? _texture;
        private ITwinTexture.TextureFunction _texFun;
        private ITwinTexture.TexturePixelFormat _pixelFormat;

        public TextureViewModel(LabURI asset) : base(asset)
        {
        }

        public TextureViewModel(LabURI asset, AssetViewModel parent) : base(asset, parent)
        {
            Debug.Assert(Asset.Parameters["pixel_storage_format"] != null && Asset.Parameters["texture_function"] != null, "Texture parameters must be filled in");
            _pixelFormat = MiscUtils.ConvertEnum<ITwinTexture.TexturePixelFormat>(Asset.Parameters["pixel_storage_format"]!);
            _texFun = MiscUtils.ConvertEnum<ITwinTexture.TextureFunction>(Asset.Parameters["texture_function"]!);
        }

        public override void Save(object? o)
        {
            var data = Asset.GetData<TextureData>();
            data.Bitmap = Texture;
            Asset.Parameters["pixel_storage_format"] = PixelStorageFormat;
            Asset.Parameters["texture_function"] = TextureFunction;
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
    }
}
