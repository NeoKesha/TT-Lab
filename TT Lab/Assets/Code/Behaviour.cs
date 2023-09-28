using System;

namespace TT_Lab.Assets.Code
{
    public abstract class Behaviour : SerializableAsset
    {

        public Behaviour() { }

        public Behaviour(LabURI package, String? variant, UInt32 id, String Name) : base(id, Name, package, variant)
        {
        }
    }
}
