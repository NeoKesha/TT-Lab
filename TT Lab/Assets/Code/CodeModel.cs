using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class CodeModel : SerializableAsset
    {
        protected override String DataExt => ".lab";
        public Dictionary<UInt32, Guid> SubScriptGuids = new Dictionary<uint, Guid>();
        public CodeModel() {}

        public CodeModel(UInt32 id, String name, PS2AnyCodeModel codeModel) : base(id, name)
        {
            assetData = new CodeModelData(codeModel);
            assetData.Import();
            Parameters.Add("sub_script_guids", SubScriptGuids);
            GenerateSubScriptGuidList();
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

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new CodeModelData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                SubScriptGuids = (Dictionary<UInt32, Guid>)Parameters["sub_script_guids"];
                IsLoaded = true;
            }
            return assetData;
        }

        private void GenerateSubScriptGuidList()
        {
            SubScriptGuids.Clear();
            var cm = (CodeModelData)assetData;
            foreach (var e in cm.ScriptIds)
            {
                SubScriptGuids.Add(e, Guid.NewGuid());
            }
        }
    }
}
