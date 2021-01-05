using Newtonsoft.Json;
using Assimp;
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
        [JsonProperty(Required = Required.Always)]
        public List<List<Vertex>> Vertexes { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<List<IndexedFace>> Faces { get; set; }
        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Save(string dataPath)
        {
            try
            {
                Scene scene = new Scene
                {
                    RootNode = new Node("Root")
                };

                for (var i = 0; i < Vertexes.Count; ++i)
                {
                    var submodel = Vertexes[i];
                    var faces = Faces[i];
                    Mesh mesh = new Mesh(PrimitiveType.Triangle);
                    foreach (var ver in submodel)
                    {
                        var pos = ver.Position;
                        var col = ver.Color;
                        var emCol = ver.EmitColor;
                        var uv = ver.UV;
                        mesh.Vertices.Add(new Vector3D(pos.X, pos.Y, pos.Z));
                        mesh.TextureCoordinateChannels[0].Add(new Vector3D(uv.X, uv.Y, uv.Z));
                        mesh.VertexColorChannels[0].Add(new Color4D(col.X, col.Y, col.Z, col.W));
                        mesh.VertexColorChannels[1].Add(new Color4D(emCol.X, emCol.Y, emCol.Z, emCol.W));
                    }
                    foreach (var face in faces)
                    {
                        mesh.Faces.Add(new Face(new int[] { face.Indexes[0], face.Indexes[1], face.Indexes[2] }));
                    }
                    mesh.MaterialIndex = 0;
                    scene.Meshes.Add(mesh);
                    scene.RootNode.MeshIndices.Add(i);
                }

                Material mat = new Material
                {
                    Name = "EmptyMaterial"
                };
                scene.Materials.Add(mat);

                AssimpContext context = new AssimpContext();
                context.ExportFile(scene, dataPath, "collada");
            }
            catch (Exception ex)
            {
                Log.WriteLine($"Error exporting for Model {twinRef.GetID()}: {ex.Message} Stack: \n{ex.StackTrace}");
            }
        }

        public override void Load(String dataPath)
        {
            Vertexes.Clear();
            Faces.Clear();
            AssimpContext cxt = new AssimpContext();
            var scene = cxt.ImportFile(dataPath);
            foreach (var mesh in scene.Meshes)
            {
                var submodel = new List<Vertex>();
                var faces = new List<IndexedFace>();
                for (var i = 0; i < mesh.VertexCount; ++i)
                {
                    Twinsanity.TwinsanityInterchange.Common.Vector4 pos =
                        new Twinsanity.TwinsanityInterchange.Common.Vector4(mesh.Vertices[i].X, mesh.Vertices[i].Y, mesh.Vertices[i].Z, 1.0f);
                    Twinsanity.TwinsanityInterchange.Common.Vector4 col =
                        new Twinsanity.TwinsanityInterchange.Common.Vector4(mesh.VertexColorChannels[0][i].R, mesh.VertexColorChannels[0][i].G, mesh.VertexColorChannels[0][i].B, mesh.VertexColorChannels[0][i].A);
                    Twinsanity.TwinsanityInterchange.Common.Vector4 emCol =
                        new Twinsanity.TwinsanityInterchange.Common.Vector4(mesh.VertexColorChannels[1][i].R, mesh.VertexColorChannels[1][i].G, mesh.VertexColorChannels[1][i].B, mesh.VertexColorChannels[1][i].A);
                    Twinsanity.TwinsanityInterchange.Common.Vector4 uv =
                        new Twinsanity.TwinsanityInterchange.Common.Vector4(mesh.TextureCoordinateChannels[0][i].X, mesh.TextureCoordinateChannels[0][i].Y, mesh.TextureCoordinateChannels[0][i].Z, 1.0f);
                    submodel.Add(new Vertex(pos, col, uv, emCol));
                }
                for (var i = 0; i < mesh.FaceCount; ++i)
                {
                    var face = new IndexedFace(new int[] { mesh.Faces[i].Indices[0], mesh.Faces[i].Indices[1], mesh.Faces[i].Indices[2] });
                    faces.Add(face);
                }
                Vertexes.Add(submodel);
                Faces.Add(faces);
            }
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
                            if ((/*offset +*/ j) % 2 == 0)
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
                Vertexes.Add(vertList);
                Faces.Add(faceList);
                //offset += e.Vertexes.Count;
                refIndex += 2;
            }
        }
    }
}
