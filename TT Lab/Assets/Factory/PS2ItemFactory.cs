using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.AssetData.Graphics.SubModels;
using TT_Lab.Util;
using Twinsanity.PS2Hardware;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Common.Lights;
using Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.PS2;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SubItems;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.RM2;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code.AgentLab;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;
using static Twinsanity.TwinsanityInterchange.Common.AgentLab.TwinBehaviourAssigner;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.RM2.Code;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.RM2.Layout;

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

        public ITwinBlendSkin GenerateBlendSkin(Int32 blendsAmount, List<SubBlendData> blends)
        {
            var assetManager = AssetManager.Get();
            var blendSkin = new PS2AnyBlendSkin
            {
                BlendsAmount = blendsAmount
            };

            foreach (var blend in blends)
            {
                var subBlend = new PS2SubBlendSkin(blendsAmount)
                {
                    Material = assetManager.GetAsset(blend.Material).ID
                };

                foreach (var model in blend.Models)
                {
                    var submodel = new PS2BlendSkinModel(blendsAmount)
                    {
                        Vertexes = new(),
                        UVW = new(),
                        Colors = new(),
                        SkinJoints = new(),
                        Faces = new()
                    };
                    var vertexBatch = model.Vertexes;
                    var i = 0;
                    foreach (var face in model.Faces)
                    {
                        i %= TwinVIFCompiler.VertexBatchAmount;

                        var idx0 = (i % 2 == 0) ? 0 : 1;
                        var idx1 = (i % 2 == 0) ? 1 : 0;
                        submodel.Vertexes.Add(new Vector4(vertexBatch[face.Indexes![idx0]].Position, 1f));
                        submodel.Vertexes.Add(new Vector4(vertexBatch[face.Indexes[idx1]].Position, 1f));
                        submodel.Vertexes.Add(new Vector4(vertexBatch[face.Indexes[2]].Position, 1f));
                        submodel.UVW.Add(new Vector4(vertexBatch[face.Indexes[idx0]].UV, 0f));
                        submodel.UVW.Add(new Vector4(vertexBatch[face.Indexes[idx1]].UV, 0f));
                        submodel.UVW.Add(new Vector4(vertexBatch[face.Indexes[2]].UV, 0f));
                        submodel.Colors.Add(vertexBatch[face.Indexes[idx0]].Color);
                        submodel.Colors.Add(vertexBatch[face.Indexes[idx1]].Color);
                        submodel.Colors.Add(vertexBatch[face.Indexes[2]].Color);
                        submodel.SkinJoints.Add(vertexBatch[face.Indexes[idx0]].JointInfo);
                        submodel.SkinJoints.Add(vertexBatch[face.Indexes[idx1]].JointInfo);
                        vertexBatch[face.Indexes[2]].JointInfo.Connection = true;
                        submodel.SkinJoints.Add(vertexBatch[face.Indexes[2]].JointInfo);

                        ++i;
                    }

                    foreach (var blendFace in model.BlendFaces)
                    {
                        var ps2BlendFace = new PS2BlendSkinFace(model.BlendShape)
                        {
                            VertexesAmount = blendFace.VertexesAmount,
                            Vertices = new()
                        };
                        foreach (var ver in blendFace.BlendShapes)
                        {
                            ps2BlendFace.Vertices.Add(new VertexBlendShape
                            {
                                BlendShape = model.BlendShape,
                                Offset = ver.Offset
                            });
                        }
                        submodel.Faces.Add(ps2BlendFace);
                    }

                    subBlend.Models.Add(submodel);
                }
            }

            return blendSkin;
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
            var collision = new PS2AnyCollisionData();
            using var reader = new BinaryReader(stream);
            collision.Read(reader, (Int32)stream.Length);
            return collision;
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
                chunkLink.ObjectMatrix.Read(reader, Constants.SIZE_MATRIX4);
                chunkLink.ChunkMatrix.Read(reader, Constants.SIZE_MATRIX4);
                if (reader.ReadBoolean())
                {
                    chunkLink.LoadingWall = new Matrix4();
                    chunkLink.LoadingWall.Read(reader, Constants.SIZE_MATRIX4);
                }
                var buildersAmount = reader.ReadInt32();
                for (Int32 j = 0; j < buildersAmount; j++)
                {
                    var builder = new TwinChunkLinkBoundingBoxBuilder();
                    builder.Read(reader, (Int32)stream.Length);
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
                    submodel.Vertexes.Add(new Vector4(vertexBatch[face.Indexes![idx0]].Position, 1f));
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
            var gameObject = new PS2AnyObject();
            using var reader = new BinaryReader(stream);
            gameObject.Type = (ITwinObject.ObjectType)reader.ReadInt32();
            gameObject.UnkTypeValue = reader.ReadByte();
            gameObject.ReactJointAmount = reader.ReadByte();
            gameObject.ExitPointAmount = reader.ReadByte();
            gameObject.SlotsMap = reader.ReadBytes(8);
            gameObject.Name = reader.ReadString();

            var triggerBehaviours = reader.ReadInt32();
            for (Int32 i = 0; i < triggerBehaviours; i++)
            {
                var triggerBehaviour = new TwinObjectTriggerBehaviour();
                triggerBehaviour.TriggerBehaviour = reader.ReadUInt16();
                triggerBehaviour.UnkTriggerValue = reader.ReadUInt16();
                triggerBehaviour.BehaviourCallerIndex = reader.ReadByte();
                gameObject.TriggerBehaviours.Add(triggerBehaviour);
            }

            void fillList<T>(IList<T> list, Func<T> readerFunc)
            {
                var amount = reader.ReadInt32();
                for (Int32 i = 0; i < amount; i++)
                {
                    list.Add(readerFunc());
                }
            }
            fillList(gameObject.OGISlots, reader.ReadUInt16);
            fillList(gameObject.AnimationSlots, reader.ReadUInt16);
            fillList(gameObject.BehaviourSlots, reader.ReadUInt16);
            fillList(gameObject.ObjectSlots, reader.ReadUInt16);
            fillList(gameObject.SoundSlots, reader.ReadUInt16);

            gameObject.InstanceStateFlags = reader.ReadUInt32();

            fillList(gameObject.InstFlags, reader.ReadUInt32);
            fillList(gameObject.InstFloats, reader.ReadSingle);
            fillList(gameObject.InstIntegers, reader.ReadUInt32);

            fillList(gameObject.RefObjects, reader.ReadUInt16);
            fillList(gameObject.RefOGIs, reader.ReadUInt16);
            fillList(gameObject.RefAnimations, reader.ReadUInt16);
            fillList(gameObject.RefCodeModels, reader.ReadUInt16);
            fillList(gameObject.RefBehaviours, reader.ReadUInt16);
            fillList(gameObject.RefUnknowns, reader.ReadUInt16);
            fillList(gameObject.RefSounds, reader.ReadUInt16);

            gameObject.BehaviourPack = new PS2BehaviourCommandPack();
            gameObject.BehaviourPack.Read(reader, (Int32)stream.Length);

            return gameObject;
        }

        public ITwinOGI GenerateOGI(Stream stream)
        {
            var ogi = new PS2AnyOGI();
            using var reader = new BinaryReader(stream);
            ogi.BoundingBox[0].Read(reader, Constants.SIZE_VECTOR4);
            ogi.BoundingBox[1].Read(reader, Constants.SIZE_VECTOR4);

            var jointIndices = reader.ReadInt32();
            for (Int32 i = 0; i < jointIndices; i++)
            {
                ogi.JointIndices.Add(reader.ReadByte());
            }

            var joints = reader.ReadInt32();
            for (Int32 i = 0; i < joints; i++)
            {
                var joint = new TwinJoint();
                joint.Read(reader, Constants.SIZE_JOINT);
                ogi.Joints.Add(joint);
            }

            var rigidModels = reader.ReadInt32();
            for (Int32 i = 0; i < rigidModels; i++)
            {
                ogi.RigidModelIds.Add(reader.ReadUInt32());
            }

            var exitPoints = reader.ReadInt32();
            for (Int32 i = 0; i < exitPoints; i++)
            {
                var exitPoint = new TwinExitPoint();
                exitPoint.Read(reader, Constants.SIZE_EXIT_POINT);
                ogi.ExitPoints.Add(exitPoint);
            }

            var matrices = reader.ReadInt32();
            for (Int32 i = 0; i < matrices; i++)
            {
                var mat = new Matrix4();
                mat.Read(reader, Constants.SIZE_MATRIX4);
                ogi.SkinInverseBindMatrices.Add(mat);
            }

            var builders = reader.ReadInt32();
            for (Int32 i = 0; i < builders; i++)
            {
                var builder = new TwinBoundingBoxBuilder();
                builder.Read(reader, (Int32)stream.Length);
                ogi.Collisions.Add(builder);
            }

            var jointToBuilders = reader.ReadInt32();
            for (Int32 i = 0; i < jointToBuilders; i++)
            {
                ogi.CollisionJointIndices.Add(reader.ReadByte());
            }

            ogi.SkinID = reader.ReadUInt32();
            ogi.BlendSkinID = reader.ReadUInt32();

            return ogi;
        }

        public ITwinParticle GenerateParticle(Stream stream)
        {
            var particles = new PS2AnyParticleData();
            using var reader = new BinaryReader(stream);
            particles.Read(reader, (Int32)stream.Length);
            return particles;
        }

        public ITwinDefaultParticle GenerateDefaultParticle(Stream stream)
        {
            var particles = new PS2DefaultParticleData();
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
                    ps2Subskin.Vertexes.Add(new Vector4(vertexBatch[face.Indexes![idx0]].Position, 1f));
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

        public ITwinSection GenerateFrontend(List<ITwinSound> sounds)
        {
            var frontend = new PS2Frontend();

            foreach (var sound in sounds)
            {
                frontend.AddItem(sound);
            }

            return frontend;
        }

        public ITwinPSF GenerateFont(List<ITwinPTC> pages, List<Vector4> unkVecs, Int32 unkInt)
        {
            var font = new PS2PSF();

            foreach (var page in pages)
            {
                font.FontPages.Add(page);
            }

            font.UnkVecs = CloneUtils.CloneList(unkVecs);
            font.UnkInt = unkInt;

            return font;
        }

        public ITwinPTC GeneratePTC(UInt32 texID, UInt32 matID, ITwinTexture texture, ITwinMaterial material)
        {
            var ptc = new PS2PTC
            {
                TexID = texID,
                MatID = matID,
                Texture = texture,
                Material = material
            };
            return ptc;
        }

        public ITwinPSM GeneratePSM(List<ITwinPTC> ptcs)
        {
            var psm = new PS2PSM();
            foreach (var ptc in ptcs)
            {
                psm.PTCs.Add(ptc);
            }

            return psm;
        }

        public ITwinSection GenerateDefault()
        {
            var @default = new PS2Default();
            var graphics = new PS2AnyGraphicsSection();
            graphics.SetID(Constants.LEVEL_GRAPHICS_SECTION);
            {
                var textures = new PS2AnyTexturesSection();
                textures.SetID(Constants.GRAPHICS_TEXTURES_SECTION);
                var materials = new PS2AnyMaterialsSection();
                materials.SetID(Constants.GRAPHICS_MATERIALS_SECTION);
                var models = new PS2AnyModelsSection();
                models.SetID(Constants.GRAPHICS_MODELS_SECTION);
                var rigids = new PS2AnyRigidModelsSection();
                rigids.SetID(Constants.GRAPHICS_RIGID_MODELS_SECTION);
                var skins = new PS2AnySkinsSection();
                skins.SetID(Constants.GRAPHICS_SKINS_SECTION);
                var blendSkins = new PS2AnyBlendSkinsSection();
                blendSkins.SetID(Constants.GRAPHICS_BLEND_SKINS_SECTION);
                var meshes = new PS2AnyMeshesSection();
                meshes.SetID(Constants.GRAPHICS_MESHES_SECTION);
                var lods = new PS2AnyLODsSection();
                lods.SetID(Constants.GRAPHICS_LODS_SECTION);
                var skydomes = new PS2AnySkydomesSection();
                skydomes.SetID(Constants.GRAPHICS_SKYDOMES_SECTION);
                graphics.AddItem(textures);
                graphics.AddItem(materials);
                graphics.AddItem(models);
                graphics.AddItem(rigids);
                graphics.AddItem(skins);
                graphics.AddItem(blendSkins);
                graphics.AddItem(meshes);
                graphics.AddItem(lods);
                graphics.AddItem(skydomes);
            }
            var code = new PS2AnyCodeSection();
            code.SetID(Constants.LEVEL_CODE_SECTION);
            {
                var objects = new PS2AnyGameObjectsSection();
                objects.SetID(Constants.CODE_GAME_OBJECTS_SECTION);
                var behaviours = new PS2AnyBehavioursSection();
                behaviours.SetID(Constants.CODE_BEHAVIOURS_SECTION);
                var animations = new PS2AnyAnimationsSection();
                animations.SetID(Constants.CODE_ANIMATIONS_SECTION);
                var ogis = new PS2AnyOGIsSection();
                ogis.SetID(Constants.CODE_OGIS_SECTION);
                var behaviourSequences = new PS2AnyBehaviourCommandsSequencesSection();
                behaviourSequences.SetID(Constants.CODE_BEHAVIOUR_COMMANDS_SEQUENCES_SECTION);
                var unknowns = new BaseTwinSection();
                unknowns.SetID(Constants.CODE_UNK_ITEM);
                var sfxs = new PS2AnySoundsSection();
                sfxs.SetID(Constants.CODE_SOUND_EFFECTS_SECTION);
                var sfxsEn = new PS2AnySoundsSection();
                sfxsEn.SetID(Constants.CODE_LANG_ENG_SECTION);
                var sfxsFr = new PS2AnySoundsSection();
                sfxsFr.SetID(Constants.CODE_LANG_FRE_SECTION);
                var sfxsGr = new PS2AnySoundsSection();
                sfxsGr.SetID(Constants.CODE_LANG_GER_SECTION);
                var sfxsSp = new PS2AnySoundsSection();
                sfxsSp.SetID(Constants.CODE_LANG_SPA_SECTION);
                var sfxsIt = new PS2AnySoundsSection();
                sfxsIt.SetID(Constants.CODE_LANG_ITA_SECTION);
                var sfxsJp = new PS2AnySoundsSection();
                sfxsJp.SetID(Constants.CODE_LANG_JPN_SECTION);
                code.AddItem(objects);
                code.AddItem(behaviours);
                code.AddItem(animations);
                code.AddItem(ogis);
                code.AddItem(behaviourSequences);
                code.AddItem(unknowns);
                code.AddItem(sfxs);
                code.AddItem(sfxsEn);
                code.AddItem(sfxsFr);
                code.AddItem(sfxsGr);
                code.AddItem(sfxsSp);
                code.AddItem(sfxsIt);
                code.AddItem(sfxsJp);
            }
            var layout1 = new PS2AnyLayoutSection();
            layout1.SetID(Constants.LEVEL_LAYOUT_1_SECTION);
            {
                var templates = new PS2AnyTemplatesSection();
                templates.SetID(Constants.LAYOUT_TEMPLATES_SECTION);
                var aiPositions = new PS2AnyAIPositionsSection();
                aiPositions.SetID(Constants.LAYOUT_AI_POSITIONS_SECTION);
                var aiPaths = new PS2AnyAIPathsSection();
                aiPaths.SetID(Constants.LAYOUT_AI_PATHS_SECTION);
                var positions = new PS2AnyPositionsSection();
                positions.SetID(Constants.LAYOUT_POSITIONS_SECTION);
                var paths = new PS2AnyPathsSection();
                paths.SetID(Constants.LAYOUT_PATHS_SECTION);
                var surfaces = new PS2AnySurfacesSection();
                surfaces.SetID(Constants.LAYOUT_SURFACES_SECTION);
                var instances = new PS2AnyInstancesSection();
                instances.SetID(Constants.LAYOUT_INSTANCES_SECTION);
                var triggers = new PS2AnyTriggersSection();
                triggers.SetID(Constants.LAYOUT_TRIGGERS_SECTION);
                var cameras = new PS2AnyCamerasSection();
                cameras.SetID(Constants.LAYOUT_CAMERAS_SECTION);
                layout1.AddItem(templates);
                layout1.AddItem(aiPositions);
                layout1.AddItem(aiPaths);
                layout1.AddItem(positions);
                layout1.AddItem(paths);
                layout1.AddItem(surfaces);
                layout1.AddItem(instances);
                layout1.AddItem(triggers);
                layout1.AddItem(cameras);
            }
            var layout2 = new PS2AnyLayoutSection();
            layout2.SetID(Constants.LEVEL_LAYOUT_2_SECTION);
            {
                var templates = new PS2AnyTemplatesSection();
                templates.SetID(Constants.LAYOUT_TEMPLATES_SECTION);
                var aiPositions = new PS2AnyAIPositionsSection();
                aiPositions.SetID(Constants.LAYOUT_AI_POSITIONS_SECTION);
                var aiPaths = new PS2AnyAIPathsSection();
                aiPaths.SetID(Constants.LAYOUT_AI_PATHS_SECTION);
                var positions = new PS2AnyPositionsSection();
                positions.SetID(Constants.LAYOUT_POSITIONS_SECTION);
                var paths = new PS2AnyPathsSection();
                paths.SetID(Constants.LAYOUT_PATHS_SECTION);
                var surfaces = new PS2AnySurfacesSection();
                surfaces.SetID(Constants.LAYOUT_SURFACES_SECTION);
                var instances = new PS2AnyInstancesSection();
                instances.SetID(Constants.LAYOUT_INSTANCES_SECTION);
                var triggers = new PS2AnyTriggersSection();
                triggers.SetID(Constants.LAYOUT_TRIGGERS_SECTION);
                var cameras = new PS2AnyCamerasSection();
                cameras.SetID(Constants.LAYOUT_CAMERAS_SECTION);
                layout2.AddItem(templates);
                layout2.AddItem(aiPositions);
                layout2.AddItem(aiPaths);
                layout2.AddItem(positions);
                layout2.AddItem(paths);
                layout2.AddItem(surfaces);
                layout2.AddItem(instances);
                layout2.AddItem(triggers);
                layout2.AddItem(cameras);
            }
            var layout3 = new PS2AnyLayoutSection();
            layout3.SetID(Constants.LEVEL_LAYOUT_3_SECTION);
            {
                var templates = new PS2AnyTemplatesSection();
                templates.SetID(Constants.LAYOUT_TEMPLATES_SECTION);
                var aiPositions = new PS2AnyAIPositionsSection();
                aiPositions.SetID(Constants.LAYOUT_AI_POSITIONS_SECTION);
                var aiPaths = new PS2AnyAIPathsSection();
                aiPaths.SetID(Constants.LAYOUT_AI_PATHS_SECTION);
                var positions = new PS2AnyPositionsSection();
                positions.SetID(Constants.LAYOUT_POSITIONS_SECTION);
                var paths = new PS2AnyPathsSection();
                paths.SetID(Constants.LAYOUT_PATHS_SECTION);
                var surfaces = new PS2AnySurfacesSection();
                surfaces.SetID(Constants.LAYOUT_SURFACES_SECTION);
                var instances = new PS2AnyInstancesSection();
                instances.SetID(Constants.LAYOUT_INSTANCES_SECTION);
                var triggers = new PS2AnyTriggersSection();
                triggers.SetID(Constants.LAYOUT_TRIGGERS_SECTION);
                var cameras = new PS2AnyCamerasSection();
                cameras.SetID(Constants.LAYOUT_CAMERAS_SECTION);
                layout3.AddItem(templates);
                layout3.AddItem(aiPositions);
                layout3.AddItem(aiPaths);
                layout3.AddItem(positions);
                layout3.AddItem(paths);
                layout3.AddItem(surfaces);
                layout3.AddItem(instances);
                layout3.AddItem(triggers);
                layout3.AddItem(cameras);
            }
            var layout4 = new PS2AnyLayoutSection();
            layout4.SetID(Constants.LEVEL_LAYOUT_4_SECTION);
            {
                var templates = new PS2AnyTemplatesSection();
                templates.SetID(Constants.LAYOUT_TEMPLATES_SECTION);
                var aiPositions = new PS2AnyAIPositionsSection();
                aiPositions.SetID(Constants.LAYOUT_AI_POSITIONS_SECTION);
                var aiPaths = new PS2AnyAIPathsSection();
                aiPaths.SetID(Constants.LAYOUT_AI_PATHS_SECTION);
                var positions = new PS2AnyPositionsSection();
                positions.SetID(Constants.LAYOUT_POSITIONS_SECTION);
                var paths = new PS2AnyPathsSection();
                paths.SetID(Constants.LAYOUT_PATHS_SECTION);
                var surfaces = new PS2AnySurfacesSection();
                surfaces.SetID(Constants.LAYOUT_SURFACES_SECTION);
                var instances = new PS2AnyInstancesSection();
                instances.SetID(Constants.LAYOUT_INSTANCES_SECTION);
                var triggers = new PS2AnyTriggersSection();
                triggers.SetID(Constants.LAYOUT_TRIGGERS_SECTION);
                var cameras = new PS2AnyCamerasSection();
                cameras.SetID(Constants.LAYOUT_CAMERAS_SECTION);
                layout4.AddItem(templates);
                layout4.AddItem(aiPositions);
                layout4.AddItem(aiPaths);
                layout4.AddItem(positions);
                layout4.AddItem(paths);
                layout4.AddItem(surfaces);
                layout4.AddItem(instances);
                layout4.AddItem(triggers);
                layout4.AddItem(cameras);
            }
            var layout5 = new PS2AnyLayoutSection();
            layout5.SetID(Constants.LEVEL_LAYOUT_5_SECTION);
            {
                var templates = new PS2AnyTemplatesSection();
                templates.SetID(Constants.LAYOUT_TEMPLATES_SECTION);
                var aiPositions = new PS2AnyAIPositionsSection();
                aiPositions.SetID(Constants.LAYOUT_AI_POSITIONS_SECTION);
                var aiPaths = new PS2AnyAIPathsSection();
                aiPaths.SetID(Constants.LAYOUT_AI_PATHS_SECTION);
                var positions = new PS2AnyPositionsSection();
                positions.SetID(Constants.LAYOUT_POSITIONS_SECTION);
                var paths = new PS2AnyPathsSection();
                paths.SetID(Constants.LAYOUT_PATHS_SECTION);
                var surfaces = new PS2AnySurfacesSection();
                surfaces.SetID(Constants.LAYOUT_SURFACES_SECTION);
                var instances = new PS2AnyInstancesSection();
                instances.SetID(Constants.LAYOUT_INSTANCES_SECTION);
                var triggers = new PS2AnyTriggersSection();
                triggers.SetID(Constants.LAYOUT_TRIGGERS_SECTION);
                var cameras = new PS2AnyCamerasSection();
                cameras.SetID(Constants.LAYOUT_CAMERAS_SECTION);
                layout5.AddItem(templates);
                layout5.AddItem(aiPositions);
                layout5.AddItem(aiPaths);
                layout5.AddItem(positions);
                layout5.AddItem(paths);
                layout5.AddItem(surfaces);
                layout5.AddItem(instances);
                layout5.AddItem(triggers);
                layout5.AddItem(cameras);
            }
            var layout6 = new PS2AnyLayoutSection();
            layout6.SetID(Constants.LEVEL_LAYOUT_6_SECTION);
            {
                var templates = new PS2AnyTemplatesSection();
                templates.SetID(Constants.LAYOUT_TEMPLATES_SECTION);
                var aiPositions = new PS2AnyAIPositionsSection();
                aiPositions.SetID(Constants.LAYOUT_AI_POSITIONS_SECTION);
                var aiPaths = new PS2AnyAIPathsSection();
                aiPaths.SetID(Constants.LAYOUT_AI_PATHS_SECTION);
                var positions = new PS2AnyPositionsSection();
                positions.SetID(Constants.LAYOUT_POSITIONS_SECTION);
                var paths = new PS2AnyPathsSection();
                paths.SetID(Constants.LAYOUT_PATHS_SECTION);
                var surfaces = new PS2AnySurfacesSection();
                surfaces.SetID(Constants.LAYOUT_SURFACES_SECTION);
                var instances = new PS2AnyInstancesSection();
                instances.SetID(Constants.LAYOUT_INSTANCES_SECTION);
                var triggers = new PS2AnyTriggersSection();
                triggers.SetID(Constants.LAYOUT_TRIGGERS_SECTION);
                var cameras = new PS2AnyCamerasSection();
                cameras.SetID(Constants.LAYOUT_CAMERAS_SECTION);
                layout6.AddItem(templates);
                layout6.AddItem(aiPositions);
                layout6.AddItem(aiPaths);
                layout6.AddItem(positions);
                layout6.AddItem(paths);
                layout6.AddItem(surfaces);
                layout6.AddItem(instances);
                layout6.AddItem(triggers);
                layout6.AddItem(cameras);
            }
            var layout7 = new PS2AnyLayoutSection();
            layout7.SetID(Constants.LEVEL_LAYOUT_7_SECTION);
            {
                var templates = new PS2AnyTemplatesSection();
                templates.SetID(Constants.LAYOUT_TEMPLATES_SECTION);
                var aiPositions = new PS2AnyAIPositionsSection();
                aiPositions.SetID(Constants.LAYOUT_AI_POSITIONS_SECTION);
                var aiPaths = new PS2AnyAIPathsSection();
                aiPaths.SetID(Constants.LAYOUT_AI_PATHS_SECTION);
                var positions = new PS2AnyPositionsSection();
                positions.SetID(Constants.LAYOUT_POSITIONS_SECTION);
                var paths = new PS2AnyPathsSection();
                paths.SetID(Constants.LAYOUT_PATHS_SECTION);
                var surfaces = new PS2AnySurfacesSection();
                surfaces.SetID(Constants.LAYOUT_SURFACES_SECTION);
                var instances = new PS2AnyInstancesSection();
                instances.SetID(Constants.LAYOUT_INSTANCES_SECTION);
                var triggers = new PS2AnyTriggersSection();
                triggers.SetID(Constants.LAYOUT_TRIGGERS_SECTION);
                var cameras = new PS2AnyCamerasSection();
                cameras.SetID(Constants.LAYOUT_CAMERAS_SECTION);
                layout7.AddItem(templates);
                layout7.AddItem(aiPositions);
                layout7.AddItem(aiPaths);
                layout7.AddItem(positions);
                layout7.AddItem(paths);
                layout7.AddItem(surfaces);
                layout7.AddItem(instances);
                layout7.AddItem(triggers);
                layout7.AddItem(cameras);
            }
            var layout8 = new PS2AnyLayoutSection();
            layout8.SetID(Constants.LEVEL_LAYOUT_8_SECTION);
            {
                var templates = new PS2AnyTemplatesSection();
                templates.SetID(Constants.LAYOUT_TEMPLATES_SECTION);
                var aiPositions = new PS2AnyAIPositionsSection();
                aiPositions.SetID(Constants.LAYOUT_AI_POSITIONS_SECTION);
                var aiPaths = new PS2AnyAIPathsSection();
                aiPaths.SetID(Constants.LAYOUT_AI_PATHS_SECTION);
                var positions = new PS2AnyPositionsSection();
                positions.SetID(Constants.LAYOUT_POSITIONS_SECTION);
                var paths = new PS2AnyPathsSection();
                paths.SetID(Constants.LAYOUT_PATHS_SECTION);
                var surfaces = new PS2AnySurfacesSection();
                surfaces.SetID(Constants.LAYOUT_SURFACES_SECTION);
                var instances = new PS2AnyInstancesSection();
                instances.SetID(Constants.LAYOUT_INSTANCES_SECTION);
                var triggers = new PS2AnyTriggersSection();
                triggers.SetID(Constants.LAYOUT_TRIGGERS_SECTION);
                var cameras = new PS2AnyCamerasSection();
                cameras.SetID(Constants.LAYOUT_CAMERAS_SECTION);
                layout8.AddItem(templates);
                layout8.AddItem(aiPositions);
                layout8.AddItem(aiPaths);
                layout8.AddItem(positions);
                layout8.AddItem(paths);
                layout8.AddItem(surfaces);
                layout8.AddItem(instances);
                layout8.AddItem(triggers);
                layout8.AddItem(cameras);
            }
            var particle = new PS2DefaultParticleData();
            particle.SetID(Constants.LEVEL_PARTICLES_ITEM);
            var collision = new BaseTwinSection();
            collision.SetID(Constants.LEVEL_COLLISION_ITEM);

            @default.AddItem(graphics);
            @default.AddItem(code);
            @default.AddItem(particle);
            @default.AddItem(collision);
            @default.AddItem(layout1);
            @default.AddItem(layout2);
            @default.AddItem(layout3);
            @default.AddItem(layout4);
            @default.AddItem(layout5);
            @default.AddItem(layout6);
            @default.AddItem(layout7);
            @default.AddItem(layout8);
            return @default;
        }

        public ITwinSection GenerateRM2()
        {
            var rm2 = new PS2AnyTwinsanityRM2();
            var graphics = new PS2AnyGraphicsSection();
            graphics.SetID(Constants.LEVEL_GRAPHICS_SECTION);
            {
                var textures = new PS2AnyTexturesSection();
                textures.SetID(Constants.GRAPHICS_TEXTURES_SECTION);
                var materials = new PS2AnyMaterialsSection();
                materials.SetID(Constants.GRAPHICS_MATERIALS_SECTION);
                var models = new PS2AnyModelsSection();
                models.SetID(Constants.GRAPHICS_MODELS_SECTION);
                var rigids = new PS2AnyRigidModelsSection();
                rigids.SetID(Constants.GRAPHICS_RIGID_MODELS_SECTION);
                var skins = new PS2AnySkinsSection();
                skins.SetID(Constants.GRAPHICS_SKINS_SECTION);
                var blendSkins = new PS2AnyBlendSkinsSection();
                blendSkins.SetID(Constants.GRAPHICS_BLEND_SKINS_SECTION);
                var meshes = new PS2AnyMeshesSection();
                meshes.SetID(Constants.GRAPHICS_MESHES_SECTION);
                var lods = new PS2AnyLODsSection();
                lods.SetID(Constants.GRAPHICS_LODS_SECTION);
                var skydomes = new PS2AnySkydomesSection();
                skydomes.SetID(Constants.GRAPHICS_SKYDOMES_SECTION);
                graphics.AddItem(textures);
                graphics.AddItem(materials);
                graphics.AddItem(models);
                graphics.AddItem(rigids);
                graphics.AddItem(skins);
                graphics.AddItem(blendSkins);
                graphics.AddItem(meshes);
                graphics.AddItem(lods);
                graphics.AddItem(skydomes);
            }
            var code = new PS2AnyCodeSection();
            code.SetID(Constants.LEVEL_CODE_SECTION);
            {
                var objects = new PS2AnyGameObjectsSection();
                objects.SetID(Constants.CODE_GAME_OBJECTS_SECTION);
                var behaviours = new PS2AnyBehavioursSection();
                behaviours.SetID(Constants.CODE_BEHAVIOURS_SECTION);
                var animations = new PS2AnyAnimationsSection();
                animations.SetID(Constants.CODE_ANIMATIONS_SECTION);
                var ogis = new PS2AnyOGIsSection();
                ogis.SetID(Constants.CODE_OGIS_SECTION);
                var behaviourSequences = new PS2AnyBehaviourCommandsSequencesSection();
                behaviourSequences.SetID(Constants.CODE_BEHAVIOUR_COMMANDS_SEQUENCES_SECTION);
                var unknowns = new BaseTwinSection();
                unknowns.SetID(Constants.CODE_UNK_ITEM);
                var sfxs = new PS2AnySoundsSection();
                sfxs.SetID(Constants.CODE_SOUND_EFFECTS_SECTION);
                var sfxsEn = new PS2AnySoundsSection();
                sfxsEn.SetID(Constants.CODE_LANG_ENG_SECTION);
                var sfxsFr = new PS2AnySoundsSection();
                sfxsFr.SetID(Constants.CODE_LANG_FRE_SECTION);
                var sfxsGr = new PS2AnySoundsSection();
                sfxsGr.SetID(Constants.CODE_LANG_GER_SECTION);
                var sfxsSp = new PS2AnySoundsSection();
                sfxsSp.SetID(Constants.CODE_LANG_SPA_SECTION);
                var sfxsIt = new PS2AnySoundsSection();
                sfxsIt.SetID(Constants.CODE_LANG_ITA_SECTION);
                var sfxsJp = new PS2AnySoundsSection();
                sfxsJp.SetID(Constants.CODE_LANG_JPN_SECTION);
                code.AddItem(objects);
                code.AddItem(behaviours);
                code.AddItem(animations);
                code.AddItem(ogis);
                code.AddItem(behaviourSequences);
                code.AddItem(unknowns);
                code.AddItem(sfxs);
                code.AddItem(sfxsEn);
                code.AddItem(sfxsFr);
                code.AddItem(sfxsGr);
                code.AddItem(sfxsSp);
                code.AddItem(sfxsIt);
                code.AddItem(sfxsJp);
            }
            var layout1 = new PS2AnyLayoutSection();
            layout1.SetID(Constants.LEVEL_LAYOUT_1_SECTION);
            {
                var templates = new PS2AnyTemplatesSection();
                templates.SetID(Constants.LAYOUT_TEMPLATES_SECTION);
                var aiPositions = new PS2AnyAIPositionsSection();
                aiPositions.SetID(Constants.LAYOUT_AI_POSITIONS_SECTION);
                var aiPaths = new PS2AnyAIPathsSection();
                aiPaths.SetID(Constants.LAYOUT_AI_PATHS_SECTION);
                var positions = new PS2AnyPositionsSection();
                positions.SetID(Constants.LAYOUT_POSITIONS_SECTION);
                var paths = new PS2AnyPathsSection();
                paths.SetID(Constants.LAYOUT_PATHS_SECTION);
                var surfaces = new PS2AnySurfacesSection();
                surfaces.SetID(Constants.LAYOUT_SURFACES_SECTION);
                var instances = new PS2AnyInstancesSection();
                instances.SetID(Constants.LAYOUT_INSTANCES_SECTION);
                var triggers = new PS2AnyTriggersSection();
                triggers.SetID(Constants.LAYOUT_TRIGGERS_SECTION);
                var cameras = new PS2AnyCamerasSection();
                cameras.SetID(Constants.LAYOUT_CAMERAS_SECTION);
                layout1.AddItem(templates);
                layout1.AddItem(aiPositions);
                layout1.AddItem(aiPaths);
                layout1.AddItem(positions);
                layout1.AddItem(paths);
                layout1.AddItem(surfaces);
                layout1.AddItem(instances);
                layout1.AddItem(triggers);
                layout1.AddItem(cameras);
            }
            var layout2 = new PS2AnyLayoutSection();
            layout2.SetID(Constants.LEVEL_LAYOUT_2_SECTION);
            {
                var templates = new PS2AnyTemplatesSection();
                templates.SetID(Constants.LAYOUT_TEMPLATES_SECTION);
                var aiPositions = new PS2AnyAIPositionsSection();
                aiPositions.SetID(Constants.LAYOUT_AI_POSITIONS_SECTION);
                var aiPaths = new PS2AnyAIPathsSection();
                aiPaths.SetID(Constants.LAYOUT_AI_PATHS_SECTION);
                var positions = new PS2AnyPositionsSection();
                positions.SetID(Constants.LAYOUT_POSITIONS_SECTION);
                var paths = new PS2AnyPathsSection();
                paths.SetID(Constants.LAYOUT_PATHS_SECTION);
                var surfaces = new PS2AnySurfacesSection();
                surfaces.SetID(Constants.LAYOUT_SURFACES_SECTION);
                var instances = new PS2AnyInstancesSection();
                instances.SetID(Constants.LAYOUT_INSTANCES_SECTION);
                var triggers = new PS2AnyTriggersSection();
                triggers.SetID(Constants.LAYOUT_TRIGGERS_SECTION);
                var cameras = new PS2AnyCamerasSection();
                cameras.SetID(Constants.LAYOUT_CAMERAS_SECTION);
                layout2.AddItem(templates);
                layout2.AddItem(aiPositions);
                layout2.AddItem(aiPaths);
                layout2.AddItem(positions);
                layout2.AddItem(paths);
                layout2.AddItem(surfaces);
                layout2.AddItem(instances);
                layout2.AddItem(triggers);
                layout2.AddItem(cameras);
            }
            var layout3 = new PS2AnyLayoutSection();
            layout3.SetID(Constants.LEVEL_LAYOUT_3_SECTION);
            {
                var templates = new PS2AnyTemplatesSection();
                templates.SetID(Constants.LAYOUT_TEMPLATES_SECTION);
                var aiPositions = new PS2AnyAIPositionsSection();
                aiPositions.SetID(Constants.LAYOUT_AI_POSITIONS_SECTION);
                var aiPaths = new PS2AnyAIPathsSection();
                aiPaths.SetID(Constants.LAYOUT_AI_PATHS_SECTION);
                var positions = new PS2AnyPositionsSection();
                positions.SetID(Constants.LAYOUT_POSITIONS_SECTION);
                var paths = new PS2AnyPathsSection();
                paths.SetID(Constants.LAYOUT_PATHS_SECTION);
                var surfaces = new PS2AnySurfacesSection();
                surfaces.SetID(Constants.LAYOUT_SURFACES_SECTION);
                var instances = new PS2AnyInstancesSection();
                instances.SetID(Constants.LAYOUT_INSTANCES_SECTION);
                var triggers = new PS2AnyTriggersSection();
                triggers.SetID(Constants.LAYOUT_TRIGGERS_SECTION);
                var cameras = new PS2AnyCamerasSection();
                cameras.SetID(Constants.LAYOUT_CAMERAS_SECTION);
                layout3.AddItem(templates);
                layout3.AddItem(aiPositions);
                layout3.AddItem(aiPaths);
                layout3.AddItem(positions);
                layout3.AddItem(paths);
                layout3.AddItem(surfaces);
                layout3.AddItem(instances);
                layout3.AddItem(triggers);
                layout3.AddItem(cameras);
            }
            var layout4 = new PS2AnyLayoutSection();
            layout4.SetID(Constants.LEVEL_LAYOUT_4_SECTION);
            {
                var templates = new PS2AnyTemplatesSection();
                templates.SetID(Constants.LAYOUT_TEMPLATES_SECTION);
                var aiPositions = new PS2AnyAIPositionsSection();
                aiPositions.SetID(Constants.LAYOUT_AI_POSITIONS_SECTION);
                var aiPaths = new PS2AnyAIPathsSection();
                aiPaths.SetID(Constants.LAYOUT_AI_PATHS_SECTION);
                var positions = new PS2AnyPositionsSection();
                positions.SetID(Constants.LAYOUT_POSITIONS_SECTION);
                var paths = new PS2AnyPathsSection();
                paths.SetID(Constants.LAYOUT_PATHS_SECTION);
                var surfaces = new PS2AnySurfacesSection();
                surfaces.SetID(Constants.LAYOUT_SURFACES_SECTION);
                var instances = new PS2AnyInstancesSection();
                instances.SetID(Constants.LAYOUT_INSTANCES_SECTION);
                var triggers = new PS2AnyTriggersSection();
                triggers.SetID(Constants.LAYOUT_TRIGGERS_SECTION);
                var cameras = new PS2AnyCamerasSection();
                cameras.SetID(Constants.LAYOUT_CAMERAS_SECTION);
                layout4.AddItem(templates);
                layout4.AddItem(aiPositions);
                layout4.AddItem(aiPaths);
                layout4.AddItem(positions);
                layout4.AddItem(paths);
                layout4.AddItem(surfaces);
                layout4.AddItem(instances);
                layout4.AddItem(triggers);
                layout4.AddItem(cameras);
            }
            var layout5 = new PS2AnyLayoutSection();
            layout5.SetID(Constants.LEVEL_LAYOUT_5_SECTION);
            {
                var templates = new PS2AnyTemplatesSection();
                templates.SetID(Constants.LAYOUT_TEMPLATES_SECTION);
                var aiPositions = new PS2AnyAIPositionsSection();
                aiPositions.SetID(Constants.LAYOUT_AI_POSITIONS_SECTION);
                var aiPaths = new PS2AnyAIPathsSection();
                aiPaths.SetID(Constants.LAYOUT_AI_PATHS_SECTION);
                var positions = new PS2AnyPositionsSection();
                positions.SetID(Constants.LAYOUT_POSITIONS_SECTION);
                var paths = new PS2AnyPathsSection();
                paths.SetID(Constants.LAYOUT_PATHS_SECTION);
                var surfaces = new PS2AnySurfacesSection();
                surfaces.SetID(Constants.LAYOUT_SURFACES_SECTION);
                var instances = new PS2AnyInstancesSection();
                instances.SetID(Constants.LAYOUT_INSTANCES_SECTION);
                var triggers = new PS2AnyTriggersSection();
                triggers.SetID(Constants.LAYOUT_TRIGGERS_SECTION);
                var cameras = new PS2AnyCamerasSection();
                cameras.SetID(Constants.LAYOUT_CAMERAS_SECTION);
                layout5.AddItem(templates);
                layout5.AddItem(aiPositions);
                layout5.AddItem(aiPaths);
                layout5.AddItem(positions);
                layout5.AddItem(paths);
                layout5.AddItem(surfaces);
                layout5.AddItem(instances);
                layout5.AddItem(triggers);
                layout5.AddItem(cameras);
            }
            var layout6 = new PS2AnyLayoutSection();
            layout6.SetID(Constants.LEVEL_LAYOUT_6_SECTION);
            {
                var templates = new PS2AnyTemplatesSection();
                templates.SetID(Constants.LAYOUT_TEMPLATES_SECTION);
                var aiPositions = new PS2AnyAIPositionsSection();
                aiPositions.SetID(Constants.LAYOUT_AI_POSITIONS_SECTION);
                var aiPaths = new PS2AnyAIPathsSection();
                aiPaths.SetID(Constants.LAYOUT_AI_PATHS_SECTION);
                var positions = new PS2AnyPositionsSection();
                positions.SetID(Constants.LAYOUT_POSITIONS_SECTION);
                var paths = new PS2AnyPathsSection();
                paths.SetID(Constants.LAYOUT_PATHS_SECTION);
                var surfaces = new PS2AnySurfacesSection();
                surfaces.SetID(Constants.LAYOUT_SURFACES_SECTION);
                var instances = new PS2AnyInstancesSection();
                instances.SetID(Constants.LAYOUT_INSTANCES_SECTION);
                var triggers = new PS2AnyTriggersSection();
                triggers.SetID(Constants.LAYOUT_TRIGGERS_SECTION);
                var cameras = new PS2AnyCamerasSection();
                cameras.SetID(Constants.LAYOUT_CAMERAS_SECTION);
                layout6.AddItem(templates);
                layout6.AddItem(aiPositions);
                layout6.AddItem(aiPaths);
                layout6.AddItem(positions);
                layout6.AddItem(paths);
                layout6.AddItem(surfaces);
                layout6.AddItem(instances);
                layout6.AddItem(triggers);
                layout6.AddItem(cameras);
            }
            var layout7 = new PS2AnyLayoutSection();
            layout7.SetID(Constants.LEVEL_LAYOUT_7_SECTION);
            {
                var templates = new PS2AnyTemplatesSection();
                templates.SetID(Constants.LAYOUT_TEMPLATES_SECTION);
                var aiPositions = new PS2AnyAIPositionsSection();
                aiPositions.SetID(Constants.LAYOUT_AI_POSITIONS_SECTION);
                var aiPaths = new PS2AnyAIPathsSection();
                aiPaths.SetID(Constants.LAYOUT_AI_PATHS_SECTION);
                var positions = new PS2AnyPositionsSection();
                positions.SetID(Constants.LAYOUT_POSITIONS_SECTION);
                var paths = new PS2AnyPathsSection();
                paths.SetID(Constants.LAYOUT_PATHS_SECTION);
                var surfaces = new PS2AnySurfacesSection();
                surfaces.SetID(Constants.LAYOUT_SURFACES_SECTION);
                var instances = new PS2AnyInstancesSection();
                instances.SetID(Constants.LAYOUT_INSTANCES_SECTION);
                var triggers = new PS2AnyTriggersSection();
                triggers.SetID(Constants.LAYOUT_TRIGGERS_SECTION);
                var cameras = new PS2AnyCamerasSection();
                cameras.SetID(Constants.LAYOUT_CAMERAS_SECTION);
                layout7.AddItem(templates);
                layout7.AddItem(aiPositions);
                layout7.AddItem(aiPaths);
                layout7.AddItem(positions);
                layout7.AddItem(paths);
                layout7.AddItem(surfaces);
                layout7.AddItem(instances);
                layout7.AddItem(triggers);
                layout7.AddItem(cameras);
            }
            var layout8 = new PS2AnyLayoutSection();
            layout8.SetID(Constants.LEVEL_LAYOUT_8_SECTION);
            {
                var templates = new PS2AnyTemplatesSection();
                templates.SetID(Constants.LAYOUT_TEMPLATES_SECTION);
                var aiPositions = new PS2AnyAIPositionsSection();
                aiPositions.SetID(Constants.LAYOUT_AI_POSITIONS_SECTION);
                var aiPaths = new PS2AnyAIPathsSection();
                aiPaths.SetID(Constants.LAYOUT_AI_PATHS_SECTION);
                var positions = new PS2AnyPositionsSection();
                positions.SetID(Constants.LAYOUT_POSITIONS_SECTION);
                var paths = new PS2AnyPathsSection();
                paths.SetID(Constants.LAYOUT_PATHS_SECTION);
                var surfaces = new PS2AnySurfacesSection();
                surfaces.SetID(Constants.LAYOUT_SURFACES_SECTION);
                var instances = new PS2AnyInstancesSection();
                instances.SetID(Constants.LAYOUT_INSTANCES_SECTION);
                var triggers = new PS2AnyTriggersSection();
                triggers.SetID(Constants.LAYOUT_TRIGGERS_SECTION);
                var cameras = new PS2AnyCamerasSection();
                cameras.SetID(Constants.LAYOUT_CAMERAS_SECTION);
                layout8.AddItem(templates);
                layout8.AddItem(aiPositions);
                layout8.AddItem(aiPaths);
                layout8.AddItem(positions);
                layout8.AddItem(paths);
                layout8.AddItem(surfaces);
                layout8.AddItem(instances);
                layout8.AddItem(triggers);
                layout8.AddItem(cameras);
            }
            var particle = new PS2AnyParticleData();
            particle.SetID(Constants.LEVEL_PARTICLES_ITEM);
            var collision = new PS2AnyCollisionData();
            collision.SetID(Constants.LEVEL_COLLISION_ITEM);

            rm2.AddItem(graphics);
            rm2.AddItem(code);
            rm2.AddItem(particle);
            rm2.AddItem(collision);
            rm2.AddItem(layout1);
            rm2.AddItem(layout2);
            rm2.AddItem(layout3);
            rm2.AddItem(layout4);
            rm2.AddItem(layout5);
            rm2.AddItem(layout6);
            rm2.AddItem(layout7);
            rm2.AddItem(layout8);
            return rm2;
        }

        public ITwinSection GenerateSM2()
        {
            var sm2 = new PS2AnyTwinsanitySM2();
            var graphics = new PS2AnyGraphicsSection();
            graphics.SetID(Constants.SCENERY_GRAPHICS_SECTION);
            {
                var textures = new PS2AnyTexturesSection();
                textures.SetID(Constants.GRAPHICS_TEXTURES_SECTION);
                var materials = new PS2AnyMaterialsSection();
                materials.SetID(Constants.GRAPHICS_MATERIALS_SECTION);
                var models = new PS2AnyModelsSection();
                models.SetID(Constants.GRAPHICS_MODELS_SECTION);
                var rigids = new PS2AnyRigidModelsSection();
                rigids.SetID(Constants.GRAPHICS_RIGID_MODELS_SECTION);
                var skins = new PS2AnySkinsSection();
                skins.SetID(Constants.GRAPHICS_SKINS_SECTION);
                var blendSkins = new PS2AnyBlendSkinsSection();
                blendSkins.SetID(Constants.GRAPHICS_BLEND_SKINS_SECTION);
                var meshes = new PS2AnyMeshesSection();
                meshes.SetID(Constants.GRAPHICS_MESHES_SECTION);
                var lods = new PS2AnyLODsSection();
                lods.SetID(Constants.GRAPHICS_LODS_SECTION);
                var skydomes = new PS2AnySkydomesSection();
                skydomes.SetID(Constants.GRAPHICS_SKYDOMES_SECTION);
                graphics.AddItem(textures);
                graphics.AddItem(materials);
                graphics.AddItem(models);
                graphics.AddItem(rigids);
                graphics.AddItem(skins);
                graphics.AddItem(blendSkins);
                graphics.AddItem(meshes);
                graphics.AddItem(lods);
                graphics.AddItem(skydomes);
            }
            var scenery = new PS2AnyScenery();
            scenery.SetID(Constants.SCENERY_SECENERY_ITEM);
            var unk1 = new BaseTwinItem();
            unk1.SetID(Constants.SCENERY_UNK_1_ITEM);
            var unk2 = new BaseTwinItem();
            unk2.SetID(Constants.SCENERY_UNK_2_ITEM);
            var unk3 = new BaseTwinItem();
            unk3.SetID(Constants.SCENERY_UNK_3_ITEM);
            var dynamicScenery = new PS2AnyDynamicScenery();
            dynamicScenery.SetID(Constants.SCENERY_DYNAMIC_SECENERY_ITEM);
            var links = new PS2AnyLink();
            links.SetID(Constants.SCENERY_LINK_ITEM);

            sm2.AddItem(graphics);
            sm2.AddItem(scenery);
            sm2.AddItem(unk1);
            sm2.AddItem(unk2);
            sm2.AddItem(unk3);
            sm2.AddItem(dynamicScenery);
            sm2.AddItem(links);
            return sm2;
        }
    }
}
