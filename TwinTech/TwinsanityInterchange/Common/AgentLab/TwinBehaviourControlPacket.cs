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
                var packet = (ControlPacketData)i;
                if (IsIntegerPacket(packet))
                {
                    Floats.Add(reader.ReadUInt32());
                }
                else
                {
                    Floats.Add(reader.ReadSingle());
                }
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
            for (var i = 0; i < Floats.Count; ++i)
            {
                var packet = (ControlPacketData)i;
                if (IsIntegerPacket(packet))
                {
                    writer.Write((UInt32)Floats[i]);
                }
                else
                {
                    writer.Write(Floats[i]);
                }
            }
            foreach (var b in Bytes)
            {
                writer.Write(b);
            }
        }
        public void WriteText(StreamWriter writer, Int32 tabs = 0)
        {
            StringUtils.WriteLineTabulated(writer, $"ControlPacket {"{"}", tabs);
            StringUtils.WriteLineTabulated(writer, "settings = {", tabs + 1);
            {
                StringUtils.WriteLineTabulated(writer, $"SpaceType = {Space}", tabs + 2);
                StringUtils.WriteLineTabulated(writer, $"MotionType = {Motion}", tabs + 2);
                StringUtils.WriteLineTabulated(writer, $"AccelerationFunction = {AccelerationFunction}", tabs + 2);
                StringUtils.WriteLineTabulated(writer, $"DoesTranslate = {Translates}", tabs + 2);
                StringUtils.WriteLineTabulated(writer, $"DoesRotate = {Rotates}", tabs + 2);
                StringUtils.WriteLineTabulated(writer, $"DoesTranslationContinue = {TranslationContinues}", tabs + 2);
                StringUtils.WriteLineTabulated(writer, $"DoesInterpolateAngles = {InterpolatesAngles}", tabs + 2);
                StringUtils.WriteLineTabulated(writer, $"DoesYawFaces = {YawFaces}", tabs + 2);
                StringUtils.WriteLineTabulated(writer, $"DoesPitchFaces = {PitchFaces}", tabs + 2);
                StringUtils.WriteLineTabulated(writer, $"DoesOrientPredicts = {OrientsPredicts}", tabs + 2);
                StringUtils.WriteLineTabulated(writer, $"KeyIsLocal = {KeyIsLocal}", tabs + 2);
                StringUtils.WriteLineTabulated(writer, $"UsesRotator = {UsesRotator}", tabs + 2);
                StringUtils.WriteLineTabulated(writer, $"UsesInterpolator = {UsesInterpolator}", tabs + 2);
                StringUtils.WriteLineTabulated(writer, $"UsesPhysics = {UsesPhysics}", tabs + 2);
                StringUtils.WriteLineTabulated(writer, $"ContinuouslyRotatesInWorldSpace = {ContinuouslyRotatesInWorldSpace}", tabs + 2);
                StringUtils.WriteLineTabulated(writer, $"Axes = {Axes}", tabs + 2);
                StringUtils.WriteLineTabulated(writer, $"DoesStall = {Stalls}", tabs + 2);
            }
            StringUtils.WriteLineTabulated(writer, "}", tabs + 1);
            StringUtils.WriteLineTabulated(writer, "data = {", tabs + 1);
            {
                for (var i = 0; i < Bytes.Count; ++i)
                {
                    if (Bytes[i] >= 0x80)
                    {
                        continue;
                    }
                    var packet = (ControlPacketData)i;
                    if (IsIntegerPacket(packet))
                    {
                        StringUtils.WriteLineTabulated(writer, $"{(ControlPacketData)i} = {(UInt32)Floats[Bytes[i]]}", tabs + 2);
                    }
                    else
                    {
                        StringUtils.WriteLineTabulated(writer, $"{(ControlPacketData)i} = {Floats[Bytes[i]]}", tabs + 2);
                    }
                }
            }
            StringUtils.WriteLineTabulated(writer, "}", tabs + 1);
            StringUtils.WriteLineTabulated(writer, "}", tabs);
        }

        public void ReadText(StreamReader reader)
        {
            String line = "";
            Bytes.Clear();
            Floats.Clear();

            // Read settings
            while (!line.EndsWith("}"))
            {
                line = reader.ReadLine().Trim();
                if (String.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                var valueString = StringUtils.GetStringAfter(line, "=").Trim();
                if (line.StartsWith("SpaceType"))
                {
                    Space = Enum.Parse<SpaceType>(valueString);
                }
                else if (line.StartsWith("MotionType"))
                {
                    Motion = Enum.Parse<MotionType>(valueString);
                }
                else if (line.StartsWith("AccelerationFunction"))
                {
                    AccelerationFunction = Enum.Parse<AccelFunction>(valueString);
                }
                else if (line.StartsWith("DoesTranslate"))
                {
                    Translates = Boolean.Parse(valueString);
                }
                else if (line.StartsWith("DoesRotate"))
                {
                    Rotates = Boolean.Parse(valueString);
                }
                else if (line.StartsWith("DoesTranslationContinue"))
                {
                    TranslationContinues = Boolean.Parse(valueString);
                }
                else if (line.StartsWith("DoesInterpolateAngles"))
                {
                    InterpolatesAngles = Boolean.Parse(valueString);
                }
                else if (line.StartsWith("DoesYawFaces"))
                {
                    YawFaces = Boolean.Parse(valueString);
                }
                else if (line.StartsWith("DoesPitchFaces"))
                {
                    PitchFaces = Boolean.Parse(valueString);
                }
                else if (line.StartsWith("DoesOrientPredicts"))
                {
                    OrientsPredicts = Boolean.Parse(valueString);
                }
                else if (line.StartsWith("KeyIsLocal"))
                {
                    KeyIsLocal = Boolean.Parse(valueString);
                }
                else if (line.StartsWith("UsesRotator"))
                {
                    UsesRotator = Boolean.Parse(valueString);
                }
                else if (line.StartsWith("UsesInterpolator"))
                {
                    UsesInterpolator = Boolean.Parse(valueString);
                }
                else if (line.StartsWith("UsesPhysics"))
                {
                    UsesPhysics = Boolean.Parse(valueString);
                }
                else if (line.StartsWith("ContinuouslyRotatesInWorldSpace"))
                {
                    ContinuouslyRotatesInWorldSpace = Boolean.Parse(valueString);
                }
                else if (line.StartsWith("Axes"))
                {
                    Axes = Enum.Parse<NaturalAxes>(valueString);
                }
                else if (line.StartsWith("DoesStall"))
                {
                    Stalls = Boolean.Parse(valueString);
                }
            }

            // Read bytes and floats
            for (var i = 0; i < PacketDataLength; i++)
            {
                Bytes.Add(0xFF);
                Floats.Add(0.0f);
            }
            var maxPacketIndex = -1;
            Byte floatIdx = 0;
            while (!line.EndsWith("}"))
            {
                line = reader.ReadLine().Trim();
                if (String.IsNullOrWhiteSpace(line) || line.StartsWith("data"))
                {
                    continue;
                }
                var packetName = StringUtils.GetStringBefore(line, "=").Trim();
                var valueString = StringUtils.GetStringAfter(line, "=").Trim();
                var packet = Enum.Parse<ControlPacketData>(packetName);
                if ((Int32)packet > maxPacketIndex)
                {
                    maxPacketIndex = (Int32)packet;
                }

                Bytes[(Int32)packet] = floatIdx;
                if (IsIntegerPacket(packet))
                {
                    var value = UInt32.Parse(valueString, CultureInfo.InvariantCulture);
                    Floats[floatIdx++] = value;
                }
                else
                {
                    var value = Single.Parse(valueString, CultureInfo.InvariantCulture);
                    Floats[floatIdx++] = value;
                }
            }
            Bytes.RemoveRange(maxPacketIndex, PacketDataLength - maxPacketIndex);
            Floats.RemoveRange(floatIdx - 1, PacketDataLength - (floatIdx - 1));
        }

        public static bool IsIntegerPacket(ControlPacketData packet)
        {
            return packet == ControlPacketData.SELECTOR || packet == ControlPacketData.KEY_INDEX || packet == ControlPacketData.SYNC_UNIT || packet == ControlPacketData.JOINT_INDEX;
        }

        public const Int32 PacketDataLength = 23;
        public enum ControlPacketData
        {
            SELECTOR,
            SYNC_INDEX = SELECTOR,
            KEY_INDEX,
            FOCUS_DATA = KEY_INDEX,
            MOVE_SPEED,
            RISE_HEIGHT = MOVE_SPEED,
            TURN_SPEED,
            RAWPOS_X,
            RAWPOS_Y,
            RAWPOS_Z,
            PITCH,
            RAWANGS_X = PITCH,
            YAW,
            RAWANGS_Y = YAW,
            ROLL,
            RAWANGS_Z = ROLL,
            DELAY,
            DURATION,
            CURVY = DURATION,
            HOMEPOWER = DURATION,
            TUMBLE_DATA,
            SPIN_DATA,
            TWIST_DATA,
            RANDRANGE,
            SQR_TOLERANCE = RANDRANGE,
            POWER,
            GRAVITY = POWER,
            BANKING = POWER,
            DAMPING,
            SPEEDLIM = DAMPING,
            BRAKING = DAMPING,
            AC_DIST,
            RT_OPT = AC_DIST,
            SHIFT_FREQ = AC_DIST,
            DEC_DIST,
            PHYS_OPT = DEC_DIST,
            SHIFT = DEC_DIST,
            BOUNCE,
            BANK_LIMIT = BOUNCE,
            SYNC_UNIT,
            JOINT_INDEX
        }

        public enum SpaceType
        {
            WORLD_SPACE = 0,
            INITIAL_SPACE,
            CURRENT_SPACE,
            TARGET_SPACE,
            PARENT_SPACE,
            CHASE_SPACE = PARENT_SPACE,
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
