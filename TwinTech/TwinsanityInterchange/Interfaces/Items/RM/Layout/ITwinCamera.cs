using System;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout
{
    public interface ITwinCamera : ITwinItem
    {
        TwinTrigger CamTrigger { get; set; }
        UInt32 CameraHeader { get; set; }
        UInt16 UnkShort { get; set; }
        Single UnkFloat1 { get; set; } // 10
        Vector4 UnkVector1 { get; set; }
        Vector4 UnkVector2 { get; set; } // 42
        Single UnkFloat2 { get; set; }
        Single UnkFloat3 { get; set; } // 50
        UInt32 UnkInt1 { get; set; }
        UInt32 UnkInt2 { get; set; }
        UInt32 UnkInt3 { get; set; }
        UInt32 UnkInt4 { get; set; } // 66
        UInt32 UnkInt5 { get; set; }
        UInt32 UnkInt6 { get; set; } // 74
        Single UnkFloat4 { get; set; }
        Single UnkFloat5 { get; set; }
        Single UnkFloat6 { get; set; }
        Single UnkFloat7 { get; set; } // 90
        UInt32 UnkInt7 { get; set; }
        UInt32 UnkInt8 { get; set; } // 98
        UInt32 UnkInt9 { get; set; }
        Single UnkFloat8 { get; set; } // 106
        UInt32 TypeIndex1 { get; set; }
        UInt32 TypeIndex2 { get; set; } // 114
        Byte UnkByte { get; set; } // 115
        CameraSubBase MainCamera1 { get; set; }
        CameraSubBase MainCamera2 { get; set; }
    }
}
