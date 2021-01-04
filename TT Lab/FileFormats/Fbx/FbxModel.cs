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
            FbxNode objectNode = Nodes[0];

            FbxNode geometryNode = new FbxNode("Geometry");
            geometryNode.Properties.Add(new FbxPropertyLong(0));
            geometryNode.Properties.Add(new FbxPropertyString($"{name}\0\u0001Geometry"));
            geometryNode.Properties.Add(new FbxPropertyString("Mesh"));

            FbxNode properties70 = new FbxNode("Properties70");
            geometryNode.Nodes.Add(properties70);

            FbxNode geometryVersion = new FbxNode("GeometryVersion", new FbxPropertyInt(124));
            geometryNode.Nodes.Add(geometryVersion);

            FbxNode vertices = new FbxNode("Vertices");
            var vertList = new List<double>(verts.Count * 3);
            foreach (var v in verts)
            {
                vertList.Add(v.Position.X);
                vertList.Add(v.Position.Y);
                vertList.Add(v.Position.Z);
            }
            vertices.Properties.Add(new FbxPropertyArrayDouble(vertList));
            geometryNode.Nodes.Add(vertices);

            FbxNode polygons = new FbxNode("PolygonVertexIndex");
            var polygonList = new List<int>(verts.Count * 3);
            foreach (var f in faces)
            {
                polygonList.AddRange(f.Indexes);
            }
            polygons.Properties.Add(new FbxPropertyArrayInt(polygonList));
            geometryNode.Nodes.Add(polygons);

            FbxNode elementUV = new FbxNode("LayerElementUV", new FbxPropertyInt(0));
            {
                FbxNode version = new FbxNode("Version", new FbxPropertyInt(101));
                elementUV.Nodes.Add(version);

                FbxNode nodeName = new FbxNode("Name", new FbxPropertyString("UVMap"));
                elementUV.Nodes.Add(nodeName);

                FbxNode mapping = new FbxNode("MappingInformationType", new FbxPropertyString("ByPolygonVertex"));
                elementUV.Nodes.Add(mapping);

                FbxNode reference = new FbxNode("ReferenceInformationType", new FbxPropertyString("Direct"));
                elementUV.Nodes.Add(reference);

                FbxNode uv = new FbxNode("UV");
                var uvList = new List<double>(verts.Count * 2);
                foreach (var v in verts)
                {
                    uvList.Add(v.UV.X);
                    uvList.Add(v.UV.Y);
                }
                uv.Properties.Add(new FbxPropertyArrayDouble(uvList));
                elementUV.Nodes.Add(uv);

                CreateNullNode(elementUV.Nodes);
            }
            geometryNode.Nodes.Add(elementUV);

            FbxNode elementColor = new FbxNode("LayerElementColor", new FbxPropertyInt(0));
            {
                FbxNode version = new FbxNode("Version", new FbxPropertyInt(101));
                elementColor.Nodes.Add(version);

                FbxNode nodeName = new FbxNode("Name", new FbxPropertyString("Col"));
                elementColor.Nodes.Add(nodeName);

                FbxNode mapping = new FbxNode("MappingInformationType", new FbxPropertyString("ByPolygonVertex"));
                elementColor.Nodes.Add(mapping);

                FbxNode reference = new FbxNode("ReferenceInformationType", new FbxPropertyString("Direct"));
                elementColor.Nodes.Add(reference);

                FbxNode colors = new FbxNode("Colors");
                var colorList = new List<double>(verts.Count * 4);
                foreach (var v in verts)
                {
                    colorList.Add(v.Color.X);
                    colorList.Add(v.Color.Y);
                    colorList.Add(v.Color.Z);
                    colorList.Add(v.Color.W);
                }
                colors.Properties.Add(new FbxPropertyArrayDouble(colorList));
                elementColor.Nodes.Add(colors);

                CreateNullNode(elementColor.Nodes);
            }
            geometryNode.Nodes.Add(elementColor);

            CreateNullNode(geometryNode.Nodes);

            objectNode.Nodes.Add(geometryNode);
        }

        public void FinalizeObjects()
        {
            FbxNode objectNode = Nodes[0];
            CreateNullNode(objectNode.Nodes);
        }
        public void CreateNullNode(List<FbxNode> root)
        {
            root.Add(new FbxNode());
        }
    }
}
