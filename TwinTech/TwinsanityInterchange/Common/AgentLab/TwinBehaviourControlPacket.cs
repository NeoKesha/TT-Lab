using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class TwinBehaviourControlPacket : ITwinSerializable
    {
        public List<Byte> Bytes { get; }
        public List<Single> Floats { get; }
        public SpaceType Space { get; set; }
        public MotionType Motion { get; set; }
        public ContinuousRotate ContRotate { get; set; }
        public AccelFunction AccelerationFunction { get; set; }
        public Boolean Translates { get; set; }
        public Boolean Rotates { get; set; }
        public Boolean TranslationContinues { get; set; }
        public Boolean TracksDestination { get; set; }
        public Boolean InterpolatesAngles { get; set; }
        public Boolean YawFaces { get; set; }
        public Boolean PitchFaces { get; set; }
        public Boolean OrientsPredicts { get; set; }
        public Boolean KeyIsLocal { get; set; }
        public Boolean UsesRotator { get; set; }
        public Boolean UsesInterpolator { get; set; }
        public Boolean UsesPhysics { get; set; }
        public Boolean ContinuouslyRotatesInWorldSpace { get; set; }
        public NaturalAxes Axes { get; set; }
        public Boolean Stalls { get; set; }


        public TwinBehaviourControlPacket()
        {
            Bytes = new List<Byte>();
            Floats = new List<Single>();
        }

        public int GetLength()
        {
            return 8 + Bytes.Count + Floats.Count * 4;
        }

        public void Read(BinaryReader reader, int length)
        {
            Byte bytesCnt = reader.ReadByte();
            Byte floatsCnt = reader.ReadByte();
            reader.ReadUInt16(); // Version should always be 0x6
            var packetSettings = reader.ReadInt32();
            {
                Space = (SpaceType)(packetSettings & 0x7);
                Motion = (MotionType)(packetSettings >> 0x3 & 0xF);
                ContRotate = (ContinuousRotate)(packetSettings >> 0x7 & 0xF);
                AccelerationFunction = (AccelFunction)(packetSettings >> 0xB & 0x3);
                Translates = (packetSettings >> 0xD & 0x1) == 1;
                Rotates = (packetSettings >> 0xE & 0x1) == 1;
                TranslationContinues = (packetSettings >> 0xF & 0x1) == 1;
                TracksDestination = (packetSettings >> 0x10 & 0x1) == 1;
                InterpolatesAngles = (packetSettings >> 0x11 & 0x1) == 1;
                YawFaces = (packetSettings >> 0x12 & 0x1) == 1;
                PitchFaces = (packetSettings >> 0x13 & 0x1) == 1;
                OrientsPredicts = (packetSettings >> 0x14 & 0x1) == 1;
                Debug.Assert((packetSettings >> 0x15 & 0x1) == 1, "Behaviour control packet data is invalid!");
                KeyIsLocal = (packetSettings >> 0x16 & 0x1) == 1;
                UsesRotator = (packetSettings >> 0x17 & 0x1) == 1;
                UsesInterpolator = (packetSettings >> 0x18 & 0x1) == 1;
                UsesPhysics = (packetSettings >> 0x19 & 0x1) == 1;
                ContinuouslyRotatesInWorldSpace = (packetSettings >> 0x1A & 0x1) == 1;
                Axes = (NaturalAxes)(packetSettings >> 0x1B & 0x7);
                Stalls = (packetSettings >> 0x1F & 0x1) == 1;
            }

            Floats.Clear();
            for (var i = 0; i < floatsCnt; ++i)
            {
                Floats.Add(reader.ReadSingle());
            }
            Bytes.Clear();
            for (var i = 0; i < bytesCnt; ++i)
            {
                Bytes.Add(reader.ReadByte());
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((Byte)Bytes.Count);
            writer.Write((Byte)Floats.Count);
            writer.Write((UInt16)0x6);
            UInt32 newPacketSettings = 0x200000 | (UInt32)Space; // Set HasValidData to true
            {
                static UInt32 BoolToUInt32(Boolean b) => b ? 1U : 0U;

                newPacketSettings |= (UInt32)Motion << 0x3;
                newPacketSettings |= (UInt32)ContRotate << 0x7;
                newPacketSettings |= (UInt32)AccelerationFunction << 0xB;
                newPacketSettings |= BoolToUInt32(Translates) << 0xD;
                newPacketSettings |= BoolToUInt32(Rotates) << 0xE;
                newPacketSettings |= BoolToUInt32(TranslationContinues) << 0xF;
                newPacketSettings |= BoolToUInt32(TracksDestination) << 0x10;
                newPacketSettings |= BoolToUInt32(InterpolatesAngles) << 0x11;
                newPacketSettings |= BoolToUInt32(YawFaces) << 0x12;
                newPacketSettings |= BoolToUInt32(PitchFaces) << 0x13;
                newPacketSettings |= BoolToUInt32(OrientsPredicts) << 0x14;
                newPacketSettings |= BoolToUInt32(KeyIsLocal) << 0x16;
                newPacketSettings |= BoolToUInt32(UsesRotator) << 0x17;
                newPacketSettings |= BoolToUInt32(UsesInterpolator) << 0x18;
                newPacketSettings |= BoolToUInt32(UsesPhysics) << 0x19;
                newPacketSettings |= BoolToUInt32(ContinuouslyRotatesInWorldSpace) << 0x1A;
                newPacketSettings |= (UInt32)Axes << 0x1B;
                newPacketSettings |= BoolToUInt32(Stalls) << 0x1F;
            }
            writer.Write(newPacketSettings);
            foreach (var f in Floats)
            {
                writer.Write(f);
            }
            foreach (var b in Bytes)
            {
                writer.Write(b);
            }
        }
        public void WriteText(StreamWriter writer, Int32 tabs = 0)
        {
            StringUtils.WriteLineTabulated(writer, $"Head {"{"}", tabs);
            StringUtils.WriteTabulated(writer, "bytes = [", tabs + 1);
            for (var i = 0; i < Bytes.Count; ++i)
            {
                writer.Write($"{Bytes[i]}");
                if (i < Bytes.Count - 1)
                {
                    writer.Write(", ");
                }
            }
            writer.WriteLine($"]");
            StringUtils.WriteTabulated(writer, "floats = [", tabs + 1);
            for (var i = 0; i < Floats.Count; ++i)
            {
                writer.Write($"{Floats[i].ToString(CultureInfo.InvariantCulture)}");
                if (i < Floats.Count - 1)
                {
                    writer.Write(", ");
                }
            }
            writer.WriteLine($"]");
            StringUtils.WriteLineTabulated(writer, "}", tabs);
        }

        public void ReadText(StreamReader reader)
        {
            String line = "";
            Bytes.Clear();
            Floats.Clear();
            while (!line.EndsWith("}"))
            {
                line = reader.ReadLine().Trim();
                if (String.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                if (line.StartsWith("bytes"))
                {
                    String[] str_bytes = StringUtils.GetStringInBetween(line, "[", "]").Split(',');
                    foreach (var str in str_bytes)
                    {
                        Bytes.Add(Byte.Parse(str));
                    }
                }
                if (line.StartsWith("floats"))
                {
                    String[] str_floats = StringUtils.GetStringInBetween(line, "[", "]").Split(',');
                    foreach (var str in str_floats)
                    {
                        Floats.Add(Single.Parse(str, CultureInfo.InvariantCulture));
                    }
                }
            }
        }

        public enum SpaceType
        {
            WORLD_SPACE = 0,
            INITIAL_SPACE,
            CURRENT_SPACE,
            TARGET_SPACE,
            PARENT_SPACE, // or CHASE_SPACE
            INITIAL_POS,
            CURRENT_POS,
            STORED_SPACE,
        }
        public enum MotionType
        {
            NO_MOTION = 0,
            CONSTANT_VEL,
            ACCELERATED,
            SPRING,
            PROJECTILE,
            LINEAR_INTERP,
            SMOOTH_PATH,
            FACE_DEST_ONLY,
            DRIVE,
            GROUND_CHASE,
            AIR_CHASE,
        }
        public enum ContinuousRotate
        {
            NO_CONT_ROTATION = 0,
            NUM_FULL_ROTS,
            RADS_PER_SECOND, // Or degrees?
            NATURAL_ROLL,
        }
        public enum NaturalAxes
        {
            NO_NATURAL = 0,
            X_NATURAL,
            Y_NATURAL,
            Z_NATURAL,
            ALL_NATURAL,
        }
        public enum AccelFunction
        {
            NO_ACCEL = 0,
            CONSTANT_ACCEL,
            SMOOTH_CURVE,
        }
    }
}
