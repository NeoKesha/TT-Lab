using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics
{
    public class PS2AnyMaterial : BaseTwinItem, ITwinMaterial
    {
        public UInt64 Header { get; set; }
        public UInt32 DmaChainIndex { get; set; }
        public String Name { get; set; }
        public List<TwinShader> Shaders { get; }
        public PS2AnyMaterial()
        {
            Shaders = new List<TwinShader>();
        }

        public override int GetLength()
        {
            Int32 shaderLength = 0;
            foreach (ITwinSerializable shader in Shaders)
            {
                shaderLength += shader.GetLength();
            }
            return 20 + Name.Length + shaderLength;
        }

        public override void Read(BinaryReader reader, int length)
        {
            Header = reader.ReadUInt64();
            DmaChainIndex = reader.ReadUInt32();
            Int32 nameLen = reader.ReadInt32();
            Name = new string(reader.ReadChars(nameLen));
            Int32 shaderCount = reader.ReadInt32();
            Shaders.Clear();
            for (int i = 0; i < shaderCount; ++i)
            {
                TwinShader shader = new TwinShader();
                shader.Read(reader, 0);
                Shaders.Add(shader);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Header);
            writer.Write(DmaChainIndex);
            writer.Write(Name.Length);
            writer.Write(Name.ToCharArray());
            writer.Write(Shaders.Count);
            foreach (ITwinSerializable shader in Shaders)
            {
                shader.Write(writer);
            }
        }

        public override String GetName()
        {
            return Name.Replace("\0", "");
        }
    }
}
