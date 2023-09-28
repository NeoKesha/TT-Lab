using System;
using System.Collections.Generic;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class BehaviourCommandsSequence : SerializableAsset
    {
        protected override String DataExt => ".lab";

        public Dictionary<UInt32, LabURI> BehaviourGraphLinks = new();

        public BehaviourCommandsSequence() { }

        public BehaviourCommandsSequence(LabURI package, String? variant, UInt32 id, String Name, PS2AnyBehaviourCommandsSequence codeModel) : base(id, Name, package, variant)
        {
            assetData = new BehaviourCommandsSequenceData(codeModel);
            assetData.Import(package, variant);
            Parameters.Add("behaviour_graph_links", BehaviourGraphLinks);
            GenerateBehaviourGraphLinks(package, variant);
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
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
                BehaviourGraphLinks = (Dictionary<UInt32, LabURI>)Parameters["behaviour_graph_links"]!;
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
