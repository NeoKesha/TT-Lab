using Assimp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.AssetData.Graphics.SubModels;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.AssetData.Graphics
{
    public class SkinData : AbstractAssetData
    {
        public SkinData()
        {
            Vertexes = new List<List<Vertex>>();
            Faces = new List<List<IndexedFace>>();
        }

        public SkinData(ITwinSkin skin) : this()
        {
            SetTwinItem(skin);
        }
        [JsonProperty(Required = Required.Always)]
        public List<List<Vertex>> Vertexes { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<List<IndexedFace>> Faces { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            Vertexes.ForEach(v => v.Clear());
            Vertexes.Clear();
            Faces.ForEach(f => f.Clear());
            Faces.Clear();
        }
        public override void Save(string dataPath, JsonSerializerSettings? settings = null)
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
                    var pos = new Vector4(ver.Position.X, ver.Position.Y, ver.Position.Z, 1.0f);
                    var emCol = ver.EmitColor;
                    var uv = ver.UV;
                    mesh.Vertices.Add(new Vector3D(pos.X, pos.Y, pos.Z));
                    mesh.VertexColorChannels[0].Add(new Color4D(emCol.X, emCol.Y, emCol.Z, emCol.W));
                    mesh.TextureCoordinateChannels[0].Add(new Vector3D(uv.X, uv.Y, 1.0f));
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
                Name = "Default"
            };
            scene.Materials.Add(mat);

            AssimpContext context = new AssimpContext();
            context.ExportFile(scene, dataPath, "collada");
        }
        public override void Load(String dataPath, JsonSerializerSettings? settings = null)
        {
            Vertexes.Clear();
            Faces.Clear();
            AssimpContext context = new AssimpContext();
            var scene = context.ImportFile(dataPath);
            foreach (var mesh in scene.Meshes)
            {
                var submodel = new List<Vertex>();
                for (var i = 0; i < mesh.VertexCount; ++i)
                {
                    submodel.Add(new Vertex(
                        new Vector4(mesh.Vertices[i].X, mesh.Vertices[i].Y, mesh.Vertices[i].Z, 0.0f),
                        new Vector4(mesh.VertexColorChannels[0][i].R, mesh.VertexColorChannels[0][i].G, mesh.VertexColorChannels[0][i].B, mesh.VertexColorChannels[0][i].A),
                        new Vector4(mesh.TextureCoordinateChannels[0][i].X, mesh.TextureCoordinateChannels[0][i].Y, 1.0f, 0.0f)
                        ));
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

        public ITwinSkin GetRef()
        {
            return GetTwinItem<ITwinSkin>();
        }

        public override void Import(LabURI package, String? variant)
        {
            ITwinSkin skin = GetTwinItem<ITwinSkin>();
            Vertexes = new List<List<Vertex>>();
            Faces = new List<List<IndexedFace>>();
            foreach (var e in skin.SubSkins)
            {
                var vertList = new List<Vertex>();
                var faceList = new List<IndexedFace>();
                Int32 refIndex = 0;
                e.CalculateData();
                for (var j = 0; j < e.Vertexes.Count; ++j)
                {
                    if (j < e.Vertexes.Count - 2)
                    {
                        if (e.SkinJoints[j + 2].Connection)
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
                    vertList.Add(new Vertex(e.Vertexes[j], e.Colors[j], e.UVW[j], e.Colors[j]));
                }
                Vertexes.Add(vertList);
                Faces.Add(faceList);
            }
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }
    }
}
