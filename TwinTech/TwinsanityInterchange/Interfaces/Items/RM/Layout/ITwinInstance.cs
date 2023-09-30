using System.Collections.Generic;
using System;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout
{
    public interface ITwinInstance : ITwinItem
    {
        Vector4 Position { get; set; }
        TwinIntegerRotation RotationX { get; set; }
        TwinIntegerRotation RotationY { get; set; }
        TwinIntegerRotation RotationZ { get; set; }
        UInt32 InstancesRelated { get; set; }
        List<UInt16> Instances { get; set; }
        UInt32 PositionsRelated { get; set; }
        List<UInt16> Positions { get; set; }
        UInt32 PathsRelated { get; set; }
        List<UInt16> Paths { get; set; }
        UInt16 ObjectId { get; set; }
        Int16 RefListIndex { get; set; }
        UInt16 OnSpawnHeaderScriptID { get; set; }
        UInt32 StateFlags { get; set; }
        List<UInt32> ParamList1 { get; set; }
        List<Single> ParamList2 { get; set; }
        List<UInt32> ParamList3 { get; set; }
    }
}
