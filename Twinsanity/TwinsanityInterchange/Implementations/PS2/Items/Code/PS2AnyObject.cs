﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Code
{
    public class PS2AnyObject : ITwinObject
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

        UInt32 id;
        public UInt32 Bitfield;
        public Byte[] SlotsMap;
        public String Name;
        public List<UInt32> UInt32Slots;
        public List<UInt16> OGISlots;
        public List<UInt16> AnimationSlots;
        public List<UInt16> ScriptSlots;
        public List<UInt16> ObjectSlots;
        public List<UInt16> SoundSlots;
        public UInt32 InstancePropsHeader;
        public UInt32 UnkUInt;
        public List<UInt32> InstFlags;
        public List<Single> InstFloats;
        public List<UInt32> InstIntegers;
        public ResourcesBitfield Resources;
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
                return (Bitfield & 0x20000000) != 0;
            }
        }

        public bool ReferencesResources
        {
            get
            {
                return (Bitfield & 0x40000000) != 0;
            }
        }

        public PS2AnyObject()
        {
            SlotsMap = new Byte[8];
            UInt32Slots = new List<UInt32>();
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

        public uint GetID()
        {
            return id;
        }

        public int GetLength()
        {
            var resourcesLength = 0;
            if (ReferencesResources)
            {
                resourcesLength += 4;
                if (Resources.HasFlag(ResourcesBitfield.OBJECTS))
                {
                    resourcesLength += 4;
                    resourcesLength += RefObjects.Count * Constants.SIZE_UINT16;
                }
                if (Resources.HasFlag(ResourcesBitfield.OGIS))
                {
                    resourcesLength += 4;
                    resourcesLength += RefOGIs.Count * Constants.SIZE_UINT16;
                }
                if (Resources.HasFlag(ResourcesBitfield.ANIMATIONS))
                {
                    resourcesLength += 4;
                    resourcesLength += RefAnimations.Count * Constants.SIZE_UINT16;
                }
                if (Resources.HasFlag(ResourcesBitfield.CODE_MODELS))
                {
                    resourcesLength += 4;
                    resourcesLength += RefCodeModels.Count * Constants.SIZE_UINT16;
                }
                if (Resources.HasFlag(ResourcesBitfield.SCRIPTS))
                {
                    resourcesLength += 4;
                    resourcesLength += RefScripts.Count * Constants.SIZE_UINT16;
                }
                if (Resources.HasFlag(ResourcesBitfield.UNKNOWN))
                {
                    resourcesLength += 4;
                    resourcesLength += RefUnknowns.Count * Constants.SIZE_UINT16;
                }
                if (Resources.HasFlag(ResourcesBitfield.SOUNDS))
                {
                    resourcesLength += 4;
                    resourcesLength += RefSounds.Count * Constants.SIZE_UINT16;
                }
            }
            // Truly a bruh moment
            return 16 + Name.Length + 24 +
                UInt32Slots.Count * Constants.SIZE_UINT32 + OGISlots.Count * Constants.SIZE_UINT16 +
                AnimationSlots.Count * Constants.SIZE_UINT16 + ScriptSlots.Count * Constants.SIZE_UINT16 +
                ObjectSlots.Count * Constants.SIZE_UINT16 + SoundSlots.Count * Constants.SIZE_UINT16 +
                (HasInstanceProperties ? 20 + InstFlags.Count * Constants.SIZE_UINT32 + InstFloats.Count * 4 +
                InstIntegers.Count * Constants.SIZE_UINT32 : 0) + resourcesLength + ScriptPack.GetLength();
        }

        public void Read(BinaryReader reader, int length)
        {
            Bitfield = reader.ReadUInt32();
            for (var i = 0; i < 8; ++i)
            {
                SlotsMap[i] = reader.ReadByte();
            }
            var strLen = reader.ReadInt32();
            Name = new String(reader.ReadChars(strLen));

            FillResourceList(reader, UInt32Slots, true);
            FillResourceList(reader, OGISlots);
            FillResourceList(reader, AnimationSlots);
            FillResourceList(reader, ScriptSlots);
            FillResourceList(reader, ObjectSlots);
            FillResourceList(reader, SoundSlots);

            if (HasInstanceProperties)
            {
                InstancePropsHeader = reader.ReadUInt32();
                UnkUInt = reader.ReadUInt32();
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

            if (ReferencesResources)
            {
                Resources = (ResourcesBitfield)reader.ReadUInt32();
                if (Resources.HasFlag(ResourcesBitfield.OBJECTS))
                {
                    FillResourceList(reader, RefObjects);
                }
                if (Resources.HasFlag(ResourcesBitfield.OGIS))
                {
                    FillResourceList(reader, RefOGIs);
                }
                if (Resources.HasFlag(ResourcesBitfield.ANIMATIONS))
                {
                    FillResourceList(reader, RefAnimations);
                }
                if (Resources.HasFlag(ResourcesBitfield.CODE_MODELS))
                {
                    FillResourceList(reader, RefCodeModels);
                }
                if (Resources.HasFlag(ResourcesBitfield.SCRIPTS))
                {
                    FillResourceList(reader, RefScripts);
                }
                if (Resources.HasFlag(ResourcesBitfield.UNKNOWN))
                {
                    FillResourceList(reader, RefUnknowns);
                }
                if (Resources.HasFlag(ResourcesBitfield.SOUNDS))
                {
                    FillResourceList(reader, RefSounds);
                }
            }
            ScriptPack = new ScriptPack();
            ScriptPack.Read(reader, length);
        }

        public void SetID(uint id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Bitfield);
            writer.Write(SlotsMap);
            writer.Write(Name.Length);
            writer.Write(Name.ToCharArray(), 0, Name.Length);

            WriteResourceList(writer, UInt32Slots, true);
            WriteResourceList(writer, OGISlots);
            WriteResourceList(writer, AnimationSlots);
            WriteResourceList(writer, ScriptSlots);
            WriteResourceList(writer, ObjectSlots);
            WriteResourceList(writer, SoundSlots);

            if (HasInstanceProperties)
            {
                writer.Write(InstancePropsHeader);
                writer.Write(UnkUInt);
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
                writer.Write((UInt32)Resources);
                if (Resources.HasFlag(ResourcesBitfield.OBJECTS))
                {
                    WriteResourceList(writer, RefObjects);
                }
                if (Resources.HasFlag(ResourcesBitfield.OGIS))
                {
                    WriteResourceList(writer, RefOGIs);
                }
                if (Resources.HasFlag(ResourcesBitfield.ANIMATIONS))
                {
                    WriteResourceList(writer, RefAnimations);
                }
                if (Resources.HasFlag(ResourcesBitfield.CODE_MODELS))
                {
                    WriteResourceList(writer, RefCodeModels);
                }
                if (Resources.HasFlag(ResourcesBitfield.SCRIPTS))
                {
                    WriteResourceList(writer, RefScripts);
                }
                if (Resources.HasFlag(ResourcesBitfield.UNKNOWN))
                {
                    WriteResourceList(writer, RefUnknowns);
                }
                if (Resources.HasFlag(ResourcesBitfield.SOUNDS))
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
    }
}