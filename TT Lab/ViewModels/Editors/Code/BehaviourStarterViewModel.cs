using Caliburn.Micro;
using System.Linq;
using TT_Lab.AssetData.Code.Behaviour;
using TT_Lab.Assets;

namespace TT_Lab.ViewModels.Editors.Code
{
    public class BehaviourStarterViewModel : ResourceEditorViewModel
    {
        private LabURI attachedScript = LabURI.Empty;
        private BindableCollection<BehaviourAssignerViewModel> assigners = new();

        protected override void Save()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var data = asset.GetData<BehaviourStarterData>();
            data.Assigners.Clear();

            var behaviourAssigner = new BehaviourAssignerData
            {
                Behaviour = attachedScript
            };
            // TODO: Finish this up
            data.Assigners.Add(behaviourAssigner);
            foreach (var assigner in assigners.Skip(1))
            {

            }
        }

        public override void LoadData()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var data = asset.GetData<BehaviourStarterData>();
            attachedScript = data.Assigners[0].Behaviour;
            assigners.Clear();
            foreach (var assigner in data.Assigners)
            {
                assigners.Add(new BehaviourAssignerViewModel(assigner));
            }
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
                    NotifyOfPropertyChange();
                }
            }
        }

        public BindableCollection<BehaviourAssignerViewModel> CallConventions
        {
            get => assigners;
        }
    }
}
