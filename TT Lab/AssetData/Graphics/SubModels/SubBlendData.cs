using System;
using System.Collections.Generic;
using System.Diagnostics;
using TT_Lab.Assets;
using TT_Lab.Assets.Graphics;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems;

namespace TT_Lab.AssetData.Graphics.SubModels
{
    public class SubBlendData : IDisposable
    {
        public LabURI Material { get; set; } = LabURI.Empty;
        public List<SubBlendModelData> Models { get; set; } = new();

        public SubBlendData(LabURI package, String? variant, ITwinSubBlendSkin blend)
        {
            Material = AssetManager.Get().GetUri(package, typeof(Material).Name, variant, blend.Material);
            if (Material == LabURI.Empty)
            {
                var allMaterials = AssetManager.Get().GetAssets().FindAll(a => a is Material).ConvertAll(a => a.URI);
                var actuallyHasTheMaterial = allMaterials.FindAll(uri => uri.ToString().Contains(blend.Material.ToString()));
                throw new Exception($"Couldn't find requested material 0x{blend.Material:X}!");
            }

            foreach (var model in blend.Models)
            {
                Models.Add(new SubBlendModelData(model));
            }
        }

        public SubBlendData(LabURI material, IEnumerable<Assimp.Mesh> meshes, IEnumerable<Assimp.Animation> animations, List<Vector3> blendShapes, int meshIndex)
        {
            Material = material;

            foreach (var mesh in meshes)
            {
                var vertexes = new List<Vertex>();

                // Convert bone -> vertex index to vertex -> bone id
                List<Dictionary<int, (int, float)>> vertexToBone = new();
                for (Int32 j = 0; j < mesh.Bones.Count; j++)
                {
                    var bone = mesh.Bones[j];
                    vertexToBone.Add(new());
                    foreach (var vertexWeight in bone.VertexWeights)
                    {
                        vertexToBone[^1].Add(vertexWeight.VertexID, new(j, vertexWeight.Weight));
                    }
                }

                for (Int32 vertIdx = 0; vertIdx < mesh.VertexCount; vertIdx++)
                {
                    var vertex = new Vertex(
                        new Vector4(mesh.Vertices[vertIdx].X, mesh.Vertices[vertIdx].Y, mesh.Vertices[vertIdx].Z, 0.0f),
                        new Vector4(mesh.VertexColorChannels[0][vertIdx].R, mesh.VertexColorChannels[0][vertIdx].G, mesh.VertexColorChannels[0][vertIdx].B, mesh.VertexColorChannels[0][vertIdx].A),
                        new Vector4(mesh.TextureCoordinateChannels[0][vertIdx].X, mesh.TextureCoordinateChannels[0][vertIdx].Y, 1.0f, 0.0f)
                        )
                    {
                        JointInfo = new VertexJointInfo()
                        {
                            Connection = false // Temporarily lost information, restored during export
                        }
                    };

                    List<(int, float)> joints = new();
                    foreach (var vertexMap in vertexToBone)
                    {
                        if (vertexMap.ContainsKey(vertIdx))
                        {
                            joints.Add(vertexMap[vertIdx]);
                        }
                    }
                    Debug.Assert(joints.Count != 0, "Vertex must be connected to at least 1 joint");
                    Debug.Assert(joints.Count <= 3, "Vertex can only have up to 3 joints connected to it");

                    vertex.JointInfo.JointIndex1 = joints[0].Item1;
                    vertex.JointInfo.Weight1 = joints[0].Item2;
                    if (joints.Count >= 2)
                    {
                        vertex.JointInfo.JointIndex2 = joints[1].Item1;
                        vertex.JointInfo.Weight2 = joints[1].Item2;
                    }
                    if (joints.Count == 3)
                    {
                        vertex.JointInfo.JointIndex3 = joints[2].Item1;
                        vertex.JointInfo.Weight3 = joints[2].Item2;
                    }
                    vertexes.Add(vertex);
                }

                var faces = new List<IndexedFace>();
                for (var j = 0; j < mesh.FaceCount; ++j)
                {
                    faces.Add(new IndexedFace(mesh.Faces[j].Indices.ToArray()));
                }

                Models.Add(new SubBlendModelData(blendShapes[meshIndex], vertexes, faces, animations));

                meshIndex++;
            }
        }

        public void Dispose()
        {
            foreach (var model in Models)
            {
                model.Dispose();
            }
            Models.Clear();

            GC.SuppressFinalize(this);
        }
    }
}
