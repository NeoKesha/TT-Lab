using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace TT_Lab.Assets.Instance
{
    public class InstanceTemplate : SerializableInstance
    {
        public override UInt32 Section => Constants.LAYOUT_TEMPLATES_SECTION;

        public InstanceTemplate(LabURI package, UInt32 id, String Name, String chunk, Int32 layId, ITwinTemplate template) : base(package, id, Name, chunk, layId)
        {
            assetData = new InstanceTemplateData(template);
        }

        public InstanceTemplate()
        {
        }

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new InstanceTemplateData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
