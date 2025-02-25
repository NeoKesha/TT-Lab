using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code.Behaviour;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code.AgentLab;

namespace TT_Lab.Assets.Code
{
    public class BehaviourCommandsSequence : SerializableAsset
    {
        protected override String DataExt => ".lab";
        public override UInt32 Section => Constants.CODE_BEHAVIOUR_COMMANDS_SEQUENCES_SECTION;
        public override String IconPath => "Command_Sequence.png";

        [JsonProperty(Required = Required.Always)]
        public Dictionary<UInt32, LabURI> BehaviourGraphLinks = new();

        public BehaviourCommandsSequence() { }

        public BehaviourCommandsSequence(LabURI package, Boolean needVariant, String variant, UInt32 id, String Name, PS2BehaviourCommandsSequence codeModel) : base(id, Name, package, needVariant, variant)
        {
            assetData = new BehaviourCommandsSequenceData(codeModel);
            assetData.Import(package, variant, LayoutID);
            GenerateBehaviourGraphLinks(package, variant);
        }

        public override void PostDeserialize()
        {
            base.PostDeserialize();
            foreach (var graphLink in BehaviourGraphLinks)
            {
                AssetManager.Get().AddAssetUnsafe(graphLink.Value, this);
            }
        }

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new BehaviourCommandsSequenceData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }

        private void GenerateBehaviourGraphLinks(LabURI package, String? variant)
        {
            BehaviourGraphLinks.Clear();
            var cm = (BehaviourCommandsSequenceData)assetData;
            foreach (var e in cm.BehaviourGraphIds)
            {
                var uri = new LabURI($"{package}/{typeof(BehaviourGraph).Name}/{e}/{ID}");
                BehaviourGraphLinks.Add(e, uri);
            }
        }
    }
}
