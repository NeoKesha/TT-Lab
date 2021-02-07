using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Code;
using TT_Lab.Project;

namespace TT_Lab.ViewModels.Code
{
    public class HeaderScriptViewModel : AssetViewModel
    {
        private Guid attachedScript;
        private ObservableCollection<UInt32> callConventions;

        public HeaderScriptViewModel(Guid asset, AssetViewModel? parent) : base(asset, parent)
        {
            callConventions = new ObservableCollection<UInt32>();
            var data = (HeaderScriptData)_asset.GetData();
            attachedScript = data.Pairs[0].Key;
            callConventions.Clear();
            foreach (var pair in data.Pairs)
            {
                callConventions.Add(pair.Value);
            }
        }

        protected override void LoadData()
        {
            base.LoadData();
        }

        public Guid AttachedScript
        {
            get => attachedScript;
            set
            {
                if (value != attachedScript)
                {
                    attachedScript = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public ObservableCollection<UInt32> CallConventions
        {
            get => callConventions;
        }
        public override String Alias
        {
            get
            {
                var obj = ProjectManagerSingleton.PM.OpenedProject.GetAsset(AttachedScript);
                return $"{base.Alias} - {obj.Alias}";
            }
            set => base.Alias = value;
        }
    }
}
