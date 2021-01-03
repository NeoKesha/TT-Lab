using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics.SubModels;

namespace TT_Lab.Util.FBX
{
    public class FbxModel
    {
        public FbxHeader Header;
        public List<FbxNode> Nodes;
        public FbxModel()
        {
            Header = new FbxHeader();
            Nodes = new List<FbxNode>();
        }
        public FbxModel(List<List<Vertex>> Vertexes, List<List<IndexedFace>> Faces) : this()
        {
            
        }
        public void SaveBinary(BinaryWriter writer)
        {
            Header.SaveBinary(writer);
            foreach (var node in Nodes)
            {
                node.SaveBinary(writer);
            }
        }
        public void ReadBinary(BinaryReader reader)
        {
            Header.ReadBinary(reader);
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                FbxNode node = new FbxNode();
                node.parent = this;
                Nodes.Add(node);
                node.ReadBinary(reader);
                
                if (node.Properties.Count == 0 && node.Name == null)
                {
                    break;
                }
            }
        }
    }
}
