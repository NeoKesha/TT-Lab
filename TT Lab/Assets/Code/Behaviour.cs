using System;

namespace TT_Lab.Assets.Code
{
    public abstract class Behaviour : SerializableAsset
    {

        public Behaviour() { }

        public Behaviour(String package, String subpackage, String? variant, UInt32 id, String Name) : base(id, Name, package, subpackage, variant)
        {
        }
    }
}
