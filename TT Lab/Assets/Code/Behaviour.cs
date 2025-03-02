using System;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace TT_Lab.Assets.Code
{
    public abstract class Behaviour : SerializableAsset
    {
        public override UInt32 Section => Constants.CODE_BEHAVIOURS_SECTION;
        public override String IconPath => "Behavior_Tree_Start.png";

        protected Behaviour() { }

        protected Behaviour(LabURI package, Boolean needVariant, String variant, UInt32 id, String name) : base(id, name, package, needVariant, variant)
        {
        }
    }
}
