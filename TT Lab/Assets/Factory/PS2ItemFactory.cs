using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.AssetData.Graphics.SubModels;
using Twinsanity.PS2Hardware;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Common.Lights;
using Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SubItems;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code.AgentLab;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;
using static Twinsanity.TwinsanityInterchange.Common.AgentLab.TwinBehaviourAssigner;

namespace TT_Lab.Assets.Factory
{
    public class PS2ItemFactory : ITwinItemFactory
    {
        public ITwinAIPath GenerateAIPath(Stream stream)
        {
            var aiPath = new PS2AnyAIPath();
            using var reader = new BinaryReader(stream);
            aiPath.Read(reader, (Int32)stream.Length);
            return aiPath;
        }

        public ITwinAIPosition GenerateAIPosition(Stream stream)
        {
            var aiPosition = new PS2AnyAIPosition();
            using var reader = new BinaryReader(stream);
            aiPosition.Read(reader, (Int32)stream.Length);
            return aiPosition;
        }

        public ITwinAnimation GenerateAnimation(Stream stream)
        {
            var animation = new PS2AnyAnimation();
            using var reader = new BinaryReader(stream);
            animation.TotalFrames = reader.ReadUInt16();
            animation.DefaultFPS = reader.ReadByte();
            animation.MainAnimation = new Twinsanity.TwinsanityInterchange.Common.Animation.TwinAnimation();
            animation.MainAnimation.Read(reader, (Int32)stream.Length);
            animation.FacialAnimation = new Twinsanity.TwinsanityInterchange.Common.Animation.TwinAnimation();
            animation.FacialAnimation.Read(reader, (Int32)stream.Length);
            animation.HasAnimationData = true;
            animation.HasFacialAnimationData = animation.FacialAnimation.TotalFrames != 0;
            return animation;
        }

        public ITwinBehaviourCommandsSequence GenerateBehaviourCommandsSequence(Stream stream)
        {
            var sequence = new PS2BehaviourCommandsSequence();
            using var reader = new StreamReader(stream);
            sequence.ReadText(reader);
            return sequence;
        }

        public ITwinBehaviourGraph GenerateBehaviourGraph(Stream stream)
        {
            var graph = new PS2BehaviourGraph();
            using var reader = new StreamReader(stream);
            graph.ReadText(reader);
            return graph;
        }

        public TwinBehaviourStarter GenerateBehaviourStarter(Stream stream)
        {
            var starter = new TwinBehaviourStarter();
            using var reader = new BinaryReader(stream);
            // HACK: Read the internal TwinBehaviourWrapper
            starter.Read(reader, (Int32)stream.Length);
            stream.Position = 4;

            starter.Assigners.Clear();
            var assignersAmount = reader.ReadUInt32();
            for (Int32 i = 0; i < assignersAmount; i++)
            {
                var assigner = new TwinBehaviourAssigner();
                assigner.Behaviour = reader.ReadInt32();
                assigner.Object = reader.ReadUInt16();
                assigner.AssignType = (AssignTypeID)reader.ReadUInt32();
                assigner.AssignLocality = (AssignLocalityID)reader.ReadUInt32();
                assigner.AssignStatus = (AssignStatusID)reader.ReadUInt32();
                assigner.AssignPreference = (AssignPreferenceID)reader.ReadUInt32();
            }
            return starter;
        }

        public ITwinBlendSkin GenerateBlendSkin(Stream stream)
        {
            throw new NotImplementedException();
        }

        public ITwinCamera GenerateCamera(Stream stream)
        {
            var camera = new PS2AnyCamera();
            using var reader = new BinaryReader(stream);
            camera.Read(reader, (Int32)stream.Length);
            return camera;
        }

        public ITwinCollision GenerateCollision(Stream stream)
        {
            throw new NotImplementedException();
        }

        public ITwinDynamicScenery GenerateDynamicScenery(Stream stream)
        {
            var dynamicScenery = new PS2AnyDynamicScenery();
            using var reader = new BinaryReader(stream);
            dynamicScenery.Read(reader, (Int32)stream.Length);
            return dynamicScenery;
        }

        public ITwinInstance GenerateInstance(Stream stream)
        {
            var instance = new PS2AnyInstance();
            using var reader = new BinaryReader(stream);
            instance.Read(reader, (Int32)stream.Length);
            return instance;
        }

        public ITwinLink GenerateLink(Stream stream)
        {
            var link = new PS2AnyLink();
            using var reader = new BinaryReader(stream);
            var linkAmount = reader.ReadInt32();
            for (Int32 i = 0; i < linkAmount; i++)
            {
                TwinChunkLink chunkLink = new();
                chunkLink.UnkFlag = reader.ReadBoolean();
                chunkLink.Path = reader.ReadString();
                chunkLink.IsRendered = reader.ReadBoolean();
                chunkLink.UnkNum = reader.ReadByte();
                chunkLink.IsLoadWallActive = reader.ReadBoolean();
                chunkLink.KeepLoaded = reader.ReadBoolean();
                chunkLink.ObjectMatrix.Read(reader, 0);
                chunkLink.ChunkMatrix.Read(reader, 0);
                if (reader.ReadBoolean())
                {
                    chunkLink.LoadingWall = new Matrix4();
                    chunkLink.LoadingWall.Read(reader, 0);
                }
                var buildersAmount = reader.ReadInt32();
                for (Int32 j = 0; j < buildersAmount; j++)
                {
                    var builder = new TwinChunkLinkBoundingBoxBuilder();
                    builder.Read(reader, 0);
                    chunkLink.ChunkLinksCollisionData.Add(builder);
                }
            }
            return link;
        }

