using System.Collections.ObjectModel;
using TT_Lab.AssetData.Code.Behaviour;
using TT_Lab.Assets;

namespace TT_Lab.ViewModels.Code
{
    public class BehaviourStarterViewModel : AssetViewModel
    {
        private LabURI attachedScript;
        private ObservableCollection<BehaviourAssignerViewModel> assigners;

        public BehaviourStarterViewModel(LabURI asset, AssetViewModel? parent) : base(asset, parent)
        {
            attachedScript = LabURI.Empty;
            assigners = new ObservableCollection<BehaviourAssignerViewModel>();
        }

        protected override void LoadData()
        {
            var data = _asset.GetData<BehaviourStarterData>();
            attachedScript = data.Assigners[0].Behaviour;
            assigners.Clear();
            foreach (var assigner in data.Assigners)
            {
                assigners.Add(new BehaviourAssignerViewModel(assigner));
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
        public ObservableCollection<BehaviourAssignerViewModel> CallConventions
        {
            get => assigners;
        }
    }
}
