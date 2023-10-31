using Newtonsoft.Json;
using System;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common.Animation;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.AssetData.Code
{
    public class AnimationData : AbstractAssetData
    {
        public AnimationData()
        {
            MainAnimation = new();
            FacialAnimation = new();
        }

        public AnimationData(ITwinAnimation animation) : this()
        {
            SetTwinItem(animation);
        }

        [JsonProperty(Required = Required.Always)]
        public UInt16 TotalFrames { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte DefaultFPS { get; set; }
        [JsonProperty(Required = Required.Always)]
        public TwinAnimation MainAnimation { get; set; }
        [JsonProperty(Required = Required.Always)]
        public TwinAnimation FacialAnimation { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import(LabURI package, String? variant)
        {
            var twinAnimation = GetTwinItem<ITwinAnimation>();
            TotalFrames = twinAnimation.TotalFrames;
            DefaultFPS = twinAnimation.DefaultFPS;
            MainAnimation = CloneUtils.DeepClone(twinAnimation.MainAnimation);
            FacialAnimation = CloneUtils.DeepClone(twinAnimation.FacialAnimation);
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            writer.Write(TotalFrames);
            writer.Write(DefaultFPS);
            MainAnimation.Write(writer);
            FacialAnimation.Write(writer);

            return factory.GenerateAnimation(ms);
        }
    }
}
