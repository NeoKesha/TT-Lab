using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code
{
    public class PS2AnyObject : BaseTwinItem, ITwinObject
    {
        [Flags]
        public enum ResourcesBitfield
        {
            OBJECTS = 1 << 0,
            OGIS = 1 << 1,
            ANIMATIONS = 1 << 2,
            CODE_MODELS = 1 << 3,
            SCRIPTS = 1 << 4,
            UNKNOWN = 1 << 5,
            SOUNDS = 1 << 6,
        }

        public Byte Type;
        public Byte UnkTypeValue;
        public Byte UnkOgiArraySize;
        public Byte OgiType2ArraySize;
        public Byte[] SlotsMap;
        public String Name;
        public List<UInt32> TriggerScripts;
        public List<UInt16> OGISlots;
        public List<UInt16> AnimationSlots;
        public List<UInt16> ScriptSlots;
        public List<UInt16> ObjectSlots;
        public List<UInt16> SoundSlots;
        public UInt32 InstanceStateFlags;
        public List<UInt32> InstFlags;
        public List<Single> InstFloats;
        public List<UInt32> InstIntegers;
        public List<UInt16> RefObjects;
        public List<UInt16> RefOGIs;
        public List<UInt16> RefAnimations;
        public List<UInt16> RefCodeModels;
        public List<UInt16> RefScripts;
        public List<UInt16> RefUnknowns;
        public List<UInt16> RefSounds;
        public ScriptPack ScriptPack;

        public bool HasInstanceProperties
        {
            get
            {
                return InstFlags.Count > 0 || InstFloats.Count > 0 || InstIntegers.Count > 0;
            }
        }

        public bool ReferencesResources
        {
            get
            {
                return RefObjects.Count > 0 || RefOGIs.Count > 0 || RefAnimations.Count > 0 ||
                    RefCodeModels.Count > 0 || RefScripts.Count > 0 || RefUnknowns.Count > 0 ||
                    RefSounds.Count > 0;
            }
        }

        public PS2AnyObject()
        {
            SlotsMap = new Byte[8];
            TriggerScripts = new List<UInt32>();
            OGISlots = new List<UInt16>();
            AnimationSlots = new List<UInt16>();
            ScriptSlots = new List<UInt16>();
            ObjectSlots = new List<UInt16>();
            SoundSlots = new List<UInt16>();
            InstFlags = new List<UInt32>();
            InstFloats = new List<Single>();
            InstIntegers = new List<UInt32>();
            RefObjects = new List<UInt16>();
            RefOGIs = new List<UInt16>();
            RefAnimations = new List<UInt16>();
            RefCodeModels = new List<UInt16>();
            RefScripts = new List<UInt16>();
            RefUnknowns = new List<UInt16>();
            RefSounds = new List<UInt16>();
        }

        public override int GetLength()
        {
            var resourcesLength = 0;
            if (ReferencesResources)
            {
                resourcesLength += 4;
                if (RefObjects.Count > 0)
                {
                    resourcesLength += 4;
                    resourcesLength += RefObjects.Count * Constants.SIZE_UINT16;
                }
                if (RefOGIs.Count > 0)
                {
                    resourcesLength += 4;
                    resourcesLength += RefOGIs.Count * Constants.SIZE_UINT16;
                }
                if (RefAnimations.Count > 0)
                {
                    resourcesLength += 4;
                    resourcesLength += RefAnimations.Count * Constants.SIZE_UINT16;
                }
                if (RefCodeModels.Count > 0)
                {
                    resourcesLength += 4;
                    resourcesLength += RefCodeModels.Count * Constants.SIZE_UINT16;
                }
                if (RefScripts.Count > 0)
                {
                    resourcesLength += 4;
                    resourcesLength += RefScripts.Count * Constants.SIZE_UINT16;
                }
                if (RefUnknowns.Count > 0)
                {
                    resourcesLength += 4;
                    resourcesLength += RefUnknowns.Count * Constants.SIZE_UINT16;
                }
                if (RefSounds.Count > 0)
                {
                    resourcesLength += 4;
                    resourcesLength += RefSounds.Count * Constants.SIZE_UINT16;
                }
            }
            // Truly a bruh moment
            return 16 + Name.Length + 24 +
                TriggerScripts.Count * Constants.SIZE_UINT32 + OGISlots.Count * Constants.SIZE_UINT16 +
                AnimationSlots.Count * Constants.SIZE_UINT16 + ScriptSlots.Count * Constants.SIZE_UINT16 +
                ObjectSlots.Count * Constants.SIZE_UINT16 + SoundSlots.Count * Constants.SIZE_UINT16 +
                (HasInstanceProperties ? 20 + InstFlags.Count * Constants.SIZE_UINT32 + InstFloats.Count * 4 +
                InstIntegers.Count * Constants.SIZE_UINT32 : 0) + resourcesLength + ScriptPack.GetLength();
        }

        public override void Read(BinaryReader reader, int length)
        {
            var bitfield = reader.ReadUInt32();
            Type = (Byte)(bitfield >> 0x14 & 0xFF);
            UnkTypeValue = (Byte)(bitfield >> 0xC & 0xFF);
            UnkOgiArraySize = (Byte)(bitfield >> 0x6 & 0x3F);
            OgiType2ArraySize = (Byte)(bitfield & 0x3F);

            var hasInstProps = (bitfield & 0x20000000) != 0;
            var refRes = (bitfield & 0x40000000) != 0;
            for (var i = 0; i < 8; ++i)
            {
                SlotsMap[i] = reader.ReadByte();
            }
            var strLen = reader.ReadInt32();
            Name = new String(reader.ReadChars(strLen));

            FillResourceList(reader, TriggerScripts, true);
            FillResourceList(reader, OGISlots);
            FillResourceList(reader, AnimationSlots);
            FillResourceList(reader, ScriptSlots);
            FillResourceList(reader, ObjectSlots);
            FillResourceList(reader, SoundSlots);

            if (hasInstProps)
            {
                reader.ReadUInt32();
                InstanceStateFlags = reader.ReadUInt32();
                FillResourceList(reader, InstFlags, true);
                // Sadly this is the only one not fitting into UI32 or UI16, smh too lazy to create a Type to BinaryReader mapper :^)
                var amount = reader.ReadInt32();
                InstFloats.Clear();
                for (var i = 0; i < amount; ++i)
                {
                    InstFloats.Add(reader.ReadSingle());
                }
                FillResourceList(reader, InstIntegers, true);
            }

            if (refRes)
            {
                var resources = (ResourcesBitfield)reader.ReadUInt32();
                if (resources.HasFlag(ResourcesBitfield.OBJECTS))
                {
                    FillResourceList(reader, RefObjects);
                }
                if (resources.HasFlag(ResourcesBitfield.OGIS))
                {
                    FillResourceList(reader, RefOGIs);
                }
                if (resources.HasFlag(ResourcesBitfield.ANIMATIONS))
                {
                    FillResourceList(reader, RefAnimations);
                }
                if (resources.HasFlag(ResourcesBitfield.CODE_MODELS))
                {
                    FillResourceList(reader, RefCodeModels);
                }
                if (resources.HasFlag(ResourcesBitfield.SCRIPTS))
                {
                    FillResourceList(reader, RefScripts);
                }
                if (resources.HasFlag(ResourcesBitfield.UNKNOWN))
                {
                    FillResourceList(reader, RefUnknowns);
                }
                if (resources.HasFlag(ResourcesBitfield.SOUNDS))
                {
                    FillResourceList(reader, RefSounds);
                }
            }
            ScriptPack = new ScriptPack();
            ScriptPack.Read(reader, length);
        }

        public override void Write(BinaryWriter writer)
        {
            UInt32 newBitfield = OgiType2ArraySize;
            if (ReferencesResources)
            {
                newBitfield |= 0x40000000;
            }
            if (HasInstanceProperties)
            {
                newBitfield |= 0x20000000;
            }
            UInt32 objType = (UInt32)(Type << 0x14);
            UInt32 objTypeRelVal = (UInt32)(UnkTypeValue << 0xC);
            UInt32 unkOgiArraySize = (UInt32)(UnkOgiArraySize << 0x6);
            newBitfield |= objType;
            newBitfield |= objTypeRelVal;
            newBitfield |= unkOgiArraySize;
            writer.Write(newBitfield);
            writer.Write(SlotsMap);
            writer.Write(Name.Length);
            writer.Write(Name.ToCharArray(), 0, Name.Length);

            WriteResourceList(writer, TriggerScripts, true);
            WriteResourceList(writer, OGISlots);
            WriteResourceList(writer, AnimationSlots);
            WriteResourceList(writer, ScriptSlots);
            WriteResourceList(writer, ObjectSlots);
            WriteResourceList(writer, SoundSlots);

            if (HasInstanceProperties)
            {
                writer.Write((Byte)InstFlags.Count);
                writer.Write((Byte)InstFloats.Count);
                writer.Write((Byte)InstIntegers.Count);
                writer.Write((Byte)0);
                writer.Write(InstanceStateFlags);
                WriteResourceList(writer, InstFlags, true);
                writer.Write(InstFloats.Count);
                for (var i = 0; i < InstFloats.Count; ++i)
                {
                    writer.Write(InstFloats[i]);
                }
                WriteResourceList(writer, InstIntegers, true);
            }

            if (ReferencesResources)
            {
                ResourcesBitfield newResources = new ResourcesBitfield();
                if (RefObjects.Count > 0)
                {
                    newResources |= ResourcesBitfield.OBJECTS;
                }
                if (RefOGIs.Count > 0)
                {
                    newResources |= ResourcesBitfield.OGIS;
                }
                if (RefAnimations.Count > 0)
                {
                    newResources |= ResourcesBitfield.ANIMATIONS;
                }
                if (RefCodeModels.Count > 0)
                {
                    newResources |= ResourcesBitfield.CODE_MODELS;
                }
                if (RefScripts.Count > 0)
                {
                    newResources |= ResourcesBitfield.SCRIPTS;
                }
                if (RefUnknowns.Count > 0)
                {
                    newResources |= ResourcesBitfield.UNKNOWN;
                }
                if (RefSounds.Count > 0)
                {
                    newResources |= ResourcesBitfield.SOUNDS;
                }
                writer.Write((UInt32)newResources);
                if (RefObjects.Count > 0)
                {
                    WriteResourceList(writer, RefObjects);
                }
                if (RefOGIs.Count > 0)
                {
                    WriteResourceList(writer, RefOGIs);
                }
                if (RefAnimations.Count > 0)
                {
                    WriteResourceList(writer, RefAnimations);
                }
                if (RefCodeModels.Count > 0)
                {
                    WriteResourceList(writer, RefCodeModels);
                }
                if (RefScripts.Count > 0)
                {
                    WriteResourceList(writer, RefScripts);
                }
                if (RefUnknowns.Count > 0)
                {
                    WriteResourceList(writer, RefUnknowns);
                }
                if (RefSounds.Count > 0)
                {
                    WriteResourceList(writer, RefSounds);
                }
            }
            ScriptPack.Write(writer);
        }

        private void FillResourceList(BinaryReader reader, IList list, bool UI32 = false)
        {
            var amount = reader.ReadInt32();
            list.Clear();
            for (var i = 0; i < amount; ++i)
            {
                if (UI32)
                {
                    list.Add(reader.ReadUInt32());
                }
                else
                {
                    list.Add(reader.ReadUInt16());
                }
            }
        }

        private void WriteResourceList(BinaryWriter writer, IList list, bool UI32 = false)
        {
            writer.Write(list.Count);
            for (var i = 0; i < list.Count; ++i)
            {
                if (UI32)
                {
                    writer.Write((UInt32)list[i]);
                }
                else
                {
                    writer.Write((UInt16)list[i]);
                }
            }
        }

        public override String GetName()
        {
            return Name.Replace("|","_");
        }
    }
}
