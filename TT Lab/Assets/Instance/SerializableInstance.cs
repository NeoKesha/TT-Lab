using System;

namespace TT_Lab.Assets.Instance
{
    public abstract class SerializableInstance : SerializableAsset
    {
        protected override String SavePath => $"Instance\\{Chunk}\\{Type}";

        public SerializableInstance(String package, String subpackage, UInt32 id, String name, String chunk, Int32? layId) : base(id, name, package, subpackage, chunk)
        {
            Chunk = chunk;
            LayoutID = layId;
        }

        protected SerializableInstance()
        {
        }

    }
}
