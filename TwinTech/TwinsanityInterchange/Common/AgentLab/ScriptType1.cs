using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class ScriptType1 : ITwinSerializable
    {
        public List<Byte> Bytes;
        public List<Single> Floats;
        public UInt16 UnkUShort;
        public Int32 UnkInt;

        public ScriptType1()
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
            UnkUShort = reader.ReadUInt16();
            UnkInt = reader.ReadInt32();
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
            writer.Write(UnkUShort);
            writer.Write(UnkInt);
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
    }
}
