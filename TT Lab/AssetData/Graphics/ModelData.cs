using Assimp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.AssetData.Graphics.SubModels;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.AssetData.Graphics
{
    public class ModelData : AbstractAssetData
    {
        public ModelData()
        {
            Vertexes = new List<List<Vertex>>();
            Faces = new List<List<IndexedFace>>();
        }

        public ModelData(ITwinModel model) : this()
        {
            SetTwinItem(model);
        }

        [JsonProperty(Required = Required.Always)]
        public List<List<Vertex>> Vertexes { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<List<IndexedFace>> Faces { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            Vertexes.ForEach(vl => vl.Clear());
            Vertexes.Clear();
            Faces.ForEach(fl => fl.Clear());
            Faces.Clear();
        }

        public override void Save(string dataPath, JsonSerializerSettings? settings = null)
        {
            try
            {
                Scene scene = new()
                {
                    RootNode = new Node("Root")
                };

                for (var i = 0; i < Vertexes.Count; ++i)
                {
                    var submodel = Vertexes[i];
                    var faces = Faces[i];
                    Mesh mesh = new(PrimitiveType.Triangle);
                    foreach (var ver in submodel)
                    {
                        mesh.Vertices.Add(new Vector3D(ver.Position.X, ver.Position.Y, ver.Position.Z));
                        mesh.TextureCoordinateChannels[0].Add(new Vector3D(ver.UV.X, ver.UV.Y, 1.0f));
                        mesh.VertexColorChannels[0].Add(new Color4D(ver.Color.X, ver.Color.Y, ver.Color.Z, ver.Color.W));
                        if (ver.HasNormals)
                        {
                            mesh.Normals.Add(new Vector3D(ver.Normal.X, ver.Normal.Y, ver.Normal.Z));
                        }
                        if (ver.HasEmitColor)
                        {
                            mesh.VertexColorChannels[1].Add(new Color4D(ver.EmitColor.X, ver.EmitColor.Y, ver.EmitColor.Z, ver.EmitColor.W));
                        }
                    }
                    foreach (var face in faces)
                    {
                        mesh.Faces.Add(new Face(new int[] { face.Indexes[0], face.Indexes[1], face.Indexes[2] }));
                    }
                    mesh.MaterialIndex = 0;
                    scene.Meshes.Add(mesh);
                    scene.RootNode.MeshIndices.Add(i);
                }

                Material mat = new()
                {
                    Name = "Default"
                };
                scene.Materials.Add(mat);

                AssimpContext context = new();
                context.ExportFile(scene, dataPath, "collada");
                context.Dispose();
            }
            catch (Exception ex)
            {
                Log.WriteLine($"Error exporting for Model {GetTwinItem<PS2AnyModel>().GetID()}: {ex.Message} Stack: \n{ex.StackTrace}");
            }
        }

        public override void Load(String dataPath, JsonSerializerSettings? settings = null)
        {
            Vertexes.Clear();
            Faces.Clear();
            AssimpContext context = new();
            var scene = context.ImportFile(dataPath);
            foreach (var mesh in scene.Meshes)
            {
                var submodel = new List<Vertex>();
                for (var i = 0; i < mesh.VertexCount; ++i)
                {
                    var ver = new Vertex(
                        new Vector4(mesh.Vertices[i].X, mesh.Vertices[i].Y, mesh.Vertices[i].Z, 0.0f),
                        new Vector4(mesh.VertexColorChannels[0][i].R, mesh.VertexColorChannels[0][i].G, mesh.VertexColorChannels[0][i].B, mesh.VertexColorChannels[0][i].A),
                        new Vector4(mesh.TextureCoordinateChannels[0][i].X, mesh.TextureCoordinateChannels[0][i].Y, 1.0f, 0.0f)
                    );
                    if (mesh.Normals.Count == mesh.Vertices.Count)
                    {
                        ver.Normal = new Vector4(mesh.Normals[i].X, mesh.Normals[i].Y, mesh.Normals[i].Z, 1.0f);
                    }
                    if (mesh.VertexColorChannels[1].Count == mesh.Vertices.Count)
                    {
                        ver.EmitColor = new Vector4(mesh.VertexColorChannels[1][i].R, mesh.VertexColorChannels[1][i].G, mesh.VertexColorChannels[1][i].B, mesh.VertexColorChannels[1][i].A);
                    }
                    submodel.Add(ver);
                }

                var faces = new List<IndexedFace>();
                for (var i = 0; i < mesh.FaceCount; ++i)
                {
                    faces.Add(new IndexedFace(mesh.Faces[i].Indices.ToArray()));
                }
                Vertexes.Add(submodel);
                Faces.Add(faces);
            }
            context.Dispose();
        }

        public override void Import(LabURI package, String? variant)
        {
            ITwinModel model = GetTwinItem<ITwinModel>();
            Vertexes = new List<List<Vertex>>();
            Faces = new List<List<IndexedFace>>();
            foreach (var e in model.SubModels)
            {
                var vertList = new List<Vertex>();
                var faceList = new List<IndexedFace>();
                Int32 refIndex = 0;
                e.CalculateData();
                for (var j = 0; j < e.Vertexes.Count; ++j)
                {
                    if (j < e.Vertexes.Count - 2)
                    {
                        if (e.Connection[j + 2])
                        {
                            if (j % 2 == 0)
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
                    var ver = new Vertex(e.Vertexes[j], e.Colors[j], e.UVW[j]);
                    if (e.EmitColor.Count == e.Vertexes.Count)
                    {
                        ver.EmitColor = new Vector4(e.EmitColor[j].X, e.EmitColor[j].Y, e.EmitColor[j].Z, e.EmitColor[j].W);
                    }
                    if (e.Normals.Count == e.Vertexes.Count)
                    {
                        ver.Normal = new Vector4(e.Normals[j].X, e.Normals[j].Y, e.Normals[j].Z, e.Normals[j].W);
                    }
                    vertList.Add(ver);
                }
                Vertexes.Add(vertList);
                Faces.Add(faceList);
            }
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            return factory.GenerateModel(Vertexes, Faces);
        }
    }
}
