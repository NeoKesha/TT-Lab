using Assimp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Archives;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SubItems;
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
            public Vector4 Normal
            {
                get => _normal;
                set
                {
                    _normal = value;
                    HasNormals = true;
                }
            }
            public Vector4 EmitColor { get; set; }
            public Vector3 UV { get; set; }

            public bool HasNormals { get; private set; }

            private Vector4 _normal;

            public override String ToString()
            {
                var r = (Byte)Math.Round(Color.X * 255.0f);
                var g = (Byte)Math.Round(Color.Y * 255.0f);
                var b = (Byte)Math.Round(Color.Z * 255.0f);
                var a = (Byte)Math.Round(Color.W * 255.0f);
                return $"{Position.X} {Position.Y} {Position.Z} {UV.X} {UV.Y} {UV.Z} {r} {g} {b} {a} {EmitColor.X} {EmitColor.Y} {EmitColor.Z} {EmitColor.W}";
            }
        }

        private class IndexedFace
        {
            public Int32[] Indexes { get; set; }
            public IndexedFace()
            {
                Indexes = null;
            }
            public IndexedFace(Int32[] indexes)
            {
                Indexes = indexes;
            }
            public override String ToString()
            {
                Debug.Assert(Indexes != null, "Indexes must be created at this point of time");

                StringBuilder builder = new();
                builder.Append(Indexes.Length);
                foreach (var index in Indexes)
                {
                    builder.Append($" {index}");
                }
                return builder.ToString();
            }
        }

        static void Main(string[] args)
        {
            String modelPath = args[0];
            var assimp = new AssimpContext();
            var scene = assimp.ImportFile(modelPath);
            List<List<Vertex>> totalVertexes = new();
            List<List<IndexedFace>> totalFaces = new();
            PS2AnyModel model = new();
            foreach (var mesh in scene.Meshes)
            {
                var submodel = new List<Vertex>();
                for (Int32 i = 0; i < mesh.VertexCount; i++)
                {
                    var vertex = new Vertex(
                        new Vector4(-mesh.Vertices[i].X, mesh.Vertices[i].Y, mesh.Vertices[i].Z, 0.0f),
                        new Vector4(1f, 1f, 1f, 1f),
                        new Vector4(mesh.TextureCoordinateChannels[0][i].X, mesh.TextureCoordinateChannels[0][i].Y, 1.0f, 0.0f)
                        )
                    {
                        Normal = new Vector4(-mesh.Normals[i].X, mesh.Normals[i].Y, mesh.Normals[i].Z, 0f)
                    };
                    submodel.Add(vertex);
                }

                var faces = new List<IndexedFace>();
                for (var i = 0; i < mesh.FaceCount; ++i)
                {
                    faces.Add(new IndexedFace(mesh.Faces[i].Indices.ToArray()));
                }

                totalVertexes.Add(submodel);
                totalFaces.Add(faces);
            }
            assimp.Dispose();

            var vertexBatchIndex = 0;
            foreach (var faces in totalFaces)
            {
                var submodel = new PS2SubModel
                {
                    UnusedBlob = Array.Empty<Byte>(),
                    Vertexes = new(),
                    UVW = new(),
                    Colors = new(),
                    EmitColor = new(),
                    Normals = new(),
                    Connection = new()
                };
                var vertexBatch = totalVertexes[vertexBatchIndex++];
                var i = 0;
                foreach (var face in faces)
                {
                    var idx0 = (i % 2 == 0) ? 0 : 1;
                    var idx1 = (i % 2 == 0) ? 1 : 0;
                    submodel.Vertexes.Add(new Vector4(vertexBatch[face.Indexes[idx0]].Position, 1f));
                    submodel.Vertexes.Add(new Vector4(vertexBatch[face.Indexes[idx1]].Position, 1f));
                    submodel.Vertexes.Add(new Vector4(vertexBatch[face.Indexes[2]].Position, 1f));
                    submodel.UVW.Add(new Vector4(vertexBatch[face.Indexes[idx0]].UV, 0f));
                    submodel.UVW.Add(new Vector4(vertexBatch[face.Indexes[idx1]].UV, 0f));
                    submodel.UVW.Add(new Vector4(vertexBatch[face.Indexes[2]].UV, 0f));
                    submodel.Colors.Add(vertexBatch[face.Indexes[idx0]].Color);
                    submodel.Colors.Add(vertexBatch[face.Indexes[idx1]].Color);
                    submodel.Colors.Add(vertexBatch[face.Indexes[2]].Color);
                    submodel.Normals.Add(vertexBatch[face.Indexes[idx0]].Normal);
                    submodel.Normals.Add(vertexBatch[face.Indexes[idx1]].Normal);
                    submodel.Normals.Add(vertexBatch[face.Indexes[2]].Normal);
                    submodel.EmitColor.Add(vertexBatch[face.Indexes[idx0]].EmitColor);
                    submodel.EmitColor.Add(vertexBatch[face.Indexes[idx1]].EmitColor);
                    submodel.EmitColor.Add(vertexBatch[face.Indexes[2]].EmitColor);
              
                    ++i;  
                    // What do?

                    submodel.Connection.Add(false);
                    submodel.Connection.Add(false);
                    submodel.Connection.Add(true);
                }
                model.SubModels.Add(submodel);
            }

            using var ps2Model = File.Create(Directory.GetCurrentDirectory() + "/compiled_ps2_model");
            using var writer = new BinaryWriter(ps2Model);
            model.Write(writer);
            writer.Flush();
            ps2Model.Flush();
            writer.Close();

        }
    }
}
