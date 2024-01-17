using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Media;
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
        private ObservableCollection<object> _textureFunctions;
        private ObservableCollection<object> _pixelFormats;

        public TextureViewModel(LabURI asset) : base(asset)
        {
            _textureFunctions = new ObservableCollection<object>(Enum.GetValues(typeof(ITwinTexture.TextureFunction)).Cast<object>());
            _pixelFormats = new ObservableCollection<object>(Enum.GetValues(typeof(ITwinTexture.TexturePixelFormat)).Cast<object>());
        }

        public TextureViewModel(LabURI asset, AssetViewModel parent) : base(asset, parent)
        {
            _pixelFormat = GetAsset<Texture>().PixelFormat;
            _texFun = GetAsset<Texture>().TextureFunction;
            _generateMipmaps = GetAsset<Texture>().GenerateMipmaps;
            _textureFunctions = new ObservableCollection<object>(Enum.GetValues(typeof(ITwinTexture.TextureFunction)).Cast<object>());
            _pixelFormats = new ObservableCollection<object>(Enum.GetValues(typeof(ITwinTexture.TexturePixelFormat)).Cast<object>());
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

        public ObservableCollection<object> TexFuns
        {
            get => _textureFunctions;
            private set
            {
                _textureFunctions = value;
                NotifyChange();
            }
        }

        public ObservableCollection<object> PixelFormats
        {
            get => _pixelFormats;
            private set
            {
                _pixelFormats = value;
                NotifyChange();
            }
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
