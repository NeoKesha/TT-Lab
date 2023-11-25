using System;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.Assets.Instance
{
    public abstract class SerializableInstance : SerializableAsset
    {
        protected override String SavePath => $"Instance\\{Chunk}\\{Type.Name}{LayoutPath}";

        private String LayoutPath => LayoutID == null ? "" : $"\\Layout_{LayoutID}";

        public SerializableInstance(LabURI package, UInt32 id, String Name, String chunk, Int32? layId) : base(id, Name, package, chunk)
        {
            Chunk = chunk;
            LayoutID = layId;
            RegenerateURI();
        }

        protected SerializableInstance()
        {
        }

        public override void ResolveChunkResources(ITwinItemFactory factory, ITwinSection section)
        {
            if (LayoutID != null)
            {
                section = section.GetItem<ITwinSection>((UInt32)LayoutID);
                section = section.GetItem<ITwinSection>(Section);
            }
            base.ResolveChunkResources(factory, section);
        }
    }
}
