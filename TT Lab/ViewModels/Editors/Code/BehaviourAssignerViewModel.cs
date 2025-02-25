using System;
using Caliburn.Micro;
using TT_Lab.AssetData.Code.Behaviour;
using TT_Lab.Assets;
using TT_Lab.Attributes;
using TT_Lab.Util;
using TT_Lab.ViewModels.Interfaces;
using static Twinsanity.TwinsanityInterchange.Common.AgentLab.TwinBehaviourAssigner;

namespace TT_Lab.ViewModels.Editors.Code
{
    public class BehaviourAssignerViewModel : Screen, IHaveParentEditor<BehaviourStarterViewModel>, ISaveableViewModel<BehaviourAssignerData>
    {
        private LabURI behaviour;
        private LabURI @object;
        private AssignTypeID assignType;
        private AssignLocalityID assignLocality;
        private AssignStatusID assignStatus;
        private AssignPreferenceID assignPreference;
        private BehaviourStarterViewModel parentEditor;
        private bool isDirty;

        public BehaviourAssignerViewModel(BehaviourAssignerData data, BehaviourStarterViewModel parentEditor)
        {
            this.parentEditor = parentEditor;
            behaviour = data.Behaviour;
            @object = data.Object;
            assignType = data.AssignType;
            assignLocality = data.AssignLocality;
            assignStatus = data.AssignStatus;
            assignPreference = data.AssignPreference;
        }

        public BehaviourStarterViewModel ParentEditor
        {
            get => parentEditor;
            set => parentEditor = value;
        }

        public void ResetDirty()
        {
            IsDirty = false;
        }

        public bool IsDirty
        {
            get => isDirty;
            set
            {
                if (isDirty != value)
                {
                    isDirty = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public void Save(BehaviourAssignerData o)
        {
            o.Behaviour = behaviour;
            o.Object = @object;
            o.AssignType = assignType;
            o.AssignLocality = assignLocality;
            o.AssignStatus = assignStatus;
            o.AssignPreference = assignPreference;
        }

        [MarkDirty]
        public LabURI Behaviour
        {
            get => behaviour;
            set
            {
                if (Equals(value, behaviour)) return;
                behaviour = value ?? throw new ArgumentNullException(nameof(value));
                NotifyOfPropertyChange();
            }
        }

        [MarkDirty]
        public LabURI AttachedObject
        {
            get => @object;
            set
            {
                if (Equals(value, @object)) return;
                @object = value ?? throw new ArgumentNullException(nameof(value));
                NotifyOfPropertyChange();
            }
        }

        [MarkDirty]
        public AssignTypeID AssignType
        {
            get => assignType;
            set
            {
                if (value == assignType) return;
                assignType = value;
                NotifyOfPropertyChange();
            }
        }

        [MarkDirty]
        public AssignLocalityID AssignLocality
        {
            get => assignLocality;
            set
            {
                if (value == assignLocality) return;
                assignLocality = value;
                NotifyOfPropertyChange();
            }
        }

        [MarkDirty]
        public AssignStatusID AssignStatus
        {
            get => assignStatus;
            set
            {
                if (value == assignStatus) return;
                assignStatus = value;
                NotifyOfPropertyChange();
            }
        }

        [MarkDirty]
        public AssignPreferenceID AssignPreference
        {
            get => assignPreference;
            set
            {
                if (value == assignPreference) return;
                assignPreference = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
