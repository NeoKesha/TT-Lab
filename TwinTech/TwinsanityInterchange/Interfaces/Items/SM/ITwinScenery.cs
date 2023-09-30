using System.Collections.Generic;
using System;
using Twinsanity.TwinsanityInterchange.Common.Lights;
using Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.SM
{
    public interface ITwinScenery : ITwinItem
    {
        UInt32 Flags { get; set; }
        String Name { get; set; }
        UInt32 UnkUInt { get; set; }
        Byte UnkByte { get; set; }
        UInt32 SkydomeID { get; set; }
        Boolean[] UnkLightFlags { get; set; }
        Byte[] ReservedBlob { get; set; }
        List<AmbientLight> AmbientLights { get; set; }
        List<DirectionalLight> DirectionalLights { get; set; }
        List<PointLight> PointLights { get; set; }
        List<NegativeLight> NegativeLights { get; set; }
        List<TwinSceneryBaseType> Sceneries { get; set; }
    }
}
