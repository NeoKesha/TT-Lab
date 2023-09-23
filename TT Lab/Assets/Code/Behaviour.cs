using System;

namespace TT_Lab.Assets.Code
{
    public abstract class Behaviour : SerializableAsset
    {

        public Behaviour() { }

        public Behaviour(String package, String subpackage, String? variant, UInt32 id, String name) : base(id, name, package, subpackage, variant)
        {
        }
    }
}