        public ITwinLOD GenerateLOD(Stream stream)
        {
            var lod = new PS2AnyLOD();
            using var reader = new BinaryReader(stream);
            lod.Read(reader, (Int32)stream.Length);
            return lod;
        }

        public ITwinMaterial GenerateMaterial(Stream stream)
        {
            var material = new PS2AnyMaterial();
            using var reader = new BinaryReader(stream);
            material.Read(reader, (Int32)stream.Length);
            return material;
        }

        public ITwinMesh GenerateMesh(Stream stream)
        {
            var mesh = new PS2AnyMesh();
            using var reader = new BinaryReader(stream);
            mesh.Read(reader, (Int32)stream.Length);
            return mesh;
        }

        public ITwinModel GenerateModel(List<List<Vertex>> vertexes, List<List<IndexedFace>> faces)
        {
            var model = new PS2AnyModel();
            var vertexBatchIndex = 0;
            foreach (var subFaces in faces)
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
                var vertexBatch = vertexes[vertexBatchIndex++];
                var i = 0;
                foreach (var face in subFaces)
                {
                    i %= TwinVIFCompiler.VertexBatchAmount;

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
                    if (vertexBatch[face.Indexes[idx0]].HasNormals)
                    {
                        submodel.Normals.Add(vertexBatch[face.Indexes[idx0]].Normal);
                        submodel.Normals.Add(vertexBatch[face.Indexes[idx1]].Normal);
                        submodel.Normals.Add(vertexBatch[face.Indexes[2]].Normal);
                    }
                    if (vertexBatch[face.Indexes[idx0]].HasEmitColor)
                    {
                        submodel.EmitColor.Add(vertexBatch[face.Indexes[idx0]].EmitColor);
                        submodel.EmitColor.Add(vertexBatch[face.Indexes[idx1]].EmitColor);
                        submodel.EmitColor.Add(vertexBatch[face.Indexes[2]].EmitColor);
                    }
                    submodel.Connection.Add(false);
                    submodel.Connection.Add(false);
                    submodel.Connection.Add(true);

                    ++i;
                }
                model.SubModels.Add(submodel);
            }

            return model;
        }

        public ITwinObject GenerateObject(Stream stream)
        {
            throw new NotImplementedException();
        }

        public ITwinOGI GenerateOGI(Stream stream)
        {
            throw new NotImplementedException();
        }

        public ITwinParticle GenerateParticle(Stream stream)
        {
            var particles = new PS2AnyParticleData();
            using var reader = new BinaryReader(stream);
            particles.Read(reader, (Int32)stream.Length);
            return particles;
        }

        public ITwinPath GeneratePath(Stream stream)
        {
            var path = new PS2AnyPath();
            using var reader = new BinaryReader(stream);
            path.Read(reader, (Int32)stream.Length);
            return path;
        }

        public ITwinPosition GeneratePosition(Stream stream)
        {
            var position = new PS2AnyPosition();
            using var reader = new BinaryReader(stream);
            position.Read(reader, (Int32)stream.Length);
            return position;
        }

        public ITwinRigidModel GenerateRigidModel(Stream stream)
        {
            var rigidModel = new PS2AnyRigidModel();
            using var reader = new BinaryReader(stream);
            rigidModel.Read(reader, (Int32)stream.Length);
            return rigidModel;
        }

