using System;
using System.Collections.ObjectModel;
using TT_Lab.AssetData.Code;
using TT_Lab.Assets;

namespace TT_Lab.ViewModels.Code
{
    public class HeaderScriptViewModel : AssetViewModel
    {
        private LabURI attachedScript;
        private ObservableCollection<UInt32> callConventions;

        public HeaderScriptViewModel(LabURI asset, AssetViewModel? parent) : base(asset, parent)
        {
            callConventions = new ObservableCollection<UInt32>();
        }

        protected override void LoadData()
        {
            var data = _asset.GetData<BehaviourStarterData>();
            attachedScript = data.Pairs[0].Key;
            callConventions.Clear();
            foreach (var pair in data.Pairs)
            {
                callConventions.Add(pair.Value);
            }
            base.LoadData();
        }

        public LabURI AttachedScript
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
    }
}
