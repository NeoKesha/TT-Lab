using System;

namespace Twinsanity.TwinsanityInterchange.Enumerations
{
    public static class Enums
    {
        public enum Layouts
        {
            LAYER_1,
            LAYER_2,
            LAYER_3,
            LAYER_4,
            LAYER_5,
            LAYER_6,
            LAYER_7,
            LAYER_8
        }

        public enum ObjectType
        {
            PLAYABLE_CHARACTER,
            TYPE_1,
            TYPE_2,
            TYPE_3,
            TYPE_4,
            TYPE_5,
            TYPE_6,
            TYPE_7,
            TYPE_8
        }

        [Flags]
        public enum InstanceState : UInt32
        {
            Deactivated = 1 << 0,
            CollisionActive = 1 << 1,
            Visible = 1 << 2,
            Unknown1 = 1 << 3,
            Unknown2 = 1 << 4,
            Unknown3 = 1 << 5,
            Unknown4 = 1 << 6,
            Unknown5 = 1 << 7,
            ReceiveOnTriggerSignals = 1 << 8,
            CanDamageCharacter = 1 << 9,
            Unknown6 = 1 << 10,
            Unknown7 = 1 << 11,
            Unknown8 = 1 << 12,
            Unknown9 = 1 << 13,
            Unknown10 = 1 << 14,
            Unknown11 = 1 << 15,
            CanAlwaysDamageCharacter = 1 << 16,
            Unknown12 = 1 << 17,
            Unknown13 = 1 << 18,
            Unknown14 = 1 << 19,
            Unknown15 = 1 << 20,
            Unknown16 = 1 << 21,
            Unknown17 = 1 << 22,
            Unknown18 = 1 << 23,
            Unknown19 = 1 << 24,
            Unknown20 = 1 << 25,
            Unknown21 = 1 << 26,
            Unknown22 = 1 << 27,
            Unknown23 = 1 << 28,
            Unknown24 = 1 << 29,
            Unknown25 = 1 << 30,
            Unknown26 = 1U << 31,
        }

        [Flags]
        public enum TriggerActivatorObjects
        {
            PlayableCharacter = 1 << 0,
            Pickups = 1 << 1,
            Crates = 1 << 2,
            Creatures = 1 << 3,
            GenericObjects = 1 << 4,
            Grabbables = 1 << 5,
            PayGates = 1 << 6,
            Graples = 1 << 7,
            Projectiles = 1 << 8
        }
    }
}
