using System;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Util;
using TT_Lab.ViewModels.Composite;
using static Twinsanity.TwinsanityInterchange.Enumerations.Enums;

namespace TT_Lab.ViewModels.Editors.Instance
{
    public class CollisionSurfaceViewModel : InstanceSectionResourceEditorViewModel
    {
        private Layouts layId;
        private SurfaceType surfId;
        private SurfaceFlags flags;
        private LabURI stepSoundId1 = LabURI.Empty;
        private LabURI stepSoundId2 = LabURI.Empty;
        private LabURI landSoundId1 = LabURI.Empty;
        private LabURI landSoundId2 = LabURI.Empty;
        private LabURI unkSoundId = LabURI.Empty;
        private UInt16 walkOnParticleSystemId1;
        private UInt16 walkOnParticleSystemId2;
        private UInt16 unkId3;
        private UInt16 landOnParticleSystemId;
        private UInt16 unkId5;
        private Single[] physicsParameters = new Single[10];
        private Vector4ViewModel unkVec = new();
        private Vector4ViewModel[] unkBoundingBox = new Vector4ViewModel[2];

        protected override void Save()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            asset.LayoutID = (int)LayoutID;
            var data = asset.GetData<CollisionSurfaceData>();
            data.SurfaceID = SurfId;
            data.Flags = Flags;
            data.StepSoundId1 = StepSoundId1;
            data.StepSoundId2 = StepSoundId2;
            data.LandSoundId1 = LandSoundId1;
            data.LandSoundId2 = LandSoundId2;
            data.UnkSoundId = UnkSoundId;
            data.WalkOnParticleSystemId1 = WalkOnParticleSystemId1;
            data.WalkOnParticleSystemId2 = WalkOnParticleSystemId2;
            data.UnkId3 = UnkId3;
            data.LandOnParticleSystemId = LandOnParticleSystemId;
            data.PhysicsParameters = CloneUtils.CloneArray(PhysicsParameters);
            data.UnkVec = new Twinsanity.TwinsanityInterchange.Common.Vector4
            {
                X = UnkVec.X,
                Y = UnkVec.Y,
                Z = UnkVec.Z,
                W = UnkVec.W
            };
            data.UnkBoundingBox[0] = new Twinsanity.TwinsanityInterchange.Common.Vector4
            {
                X = UnkBoundingBox[0].X,
                Y = UnkBoundingBox[0].Y,
                Z = UnkBoundingBox[0].Z,
                W = UnkBoundingBox[0].W
            };
            data.UnkBoundingBox[1] = new Twinsanity.TwinsanityInterchange.Common.Vector4
            {
                X = UnkBoundingBox[1].X,
                Y = UnkBoundingBox[1].Y,
                Z = UnkBoundingBox[1].Z,
                W = UnkBoundingBox[1].W
            };
        }

        public override void LoadData()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var surfData = asset.GetData<CollisionSurfaceData>();
            surfId = surfData.SurfaceID;
            flags = surfData.Flags;
            physicsParameters = CloneUtils.CloneArray(surfData.PhysicsParameters);
            stepSoundId1 = surfData.StepSoundId1;
            stepSoundId2 = surfData.StepSoundId2;
            landSoundId1 = surfData.LandSoundId1;
            landSoundId2 = surfData.LandSoundId2;
            unkSoundId = surfData.UnkSoundId;
            walkOnParticleSystemId1 = surfData.WalkOnParticleSystemId1;
            walkOnParticleSystemId2 = surfData.WalkOnParticleSystemId2;
            unkId3 = surfData.UnkId3;
            landOnParticleSystemId = surfData.LandOnParticleSystemId;
            unkVec = new Vector4ViewModel(surfData.UnkVec);
            unkBoundingBox = new Vector4ViewModel[2];
            for (var i = 0; i < unkBoundingBox.Length; ++i)
            {
                unkBoundingBox[i] = new Vector4ViewModel(surfData.UnkBoundingBox[i]);
            }
            layId = MiscUtils.ConvertEnum<Layouts>(asset.LayoutID!.Value);
        }

        public Layouts LayoutID
        {
            get => layId;
            set
            {
                if (layId != value)
                {
                    layId = value;
                    IsDirty = true;
                    NotifyOfPropertyChange();
                }
            }
        }

        public SurfaceType SurfId
        {
            get => surfId;
            set
            {
                if (surfId != value)
                {
                    surfId = value;
                    IsDirty = true;
                    NotifyOfPropertyChange();
                }
            }
        }

        public SurfaceFlags Flags
        {
            get => flags;
            set
            {
                if (flags != value)
                {
                    flags = value;
                    IsDirty = true;
                    NotifyOfPropertyChange();
                }
            }
        }

        public LabURI StepSoundId1
        {
            get => stepSoundId1;
            set
            {
                if (stepSoundId1 != value)
                {
                    stepSoundId1 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public LabURI StepSoundId2
        {
            get => stepSoundId2;
            set
            {
                if (stepSoundId2 != value)
                {
                    stepSoundId2 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public LabURI LandSoundId1
        {
            get => landSoundId1;
            set
            {
                if (landSoundId1 != value)
                {
                    landSoundId1 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public LabURI LandSoundId2
        {
            get => landSoundId2;
            set
            {
                if (landSoundId2 != value)
                {
                    landSoundId2 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public LabURI UnkSoundId
        {
            get => unkSoundId;
            set
            {
                if (unkSoundId != value)
                {
                    unkSoundId = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public UInt16 WalkOnParticleSystemId1
        {
            get => walkOnParticleSystemId1;
            set
            {
                if (walkOnParticleSystemId1 != value)
                {
                    walkOnParticleSystemId1 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public UInt16 WalkOnParticleSystemId2
        {
            get => walkOnParticleSystemId2;
            set
            {
                if (walkOnParticleSystemId2 != value)
                {
                    walkOnParticleSystemId2 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public UInt16 UnkId3
        {
            get => unkId3;
            set
            {
                if (unkId3 != value)
                {
                    unkId3 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public UInt16 LandOnParticleSystemId
        {
            get => landOnParticleSystemId;
            set
            {
                if (landOnParticleSystemId != value)
                {
                    landOnParticleSystemId = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public UInt16 UnkId5
        {
            get => unkId5;
            set
            {
                if (unkId5 != value)
                {
                    unkId5 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public Single[] PhysicsParameters
        {
            get => physicsParameters;
            set
            {
                if (physicsParameters != value)
                {
                    physicsParameters = value;
                    IsDirty = true;
                    NotifyOfPropertyChange();
                }
            }
        }

        public Vector4ViewModel UnkVec
        {
            get => unkVec;
        }

        public Vector4ViewModel[] UnkBoundingBox
        {
            get => unkBoundingBox;
        }
    }
}
