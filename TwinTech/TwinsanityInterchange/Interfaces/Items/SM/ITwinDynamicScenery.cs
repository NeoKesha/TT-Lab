﻿using System;
using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Common.DynamicScenery;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.SM
{
    public interface ITwinDynamicScenery : ITwinItem
    {
        /// <summary>
        /// Unknown integer parameter
        /// </summary>
        Int32 UnkInt { get; set; }
        /// <summary>
        /// All the dynamic models for this scenery
        /// </summary>
        List<TwinDynamicSceneryModel> DynamicModels { get; set; }
    }
}
