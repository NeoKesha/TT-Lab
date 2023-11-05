using System;
using System.IO;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2;
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

        public ITwinModel GenerateModel(Stream stream)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public ITwinSkin GenerateSkin(Stream stream)
        {
            throw new NotImplementedException();
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
