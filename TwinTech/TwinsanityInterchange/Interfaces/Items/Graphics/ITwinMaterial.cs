using System;
using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items
{
    public interface ITwinMaterial : ITwinItem
    {
        /// <summary>
        /// Bitmap of the activated shaders
        /// </summary>
        UInt64 Header { get; set; }
        /// <summary>
        /// Special DMA index in game's DMA chain manager
        /// </summary>
        UInt32 DmaChainIndex { get; set; }
        /// <summary>
        /// Name of the material
        /// </summary>
        String Name { get; set; }
        /// <summary>
        /// List of shaders to apply
        /// </summary>
        List<TwinShader> Shaders { get; set; }
    }
}
