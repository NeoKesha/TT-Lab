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

        public Dictionary<UInt32, LabURI> BehaviourGraphLinks = new Dictionary<uint, LabURI>();

        public BehaviourCommandsSequence() { }

        public BehaviourCommandsSequence(String package, String subpackage, String? variant, UInt32 id, String name, PS2AnyBehaviourCommandsSequence codeModel) : base(id, name, package, subpackage, variant)
        {
            assetData = new BehaviourCommandsSequenceData(codeModel);
            assetData.Import(package, subpackage, variant);
            Parameters.Add("behaviour_graph_links", BehaviourGraphLinks);
            GenerateBehaviourGraphLinks(package, subpackage, variant);
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

        private void GenerateBehaviourGraphLinks(String package, String subpackage, String? variant)
        {
            BehaviourGraphLinks.Clear();
            var cm = (BehaviourCommandsSequenceData)assetData;
            foreach (var e in cm.BehaviourGraphIds)
            {
                BehaviourGraphLinks.Add(e, new LabURI($"res://{package}/{subpackage}/{typeof(BehaviourGraph).Name}/{e}"));
            }
        }
    }
}
