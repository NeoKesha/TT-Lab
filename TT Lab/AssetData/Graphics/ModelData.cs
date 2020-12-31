using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using TT_Lab.AssetData.Graphics.SubModels;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.AssetData.Graphics
{
    public class ModelData : AbstractAssetData
    {
        public ModelData()
        {
            Vertexes = new List<List<Vertex>>();
            Faces = new List<List<IndexedFace>>();
        }

        public ModelData(PS2AnyModel model) : this()
        {
            twinRef = model;
        }
        public List<List<Vertex>> Vertexes { get; set; }
        public List<List<IndexedFace>> Faces { get; set; }
        protected override void Dispose(Boolean disposing)
        {
            return;
        }
        /*public override void Save(string dataPath)
        {
            using (FileStream fs = new FileStream(dataPath, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                //Fuck everything
                var back = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
                //Fuck everything
                writer.WriteLine("ply");
                writer.WriteLine("format ascii 1.0");
                writer.WriteLine("comment made using TT Lab");
                writer.WriteLine($"element vertex {Vertexes.Count}");
                writer.WriteLine("property float x");
                writer.WriteLine("property float y");
                writer.WriteLine("property float z");
                writer.WriteLine("property float s");
                writer.WriteLine("property float t");
                writer.WriteLine("property float q");
                writer.WriteLine("property uchar red");
                writer.WriteLine("property uchar green");
                writer.WriteLine("property uchar blue");
                writer.WriteLine("property uchar alpha");
                writer.WriteLine("property float ered");
                writer.WriteLine("property float egreen");
                writer.WriteLine("property float eblue");
                writer.WriteLine("property float ealpha");
                writer.WriteLine($"element face {Faces.Count}");
                writer.WriteLine("property list uchar uint vertex_indices");
                writer.WriteLine("end_header");
                foreach (var vertex in Vertexes)
                {
                    writer.WriteLine(vertex.ToString());
                }
                foreach (var face in Faces)
                {
                    writer.WriteLine(face.ToString());
                }
                writer.Flush();
                //Fuck everything
                Thread.CurrentThread.CurrentCulture = back;
                //Fuck everything
            }
        }

        public override void Load(String dataPath)
        {
            Vertexes.Clear();
            Faces.Clear();
            using (FileStream fs = new FileStream(dataPath, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fs))
            {
                bool Header = true;
                Int32 Items = 0;
                String CurrentElement = "";
                Int32 ElementIndex = 0;
                List<KeyValuePair<String,Int32>> elements = new List<KeyValuePair<String, Int32>>();
                Dictionary<String, List<KeyValuePair<String,String>>> properties = new Dictionary<string, List<KeyValuePair<String, String>>>();
                while (!reader.EndOfStream)
                {
                    String line = reader.ReadLine().Trim().ToLower();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    String[] tokens = line.Split(' ');
                    if (Header)
                    {
                        switch (tokens[0])
                        {
                            case "end_header":
                                Header = false;
                                ElementIndex = 0;
                                Items = elements[0].Value;
                                CurrentElement = elements[0].Key;
                                break;
                            case "element":
                                CurrentElement = tokens[1];
                                properties.Add(CurrentElement, new List<KeyValuePair<string, string>>());
                                elements.Add(new KeyValuePair<string, int>(CurrentElement, Int32.Parse(tokens[2])));
                                break;
                            case "property":
                                if (tokens[1] != "list")
                                {
                                    properties[CurrentElement].Add(new KeyValuePair<string, string>(tokens[2], tokens[1]));
                                }
                                else
                                {
                                    properties[CurrentElement].Add(new KeyValuePair<string, string>(tokens[4], tokens[1]));
                                }
                                break;
                        }
                    } 
                    else
                    {
                        var props = properties[CurrentElement];
                        switch (CurrentElement)
                        {
                            case "vertex":
                                Vertex vert = new Vertex();
                                for (var i = 0; i < props.Count; ++i)
                                {
                                    var name = props[i].Key;
                                    var type = props[i].Value;
                                    var value = ConvertTypeFloat(tokens[i], type);
                                    switch (name)
                                    {
                                        case "x": vert.Position.X = value; break;
                                        case "y": vert.Position.Y = value; break;
                                        case "z": vert.Position.Z = value; break;
                                        case "s": vert.UV.X = value; break;
                                        case "t": vert.UV.Y = value; break;
                                        case "q": vert.UV.Z = value; break;
                                        case "red": vert.Color.X = value / 255.0f; break;
                                        case "green": vert.Color.Y = value / 255.0f; break;
                                        case "blue": vert.Color.Z = value / 255.0f; break;
                                        case "ered": vert.EmitColor.X = value; break;
                                        case "egreen": vert.EmitColor.Y = value; break;
                                        case "eblue": vert.EmitColor.Z = value; break;
                                    }
                                }
                                Vertexes.Add(vert);
                                break;
                            case "face":
                                for (var i = 0; i < props.Count; ++i)
                                {
                                    var name = props[i].Key;
                                    var type = props[i].Value;
                                    if (name == "vertex_indices" && type == "list")
                                    {
                                        var indexes = new int[Int32.Parse(tokens[0])];
                                        for (var j = 1; j < tokens.Length; ++j)
                                        {
                                            indexes[j - 1] = Int32.Parse(tokens[j]);
                                        }
                                        IndexedFace face = new IndexedFace(indexes);
                                        Faces.Add(face);
                                    }
                                }
                                break;
                        }
                        --Items;
                        if (Items <= 0)
                        {
                            ++ElementIndex;
                            if (ElementIndex >= elements.Count)
                            {
                                break;
                            }
                            Items = elements[ElementIndex].Value;
                            CurrentElement = elements[ElementIndex].Key;
                        }
                    }
                }
            }
        }*/
        private float ConvertTypeFloat(String value, String type) //idk i am not smart :((((
        {
            return Single.Parse(value, CultureInfo.InvariantCulture);
        }

        public override void Import()
        {
            PS2AnyModel model = (PS2AnyModel)twinRef;
            Vertexes = new List<List<Vertex>>();
            Faces = new List<List<IndexedFace>>();
            var refIndex = 0;
            var offset = 0;
            foreach (var e in model.SubModels)
            {
                var vertList = new List<Vertex>();
                var faceList = new List<IndexedFace>();
                refIndex = 0;
                e.CalculateData();
                for (var j = 0; j < e.Vertexes.Count; ++j)
                {
                    if (j < e.Vertexes.Count - 2)
                    {
                        if (e.Connection[j + 2])
                        {
                            if ((offset + j) % 2 == 0)
                            {
                                faceList.Add(new IndexedFace(new int[] { refIndex, refIndex + 1, refIndex + 2 }));
                            }
                            else
                            {
                                faceList.Add(new IndexedFace(new int[] { refIndex + 1, refIndex, refIndex + 2 }));
                            }
                        }
                        ++refIndex;
                    }
                    vertList.Add(new Vertex(e.Vertexes[j], e.Colors[j], e.UVW[j], e.EmitColor[j]));
                }
                offset += e.Vertexes.Count;
                refIndex += 2;
            }
        }
    }
}
