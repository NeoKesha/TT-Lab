using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics.SubModels;
using TT_Lab.FileFormats.Fbx.FbxProperties;
using TT_Lab.Util;

namespace TT_Lab.FileFormats.Fbx
{
    public class FbxModel
    {
        private static Dictionary<string,object> FbxTemplate = JsonConvert.DeserializeObject<Dictionary<string, object>>(ManifestResourceLoader.LoadTextFile(@"FileFormats\Fbx\FbxTemplate.json"));
        public FbxHeader Header;
        public List<FbxNode> Nodes;
        public FbxModel()
        {
            Header = new FbxHeader();
            Nodes = new List<FbxNode>();
        }
        public void Init()
        {
            ApplyTemplate(FbxTemplate);
        }
        private void ApplyTemplate(Dictionary<string, object> template)
        {
            foreach (var key in template.Keys)
            {
                var index = key.IndexOf("_");
                var keyName = key;
                if (index != -1)
                {
                    keyName = keyName.Substring(0, index);
                }
                var keyValue = ((JObject)template[key]).ToObject<Dictionary<string, object>>();
                FbxNode node = new FbxNode(keyName);
                Nodes.Add(node);
                ApplyTemplate(keyValue, node);
            }
            if (Nodes.Any())
            {
                CreateNullNode(Nodes);
            }
        }
        private void ApplyTemplate(Dictionary<string, object> template, FbxNode root)
        {
            foreach (var key in template.Keys)
            {
                var index = key.IndexOf("_");
                var keyName = key;
                if (index != -1)
                {
                    keyName = keyName.Substring(0, index);
                }
                var keyValue = template[key];
                switch (keyName)
                {
                    case "string":
                        root.Properties.Add(new FbxPropertyString((String)keyValue));
                        break;
                    case "int":
                        root.Properties.Add(new FbxPropertyInt(Int32.Parse((String)keyValue)));
                        break;
                    case "long":
                        root.Properties.Add(new FbxPropertyLong(Int64.Parse((String)keyValue)));
                        break;
                    case "double":
                        root.Properties.Add(new FbxPropertyDouble(Double.Parse((String)keyValue, System.Globalization.CultureInfo.InvariantCulture)));
                        break;
                    default:
                        {
                            FbxNode node = new FbxNode(keyName);
                            root.Nodes.Add(node);
                            ApplyTemplate(((JObject)template[key]).ToObject<Dictionary<string, object>>(), node);
                        }
                        break;
                }
            }
            if (root.Nodes.Any())
            {
                CreateNullNode(root.Nodes);
            }
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
        public void AddGeometry(String name, List<Vertex> verts, List<IndexedFace> faces)
        {
            // In node Objects [8] create sub node Geometry
            // Properties UnkLong, String = Name + \0 + \u0001 + Geometry, String = Mesh
            // Nodes: 
            // Empty Properties70
            // GeometryVersion: Int = 124
            // Vertices : Array Double (3 * 8)
            // Polygon Vertex Index: Array Int (24)
            // Edges: Array Int
            // LayerElementNormal: 0
                //Version: 101
                //Name: String = ""
                //MappingInformationType: ByPolygonVertex
                //ReferenceInformationType: Direct
                //Normals: Array Double (24 * 3)
            // LayerElementUV: 0
                //Version: 101
                //Name: UVMap
                //MappingInformationType: ByPolygonVertex
                //ReferenceInformationType: Direct 
                //UV: 2 * verts
            // LayerElementColor: 0
                //Version: 101
                //Name: Col
                //MappingInformationType: ByPolygonVertex
                //ReferenceInformationType: Direct 
                //Colors: 4 * verts
            // Layer: Int 0
                // Version: 100
                // LayerElement: 
                    // Type: LayerElementNormal
                    // TypedIndex: 0 
                // LayerElement: 
                    // Type: LayerElementColor
                    // TypedIndex: 0 
                // LayerElement: 
                    // Type: LayerElementUV
                    // TypedIndex: 0 
        }
        public void CreateNullNode(List<FbxNode> root)
        {
            root.Add(new FbxNode());
        }
    }
}
