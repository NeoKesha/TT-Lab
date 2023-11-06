using System.Diagnostics;
using System.Drawing;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
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
            var pixelFormat = Asset.GetParameter<ITwinTexture.TexturePixelFormat?>("pixel_storage_format");
            var texFun = Asset.GetParameter<ITwinTexture.TextureFunction?>("texture_function");
            Debug.Assert(pixelFormat != null && texFun != null, "Texture parameters must be filled in");
            _pixelFormat = (ITwinTexture.TexturePixelFormat)pixelFormat;
            _texFun = (ITwinTexture.TextureFunction)texFun;
        }

        public override void Save(object? o)
        {
            var data = Asset.GetData<TextureData>();
            data.Bitmap = Texture;
            Asset.SetParameter("pixel_storage_format", PixelStorageFormat);
            Asset.SetParameter("texture_function", TextureFunction);
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
