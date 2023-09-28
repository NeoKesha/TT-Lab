using System;

namespace TT_Lab.Assets.Instance
{
    public abstract class SerializableInstance : SerializableAsset
    {
        protected override String SavePath => $"Instance\\{Chunk}\\{Type}";

        public SerializableInstance(LabURI package, UInt32 id, String Name, String chunk, Int32? layId) : base(id, Name, package, chunk)
        {
            Chunk = chunk;
            LayoutID = layId;
        }

        protected SerializableInstance()
        {
        }

    }
}
