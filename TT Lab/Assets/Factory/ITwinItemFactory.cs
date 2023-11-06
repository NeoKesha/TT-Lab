using System.Collections.Generic;
using System.IO;
using TT_Lab.AssetData.Graphics.SubModels;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code.AgentLab;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;

namespace TT_Lab.Assets.Factory
{
    public interface ITwinItemFactory
    {
        ITwinBlendSkin GenerateBlendSkin(Stream stream);
        ITwinLOD GenerateLOD(Stream stream);
        ITwinMaterial GenerateMaterial(Stream stream);
        ITwinMesh GenerateMesh(Stream stream);
        ITwinModel GenerateModel(List<List<Vertex>> vertexes, List<List<IndexedFace>> faces);
        ITwinRigidModel GenerateRigidModel(Stream stream);
        ITwinSkin GenerateSkin(Stream stream);
        ITwinSkydome GenerateSkydome(Stream stream);
        ITwinTexture GenerateTexture();
        ITwinAnimation GenerateAnimation(Stream stream);
        TwinBehaviourStarter GenerateBehaviourStarter(Stream stream);
        ITwinBehaviourGraph GenerateBehaviourGraph(Stream stream);
        ITwinBehaviourCommandsSequence GenerateBehaviourCommandsSequence(Stream stream);
        ITwinObject GenerateObject(Stream stream);
        ITwinOGI GenerateOGI(Stream stream);
        ITwinSound GenerateSound();
        ITwinAIPath GenerateAIPath(Stream stream);
        ITwinAIPosition GenerateAIPosition(Stream stream);
        ITwinCamera GenerateCamera(Stream stream);
        ITwinInstance GenerateInstance(Stream stream);
        ITwinPath GeneratePath(Stream stream);
        ITwinPosition GeneratePosition(Stream stream);
        ITwinSurface GenerateSurface(Stream stream);
        ITwinTemplate GenerateTemplate(Stream stream);
        ITwinTrigger GenerateTrigger(Stream stream);
        ITwinCollision GenerateCollision(Stream stream);
        ITwinParticle GenerateParticle(Stream stream);
        ITwinDynamicScenery GenerateDynamicScenery(Stream stream);
        ITwinLink GenerateLink(Stream stream);
        ITwinScenery GenerateScenery(Stream stream);
    }
}
