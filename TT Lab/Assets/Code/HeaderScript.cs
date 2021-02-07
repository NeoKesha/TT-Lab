using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Code;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class HeaderScript : Script
    {
        protected override String DataExt => ".lab";
        public HeaderScript() { }

        public HeaderScript(UInt32 id, String name, PS2HeaderScript script) : base(id, name)
        {
            assetData = new HeaderScriptData(script);
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }

        public override AssetViewModel GetViewModel(AssetViewModel parent = null)
        {
            if (viewModel == null)
            {
                viewModel = new HeaderScriptViewModel(UUID, parent);
            }
            return base.GetViewModel(parent);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new HeaderScriptData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
