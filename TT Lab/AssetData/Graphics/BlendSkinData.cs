using Newtonsoft.Json;
using SharpGLTF.Geometry;
using SharpGLTF.Schema2;
using System;
using System.Collections.Generic;
using System.Linq;
using TT_Lab.AssetData.Graphics.SubModels;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Attributes;
using TT_Lab.Extensions;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.AssetData.Graphics
{
    using COLOR_UV = SharpGLTF.Geometry.VertexTypes.VertexColor1Texture1;
    using JOINT_WEIGHT = SharpGLTF.Geometry.VertexTypes.VertexJoints4;
    using VERTEX = SharpGLTF.Geometry.VertexTypes.VertexPosition;
    using VERTEX_BUILDER = VertexBuilder<SharpGLTF.Geometry.VertexTypes.VertexPosition, SharpGLTF.Geometry.VertexTypes.VertexColor1Texture1, SharpGLTF.Geometry.VertexTypes.VertexJoints4>;

    [ReferencesAssets]
    public class BlendSkinData : AbstractAssetData
    {
        public BlendSkinData()
        {
        }

        public BlendSkinData(ITwinBlendSkin blendSkin) : this()
        {
            SetTwinItem(blendSkin);
        }

        public UInt32? CompileScale { get; set; }
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

        protected override void SaveInternal(String dataPath, JsonSerializerSettings? settings = null)
        {
            var scene = new SharpGLTF.Scenes.SceneBuilder("TwinsanityBlendSkin");
            var root = new SharpGLTF.Scenes.NodeBuilder("blend_skin_root");
            var metadata = new Metadata();
            scene.AddNode(root);

            static VERTEX_BUILDER generateVertexFromTwinVertex(Vertex vertex)
            {
                return new VERTEX_BUILDER(new VERTEX(vertex.Position.ToSystem()),
                        new COLOR_UV(
                            vertex.Color.ToSystem(),
                            new System.Numerics.Vector2(vertex.UV.X, vertex.UV.Y)),
                        new JOINT_WEIGHT(
                            (vertex.JointInfo.JointIndex1, vertex.JointInfo.Weight1),
                            (vertex.JointInfo.JointIndex2, vertex.JointInfo.Weight2),
                            (vertex.JointInfo.JointIndex3, vertex.JointInfo.Weight3)));
            };

            var jointsAmount = 0;
            foreach (var blend in Blends)
            {
                foreach (var blendModel in blend.Models)
                {
                    foreach (var ver in blendModel.Vertexes)
                    {
                        if (ver.JointInfo.JointIndex1 > jointsAmount)
                        {
                            jointsAmount = ver.JointInfo.JointIndex1;
                        }
                        if (ver.JointInfo.JointIndex2 > jointsAmount)
                        {
                            jointsAmount = ver.JointInfo.JointIndex2;
                        }
                        if (ver.JointInfo.JointIndex3 > jointsAmount)
                        {
                            jointsAmount = ver.JointInfo.JointIndex3;
                        }
                    }
                }

            }

            // Create all the joint nodes
            List<SharpGLTF.Scenes.NodeBuilder> nodes = new(jointsAmount + 1);
            var subSkinNodes = new List<(SharpGLTF.Scenes.NodeBuilder, System.Numerics.Matrix4x4)>();
            for (var i = 0; i < nodes.Capacity; ++i)
            {
                var node = new SharpGLTF.Scenes.NodeBuilder($"joint_{i}");
                root.AddNode(node);
                subSkinNodes.Add((node, System.Numerics.Matrix4x4.Identity));
                nodes.Add(node);
            }

            var index = 0;
            var materialIndex = 0;
            foreach (var blend in Blends)
            {
                var twinMaterial = AssetManager.Get().GetAssetData<MaterialData>(blend.Material);
                metadata.Materials.Add(AssetManager.Get().GetAsset(blend.Material).URI);
                var texture = twinMaterial.Shaders[0].TextureId == LabURI.Empty ? null : AssetManager.Get().GetAsset<Assets.Graphics.Texture>(twinMaterial.Shaders[0].TextureId);
                var texturePath = texture == null ? null : $"{typeof(Assets.Graphics.Texture).Name}/{texture.Data}";
                var material = new SharpGLTF.Materials.MaterialBuilder($"Material_{materialIndex++}")
                    .WithDoubleSide(true)
                    .WithAlpha(SharpGLTF.Materials.AlphaMode.OPAQUE);

                if (texturePath == null)
                {
                    material.WithBaseColor(new System.Numerics.Vector4(1, 1, 1, 1));
                }
                else
                {
                    material.WithBaseColor(texturePath);
                }

                foreach (var blendModel in blend.Models)
                {
                    var mesh = new MeshBuilder<VERTEX, COLOR_UV, JOINT_WEIGHT>($"blend_subskin_{index++}");
                    var blendShapeInfo = System.Text.Json.JsonSerializer.Serialize(blendModel.BlendShape);
                    mesh.Extras = System.Text.Json.Nodes.JsonNode.Parse(System.Text.Json.JsonSerializer.Serialize(blendModel.BlendShape));

                    foreach (var face in blendModel.Faces)
                    {
                        var ver1 = blendModel.Vertexes[face.Indexes![0]];
                        var ver2 = blendModel.Vertexes[face.Indexes[1]];
                        var ver3 = blendModel.Vertexes[face.Indexes[2]];
                        var primitive = mesh.UsePrimitive(material);
                        primitive.AddTriangle(generateVertexFromTwinVertex(ver1), generateVertexFromTwinVertex(ver2), generateVertexFromTwinVertex(ver3));
                    }

                    int findVertexIndex(VERTEX vertex)
                    {
                        var index = -1;
                        foreach (var ver in blendModel.Vertexes)
                        {
                            if (ver.Position.ToSystem() == vertex.Position)
                            {
                                return index + 1;
                            }

                            index++;
                        }

                        return -1;
                    }

                    var morphs = new List<IMorphTargetBuilder>();
                    for (Int32 i = 0; i < blendModel.BlendFaces.Count; i++)
                    {
                        var blendFace = blendModel.BlendFaces[i];
                        var morph = mesh.UseMorphTarget(i);
                        morphs.Add(morph);
                        foreach (var vertex in morph.Vertices)
                        {
                            var newVer = vertex;
                            var shapeIndex = findVertexIndex(vertex);
                            var blendVec = blendFace.BlendShapes[shapeIndex].Offset;

                            newVer.Position += new System.Numerics.Vector3(blendVec.X, blendVec.Y, blendVec.Z);

                            morph.SetVertex(vertex, newVer);
                        }
                    }

                    scene.AddSkinnedMesh(mesh, subSkinNodes.ToArray());
                }
            }

            var model = scene.ToGltf2();
            model.SaveGLB(dataPath);

            using System.IO.FileStream fs = new(dataPath + ".meta", System.IO.FileMode.Create, System.IO.FileAccess.Write);
            using System.IO.BinaryWriter writer = new(fs);
            writer.Write(JsonConvert.SerializeObject(metadata, Formatting.Indented, settings).ToCharArray());
        }


        protected override void LoadInternal(String dataPath, JsonSerializerSettings? settings = null)
        {
            var metadata = new Metadata();
            var blendSkin = ModelRoot.Load(dataPath, SharpGLTF.Validation.ValidationMode.Strict);

            using System.IO.FileStream fs = new(dataPath + ".meta", System.IO.FileMode.Open, System.IO.FileAccess.Read);
            using System.IO.StreamReader reader = new(fs);
            JsonConvert.PopulateObject(value: reader.ReadToEnd(), target: metadata, settings);

            BlendsAmount = blendSkin.LogicalMeshes[0].Primitives[0].GetVertexColumns().MorphTargets.Count;

            foreach (var material in blendSkin.LogicalMaterials)
            {
                var labMaterial = metadata.Materials[material.LogicalIndex];
                var meshes = blendSkin.LogicalMeshes.Where(m => m.Primitives.All(p => p.Material.LogicalIndex == material.LogicalIndex)).ToList();
                Blends.Add(new SubBlendData(labMaterial, meshes, BlendsAmount));
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
            return factory.GenerateBlendSkin(BlendsAmount, Blends, CompileScale);
        }

        public override ITwinItem? ResolveChunkResouces(ITwinItemFactory factory, ITwinSection section, UInt32 id, Int32? layoutID = null)
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
        }
    }
}
