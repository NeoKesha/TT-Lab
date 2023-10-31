using System;
using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Common.Lights;
using Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.SM
{
    public interface ITwinScenery : ITwinItem
    {
        /// <summary>
        /// Flags
        /// </summary>
        UInt32 Flags { get; set; }
        /// <summary>
        /// Name of the scenery
        /// </summary>
        String Name { get; set; }
        /// <summary>
        /// Unknown unsigned integer parameter
        /// </summary>
        UInt32 UnkUInt { get; set; }
        /// <summary>
        /// Unknown byte parameter
        /// </summary>
        Byte UnkByte { get; set; }
        /// <summary>
        /// Skydome's ID to render
        /// </summary>
        UInt32 SkydomeID { get; set; }
        /// <summary>
        /// Unknown flags for the lights
        /// </summary>
        Boolean[] UnkLightFlags { get; set; }
        /// <summary>
        /// Reserved binary data. DO NOT EDIT
        /// </summary>
        Byte[] ReservedBlob { get; set; }
        /// <summary>
        /// Ambient lights present in the scenery
        /// </summary>
        List<AmbientLight> AmbientLights { get; set; }
        /// <summary>
        /// Directional lights present in the scenery
        /// </summary>
        List<DirectionalLight> DirectionalLights { get; set; }
        /// <summary>
        /// Point lights present in the scenery
        /// </summary>
        List<PointLight> PointLights { get; set; }
        /// <summary>
        /// Negative lights present in the scenery
        /// </summary>
        List<NegativeLight> NegativeLights { get; set; }
        /// <summary>
        /// Scenery tree
        /// </summary>
        List<TwinSceneryBaseType> Sceneries { get; set; }
    }
}
