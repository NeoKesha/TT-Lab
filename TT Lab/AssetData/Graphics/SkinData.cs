using Assimp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            SubSkins = new List<SubSkinData>();
        }

        public SkinData(ITwinSkin skin) : this()
        {
            SetTwinItem(skin);
        }

        [JsonProperty(Required = Required.Always)]
        public List<SubSkinData> SubSkins { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            SubSkins.ForEach(s => s.Dispose());
        }

        public override void Save(string dataPath, JsonSerializerSettings? settings = null)
        {
            Scene scene = new()
            {
                RootNode = new Node("Root")
            };

            var materialIndex = 0;
            var materials = new List<Material>();
            foreach (var subSkin in SubSkins)
            {
                Mesh mesh = new(PrimitiveType.Triangle);

                // Conversion to jointIndex -> vertex index + weight
                var bones = new Dictionary<int, List<(int, float)>>();
                var vertexIndex = 0;
                foreach (var ver in subSkin.Vertexes)
                {
                    var pos = new Vector4(ver.Position.X, ver.Position.Y, ver.Position.Z, 1.0f);
                    var color = ver.Color;
                    var uv = ver.UV;
                    var jointInfo = ver.JointInfo;
                    mesh.Vertices.Add(new Vector3D(pos.X, pos.Y, pos.Z));
                    mesh.VertexColorChannels[0].Add(new Color4D(color.X, color.Y, color.Z, color.W));
                    mesh.TextureCoordinateChannels[0].Add(new Vector3D(uv.X, uv.Y, 1.0f));

                    var jointAmount = jointInfo.GetJointConnectionsAmount();
                    if (!bones.ContainsKey(jointInfo.JointIndex1))
                    {
                        bones.Add(jointInfo.JointIndex1, new());
                    }
                    bones[jointInfo.JointIndex1].Add((vertexIndex, jointInfo.Weight1));
                    if (jointAmount >= 2)
                    {
                        if (!bones.ContainsKey(jointInfo.JointIndex2))
                        {
                            bones.Add(jointInfo.JointIndex2, new());
                        }
                        bones[jointInfo.JointIndex2].Add((vertexIndex, jointInfo.Weight2));
                    }
                    if (jointAmount == 3)
                    {
                        if (!bones.ContainsKey(jointInfo.JointIndex3))
                        {
                            bones.Add(jointInfo.JointIndex3, new());
                        }
                        bones[jointInfo.JointIndex3].Add((vertexIndex, jointInfo.Weight3));
                    }
                    vertexIndex++;
                }

                foreach (var boneMap in bones)
                {
                    var bone = new Bone
                    {
                        Name = $"Bone_{boneMap.Key}",
                        OffsetMatrix = Matrix4x4.Identity, // We don't really care at the moment
                    };
                    foreach (var boneInfo in boneMap.Value)
                    {
                        bone.VertexWeights.Add(new VertexWeight
                        {
                            VertexID = boneInfo.Item1,
                            Weight = boneInfo.Item2,
                        });
                    }
                    mesh.Bones.Add(bone);
                }

                foreach (var face in subSkin.Faces)
                {
                    mesh.Faces.Add(new Face(new int[] { face.Indexes[0], face.Indexes[1], face.Indexes[2] }));
                }

                var material = new Material
                {
                    Name = $"Material_{materialIndex}",
                    TextureAmbient = new TextureSlot
                    {
                        TextureType = TextureType.Ambient,
                        UVIndex = 0,
                        WrapModeU = TextureWrapMode.Wrap,
                        WrapModeV = TextureWrapMode.Wrap,
                        FilePath = AssetManager.Get().GetAsset(subSkin.Material).Data,
                        Mapping = TextureMapping.FromUV,
                        Operation = TextureOperation.Add,
                        TextureIndex = 0,
                        BlendFactor = 1.0f,
                    }
                };
                materials.Add(material);
                scene.Meshes.Add(mesh);
                scene.RootNode.MeshIndices.Add(materialIndex);
                mesh.MaterialIndex = materialIndex++;
                scene.RootNode.Metadata.Add(material.Name, new Metadata.Entry(MetaDataType.String, AssetManager.Get().GetAsset(subSkin.Material).URI.ToString()));
            }

            scene.Materials.AddRange(materials);

            using AssimpContext context = new();
            context.ExportFile(scene, dataPath, "collada");
        }

        public override void Load(String dataPath, JsonSerializerSettings? settings = null)
        {
            SubSkins.Clear();
            using AssimpContext context = new();
            var scene = context.ImportFile(dataPath);
            var materialIndex = 0;
            foreach (var mesh in scene.Meshes)
            {
                var vertexes = new List<Vertex>();

                // Convert bone -> vertex index to vertex -> bone id
                List<Dictionary<int, (int, float)>> vertexToBone = new();
                for (Int32 i = 0; i < mesh.Bones.Count; i++)
                {
                    var bone = mesh.Bones[i];
                    vertexToBone.Add(new());
                    foreach (var vertexWeight in bone.VertexWeights)
                    {
                        vertexToBone[^1].Add(vertexWeight.VertexID, new(i, vertexWeight.Weight));
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
                for (var i = 0; i < mesh.FaceCount; ++i)
                {
                    faces.Add(new IndexedFace(mesh.Faces[i].Indices.ToArray()));
                }

                var material = (LabURI)(String)scene.RootNode.Metadata[scene.Materials[materialIndex++].Name].Data;
                SubSkins.Add(new SubSkinData(material, vertexes, faces));
            }
        }

        public ITwinSkin GetRef()
        {
            return GetTwinItem<ITwinSkin>();
        }

        public override void Import(LabURI package, String? variant)
        {
            ITwinSkin skin = GetTwinItem<ITwinSkin>();
            SubSkins = new List<SubSkinData>();
            foreach (var e in skin.SubSkins)
            {
                SubSkins.Add(new SubSkinData(package, variant, e));
            }
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            return factory.GenerateSkin(SubSkins);
        }
    }
}
