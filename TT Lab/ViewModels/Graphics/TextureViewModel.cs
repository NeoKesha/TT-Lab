using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.ViewModels.Graphics
{
    public class TextureViewModel : AssetViewModel
    {
        private Bitmap _texture;
        private PS2AnyTexture.TextureFunction _texFun;
        private PS2AnyTexture.TexturePixelFormat _pixelFormat;

        public TextureViewModel(Guid asset) : base(asset)
        {
        }

        public TextureViewModel(Guid asset, AssetViewModel parent) : base(asset, parent)
        {
            _pixelFormat = MiscUtils.ConvertEnum<PS2AnyTexture.TexturePixelFormat>(Asset.Parameters["pixel_storage_format"]);
            _texFun = MiscUtils.ConvertEnum<PS2AnyTexture.TextureFunction>(Asset.Parameters["texture_function"]);
        }

        public override void Save(object? o)
        {
            var data = (TextureData)Asset.GetData();
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
                if (_texture == null)
                {
                    _texture = (Bitmap)((TextureData)_asset.GetData()).Bitmap.Clone();
                }
                return _texture;
            }
            set
            {
                //_texture?.Dispose();
                _texture = (Bitmap)value.Clone();
                IsDirty = true;
            }
        }

        public PS2AnyTexture.TextureFunction TextureFunction
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

        public PS2AnyTexture.TexturePixelFormat PixelStorageFormat
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
