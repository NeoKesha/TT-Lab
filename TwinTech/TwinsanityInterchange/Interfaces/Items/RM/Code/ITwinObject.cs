using System.Collections.Generic;
using System;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code
{
    public interface ITwinObject : ITwinItem
    {
        [Flags]
        enum ResourcesBitfield
        {
            OBJECTS = 1 << 0,
            OGIS = 1 << 1,
            ANIMATIONS = 1 << 2,
            CODE_MODELS = 1 << 3,
            SCRIPTS = 1 << 4,
            UNKNOWN = 1 << 5,
            SOUNDS = 1 << 6,
        }

        enum ObjectType
        {
            Character,
            Pickup,
            Crate,
            Creature,
            GenericObject, // aka Furniture, internal name by Twinsanity's engine
            Grabbable, // aka ChiChiGrass
            PayGate,   // Unused by all the objects included in the game
            Graple,    // aka Nina's Hand/Foofie in Evolution
            Projectile
        }
        ObjectType Type { get; set; }
        Byte UnkTypeValue { get; set; }
        Byte ReactJointAmount { get; set; }
        Byte ExitPointAmount { get; set; }
        Byte[] SlotsMap { get; set; }
        String Name { get; set; }
        List<UInt32> TriggerScripts { get; set; }
        List<UInt16> OGISlots { get; set; }
        List<UInt16> AnimationSlots { get; set; }
        List<UInt16> ScriptSlots { get; set; }
        List<UInt16> ObjectSlots { get; set; }
        List<UInt16> SoundSlots { get; set; }
        UInt32 InstanceStateFlags { get; set; }
        List<UInt32> InstFlags { get; set; }
        List<Single> InstFloats { get; set; }
        List<UInt32> InstIntegers { get; set; }
        List<UInt16> RefObjects { get; set; }
        List<UInt16> RefOGIs { get; set; }
        List<UInt16> RefAnimations { get; set; }
        List<UInt16> RefCodeModels { get; set; }
        List<UInt16> RefScripts { get; set; }
        List<UInt16> RefUnknowns { get; set; }
        List<UInt16> RefSounds { get; set; }
        TwinBehaviourCommandPack ScriptPack { get; set; }

        bool HasInstanceProperties { get; }

        bool ReferencesResources { get; }
    }
}
