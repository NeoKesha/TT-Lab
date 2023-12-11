using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Global;

namespace TT_Lab.Assets.Global
{
    /// <summary>
    /// Save icons are Sony custom model formats that get displayed at PS2's memory card saves editor.
    /// They are not just a 3D model but can also have animation data baked in.
    /// <para>
    /// TODO: Add support/write a separate library for working with these (Who wouldn't want a custom save icon for their mods?)
    /// </para>
    /// </summary>
    public class SaveIcon : SerializableAsset
    {
        protected override String DataExt => ".bin";
        protected override String TwinDataExt => ".ico";
        public override UInt32 Section => throw new NotImplementedException();

        public SaveIcon()
        {
        }

        public SaveIcon(LabURI package, String? variant, String name, Byte[] data) : base((UInt32)Guid.NewGuid().GetHashCode(), name, package, variant)
        {
            assetData = new SaveIconData(data);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new SaveIconData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
