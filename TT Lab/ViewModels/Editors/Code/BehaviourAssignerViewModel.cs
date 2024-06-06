using TT_Lab.AssetData.Code.Behaviour;
using TT_Lab.Assets;
using static Twinsanity.TwinsanityInterchange.Common.AgentLab.TwinBehaviourAssigner;

namespace TT_Lab.ViewModels.Editors.Code
{
    public class BehaviourAssignerViewModel : ObservableObject
    {
        private LabURI behaviour;
        private LabURI @object;
        private AssignTypeID assignType;
        private AssignLocalityID assignLocality;
        private AssignStatusID assignStatus;
        private AssignPreferenceID assignPreference;

        public BehaviourAssignerViewModel(BehaviourAssignerData assigner)
        {

        }
    }
}
