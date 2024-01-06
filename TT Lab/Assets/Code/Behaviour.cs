using System;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace TT_Lab.Assets.Code
{
    public abstract class Behaviour : SerializableAsset
    {
        public override UInt32 Section => Constants.CODE_BEHAVIOURS_SECTION;

        public Behaviour() { }

        public Behaviour(LabURI package, Boolean needVariant, String variant, UInt32 id, String Name) : base(id, Name, package, needVariant, variant)
        {
        }
    }
}
