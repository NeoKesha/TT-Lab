using Assimp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TT_Lab.AssetData.Graphics.SubModels;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.AssetData.Graphics
{
    public class BlendSkinData : AbstractAssetData
    {
        public BlendSkinData()
        {
        }

        public BlendSkinData(ITwinBlendSkin blendSkin) : this()
        {
            SetTwinItem(blendSkin);
        }

        public Int32 BlendsAmount { get; set; }
        public List<SubBlendData> Blends { get; set; } = new();

        protected override void Dispose(Boolean disposing)
        {
            foreach (var blend in Blends)
            {
                blend.Dispose();
            }
            Blends.Clear();
        }

        public override void Save(String dataPath, JsonSerializerSettings? settings = null)
        {
            Scene scene = new()
            {
                RootNode = new Node("Root")
            };

            Node blendSkinNode = new("BlendSkin");
            scene.RootNode.Children.Add(blendSkinNode);

            var materialIndex = 0;
            var meshIndex = 0;
            var materials = new List<Material>();
            var metadata = new Metadata();
            var boneNodes = new Dictionary<String, Node>();
            foreach (var blend in Blends)
            {
                foreach (var subSkin in blend.Models)
                {
                    Mesh mesh = new(PrimitiveType.Triangle)
                    {
                        Name = $"SubSkin_{meshIndex}"
                    };

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
                        if (boneNodes.ContainsKey(bone.Name))
                        {
                            Node boneNode = new()
                            {
                                Name = bone.Name,
                                Transform = Matrix4x4.Identity
                            };
                            boneNodes.Add(bone.Name, boneNode);
                            blendSkinNode.Children.Add(boneNode);
                        }
                        mesh.Bones.Add(bone);
                    }

                    foreach (var face in subSkin.Faces)
                    {
                        mesh.Faces.Add(new Face(new int[] { face.Indexes![0], face.Indexes[1], face.Indexes[2] }));
                    }

                    Node meshNode = new(mesh.Name)
                    {
                        Transform = Matrix4x4.Identity
                    };
                    meshNode.MeshIndices.Add(meshIndex);
                    blendSkinNode.Children.Add(meshNode);

                    var blendFaceIndex = 0;
                    foreach (var blendFace in subSkin.BlendFaces)
                    {
                        Animation animation = new()
                        {
                            DurationInTicks = blendFace.BlendShapes.Count,
                            TicksPerSecond = 1,
                            Name = $"BlendShape_{materialIndex}_{meshIndex}_{blendFaceIndex++}"
                        };
                        animation.NodeAnimationChannels.Add(new NodeAnimationChannel
                        {
                            NodeName = meshNode.Name
                        });

                        var vecIndex = 0;
                        foreach (var vec in blendFace.BlendShapes)
                        {
                            animation.NodeAnimationChannels[0].PositionKeys.Add(new VectorKey
                            {
                                Time = vecIndex,
                                Value = new Vector3D(vec.Offset.X, vec.Offset.Y, vec.Offset.Z)
                            });
                            animation.NodeAnimationChannels[0].ScalingKeys.Add(new VectorKey
                            {
                                Time = vecIndex,
                                Value = new Vector3D(1.0f)
                            });
                            animation.NodeAnimationChannels[0].RotationKeys.Add(new QuaternionKey
                            {
                                Time = vecIndex,
                                Value = new Quaternion(0, 0, 0)
                            });
                            vecIndex++;
                        }

                        scene.Animations.Add(animation);
                    }

                    metadata.BlendShapes.Add(subSkin.BlendShape);

                    scene.Meshes.Add(mesh);
                    mesh.MaterialIndex = materialIndex;
                }

                var material = new Material
                {
                    Name = $"Material_{materialIndex++}",
                    TextureAmbient = new TextureSlot
                    {
                        TextureType = TextureType.Ambient,
                        UVIndex = 0,
                        WrapModeU = TextureWrapMode.Wrap,
                        WrapModeV = TextureWrapMode.Wrap,
                        FilePath = AssetManager.Get().GetAsset(blend.Material).Data,
                        Mapping = TextureMapping.FromUV,
                        Operation = TextureOperation.Add,
                        TextureIndex = 0,
                        BlendFactor = 1.0f,
                    }
                };
                materials.Add(material);
                metadata.Materials.Add(AssetManager.Get().GetAsset(blend.Material).URI);
            }

            scene.Materials.AddRange(materials);

            metadata.BlendsAmount = BlendsAmount;

            using System.IO.FileStream fs = new(dataPath + ".meta", System.IO.FileMode.Create, System.IO.FileAccess.Write);
            using System.IO.BinaryWriter writer = new(fs);
            writer.Write(JsonConvert.SerializeObject(metadata, Formatting.Indented, settings).ToCharArray());

            using AssimpContext context = new();
            context.ExportFile(scene, dataPath, "collada");
        }


        protected override void LoadInternal(String dataPath, JsonSerializerSettings? settings = null)
        {
            using AssimpContext context = new();
            var scene = context.ImportFile(dataPath);
            var metadata = new Metadata();

            using System.IO.FileStream fs = new(dataPath + ".meta", System.IO.FileMode.Open, System.IO.FileAccess.Read);
            using System.IO.StreamReader reader = new(fs);
            JsonConvert.PopulateObject(value: reader.ReadToEnd(), target: metadata, settings);

            BlendsAmount = metadata.BlendsAmount;

            var meshIndex = 0;
            for (var i = 0; i < scene.MaterialCount; i++)
            {
                var material = metadata.Materials[i];
                var meshes = scene.Meshes.Where(m => m.MaterialIndex == i);
                var animations = scene.Animations.Where(a => a.Name.Contains($"BlendShape_{i}"));
                Blends.Add(new SubBlendData(material, meshes, animations, metadata.BlendShapes, meshIndex));
                meshIndex += meshes.Count();
            }
        }

        public override void Import(LabURI package, String? variant, Int32? layoutId)
        {
            ITwinBlendSkin blendSkin = GetTwinItem<ITwinBlendSkin>();
            BlendsAmount = blendSkin.BlendsAmount;
            foreach (var blend in blendSkin.SubBlends)
            {
                Blends.Add(new SubBlendData(package, variant, blend));
            }
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            return factory.GenerateBlendSkin(BlendsAmount, Blends);
        }

        public override ITwinItem? ResolveChunkResouces(ITwinItemFactory factory, ITwinSection section, UInt32 id)
        {
            var assetManager = AssetManager.Get();
            var graphicsSection = section.GetParent();
            var materialsSection = graphicsSection.GetItem<ITwinSection>(Constants.GRAPHICS_MATERIALS_SECTION);
            foreach (var blend in Blends)
            {
                assetManager.GetAsset(blend.Material).ResolveChunkResources(factory, materialsSection);
            }
            return base.ResolveChunkResouces(factory, section, id);
        }

        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        private class Metadata
        {
            [JsonProperty(Required = Required.Always)]
            public List<LabURI> Materials { get; set; } = new();
            [JsonProperty(Required = Required.Always)]
            public List<Vector3> BlendShapes { get; set; } = new();
            [JsonProperty(Required = Required.Always)]
            public Int32 BlendsAmount { get; set; }
        }
    }
}
