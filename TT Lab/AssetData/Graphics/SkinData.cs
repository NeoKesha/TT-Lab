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
            SubSkins = new List<SubSkinData>();
            foreach (var e in skin.SubSkins)
            {
                e.CalculateData();
                SubSkins.Add(new SubSkinData(e));
            }
        }
        [JsonProperty(Required = Required.Always)]
        List<SubSkinData> SubSkins { get; set; }
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
                int vertexes = 0;
                int faces = 0;
                for (var i = 0; i < SubSkins.Count; ++i)
                {
                    for (var j = 0; j < SubSkins[i].Vertexes.Count - 2; ++j)
                    {
                        var conn = (SubSkins[i].UVW[j + 2].GetBinaryW() & 0xFF00) >> 8;
                        if (conn != 128)
                        {
                            ++faces;
                        }
                    }
                    vertexes += SubSkins[i].Vertexes.Count;
                }
                writer.WriteLine($"element vertex {vertexes}");
                writer.WriteLine("property float x");
                writer.WriteLine("property float y");
                writer.WriteLine("property float z");
                writer.WriteLine("property float nx");
                writer.WriteLine("property float ny");
                writer.WriteLine("property float nz");
                writer.WriteLine("property float s");
                writer.WriteLine("property float t");
                writer.WriteLine("property uchar red");
                writer.WriteLine("property uchar green");
                writer.WriteLine("property uchar blue");
                writer.WriteLine("property uchar alpha");
                writer.WriteLine($"element face {faces}");
                writer.WriteLine("property list uchar uint vertex_indices");
                foreach (var subModel in SubSkins)
                {
                    writer.WriteLine($"comment subModel verts: {subModel.Vertexes.Count}");
                }
                writer.WriteLine("end_header");
                foreach (var subModel in SubSkins)
                {
                    for (var i = 0; i < subModel.Vertexes.Count; ++i)
                    {
                        var pos = subModel.Vertexes[i];
                        var n = subModel.Normals[i];
                        var uvw = subModel.UVW[i];
                        var color = subModel.Colors[i];
                        var r = (Byte)Math.Round(color.X * 255.0f);
                        var g = (Byte)Math.Round(color.Y * 255.0f);
                        var b = (Byte)Math.Round(color.Z * 255.0f);
                        var a = (Byte)Math.Round(color.W * 255.0f);
                        writer.WriteLine($"{pos.X} {pos.Y} {pos.Z} {n.X} {n.Y} {n.Z} {uvw.X} {uvw.Y} {r} {g} {b} {a}");
                    }
                }
                var refIndex = 0;
                var offset = 0;
                for (var i = 0; i < SubSkins.Count; ++i)
                {
                    for (var j = 0; j < SubSkins[i].Vertexes.Count - 2; ++j)
                    {
                        var conn = (SubSkins[i].UVW[j + 2].GetBinaryW() & 0xFF00) >> 8;
                        if (conn != 128)
                        {
                            if ((offset + j) % 2 == 0)
                            {
                                writer.WriteLine($"3 {refIndex} {refIndex + 1} {refIndex + 2}");
                            }
                            else
                            {
                                writer.WriteLine($"3 {refIndex + 1} {refIndex} {refIndex + 2}");
                            }
                        }
                        ++refIndex;
                    }
                    offset += SubSkins[i].Vertexes.Count;
                    refIndex += 2;
                }
                writer.Flush();
                //Fuck everything
                Thread.CurrentThread.CurrentCulture = back;
                //Fuck everything
            }
        }
    }
}
