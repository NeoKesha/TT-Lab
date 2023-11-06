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
            PICKUP,
            CRATE,
            CREATURE,
            GENERIC_OBJECT,
            GRABBABLE,
            PAY_GATE,
            GRAPLE,
            PROJECTILE
        }

        public enum LodType
        {
            FULL = 0x1001,
            COMPRESSED = 0x1002
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

        [Flags]
        public enum AppliedShaders : UInt64
        {
            LitSkinnedModel = 0x2,
            StandardLit = 0x4,
            StandardUnlit = 0x8,
            UnlitBillboard = StandardUnlit,
            UnlitSkydome = 0x10,
            ColorOnly = 0x20,
            LitEnvironmentMap = 0x40,
            LitMetallic = 0x200,
            LitReflectionSurface = 0x400,
            Particle = 0x1000,
            Decal = 0x2000,
            UnlitGlossy = 0x4000,
            UnlitEnvironmentMap = 0x8000,
            UnlitClothDeformation = 0x10000,
            UnlitClothDeformation2 = 0x80000,
            UiShader = 0x10000000,
        }

        public enum SurfaceTypes : UInt32
        {
            SURF_DEFAULT = 0,
            SURF_GENERIC_SLIGHTLY_SLIPPY = 1,
            SURF_GENERIC_MEDIUM_SLIPPY = 2,
            SURF_LAVA = 3,
            SURF_GENERIC_INSTANT_DEATH = 4,
            SURF_FALL_THRU_DEATH = 5,
            SURF_NORMAL_GRASS = 6,
            SURF_SLIPPY_METAL = 7,
            SURF_NORMAL_WOOD = 8,
            SURF_NORMAL_METAL = 9,
            SURF_NORMAL_SAND = 10,
            SURF_NORMAL_MUD = 11,
            SURF_NORMAL_WATER = 12,
            SURF_NORMAL_ROCK = 13,
            SURF_SLIPPY_ROCK = 14,
            SURF_NORMAL_SNOW = 15,
            SURF_STICKY_SNOW = 16,
            SURF_ICE = 17,
            SURF_GLASS_WALL = 18,
            SURF_HACK_RAIL = 19,
            SURF_CAMERA_BLOCKING = 20,
            SURF_NORMAL_STONE_TILES = 21,
            SURF_ICE_LOW_SLIPPY = 22,
            SURF_DROWNING_PLANE = 23,
            SURF_BLOCK_PLAYER = 24,
            SURF_GENERIC_MEDIUM_SLIPPY_RIGID_ONLY = 25,
            SURF_NONSOLID_ELECTRIC_DEATH = 26,
            SURF_BLOCK_AI_ONLY = 27,
        }
    }
}
