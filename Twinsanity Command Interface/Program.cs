using System;
using System.Collections.Generic;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Archives;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.Graphics;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity_Command_Interface
{
    class Program
    {
        private class Vertex
        {
            public Vertex()
            {
                Position = new Vector3();
                Color = new Vector4();
                UV = new Vector3();
                Normal = new Vector4();
                EmitColor = new Vector4();
            }
            public Vertex(Vector4 pos) : this()
            {
                Position.X = pos.X;
                Position.Y = pos.Y;
                Position.Z = pos.Z;
            }
            public Vertex(Vector4 pos, Vector4 color) : this(pos)
            {
                Color.X = color.X;
                Color.Y = color.Y;
                Color.Z = color.Z;
                Color.W = color.W;
            }
            public Vertex(Vector4 pos, Vector4 color, Vector4 uv) : this(pos, color)
            {
                UV.X = uv.X;
                UV.Y = uv.Y;
                UV.Z = uv.Z;
            }
            public Vertex(Vector4 pos, Vector4 color, Vector4 uv, Vector4 emitColor) : this(pos, color, uv)
            {
                EmitColor.X = emitColor.X;
                EmitColor.Y = emitColor.Y;
                EmitColor.Z = emitColor.Z;
                EmitColor.W = emitColor.W;
            }
            public Vector3 Position { get; set; }
            public Vector4 Color { get; set; }
            public Vector4 Normal { get; set; }
            public Vector4 EmitColor { get; set; }
            public Vector3 UV { get; set; }

            public override String ToString()
            {
                var r = (Byte)Math.Round(Color.X * 255.0f);
                var g = (Byte)Math.Round(Color.Y * 255.0f);
                var b = (Byte)Math.Round(Color.Z * 255.0f);
                var a = (Byte)Math.Round(Color.W * 255.0f);
                return $"{Position.X} {Position.Y} {Position.Z} {UV.X} {UV.Y} {UV.Z} {r} {g} {b} {a} {EmitColor.X} {EmitColor.Y} {EmitColor.Z} {EmitColor.W}";
            }
        }

        static void Main(string[] args)
        {
            String readArchives = args[0];
            String testPath = args[1];
            String testSavePath = args[2];
            if (readArchives == "Y")
            {
                testPath += @"\Crash6\";
                String[] archivePaths = System.IO.Directory.GetFiles(testPath, "*.BD", System.IO.SearchOption.TopDirectoryOnly);
                archivePaths = archivePaths.Concat(System.IO.Directory.GetFiles(testPath, "*.MB", System.IO.SearchOption.TopDirectoryOnly)).ToArray();
                List<ITwinSerializable> archives = new List<ITwinSerializable>();
                foreach (var path in archivePaths)
                {
                    var headerPath = String.Empty;
                    ITwinSerializable archive = null;
                    if (path.EndsWith("MB") || path.EndsWith("mb"))
                    {
                        headerPath = path.Replace(".MB", ".MH");
                        if (headerPath == path)
                        {
                            headerPath = path.Replace(".mb", ".mh");
                        }
                        archive = new PS2MB(headerPath, System.IO.Path.Combine(testSavePath, System.IO.Path.GetFileName(headerPath)));
                    }
                    else if (path.EndsWith("BD"))
                    {
                        headerPath = path.Replace(".BD", ".BH");
                        archive = new PS2BD(headerPath, System.IO.Path.Combine(testSavePath, System.IO.Path.GetFileName(headerPath)));
                    }
                    using (System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    using (System.IO.BinaryReader reader = new System.IO.BinaryReader(stream))
                    {
                        archive.Read(reader, (Int32)reader.BaseStream.Length);
                        archives.Add(archive);
                    }
                    using (System.IO.FileStream stream = new System.IO.FileStream(System.IO.Path.Combine(testSavePath, System.IO.Path.GetFileName(path)),
                        System.IO.FileMode.Create, System.IO.FileAccess.Write))
                    using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream))
                    {
                        archive.Write(writer);
                    }
                }
                Console.WriteLine("Done");
            }
            else
            {
                String[] levelsPaths = System.IO.Directory.GetFiles(testPath, "*.rm2", System.IO.SearchOption.AllDirectories);
                levelsPaths = levelsPaths.Concat(System.IO.Directory.GetFiles(testPath, "*.sm2", System.IO.SearchOption.AllDirectories)).ToArray();
                List<ITwinSection> levels = new List<ITwinSection>();
                foreach (String levelPath in levelsPaths)
                {
                    ITwinSection twinLevel = null;
                    if (levelPath.EndsWith("Default.rm2"))
                    {
                        twinLevel = new PS2Default();
                    }
                    else if (levelPath.EndsWith("rm2"))
                    {
                        twinLevel = new PS2AnyTwinsanityRM2();
                    }
                    else if (levelPath.EndsWith("sm2"))
                    {
                        twinLevel = new PS2AnyTwinsanitySM2();
                    }
                    else
                    {
                        twinLevel = new BaseTwinSection();
                    }
                    using (System.IO.FileStream stream = new System.IO.FileStream(levelPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    using (System.IO.BinaryReader reader = new System.IO.BinaryReader(stream))
                    {
                        twinLevel.Read(reader, (Int32)reader.BaseStream.Length);
                        levels.Add(twinLevel);
                    }
                    if (twinLevel is PS2AnyTwinsanityRM2)
                    {
                        if (!System.IO.Directory.Exists(System.IO.Path.Combine(testSavePath, "skins")))
                        {
                            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(testSavePath, "skins"));
                        }
                        var graph = twinLevel.GetItem<PS2AnyGraphicsSection>(Constants.LEVEL_GRAPHICS_SECTION).GetItem<PS2AnySkinsSection>(Constants.GRAPHICS_SKINS_SECTION);
                        for (var i = 0; i < graph.GetItemsAmount(); ++i)
                        {
                            var skin = graph.GetItem(i) as PS2AnySkin;
                            var Vertexes = new List<List<Vertex>>();
                            var Faces = new List<List<int[]>>();
                            //var SubBlendIndex = new List<int>();
                            var refIndex = 0;
                            var offset = 0;
                            var fullSkinData = new List<List<Vector4>>();
                            //var subIndex = 0;
                            foreach (var e in skin.SubSkins)
                            {
                                var vertList = new List<Vertex>();
                                var faceList = new List<int[]>();
                                refIndex = 0;
                                try
                                {
                                    e.CalculateData();
                                }
                                catch (NullReferenceException ex)
                                {
                                    Console.WriteLine($"Failed to calculate blend skin vertex data for skin {skin.GetID():x}: {ex.Message}");
                                    fullSkinData.Clear();
                                    break;
                                }

                                //foreach (var list in e.Vertexes)
                                {
                                    //fullSkinData.Add(e.DecompressedData);
                                }
                                /*for (var j = 0; j < e.Vertexes.Count; ++j)
                                {
                                    if (j < e.Vertexes.Count - 2)
                                    {
                                        if (e.Connection[j + 2])
                                        {
                                            if ((offset + j) % 2 == 0)
                                            {
                                                faceList.Add(new int[] { refIndex, refIndex + 1, refIndex + 2 });
                                            }
                                            else
                                            {
                                                faceList.Add(new int[] { refIndex + 1, refIndex, refIndex + 2 });
                                            }
                                        }
                                        ++refIndex;
                                    }
                                    vertList.Add(new Vertex(e.Vertexes[j], e.Normals[j], e.UVW[j], e.EmitColor[j]));
                                }
                                Vertexes.Add(vertList);
                                Faces.Add(faceList);
                                offset += e.Vertexes.Count;
                                refIndex += 2;*/
                                //SubBlendIndex.Add(subIndex);
                            }
                            var skinPath = System.IO.Path.Combine(System.IO.Path.Combine(testSavePath, "skins"), skin.GetName()) + ".bin";
                            using (System.IO.FileStream skinFile = new System.IO.FileStream(skinPath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                            {
                                foreach (var list in fullSkinData)
                                {
                                    foreach (var v in list)
                                    {
                                        if (v == null)
                                            break;
                                        skinFile.Write(BitConverter.GetBytes(v.GetBinaryX()));
                                        skinFile.Write(BitConverter.GetBytes(v.GetBinaryY()));
                                        skinFile.Write(BitConverter.GetBytes(v.GetBinaryZ()));
                                        skinFile.Write(BitConverter.GetBytes(v.GetBinaryW()));
                                    }
                                }
                            }
                            // Import
                            /*Scene scene = new Scene
                            {
                                RootNode = new Node("Root")
                            };*/

                            for (var j = 0; j < Vertexes.Count; ++j)
                            {
                                /*var submodel = Vertexes[j];
                                var faces = Faces[j];
                                Mesh mesh = new Mesh(PrimitiveType.Triangle);
                                foreach (var ver in submodel)
                                {
                                    var pos = new Vector4(ver.Position.X, ver.Position.Y, ver.Position.Z, 1.0f);
                                    var adjustment = ver.Color;
                                    var emCol = ver.EmitColor;
                                    var uv = ver.UV;
                                    mesh.Vertices.Add(new Vector3D(pos.X, pos.Y, pos.Z));
                                    mesh.VertexColorChannels[0].Add(new Color4D(emCol.X, emCol.Y, emCol.Z, emCol.W));
                                    mesh.TextureCoordinateChannels[0].Add(new Vector3D(uv.X, uv.Y, 1.0f));
                                }
                                foreach (var face in faces)
                                {
                                    mesh.Faces.Add(new Face(new int[] { face[0], face[1], face[2] }));
                                }
                                mesh.MaterialIndex = 0;
                                scene.Meshes.Add(mesh);
                                scene.RootNode.MeshIndices.Add(j);*/

                                /*var graphSec = twinLevel.GetItem<PS2AnyGraphicsSection>(Constants.LEVEL_GRAPHICS_SECTION);
                                var twinMat = graphSec.GetItem<PS2AnyMaterialsSection>(Constants.GRAPHICS_MATERIALS_SECTION).GetItem<PS2AnyMaterial>(skin.SubBlends[SubBlendIndex[j]].Material);
                                if (twinMat.Shaders[0].TxtMapping != TwinShader.TextureMapping.OFF)
                                {
                                    var twinTex = graphSec.GetItem<PS2AnyTexturesSection>(Constants.GRAPHICS_TEXTURES_SECTION).GetItem<PS2AnyTexture>(twinMat.Shaders[0].TextureId);
                                    twinTex.CalculateData();
                                    Int32 width = (Int32)Math.Pow(2, twinTex.ImageWidthPower);
                                    Int32 height = (Int32)Math.Pow(2, twinTex.ImageHeightPower);

                                    var Bits = new UInt32[width * height];
                                    var BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
                                    var tmpBmp = new Bitmap(width, height, width * 4, System.Drawing.Imaging.PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());

                                    for (var x = 0; x < width; ++x)
                                    {
                                        for (var y = 0; y < height; ++y)
                                        {
                                            var dstx = x;
                                            var dsty = height - 1 - y;
                                            Bits[dstx + dsty * width] = twinTex.Colors[x + y * width].ToARGB();
                                        }
                                    }

                                    var bmp = new Bitmap(tmpBmp);
                                    var texturePath = System.IO.Path.Combine(System.IO.Path.Combine(testSavePath, "skins"), skin.GetName()) + $"{j}.png";
                                    bmp.Save(texturePath, System.Drawing.Imaging.ImageFormat.Png);
                                    tmpBmp.Dispose();
                                    BitsHandle.Free();

                                    Material mat = new Material
                                    {
                                        Name = twinMat.Name
                                    };
                                    mat.PBR.TextureBaseColor = new TextureSlot(texturePath, TextureType.BaseColor, j, TextureMapping.FromUV, 0, 1.0f,
                                            TextureOperation.Multiply, TextureWrapMode.Wrap, TextureWrapMode.Wrap, 0);
                                    scene.Materials.Add(mat);
                                }*/

                            }

                            /*Material mat = new Material
                            {
                                Name = "Default"
                            };

                            scene.Materials.Add(mat);

                            AssimpContext context = new AssimpContext();
                            var dataPath = System.IO.Path.Combine(System.IO.Path.Combine(testSavePath, "models"), skin.GetName()) + ".dae";
                            context.ExportFile(scene, dataPath, "collada");*/
                        }
                    }
                    using (System.IO.FileStream stream = new System.IO.FileStream(System.IO.Path.Combine(testSavePath, System.IO.Path.GetFileName(levelPath)),
                        System.IO.FileMode.Create, System.IO.FileAccess.Write))
                    using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream))
                    {
                        twinLevel.Write(writer);
                    }
                }
            }
        }
    }
}
