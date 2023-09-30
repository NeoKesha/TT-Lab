using System;
using System.Collections.Generic;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM
{
    public interface ITwinDefaultParticle
    {
        UInt32[] TextureIDs { get; set; }
        UInt32[] MaterialIDs { get; set; }
        UInt32 DecalTextureID { get; set; }
        UInt32 DecalMaterialID { get; set; }
        Byte[] UnkData { get; set; }
        Byte[] UnkBlob { get; set; }
        Int32[] UnkInts { get; set; }
        List<Byte[]> UnkBlobs { get; set; }
    }
}
