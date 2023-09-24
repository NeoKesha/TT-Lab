using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Editors.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Assets.Graphics
{
    public class Model : SerializableAsset
    {
        protected override String DataExt => ".dae";

        public Model(String package, String subpackage, String? variant, UInt32 id, String Name, PS2AnyModel model) : base(id, Name, package, subpackage, variant)
        {
            assetData = new ModelData(model);
            Raw = false;
        }

        public Model()
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
            return typeof(ModelEditor);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new ModelData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
