using System.Collections.Generic;
using System;
using Twinsanity.TwinsanityInterchange.Common.DynamicScenery;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.SM
{
    public interface ITwinDynamicScenery : ITwinItem
    {
        Int32 UnkInt { get; set; }
        List<TwinDynamicSceneryModel> DynamicModels { get; set; }
    }
}
