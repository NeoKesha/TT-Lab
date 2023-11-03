using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
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

        public override void Import(LabURI package, String? variant)
        {
            TwinBehaviourStarter behaviourStarter = GetTwinItem<TwinBehaviourStarter>();
            foreach (var assigner in behaviourStarter.Assigners)
            {
                Assigners.Add(new BehaviourAssignerData(package, variant, assigner));
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

            ms.Position = 0;
            return factory.GenerateBehaviourStarter(ms);
        }
    }
}
