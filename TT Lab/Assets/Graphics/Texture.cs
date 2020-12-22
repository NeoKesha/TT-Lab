﻿using Newtonsoft.Json;
using System;
using TT_Lab.AssetData.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Assets.Graphics
{
    public class Texture : SerializableAsset<TextureData>
    {
        public override String Type => "Texture";

        public Texture(UInt32 id, String name, PS2AnyTexture texture) : base(id, name)
        {
            assetData = new TextureData(texture);
        }

        public Texture()
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
    }
}
