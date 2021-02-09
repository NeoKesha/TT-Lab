using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace TT_Lab.ViewModels.Instance
{
    public class CollisionSurfaceViewModel : AssetViewModel
    {
        private Enums.Layouts layId;
        private UInt16 surfId;
        private UInt32 flags;
        private Guid stepSoundId1;
        private Guid stepSoundId2;
        private Guid landSoundId1;
        private Guid landSoundId2;
        private Guid unkSoundId;
        private UInt16 unkId1;
        private UInt16 unkId2;
        private UInt16 unkId3;
        private UInt16 unkId4;
        private UInt16 unkId5;
        private Single[] floatParams;
        private Vector4ViewModel unkVec;
        private Vector4ViewModel[] unkBoundingBox;

        public CollisionSurfaceViewModel(Guid asset, AssetViewModel parent) : base(asset, parent)
        {
            var surfData = (CollisionSurfaceData)_asset.GetData();
            surfId = surfData.SurfaceID;
            flags = surfData.Flags;
            floatParams = CloneUtils.CloneArray(surfData.Parameters);
            stepSoundId1 = surfData.StepSoundId1;
            stepSoundId2 = surfData.StepSoundId2;
            landSoundId1 = surfData.LandSoundId1;
            landSoundId2 = surfData.LandSoundId2;
            unkSoundId = surfData.UnkSoundId;
            unkId1 = surfData.UnkId1;
            unkId2 = surfData.UnkId2;
            unkId3 = surfData.UnkId3;
            unkId4 = surfData.UnkId4;
            unkId5 = surfData.UnkId5;
            unkVec = new Vector4ViewModel(surfData.UnkVec);
            unkVec.PropertyChanged += Vector_PropertyChanged;
            unkBoundingBox = new Vector4ViewModel[2];
            for (var i = 0; i < unkBoundingBox.Length; ++i)
            {
                unkBoundingBox[i] = new Vector4ViewModel(surfData.UnkBoundingBox[i]);
                unkBoundingBox[i].PropertyChanged += Vector_PropertyChanged;
            }
            layId = MiscUtils.ConvertEnum<Enums.Layouts>(_asset.LayoutID!.Value);
        }

        public override void Save(object? o)
        {
            _asset.LayoutID = (int)LayoutID;
            var data = (CollisionSurfaceData)_asset.GetData();
            data.SurfaceID = SurfId;
            data.Flags = Flags;
            data.StepSoundId1 = StepSoundId1;
            data.StepSoundId2 = StepSoundId2;
            data.LandSoundId1 = LandSoundId1;
            data.LandSoundId2 = LandSoundId2;
            data.UnkSoundId = UnkSoundId;
            data.UnkId1 = UnkId1;
            data.UnkId2 = UnkId2;
            data.UnkId3 = UnkId3;
            data.UnkId4 = UnkId4;
            data.UnkId5 = UnkId5;
            data.Parameters = CloneUtils.CloneArray(FloatParams);
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
            base.Save(o);
        }

        private void Vector_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(UnkVec));
            NotifyChange(nameof(unkBoundingBox));
        }

        public Enums.Layouts LayoutID
        {
            get => layId;
            set
            {
                if (layId != value)
                {
                    layId = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt16 SurfId
        {
            get => surfId;
            set
            {
                if (surfId != value)
                {
                    surfId = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt32 Flags
        {
            get => flags;
            set
            {
                if (flags != value)
                {
                    flags = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Guid StepSoundId1
        {
            get => stepSoundId1;
            set
            {
                if (stepSoundId1 != value)
                {
                    stepSoundId1 = value;
                    NotifyChange();
                }
            }
        }
        public Guid StepSoundId2
        {
            get => stepSoundId2;
            set
            {
                if (stepSoundId2 != value)
                {
                    stepSoundId2 = value;
                    NotifyChange();
                }
            }
        }
        public Guid LandSoundId1
        {
            get => landSoundId1;
            set
            {
                if (landSoundId1 != value)
                {
                    landSoundId1 = value;
                    NotifyChange();
                }
            }
        }
        public Guid LandSoundId2
        {
            get => landSoundId2;
            set
            {
                if (landSoundId2 != value)
                {
                    landSoundId2 = value;
                    NotifyChange();
                }
            }
        }
        public Guid UnkSoundId
        {
            get => unkSoundId;
            set
            {
                if (unkSoundId != value)
                {
                    unkSoundId = value;
                    NotifyChange();
                }
            }
        }
        public UInt16 UnkId1
        {
            get => unkId1;
            set
            {
                if (unkId1 != value)
                {
                    unkId1 = value;
                    NotifyChange();
                }
            }
        }
        public UInt16 UnkId2
        {
            get => unkId2;
            set
            {
                if (unkId2 != value)
                {
                    unkId2 = value;
                    NotifyChange();
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
                    NotifyChange();
                }
            }
        }
        public UInt16 UnkId4
        {
            get => unkId4;
            set
            {
                if (unkId4 != value)
                {
                    unkId4 = value;
                    NotifyChange();
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
                    NotifyChange();
                }
            }
        }
        public Single[] FloatParams
        {
            get => floatParams;
            set
            {
                if (floatParams != value)
                {
                    floatParams = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Vector4ViewModel UnkVec
        {
            get => unkVec;
            set
            {
                if (unkVec != value)
                {
                    unkVec = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Vector4ViewModel[] UnkBoundingBox
        {
            get => unkBoundingBox;
            set
            {
                if (unkBoundingBox != value)
                {
                    unkBoundingBox = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
    }
}
