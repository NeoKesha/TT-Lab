using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.ViewModels.Graphics
{
    public class TextureViewModel : AssetViewModel
    {
        public TextureViewModel(Guid asset) : base(asset)
        {
        }

        public TextureViewModel(Guid asset, AssetViewModel parent) : base(asset, parent)
        {
        }

        private T ConvertEnum<T>(object o)
        {
            return (T)Enum.Parse(typeof(T), o.ToString());
        }

        public PS2AnyTexture.TextureFunction TextureFunction
        {
            get
            {
                return ConvertEnum<PS2AnyTexture.TextureFunction>(_asset.Parameters["texture_function"]);
            }
            set
            {
                if (value != ConvertEnum<PS2AnyTexture.TextureFunction>(_asset.Parameters["texture_function"]))
                {
                    _asset.Parameters["texture_function"] = value;
                    NotifyChange();
                }
            }
        }

        public PS2AnyTexture.TexturePixelFormat PixelStorageFormat
        {
            get
            {
                return ConvertEnum<PS2AnyTexture.TexturePixelFormat>(_asset.Parameters["pixel_storage_format"]);
            }
            set
            {
                if (value != ConvertEnum<PS2AnyTexture.TexturePixelFormat>(_asset.Parameters["pixel_storage_format"]))
                {
                    _asset.Parameters["pixel_storage_format"] = value;
                    NotifyChange();
                }
            }
        }
    }
}
