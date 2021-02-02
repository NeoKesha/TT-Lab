using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.Editors.Instance;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Instance;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.Assets.Instance
{
    public class AiPosition : SerializableInstance
    {
        public AiPosition(UInt32 id, String name, String chunk, Int32 layId, PS2AnyAIPosition position) : base(id, name, chunk, layId)
        {
            assetData = new AiPositionData(position);
        }

        public AiPosition()
        {
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
            return typeof(AiNavPositionEditor);
        }

        public override AssetViewModel GetViewModel(AssetViewModel parent = null)
        {
            if (viewModel == null)
            {
                viewModel = new AIPositionViewModel(UUID, parent);
            }
            return viewModel;
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new AiPositionData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
