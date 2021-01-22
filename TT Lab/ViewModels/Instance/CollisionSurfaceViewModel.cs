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
        private Guid[] soundIds;
        private Single[] floatParams;
        private Vector4ViewModel unkVec;
        private Vector4ViewModel[] unkBoundingBox;

        public CollisionSurfaceViewModel(Guid asset, AssetViewModel parent) : base(asset, parent)
        {
            var surfData = (CollisionSurfaceData)_asset.GetData();
            surfId = surfData.SurfaceID;
            flags = surfData.Flags;
            soundIds = CloneUtils.CloneArray(surfData.StepSoundIds);
            floatParams = CloneUtils.CloneArray(surfData.Parameters);
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

        public override void Save()
        {
            _asset.LayoutID = (int)LayoutID;
            var data = (CollisionSurfaceData)_asset.GetData();
            data.SurfaceID = SurfId;
            data.Flags = Flags;
            data.StepSoundIds = CloneUtils.CloneArray(SoundIds);
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
            base.Save();
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
        public Guid[] SoundIds
        {
            get => soundIds;
            set
            {
                if (soundIds != value)
                {
                    soundIds = value;
                    IsDirty = true;
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
