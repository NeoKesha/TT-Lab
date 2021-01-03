using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Util.FBX.FbxProperties;

namespace TT_Lab.Util.FBX
{
    public class FbxNode
    {
        
        public String Name { get; set; }
        public List<FbxProperty> Properties { get; set; }
        public List<FbxNode> Nodes { get; set; }
        public FbxModel parent = null;

        public FbxNode()
        {
            Properties = new List<FbxProperty>();
            Nodes = new List<FbxNode>();
        }
        public void SaveBinary(BinaryWriter writer)
        {
            if (Name != null)
            {
                writer.Write((UInt32)(writer.BaseStream.Position + GetLength()));
                writer.Write((UInt32)(Properties.Count));
                writer.Write((UInt32)(GetPropListLen()));
                writer.Write((Byte)((Name == null) ? 0 : Name.Length));
                writer.Write(Name.ToCharArray());
                foreach (var property in Properties)
                {
                    property.SaveBinary(writer);
                }
                foreach (var node in Nodes)
                {
                    node.SaveBinary(writer);
                }
            } 
            else
            {
                writer.Write((UInt32)(0));
                writer.Write((UInt32)(0));
                writer.Write((UInt32)(0));
                writer.Write((Byte)(0));
            }
        }
        public void ReadBinary(BinaryReader reader)
        {
            var endOffset = reader.ReadInt32();
            var propCnt = reader.ReadInt32();
            var propLen = reader.ReadInt32();
            var nameLen = reader.ReadByte();
            if (nameLen == 0)
            {
                return;
            }
            Name = new string(reader.ReadChars(nameLen));
            Properties.Clear();
            for (var i = 0; i < propCnt; ++i)
            {
                Properties.Add(FbxProperty.ReadProp(reader));
            }
            Nodes.Clear();
            while (reader.BaseStream.Position != endOffset)
            {
                FbxNode node = new FbxNode();
                node.parent = parent;
                Nodes.Add(node);
                node.ReadBinary(reader);      
            }
        }
        public UInt32 GetLength()
        {
            UInt32 len = (UInt32)(13 + ((Name == null) ? 0 : Name.Length)) + GetPropListLen() + GetNodeListLen();
            return len;
        }
        public UInt32 GetPropListLen()
        {
            UInt32 len = 0;
            foreach (var e in Properties)
            {
                len += e.GetLength();
            }
            return len;
        }
        public UInt32 GetNodeListLen()
        {
            UInt32 len = 0;
            foreach (var e in Nodes)
            {
                len += e.GetLength();
            }
            return len;
        }
        public override String ToString()
        {
            return (Name == null) ? "NULL_NODE" : Name; 
        }
    }
}
