using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Instance;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace TT_Lab.Assets.Instance
{
    public class CollisionSurface : SerializableInstance
    {
        public static Color[] DefaultColors = new Color[]
        {
            new Color(192,192,192,255),
            new Color(  0,  0,192,255),
            new Color(  0,  0,127,255),
            new Color(255, 96,  0,255),
            new Color(255,  0,  0,127),
            new Color(  0,  0,  0,127),
            new Color(  0,255,  0,255),
            new Color( 96, 96,127,255),
            new Color( 64, 32,  0,255),
            new Color( 96, 96, 96,255),
            new Color(192,192,  0,255),
            new Color( 32, 16,  0,255),
            new Color(  0,  0,255, 64),
            new Color( 32, 32, 32,255),
            new Color( 32, 32, 64,255),
            new Color(230,230,255,255),
            new Color(200,200,255,255),
            new Color( 32, 32,192,255),
            new Color(192,192,192,127),
            new Color(  0,255,  0,255),
            new Color(  0,127,  0, 64),
            new Color( 32, 32, 32,255),
            new Color( 64, 64,127,255),
            new Color(  0,  0,255,127),
            new Color(255,  0,  0,127),
            new Color(127,127,192,255),
            new Color(  0,  0,127,127),
            new Color(255,  0,255,127),
        };
        public static Color DefaultColor = new Color(127, 127, 127);

        public override UInt32 Section => Constants.LAYOUT_SURFACES_SECTION;

        public CollisionSurface(LabURI package, UInt32 id, String name, String chunk, Int32 layId, ITwinSurface surface) : base(package, id, name, chunk, layId)
        {
            assetData = new CollisionSurfaceData(surface);
            if (id < DefaultColors.Length)
            {
                Parameters.Add("editor_surface_color", DefaultColors[id]);
            }
            else
            {
                Parameters.Add("editor_surface_color", DefaultColors);
            }
        }

        public CollisionSurface()
        {
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }

        public override AssetViewModel GetViewModel(AssetViewModel? parent = null)
        {
            viewModel ??= new CollisionSurfaceViewModel(URI, parent);
            return viewModel;
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new CollisionSurfaceData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
