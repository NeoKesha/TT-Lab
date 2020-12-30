using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

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
        public CollisionSurface(UInt32 id, String name, String chunk, Int32 layId, PS2AnyCollisionSurface surface) : base(id, name, chunk, layId)
        {
            assetData = new CollisionSurfaceData(surface);
            if (id < DefaultColors.Length)
            {
                parameters.Add("editor_surface_color", DefaultColors[id]);
            }
            else
            {
                parameters.Add("editor_surface_color", DefaultColors);
            }
        }

        public CollisionSurface()
        {
        }

        public override String Type => "CollisionSurface";

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
