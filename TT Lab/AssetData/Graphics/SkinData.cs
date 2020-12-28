using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TT_Lab.AssetData.Graphics.SubModels;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.AssetData.Graphics
{
    public class SkinData : AbstractAssetData
    {
        public SkinData()
        {
        }

        public SkinData(PS2AnySkin skin) : this()
        {
            Vertexes = new List<Vertex>();
            Faces = new List<IndexedFace>();
            foreach (var e in skin.SubSkins)
            {
                e.CalculateData();
                var refIndex = 0;
                var offset = 0;
                for (var j = 0; j < e.Vertexes.Count; ++j)
                {
                    if (j < e.Vertexes.Count - 2)
                    {
                        if (e.Connection[j + 2])
                        {
                            if ((offset + j) % 2 == 0)
                            {
                                Faces.Add(new IndexedFace(new int[] { refIndex, refIndex + 1, refIndex + 2 }));
                            }
                            else
                            {
                                Faces.Add(new IndexedFace(new int[] { refIndex + 1, refIndex, refIndex + 2 }));
                            }
                        }
                        ++refIndex;
                    }
                    offset += e.Vertexes.Count;
                    refIndex += 2;
                    Vertexes.Add(new Vertex(e.Vertexes[j], e.Colors[j], e.UVW[j], e.EmitColor[j]));
                }
            }
        }
        List<Vertex> Vertexes { get; set; }
        List<IndexedFace> Faces { get; set; }
        protected override void Dispose(Boolean disposing)
        {
            return;
        }
        public override void Save(string dataPath)
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
    }
}
