﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class ScriptCommand : ITwinSerializable
    {
        public UInt32 Bitfield;
        public UInt16 CommandIndex;
        public List<UInt32> Arguments;

        internal Boolean hasNext;

        public UInt32 CommandSize
        {
            get
            {
                return GetCommandSize(CommandIndex);
            }
        }

        public ScriptCommand()
        {
            Arguments = new List<UInt32>();
        }

        public int GetLength()
        {
            return 4 + Arguments.Count * 4;
        }

        public void Read(BinaryReader reader, int length)
        {
            Bitfield = reader.ReadUInt32();
            CommandIndex = (UInt16)(Bitfield & 0xFFFF);
            Arguments.Clear();
            if (CommandSize - 0xC > 0)
            {
                var args = (CommandSize - 0xC) / 4;
                for (int i = 0; i < args; ++i)
                {
                    Arguments.Add(reader.ReadUInt32());
                }
            }
        }

        public void Read(BinaryReader reader, int length, IList<ScriptCommand> commands)
        {
            Read(reader, length);
            var hasNext = (Bitfield & 0x1000000) != 0;
            if (hasNext)
            {
                var com = new ScriptCommand();
                commands.Add(com);
                com.Read(reader, length, commands);
            }
        }

        public void Write(BinaryWriter writer)
        {
            UInt32 newBitfield = CommandIndex;
            if (hasNext)
            {
                newBitfield |= 0x1000000;
            }
            // In reality all Bitfields should be obsoleted and only used to construct
            // the member variables during Read only, but for unresearched stuff we need
            // to carry over the unknown bits to preserve consistency
            newBitfield |= (Bitfield & 0xFEFF0000);
            writer.Write(newBitfield);
            foreach (UInt32 arg in Arguments)
            {
                writer.Write(arg);
            }
        }

        public static UInt32 GetCommandSize(UInt16 index)
        {
            if (index < 0 || index >= commandSizeMap.Length)
            {
                return 0;
            }
            return commandSizeMap[index];
        }

        static UInt32[] commandSizeMap = {
                        0x00, 0x80, 0x0C, 0x20, 0x10, 0x0C, 0x00, 0x0C, 0x30, 0x24, 0x30, 0x48, 0x94, 0x0C, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x00, 0x00, 0x00, 0x10, 0x10, 0x00, 0x00, 0x10, 0x10, 0x20, 0x00, 0x10,
                        0x00, 0x10, 0x10, 0x0C, 0x0C, 0x00, 0x00, 0x0C, 0x14, 0x00, 0x10, 0x00, 0x50, 0x10, 0x00, 0x30, 0x30, 0x30, 0x0C, 0x20, 0x0C, 0x0C, 0x1C, 0x40, 0x14, 0x10, 0x00, 0x10, 0x60, 0x0C, 0x20, 0x0C,
                        0x30, 0x1C, 0x0C, 0x10, 0x14, 0x18, 0x00, 0x0C, 0x50, 0x00, 0x10, 0x10, 0x30, 0x0C, 0x14, 0x10, 0x50, 0x0C, 0x94, 0x94, 0x0C, 0x10, 0x28, 0x1C, 0x20, 0x10, 0x10, 0x10, 0x10, 0x10, 0x30, 0x10,
                        0xC0, 0x0C, 0x0C, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x20, 0x10, 0x00, 0x60, 0x20, 0x0C, 0x0C, 0x30, 0x1C, 0x0C, 0x0C, 0x0C, 0x14, 0x14, 0x0C, 0x0C, 0x14, 0x10, 0x0C, 0x10, 0x20, 0x0C, 0x10,
                        0x0C, 0x0C, 0x1C, 0x0C, 0x10, 0x0C, 0x0C, 0x0C, 0x14, 0x14, 0x14, 0x10, 0x10, 0x10, 0x10, 0x0C, 0x0C, 0x10, 0x10, 0x0C, 0x1C, 0x14, 0x18, 0x0C, 0x1C, 0x20, 0x10, 0x10, 0x10, 0x10, 0x98, 0x0C,
                        0x0C, 0x0C, 0x14, 0x10, 0x18, 0x40, 0x10, 0x10, 0x30, 0x14, 0x18, 0x14, 0x10, 0x10, 0x0C, 0x0C, 0x14, 0x30, 0x30, 0x30, 0x14, 0x0C, 0x0C, 0x10, 0x10, 0x14, 0x0C, 0x1C, 0x24, 0x20, 0x24, 0x10,
                        0x10, 0x30, 0x14, 0x0C, 0x0C, 0x30, 0x18, 0x20, 0x18, 0x18, 0x0C, 0x10, 0x2C, 0x14, 0x0C, 0x0C, 0x10, 0x10, 0x10, 0x0C, 0x0C, 0x0C, 0x10, 0x18, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x14, 0x10, 0x40, 0x10, 0x10, 0x0C, 0x14, 0x0C, 0x0C, 0x14, 0x0C, 0x3C, 0x18, 0x40, 0x2C, 0x10, 0x10, 0x10, 0x20, 0x0C, 0x10, 0x10, 0x14, 0x0C, 0x10, 0x10, 0x0C, 0x1C, 0x24, 0x80, 0x0C, 0x24,
                        0x30, 0x48, 0x40, 0x00, 0x30, 0x50, 0x10, 0x0C, 0x0C, 0x10, 0x0C, 0x18, 0x0C, 0x40, 0x10, 0x18, 0x0C, 0x10, 0x0C, 0x40, 0x40, 0x40, 0x0C, 0x0C, 0x00, 0x10, 0x10, 0x10, 0x00, 0x18, 0x54, 0x14,
                        0x10, 0x1C, 0x10, 0x10, 0x20, 0x10, 0x4C, 0x54, 0x0C, 0x10, 0x10, 0x10, 0x0C, 0x10, 0x10, 0x3C, 0x10, 0x10, 0x14, 0x18, 0x18, 0x10, 0x0C, 0x0C, 0x0C, 0x0C, 0x24, 0x28, 0x0C, 0x10, 0x0C, 0x0C,
                        0x0C, 0x0C, 0x0C, 0x0C, 0x0C, 0x0C, 0x10, 0x10, 0x10, 0x18, 0x0C, 0x10, 0x10, 0x0C, 0x10, 0x0C, 0x0C, 0x14, 0x0C, 0x0C, 0x14, 0x18, 0x10, 0x10, 0x10, 0x18, 0x14, 0x00, 0x10, 0x0C, 0x18, 0x10,
                        0x0C, 0x24, 0x24, 0x24, 0x24, 0x10, 0x00, 0x14, 0x10, 0x0C, 0x10, 0x10, 0x0C, 0x24, 0x0C, 0x28, 0x0C, 0x24, 0x28, 0x10, 0x10, 0x68, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, };

    }
}