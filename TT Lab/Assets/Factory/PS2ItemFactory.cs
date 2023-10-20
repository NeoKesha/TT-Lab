using System;
using System.IO;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code.AgentLab;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;

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
            animation.Read(reader, (Int32)stream.Length);
            return animation;
        }

        public ITwinBehaviour GenerateBehaviour(Stream stream)
        {
            throw new NotImplementedException();
        }

        public ITwinBehaviourCommandsSequence GenerateBehaviourCommandsSequence(Stream stream)
        {
            throw new NotImplementedException();
        }

        public ITwinBehaviourGraph GenerateBehaviourGraph(Stream stream)
        {
            throw new NotImplementedException();
        }

        public TwinBehaviourStarter GenerateBehaviourStarter(Stream stream)
        {
            throw new NotImplementedException();
        }

        public ITwinBlendSkin GenerateBlendSkin(Stream stream)
        {
            throw new NotImplementedException();
        }

        public ITwinCamera GenerateCamera(Stream stream)
        {
            throw new NotImplementedException();
        }

        public ITwinCollision GenerateCollision(Stream stream)
        {
            throw new NotImplementedException();
        }

        public ITwinDynamicScenery GenerateDynamicScenery(Stream stream)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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

        public ITwinSound GenerateSound(Stream stream)
        {
            throw new NotImplementedException();
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

        public ITwinTexture GenerateTexture(Stream stream)
        {
            throw new NotImplementedException();
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
