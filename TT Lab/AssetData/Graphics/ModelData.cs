using Newtonsoft.Json;
using SharpGLTF.Schema2;
using System;
using System.Collections.Generic;
using System.Linq;
using TT_Lab.AssetData.Graphics.SubModels;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.AssetData.Graphics
{
    using COLOR_EMIT_UV = SharpGLTF.Geometry.VertexTypes.VertexColor2Texture1;
    using COLOR_UV = SharpGLTF.Geometry.VertexTypes.VertexColor1Texture1;
    using VERTEX = SharpGLTF.Geometry.VertexTypes.VertexPosition;
    using VERTEX_BUILDER_VCEU = SharpGLTF.Geometry.VertexBuilder<SharpGLTF.Geometry.VertexTypes.VertexPosition, SharpGLTF.Geometry.VertexTypes.VertexColor2Texture1, SharpGLTF.Geometry.VertexTypes.VertexEmpty>;
    using VERTEX_BUILDER_VCU = SharpGLTF.Geometry.VertexBuilder<SharpGLTF.Geometry.VertexTypes.VertexPosition, SharpGLTF.Geometry.VertexTypes.VertexColor1Texture1, SharpGLTF.Geometry.VertexTypes.VertexEmpty>;
    using VERTEX_BUILDER_VNCEU = SharpGLTF.Geometry.VertexBuilder<SharpGLTF.Geometry.VertexTypes.VertexPositionNormal, SharpGLTF.Geometry.VertexTypes.VertexColor2Texture1, SharpGLTF.Geometry.VertexTypes.VertexEmpty>;
    using VERTEX_BUILDER_VNCU = SharpGLTF.Geometry.VertexBuilder<SharpGLTF.Geometry.VertexTypes.VertexPositionNormal, SharpGLTF.Geometry.VertexTypes.VertexColor1Texture1, SharpGLTF.Geometry.VertexTypes.VertexEmpty>;
    using VERTEX_NORMAL = SharpGLTF.Geometry.VertexTypes.VertexPositionNormal;

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

        public List<List<Vertex>> Vertexes { get; set; }
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
            var material = new SharpGLTF.Materials.MaterialBuilder()
                .WithDoubleSide(true)
                .WithMetallicRoughnessShader()
                .WithBaseColor(new System.Numerics.Vector4(1, 1, 1, 1))
                .WithEmissive(new System.Numerics.Vector3(0.2f, 0.2f, 0.2f));

            var scene = new SharpGLTF.Scenes.SceneBuilder("TwinsanityMesh");

            static VERTEX_BUILDER_VNCEU generateVertexFromTwinVertexVNCEU(Vertex vertex)
            {
                return new VERTEX_BUILDER_VNCEU(new VERTEX_NORMAL(
                        vertex.Position.X, vertex.Position.Y, vertex.Position.Z,
                        vertex.Normal.X, vertex.Normal.Y, vertex.Normal.Z),
                        new COLOR_EMIT_UV(
                            new System.Numerics.Vector4(vertex.Color.X, vertex.Color.Y, vertex.Color.Z, vertex.Color.W),
                            new System.Numerics.Vector4(vertex.EmitColor.X, vertex.EmitColor.Y, vertex.EmitColor.Z, vertex.EmitColor.W),
                            new System.Numerics.Vector2(vertex.UV.X, vertex.UV.Y)
                            ));
            };

            static VERTEX_BUILDER_VNCU generateVertexFromTwinVertexVNCU(Vertex vertex)
            {
                return new VERTEX_BUILDER_VNCU(new VERTEX_NORMAL(
                        vertex.Position.X, vertex.Position.Y, vertex.Position.Z,
                        vertex.Normal.X, vertex.Normal.Y, vertex.Normal.Z),
                        new COLOR_UV(
                            new System.Numerics.Vector4(vertex.Color.X, vertex.Color.Y, vertex.Color.Z, vertex.Color.W),
                            new System.Numerics.Vector2(vertex.UV.X, vertex.UV.Y)
                            ));
            };

            static VERTEX_BUILDER_VCEU generateVertexFromTwinVertexVCEU(Vertex vertex)
            {
                return new VERTEX_BUILDER_VCEU(new VERTEX(
                        vertex.Position.X, vertex.Position.Y, vertex.Position.Z),
                        new COLOR_EMIT_UV(
                            new System.Numerics.Vector4(vertex.Color.X, vertex.Color.Y, vertex.Color.Z, vertex.Color.W),
                            new System.Numerics.Vector4(vertex.EmitColor.X, vertex.EmitColor.Y, vertex.EmitColor.Z, vertex.EmitColor.W),
                            new System.Numerics.Vector2(vertex.UV.X, vertex.UV.Y)
                            ));
            };

            static VERTEX_BUILDER_VCU generateVertexFromTwinVertexVCU(Vertex vertex)
            {
                return new VERTEX_BUILDER_VCU(new VERTEX(
                        vertex.Position.X, vertex.Position.Y, vertex.Position.Z),
                        new COLOR_UV(
                            new System.Numerics.Vector4(vertex.Color.X, vertex.Color.Y, vertex.Color.Z, vertex.Color.W),
                            new System.Numerics.Vector2(vertex.UV.X, vertex.UV.Y)
                            ));
            };

            static SharpGLTF.Geometry.MeshBuilder<VERTEX_NORMAL, COLOR_EMIT_UV> getMeshBuilderVNCEU(int idx)
            {
                return new SharpGLTF.Geometry.MeshBuilder<VERTEX_NORMAL, COLOR_EMIT_UV>($"mesh_{idx}");
            }

            static SharpGLTF.Geometry.MeshBuilder<VERTEX_NORMAL, COLOR_UV> getMeshBuilderVNCU(int idx)
            {
                return new SharpGLTF.Geometry.MeshBuilder<VERTEX_NORMAL, COLOR_UV>($"mesh_{idx}");
            }

            static SharpGLTF.Geometry.MeshBuilder<VERTEX, COLOR_EMIT_UV> getMeshBuilderVCEU(int idx)
            {
                return new SharpGLTF.Geometry.MeshBuilder<VERTEX, COLOR_EMIT_UV>($"mesh_{idx}");
            }

            static SharpGLTF.Geometry.MeshBuilder<VERTEX, COLOR_UV> getMeshBuilderVCU(int idx)
            {
                return new SharpGLTF.Geometry.MeshBuilder<VERTEX, COLOR_UV>($"mesh_{idx}");
            }

            /// Generate mesh with positions, normals, colors, emission and UV coordinates
            void generateMeshWithVNCEU(int idx, List<IndexedFace> faces, List<Vertex> submodel)
            {
                var mesh = getMeshBuilderVNCEU(idx);
                var vertexGenerator = generateVertexFromTwinVertexVNCEU;
                foreach (var face in faces)
                {
                    var ver1 = submodel[face.Indexes![0]];
                    var ver2 = submodel[face.Indexes[1]];
                    var ver3 = submodel[face.Indexes[2]];
                    var primitive = mesh.UsePrimitive(material);
                    primitive.AddTriangle(vertexGenerator(ver1), vertexGenerator(ver2), vertexGenerator(ver3));
                }

                scene.AddRigidMesh(mesh, SharpGLTF.Transforms.AffineTransform.Identity);
            }

            /// Generate mesh with positions, normals, colors and UV coordinates
            void generateMeshWithVNCU(int idx, List<IndexedFace> faces, List<Vertex> submodel)
            {
                var mesh = getMeshBuilderVNCU(idx);
                var vertexGenerator = generateVertexFromTwinVertexVNCU;
                foreach (var face in faces)
                {
                    var ver1 = submodel[face.Indexes![0]];
                    var ver2 = submodel[face.Indexes[1]];
                    var ver3 = submodel[face.Indexes[2]];
                    var primitive = mesh.UsePrimitive(material);
                    primitive.AddTriangle(vertexGenerator(ver1), vertexGenerator(ver2), vertexGenerator(ver3));
                }

                scene.AddRigidMesh(mesh, SharpGLTF.Transforms.AffineTransform.Identity);
            }

            /// Generate mesh with positions, colors, emission and UV coordinates
            void generateMeshWithVCEU(int idx, List<IndexedFace> faces, List<Vertex> submodel)
            {
                var mesh = getMeshBuilderVCEU(idx);
                var vertexGenerator = generateVertexFromTwinVertexVCEU;
                foreach (var face in faces)
                {
                    var ver1 = submodel[face.Indexes![0]];
                    var ver2 = submodel[face.Indexes[1]];
                    var ver3 = submodel[face.Indexes[2]];
                    var primitive = mesh.UsePrimitive(material);
                    primitive.AddTriangle(vertexGenerator(ver1), vertexGenerator(ver2), vertexGenerator(ver3));
                }

                scene.AddRigidMesh(mesh, SharpGLTF.Transforms.AffineTransform.Identity);
            }

            /// Generate mesh with positions, colors and UV coordinates
            void generateMeshWithVCU(int idx, List<IndexedFace> faces, List<Vertex> submodel)
            {
                var mesh = getMeshBuilderVCU(idx);
                var vertexGenerator = generateVertexFromTwinVertexVCU;
                foreach (var face in faces)
                {
                    var ver1 = submodel[face.Indexes![0]];
                    var ver2 = submodel[face.Indexes[1]];
                    var ver3 = submodel[face.Indexes[2]];
                    var primitive = mesh.UsePrimitive(material);
                    primitive.AddTriangle(vertexGenerator(ver1), vertexGenerator(ver2), vertexGenerator(ver3));
                }

                scene.AddRigidMesh(mesh, SharpGLTF.Transforms.AffineTransform.Identity);
            }

            for (var i = 0; i < Vertexes.Count; i++)
            {
                var submodel = Vertexes[i];
                var hasNormals = submodel.Where(v => v.HasNormals).Any();
                var hasEmits = submodel.Where(v => v.HasEmitColor).Any();
                if (hasNormals && hasEmits)
                {
                    generateMeshWithVNCEU(i, Faces[i], submodel);
                }
                else if (!hasNormals && hasEmits)
                {
                    generateMeshWithVCEU(i, Faces[i], submodel);
                }
                else if (hasNormals && !hasEmits)
                {
                    generateMeshWithVNCU(i, Faces[i], submodel);
                }
                else
                {
                    generateMeshWithVCU(i, Faces[i], submodel);
                }
            }

            var model = scene.ToGltf2();
            model.SaveGLB(dataPath);
        }

        protected override void LoadInternal(String dataPath, JsonSerializerSettings? settings = null)
        {
            Vertexes.Clear();
            Faces.Clear();
            var model = ModelRoot.Load(dataPath);

            foreach (var mesh in model.LogicalMeshes)
            {
                var submodel = new List<Vertex>();
                var faces = new List<IndexedFace>();
                foreach (var primitive in mesh.Primitives)
                {
                    var vertexes = primitive.GetVertexColumns();
                    for (var i = 0; i < vertexes.Positions.Count; i++)
                    {
                        var ver = new Vertex(
                                vertexes.Positions[i].ToTwin(),
                                vertexes.Colors0[i].ToTwin(),
                                vertexes.TexCoords0[i].ToTwin());
                        if (vertexes.Normals != null)
                        {
                            ver.Normal = vertexes.Normals[i].ToTwin();
                        }
                        if (vertexes.Colors1 != null)
                        {
                            ver.EmitColor = vertexes.Colors1[i].ToTwin();
                        }
                        submodel.Add(ver);
                    }

                    foreach (var (idx1, idx2, idx3) in primitive.GetTriangleIndices())
                    {
                        faces.Add(new IndexedFace(new int[] { idx1, idx2, idx3 }));
                    }
                }
                Vertexes.Add(submodel);
                Faces.Add(faces);
            }
        }

        public override void Import(LabURI package, String? variant, Int32? layoutId)
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
                                var triIndices = new int[] { refIndex, refIndex + 1, refIndex + 2 };
                                faceList.Add(new IndexedFace(triIndices));
                            }
                            else
                            {
                                var triIndices = new int[] { refIndex + 1, refIndex, refIndex + 2 };
                                faceList.Add(new IndexedFace(triIndices));
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
                        ver.Normal.Normalize();
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
