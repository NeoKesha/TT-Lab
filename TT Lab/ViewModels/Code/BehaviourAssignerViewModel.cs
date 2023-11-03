using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Code.Behaviour;
using TT_Lab.Assets;
using static Twinsanity.TwinsanityInterchange.Common.AgentLab.TwinBehaviourAssigner;

namespace TT_Lab.ViewModels.Code
{
    public class BehaviourAssignerViewModel : ObservableObject
    {
        private LabURI beaviour;
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
