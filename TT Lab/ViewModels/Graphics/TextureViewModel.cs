using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets.Graphics;

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

        public int TextureFunction
        {
            get
            {
                return Convert.ToInt32(_asset.Parameters["texture_function"]);
            }
            set
            {
                if (value != (int)_asset.Parameters["texture_function"])
                {
                    _asset.Parameters["texture_function"] = value;
                    NotifyChange("TextureFunction");
                }
            }
        }

        public int PixelStorageFormat
        {
            get
            {
                return Convert.ToInt32(_asset.Parameters["pixel_storage_format"]);
            }
            set
            {
                if (value != (int)_asset.Parameters["pixel_storage_format"])
                {
                    _asset.Parameters["pixel_storage_format"] = value;
                    NotifyChange("PixelStorageFormat");
                }
            }
        }
    }
}
