using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.AssetData.Code.Behaviour
{
    public class BehaviourStarterData : BehaviourData
    {
        public BehaviourStarterData()
        {
        }

        public BehaviourStarterData(TwinBehaviourStarter behaviourStarter) : base(behaviourStarter)
        {
            SetTwinItem(behaviourStarter);
        }

        [JsonProperty(Required = Required.Always)]
        public List<BehaviourAssignerData> Assigners { get; set; } = new();

        protected override void Dispose(Boolean disposing)
        {
            Assigners.Clear();
        }

        public override void Import(LabURI package, String? variant, Int32? layoutId)
        {
            TwinBehaviourStarter behaviourStarter = GetTwinItem<TwinBehaviourStarter>();
            var index = 0;
            foreach (var assigner in behaviourStarter.Assigners)
            {
                Assigners.Add(new BehaviourAssignerData(package, variant, assigner));
                if (index == 0)
                {
                    if (Assigners[0].Behaviour == LabURI.Empty)
                    {
                        throw new Exception("SOMETHING WENT WRONG THE STARTER IS NOT REFERENCING ANY GRAPH");
                    }
                }
                index++;
            }
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            var assetManager = AssetManager.Get();
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            writer.Write((UInt16)ID);
            writer.Write(Priority);
            writer.Write((Byte)0);
            writer.Write((UInt32)Assigners.Count);
            foreach (var assigner in Assigners)
            {
                writer.Write(assigner.Behaviour == LabURI.Empty ? 0 : (Int32)assetManager.GetAsset(assigner.Behaviour).ID + 1);
                writer.Write(assigner.Object == LabURI.Empty ? UInt16.MaxValue : (UInt16)assetManager.GetAsset(assigner.Object).ID);
                writer.Write((UInt32)assigner.AssignType);
                writer.Write((UInt32)assigner.AssignLocality);
                writer.Write((UInt32)assigner.AssignStatus);
                writer.Write((UInt32)assigner.AssignPreference);
            }

            writer.Flush();
            ms.Position = 0;
            return factory.GenerateBehaviourStarter(ms);
        }

        public override ITwinItem? ResolveChunkResouces(ITwinItemFactory factory, ITwinSection section, UInt32 id)
        {
            var assetManager = AssetManager.Get();
            var codeSection = section.GetParent();
            var objectsSection = codeSection.GetItem<ITwinSection>(Constants.CODE_GAME_OBJECTS_SECTION);
            foreach (var assigner in Assigners)
            {
                if (assigner.Object != LabURI.Empty)
                {
                    assetManager.GetAsset(assigner.Object).ResolveChunkResources(factory, objectsSection);
                }
                if (assigner.Behaviour != LabURI.Empty)
                {
                    assetManager.GetAsset(assigner.Behaviour).ResolveChunkResources(factory, section);
                }
                if (Assigners.Count == 0)
                {
                    Console.WriteLine("WTF HAPPENED???");
                }
            }
            return base.ResolveChunkResouces(factory, section, id);
        }
    }
}