        public ITwinScenery GenerateScenery(Stream stream)
        {
            var scenery = new PS2AnyScenery();
            using var reader = new BinaryReader(stream);
            scenery.Name = reader.ReadString();
            scenery.UnkUInt = reader.ReadUInt32();
            scenery.UnkByte = reader.ReadByte();
            scenery.SkydomeID = reader.ReadUInt32();
            scenery.HasLighting = reader.ReadBoolean();
            if (scenery.HasLighting)
            {
                for (Int32 i = 0; i < scenery.UnkLightFlags.Length; i++)
                {
                    scenery.UnkLightFlags[i] = reader.ReadBoolean();
                }

                var ambientLights = reader.ReadInt32();
                for (var i = 0; i < ambientLights; ++i)
                {
                    var ambient = new AmbientLight();
                    ambient.Read(reader, ambient.GetLength());
                    scenery.AmbientLights.Add(ambient);
                }

                var dirLights = reader.ReadInt32();
                for (var i = 0; i < dirLights; ++i)
                {
                    var directional = new DirectionalLight();
                    directional.Read(reader, directional.GetLength());
                    scenery.DirectionalLights.Add(directional);
                }

                var pointLights = reader.ReadInt32();
                for (var i = 0; i < pointLights; ++i)
                {
                    var point = new PointLight();
                    point.Read(reader, point.GetLength());
                    scenery.PointLights.Add(point);
                }

                var negativeLights = reader.ReadInt32();
                for (var i = 0; i < negativeLights; ++i)
                {
                    var negative = new NegativeLight();
                    negative.Read(reader, negative.GetLength());
                    scenery.NegativeLights.Add(negative);
                }
            }

            var sceneries = reader.ReadInt32();
            for (Int32 i = 0; i < sceneries; i++)
            {
                var type = (ITwinScenery.SceneryType)reader.ReadInt32();
                switch (type)
                {
                    case ITwinScenery.SceneryType.Root:
                        var root = new TwinSceneryRoot();
                        root.Read(reader, 0);
                        scenery.Sceneries.Add(root);
                        break;
                    case ITwinScenery.SceneryType.Node:
                        var node = new TwinSceneryNode();
                        node.Read(reader, 0);
                        scenery.Sceneries.Add(node);
                        break;
                    case ITwinScenery.SceneryType.Leaf:
                        var leaf = new TwinSceneryLeaf();
                        leaf.Read(reader, 0);
                        scenery.Sceneries.Add(leaf);
                        break;
                }
            }

            return scenery;
        }

        public ITwinSkin GenerateSkin(List<SubSkinData> subskins)
        {
            PS2AnySkin skin = new();

            foreach (var subskin in subskins)
            {
                var ps2Subskin = new PS2SubSkin
                {
                    Vertexes = new(),
                    UVW = new(),
                    Colors = new(),
                    SkinJoints = new(),
                    Material = AssetManager.Get().GetAsset(subskin.Material).ID,
                };
                var vertexBatch = subskin.Vertexes;
                var i = 0;
                foreach (var face in subskin.Faces)
                {
                    i %= TwinVIFCompiler.VertexBatchAmount;

                    var idx0 = (i % 2 == 0) ? 0 : 1;
                    var idx1 = (i % 2 == 0) ? 1 : 0;
                    ps2Subskin.Vertexes.Add(new Vector4(vertexBatch[face.Indexes[idx0]].Position, 1f));
                    ps2Subskin.Vertexes.Add(new Vector4(vertexBatch[face.Indexes[idx1]].Position, 1f));
                    ps2Subskin.Vertexes.Add(new Vector4(vertexBatch[face.Indexes[2]].Position, 1f));
                    ps2Subskin.UVW.Add(new Vector4(vertexBatch[face.Indexes[idx0]].UV, 0f));
                    ps2Subskin.UVW.Add(new Vector4(vertexBatch[face.Indexes[idx1]].UV, 0f));
                    ps2Subskin.UVW.Add(new Vector4(vertexBatch[face.Indexes[2]].UV, 0f));
                    ps2Subskin.Colors.Add(vertexBatch[face.Indexes[idx0]].Color);
                    ps2Subskin.Colors.Add(vertexBatch[face.Indexes[idx1]].Color);
                    ps2Subskin.Colors.Add(vertexBatch[face.Indexes[2]].Color);
                    ps2Subskin.SkinJoints.Add(vertexBatch[face.Indexes[idx0]].JointInfo);
                    ps2Subskin.SkinJoints.Add(vertexBatch[face.Indexes[idx1]].JointInfo);
                    vertexBatch[face.Indexes[2]].JointInfo.Connection = true;
                    ps2Subskin.SkinJoints.Add(vertexBatch[face.Indexes[2]].JointInfo);

                    ++i;
                }
                skin.SubSkins.Add(ps2Subskin);
            }
            return skin;
        }

        public ITwinSkydome GenerateSkydome(Stream stream)
        {
            var skydome = new PS2AnySkydome();
            using var reader = new BinaryReader(stream);
            skydome.Read(reader, (Int32)stream.Length);
            return skydome;
        }

        public ITwinSound GenerateSound()
        {
            return new PS2AnySound();
        }

        public ITwinSurface GenerateSurface(Stream stream)
        {
            var surface = new PS2AnyCollisionSurface();
            using var reader = new BinaryReader(stream);
            surface.Read(reader, (Int32)stream.Length);
            return surface;
        }

        public ITwinTemplate GenerateTemplate(Stream stream)
        {
            var template = new PS2AnyTemplate();
            using var reader = new BinaryReader(stream);
            template.Read(reader, (Int32)stream.Length);
            return template;
        }

        public ITwinTexture GenerateTexture()
        {
            return new PS2AnyTexture();
        }

        public ITwinTrigger GenerateTrigger(Stream stream)
        {
            var trigger = new PS2AnyTrigger();
            using var reader = new BinaryReader(stream);
            trigger.Read(reader, (Int32)stream.Length);
            return trigger;
        }
    }
}
