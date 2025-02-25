using Caliburn.Micro;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.Attributes;
using TT_Lab.Util;
using TT_Lab.ViewModels.Composite;
using TT_Lab.ViewModels.Interfaces;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.Particles;

namespace TT_Lab.ViewModels.Editors.Instance.Particles
{
    public class ParticleTypeViewModel : Conductor<IScreen>.Collection.AllActive, ISaveableViewModel<TwinParticleSystem>, IHaveChildrenEditors
    {
        private UInt32 version;
        private String name = "Particle Type";
        private Byte unkByte1;
        private UInt16 unkUShort1;
        private UInt16 unkUShort2;
        private UInt16 unkUShort3;
        private UInt16 unkUShort4;
        private UInt16 unkUShort5;
        private UInt16 unkUShort6;
        private UInt16 unkUShort7;
        private Byte unkByte2;
        private Byte unkByte3;
        private Byte unkByte4;
        private Byte unkByte5;
        private Single unkFloat1;
        private Single unkFloat2;
        private Single unkFloat3;
        private Single unkFloat4;
        private Single unkFloat5;
        private Single unkFloat6;
        private Single unkFloat7;
        private Vector3ViewModel unkVec1 = new();
        private Vector3ViewModel unkVec2 = new();
        private Single unkFloat8;
        private Single unkFloat9;
        private Single unkFloat10;
        private Single unkFloat11;
        private Single unkFloat12;
        private Single unkFloat13;
        private Single unkFloat14;
        private Single unkFloat15;
        private Single unkFloat16;
        private Single unkFloat17;
        private Single unkFloat18;
        private Single unkFloat19;
        private Single unkFloat20;
        private Single unkFloat21;
        private UInt16 unkUShort8;
        private Byte unkByte6;
        private Byte unkByte7;
        private Single unkFloat22;
        private Single unkFloat23;
        private Single unkFloat24;
        private Single unkFloat25;
        private Single unkFloat26;
        private BindableCollection<Vector4ViewModel> unkVecs = new();
        private BindableCollection<PrimitiveWrapperViewModel<Int64>> unkLongs1 = new();
        private Single unkFloat27;
        private Single unkFloat28;
        private Single unkFloat29;
        private Single unkFloat30;
        private BindableCollection<PrimitiveWrapperViewModel<Int64>> unkLongs2 = new();
        private BindableCollection<PrimitiveWrapperViewModel<Int64>> unkLongs3 = new();
        private Single unkFloat31;
        private Single unkFloat32;
        private BindableCollection<PrimitiveWrapperViewModel<Int64>> unkLongs4 = new();
        private BindableCollection<PrimitiveWrapperViewModel<Int64>> unkLongs5 = new();
        private BindableCollection<PrimitiveWrapperViewModel<Int64>> unkLongs6 = new();
        private Single unkFloat33;
        private Single unkFloat34;
        private Single unkFloat35;
        private Single unkFloat36;
        private BindableCollection<PrimitiveWrapperViewModel<Int64>> unkLongs7 = new();
        private Byte unkByte8;
        private Byte unkByte9;
        private Single unkFloat37;
        private BindableCollection<PrimitiveWrapperViewModel<Int16>> unkShorts = new();
        private Single unkFloat38;
        private Single unkFloat39;
        private Single unkFloat40;
        private Int32 unkInt;
        private Vector4ViewModel unkVec3 = new();
        private bool isDirty;
        private DirtyTracker dirtyTracker;

        public ParticleTypeViewModel()
        {
            dirtyTracker = new DirtyTracker(this);
            dirtyTracker.AddChild(unkVec1);
            dirtyTracker.AddChild(unkVec2);
            dirtyTracker.AddChild(unkVec3);
            dirtyTracker.AddBindableCollection(unkVecs);
            dirtyTracker.AddBindableCollection(unkLongs1);
            dirtyTracker.AddBindableCollection(unkLongs2);
            dirtyTracker.AddBindableCollection(unkLongs3);
            dirtyTracker.AddBindableCollection(unkLongs4);
            dirtyTracker.AddBindableCollection(unkLongs5);
            dirtyTracker.AddBindableCollection(unkLongs6);
            dirtyTracker.AddBindableCollection(unkLongs7);
            dirtyTracker.AddBindableCollection(unkShorts);
        }

        public ParticleTypeViewModel(TwinParticleSystem pt)
        {
            dirtyTracker = new DirtyTracker(this);
            version = pt.Version;
            name = new String(pt.Name);
            unkByte1 = pt.UnkByte1;
            unkByte2 = pt.UnkByte2;
            unkByte3 = pt.UnkByte3;
            unkByte4 = pt.UnkByte4;
            unkByte5 = pt.UnkByte5;
            unkByte6 = pt.UnkByte6;
            unkByte7 = pt.UnkByte7;
            unkByte8 = pt.UnkByte8;
            unkByte9 = pt.UnkByte9;
            unkUShort1 = pt.UnkUShort1;
            unkUShort2 = pt.UnkUShort2;
            unkUShort3 = pt.UnkUShort3;
            unkUShort4 = pt.UnkUShort4;
            unkUShort5 = pt.UnkUShort5;
            unkUShort6 = pt.UnkUShort6;
            unkUShort7 = pt.UnkUShort7;
            unkUShort8 = pt.UnkUShort8;
            unkFloat1 = pt.UnkFloat1;
            unkFloat2 = pt.UnkFloat2;
            unkFloat3 = pt.UnkFloat3;
            unkFloat4 = pt.UnkFloat4;
            unkFloat5 = pt.UnkFloat5;
            unkFloat6 = pt.UnkFloat6;
            unkFloat7 = pt.UnkFloat7;
            unkFloat8 = pt.UnkFloat8;
            unkFloat9 = pt.UnkFloat9;
            unkFloat10 = pt.UnkFloat10;
            unkFloat11 = pt.UnkFloat11;
            unkFloat12 = pt.UnkFloat12;
            unkFloat13 = pt.UnkFloat13;
            unkFloat14 = pt.UnkFloat14;
            unkFloat15 = pt.UnkFloat15;
            unkFloat16 = pt.UnkFloat16;
            unkFloat17 = pt.UnkFloat17;
            unkFloat18 = pt.UnkFloat18;
            unkFloat19 = pt.UnkFloat19;
            unkFloat20 = pt.UnkFloat20;
            unkFloat21 = pt.UnkFloat21;
            unkFloat22 = pt.UnkFloat22;
            unkFloat23 = pt.UnkFloat23;
            unkFloat24 = pt.UnkFloat24;
            unkFloat25 = pt.UnkFloat25;
            unkFloat26 = pt.UnkFloat26;
            unkFloat27 = pt.UnkFloat27;
            unkFloat28 = pt.UnkFloat28;
            unkFloat29 = pt.UnkFloat29;
            unkFloat30 = pt.UnkFloat30;
            unkFloat31 = pt.UnkFloat31;
            unkFloat32 = pt.UnkFloat32;
            unkFloat33 = pt.UnkFloat33;
            unkFloat34 = pt.UnkFloat34;
            unkFloat35 = pt.UnkFloat35;
            unkFloat36 = pt.UnkFloat36;
            unkFloat37 = pt.UnkFloat37;
            unkFloat38 = pt.UnkFloat38;
            unkFloat39 = pt.UnkFloat39;
            unkFloat40 = pt.UnkFloat40;
            unkVec1 = new Vector3ViewModel(pt.UnkVec1);
            unkVec2 = new Vector3ViewModel(pt.UnkVec2);
            unkVec3 = new Vector4ViewModel(pt.UnkVec3);
            dirtyTracker.AddChild(unkVec1);
            dirtyTracker.AddChild(unkVec2);
            dirtyTracker.AddChild(unkVec3);
            unkVecs = new BindableCollection<Vector4ViewModel>();
            dirtyTracker.AddBindableCollection(unkVecs);
            for (var i = 0; i < pt.UnkVecs.Length; ++i)
            {
                unkVecs.Add(new Vector4ViewModel(pt.UnkVecs[i]));
            }
            unkLongs1 = new BindableCollection<PrimitiveWrapperViewModel<Int64>>();
            dirtyTracker.AddBindableCollection(unkLongs1);
            for (int i = 0; i < pt.UnkLongs1.Length; i++)
            {
                unkLongs1.Add(new PrimitiveWrapperViewModel<Int64>(pt.UnkLongs1[i]));
            }
            unkLongs2 = new BindableCollection<PrimitiveWrapperViewModel<Int64>>();
            dirtyTracker.AddBindableCollection(unkLongs2);
            for (int i = 0; i < pt.UnkLongs2.Length; i++)
            {
                unkLongs2.Add(new PrimitiveWrapperViewModel<Int64>(pt.UnkLongs2[i]));
            }
            unkLongs3 = new BindableCollection<PrimitiveWrapperViewModel<Int64>>();
            dirtyTracker.AddBindableCollection(unkLongs3);
            for (int i = 0; i < pt.UnkLongs3.Length; i++)
            {
                unkLongs3.Add(new PrimitiveWrapperViewModel<Int64>(pt.UnkLongs3[i]));
            }
            unkLongs4 = new BindableCollection<PrimitiveWrapperViewModel<Int64>>();
            dirtyTracker.AddBindableCollection(unkLongs4);
            for (int i = 0; i < pt.UnkLongs4.Length; i++)
            {
                unkLongs4.Add(new PrimitiveWrapperViewModel<Int64>(pt.UnkLongs4[i]));
            }
            unkLongs5 = new BindableCollection<PrimitiveWrapperViewModel<Int64>>();
            dirtyTracker.AddBindableCollection(unkLongs5);
            for (int i = 0; i < pt.UnkLongs5.Length; i++)
            {
                unkLongs5.Add(new PrimitiveWrapperViewModel<Int64>(pt.UnkLongs5[i]));
            }
            unkLongs6 = new BindableCollection<PrimitiveWrapperViewModel<Int64>>();
            dirtyTracker.AddBindableCollection(unkLongs6);
            for (int i = 0; i < pt.UnkLongs6.Length; i++)
            {
                unkLongs1.Add(new PrimitiveWrapperViewModel<Int64>(pt.UnkLongs6[i]));
            }
            unkLongs1 = new BindableCollection<PrimitiveWrapperViewModel<Int64>>();
            dirtyTracker.AddBindableCollection(unkLongs7);
            for (int i = 0; i < pt.UnkLongs7.Length; i++)
            {
                unkLongs7.Add(new PrimitiveWrapperViewModel<Int64>(pt.UnkLongs7[i]));
            }
            unkShorts = new BindableCollection<PrimitiveWrapperViewModel<Int16>>();
            dirtyTracker.AddBindableCollection(unkShorts);
            for (int i = 0; i < pt.UnkShorts.Length; i++)
            {
                unkShorts.Add(new PrimitiveWrapperViewModel<Int16>(pt.UnkShorts[i]));
            }
        }

        public void ResetDirty()
        {
            dirtyTracker.ResetDirty();
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

        public void Save(TwinParticleSystem pt)
        {
            pt.Version = Version;
            pt.Name = Name.ToCharArray();
            pt.UnkByte1 = UnkByte1;
            pt.UnkByte2 = UnkByte2;
            pt.UnkByte3 = UnkByte3;
            pt.UnkByte4 = UnkByte4;
            pt.UnkByte5 = UnkByte5;
            pt.UnkByte6 = UnkByte6;
            pt.UnkByte7 = UnkByte7;
            pt.UnkByte8 = UnkByte8;
            pt.UnkByte9 = UnkByte9;
            pt.UnkUShort1 = UnkUShort1;
            pt.UnkUShort2 = UnkUShort2;
            pt.UnkUShort3 = UnkUShort3;
            pt.UnkUShort4 = UnkUShort4;
            pt.UnkUShort5 = UnkUShort5;
            pt.UnkUShort6 = UnkUShort6;
            pt.UnkUShort7 = UnkUShort7;
            pt.UnkUShort8 = UnkUShort8;
            pt.UnkFloat1 = UnkFloat1;
            pt.UnkFloat2 = UnkFloat2;
            pt.UnkFloat3 = UnkFloat3;
            pt.UnkFloat4 = UnkFloat4;
            pt.UnkFloat5 = UnkFloat5;
            pt.UnkFloat6 = UnkFloat6;
            pt.UnkFloat7 = UnkFloat7;
            pt.UnkFloat8 = UnkFloat8;
            pt.UnkFloat9 = UnkFloat9;
            pt.UnkFloat10 = UnkFloat10;
            pt.UnkFloat11 = UnkFloat11;
            pt.UnkFloat12 = UnkFloat12;
            pt.UnkFloat13 = UnkFloat13;
            pt.UnkFloat14 = UnkFloat14;
            pt.UnkFloat15 = UnkFloat15;
            pt.UnkFloat16 = UnkFloat16;
            pt.UnkFloat17 = UnkFloat17;
            pt.UnkFloat18 = UnkFloat18;
            pt.UnkFloat19 = UnkFloat19;
            pt.UnkFloat20 = UnkFloat20;
            pt.UnkFloat21 = UnkFloat21;
            pt.UnkFloat22 = UnkFloat22;
            pt.UnkFloat23 = UnkFloat23;
            pt.UnkFloat24 = UnkFloat24;
            pt.UnkFloat25 = UnkFloat25;
            pt.UnkFloat26 = UnkFloat26;
            pt.UnkFloat27 = UnkFloat27;
            pt.UnkFloat28 = UnkFloat28;
            pt.UnkFloat29 = UnkFloat29;
            pt.UnkFloat30 = UnkFloat30;
            pt.UnkFloat31 = UnkFloat31;
            pt.UnkFloat32 = UnkFloat32;
            pt.UnkFloat33 = UnkFloat33;
            pt.UnkFloat34 = UnkFloat34;
            pt.UnkFloat35 = UnkFloat35;
            pt.UnkFloat36 = UnkFloat36;
            pt.UnkFloat37 = UnkFloat37;
            pt.UnkFloat38 = UnkFloat38;
            pt.UnkFloat39 = UnkFloat39;
            pt.UnkFloat40 = UnkFloat40;
            pt.UnkVec1 = new Vector3
            {
                X = UnkVec1.X,
                Y = UnkVec1.Y,
                Z = UnkVec1.Z,
            };
            pt.UnkVec2 = new Vector3
            {
                X = UnkVec2.X,
                Y = UnkVec2.Y,
                Z = UnkVec2.Z,
            };
            pt.UnkVec3 = new Vector4
            {
                X = UnkVec3.X,
                Y = UnkVec3.Y,
                Z = UnkVec3.Z,
                W = UnkVec3.W,
            };
            pt.UnkVecs = new Vector4[UnkVecs.Count];
            for (var i = 0; i < UnkVecs.Count; ++i)
            {
                pt.UnkVecs[i] = new Vector4
                {
                    X = UnkVecs[i].X,
                    Y = UnkVecs[i].Y,
                    Z = UnkVecs[i].Z,
                    W = UnkVecs[i].W,
                };
            }
            pt.UnkShorts = new Int16[UnkShorts.Count];
            for (var i = 0; i < UnkShorts.Count; ++i)
            {
                pt.UnkShorts[i] = UnkShorts[i].Value;
            }
            pt.UnkLongs1 = new Int64[UnkLongs1.Count];
            for (var i = 0; i < UnkLongs1.Count; ++i)
            {
                pt.UnkLongs1[i] = UnkLongs1[i].Value;
            }
            pt.UnkLongs2 = new Int64[UnkLongs2.Count];
            for (var i = 0; i < UnkLongs2.Count; ++i)
            {
                pt.UnkLongs2[i] = UnkLongs2[i].Value;
            }
            pt.UnkLongs3 = new Int64[UnkLongs3.Count];
            for (var i = 0; i < UnkLongs3.Count; ++i)
            {
                pt.UnkLongs3[i] = UnkLongs3[i].Value;
            }
            pt.UnkLongs4 = new Int64[UnkLongs4.Count];
            for (var i = 0; i < UnkLongs4.Count; ++i)
            {
                pt.UnkLongs4[i] = UnkLongs4[i].Value;
            }
            pt.UnkLongs5 = new Int64[UnkLongs5.Count];
            for (var i = 0; i < UnkLongs5.Count; ++i)
            {
                pt.UnkLongs5[i] = UnkLongs5[i].Value;
            }
            pt.UnkLongs6 = new Int64[UnkLongs6.Count];
            for (var i = 0; i < UnkLongs6.Count; ++i)
            {
                pt.UnkLongs6[i] = UnkLongs6[i].Value;
            }
            pt.UnkLongs7 = new Int64[UnkLongs7.Count];
            for (var i = 0; i < UnkLongs7.Count; ++i)
            {
                pt.UnkLongs7[i] = UnkLongs7[i].Value;
            }
            pt.UnkInt = UnkInt;
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            ActivateItemAsync(unkVec1, cancellationToken);
            ActivateItemAsync(unkVec2, cancellationToken);
            ActivateItemAsync(unkVec3, cancellationToken);

            foreach (var unkVec in unkVecs)
            {
                ActivateItemAsync(unkVec, cancellationToken);
            }

            return base.OnInitializeAsync(cancellationToken);
        }

        public UInt32 Version => version;

        [MarkDirty]
        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value != name && value.Length <= 16)
                {
                    name = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Byte UnkByte1
        {
            get
            {
                return unkByte1;
            }
            set
            {
                if (value != unkByte1)
                {
                    unkByte1 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public UInt16 UnkUShort1
        {
            get
            {
                return unkUShort1;
            }
            set
            {
                if (value != unkUShort1)
                {
                    unkUShort1 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public UInt16 UnkUShort2
        {
            get
            {
                return unkUShort2;
            }
            set
            {
                if (value != unkUShort2)
                {
                    unkUShort2 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public UInt16 UnkUShort3
        {
            get
            {
                return unkUShort3;
            }
            set
            {
                if (value != unkUShort3)
                {
                    unkUShort3 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public UInt16 UnkUShort4
        {
            get
            {
                return unkUShort4;
            }
            set
            {
                if (value != unkUShort4)
                {
                    unkUShort4 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public UInt16 UnkUShort5
        {
            get
            {
                return unkUShort5;
            }
            set
            {
                if (value != unkUShort5)
                {
                    unkUShort5 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public UInt16 UnkUShort6
        {
            get
            {
                return unkUShort6;
            }
            set
            {
                if (value != unkUShort6)
                {
                    unkUShort6 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public UInt16 UnkUShort7
        {
            get
            {
                return unkUShort7;
            }
            set
            {
                if (value != unkUShort7)
                {
                    unkUShort7 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Byte UnkByte2
        {
            get
            {
                return unkByte2;
            }
            set
            {
                if (value != unkByte2)
                {
                    unkByte2 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Byte UnkByte3
        {
            get
            {
                return unkByte3;
            }
            set
            {
                if (value != unkByte3)
                {
                    unkByte3 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Byte UnkByte4
        {
            get
            {
                return unkByte4;
            }
            set
            {
                if (value != unkByte4)
                {
                    unkByte4 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Byte UnkByte5
        {
            get
            {
                return unkByte5;
            }
            set
            {
                if (value != unkByte5)
                {
                    unkByte5 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat1
        {
            get
            {
                return unkFloat1;
            }
            set
            {
                if (unkFloat1 != value)
                {
                    unkFloat1 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat2
        {
            get
            {
                return unkFloat2;
            }
            set
            {
                if (unkFloat2 != value)
                {
                    unkFloat2 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat3
        {
            get
            {
                return unkFloat3;
            }
            set
            {
                if (unkFloat3 != value)
                {
                    unkFloat3 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat4
        {
            get
            {
                return unkFloat4;
            }
            set
            {
                if (unkFloat4 != value)
                {
                    unkFloat4 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat5
        {
            get
            {
                return unkFloat5;
            }
            set
            {
                if (unkFloat5 != value)
                {
                    unkFloat5 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat6
        {
            get
            {
                return unkFloat6;
            }
            set
            {
                if (unkFloat6 != value)
                {
                    unkFloat6 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat7
        {
            get
            {
                return unkFloat7;
            }
            set
            {
                if (unkFloat7 != value)
                {
                    unkFloat7 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public Vector3ViewModel UnkVec1
        {
            get => unkVec1;
        }

        public Vector3ViewModel UnkVec2
        {
            get => unkVec2;
        }

        [MarkDirty]
        public Single UnkFloat8
        {
            get
            {
                return unkFloat8;
            }
            set
            {
                if (unkFloat8 != value)
                {
                    unkFloat9 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat9
        {
            get
            {
                return unkFloat9;
            }
            set
            {
                if (unkFloat9 != value)
                {
                    unkFloat9 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat10
        {
            get
            {
                return unkFloat10;
            }
            set
            {
                if (unkFloat10 != value)
                {
                    unkFloat10 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat11
        {
            get
            {
                return unkFloat11;
            }
            set
            {
                if (unkFloat11 != value)
                {
                    unkFloat11 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat12
        {
            get
            {
                return unkFloat12;
            }
            set
            {
                if (unkFloat12 != value)
                {
                    unkFloat12 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat13
        {
            get
            {
                return unkFloat13;
            }
            set
            {
                if (unkFloat13 != value)
                {
                    unkFloat13 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat14
        {
            get
            {
                return unkFloat14;
            }
            set
            {
                if (unkFloat14 != value)
                {
                    unkFloat14 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat15
        {
            get
            {
                return unkFloat15;
            }
            set
            {
                if (unkFloat15 != value)
                {
                    unkFloat15 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat16
        {
            get
            {
                return unkFloat16;
            }
            set
            {
                if (unkFloat16 != value)
                {
                    unkFloat16 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat17
        {
            get
            {
                return unkFloat17;
            }
            set
            {
                if (unkFloat17 != value)
                {
                    unkFloat17 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat18
        {
            get
            {
                return unkFloat18;
            }
            set
            {
                if (unkFloat18 != value)
                {
                    unkFloat18 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat19
        {
            get
            {
                return unkFloat19;
            }
            set
            {
                if (unkFloat19 != value)
                {
                    unkFloat19 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat20
        {
            get
            {
                return unkFloat20;
            }
            set
            {
                if (unkFloat20 != value)
                {
                    unkFloat20 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat21
        {
            get
            {
                return unkFloat21;
            }
            set
            {
                if (unkFloat21 != value)
                {
                    unkFloat21 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public UInt16 UnkUShort8
        {
            get
            {
                return unkUShort8;
            }
            set
            {
                if (value != unkUShort8)
                {
                    unkUShort8 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Byte UnkByte6
        {
            get
            {
                return unkByte6;
            }
            set
            {
                if (value != unkByte6)
                {
                    unkByte6 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Byte UnkByte7
        {
            get
            {
                return unkByte7;
            }
            set
            {
                if (value != unkByte7)
                {
                    unkByte7 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat22
        {
            get
            {
                return unkFloat22;
            }
            set
            {
                if (unkFloat22 != value)
                {
                    unkFloat22 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat23
        {
            get
            {
                return unkFloat23;
            }
            set
            {
                if (unkFloat23 != value)
                {
                    unkFloat23 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat24
        {
            get
            {
                return unkFloat24;
            }
            set
            {
                if (unkFloat24 != value)
                {
                    unkFloat24 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat25
        {
            get
            {
                return unkFloat25;
            }
            set
            {
                if (unkFloat25 != value)
                {
                    unkFloat25 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat26
        {
            get
            {
                return unkFloat26;
            }
            set
            {
                if (unkFloat26 != value)
                {
                    unkFloat26 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public BindableCollection<Vector4ViewModel> UnkVecs
        {
            get => unkVecs;
        }

        public BindableCollection<PrimitiveWrapperViewModel<Int64>> UnkLongs1 => unkLongs1;

        [MarkDirty]
        public Single UnkFloat27
        {
            get
            {
                return unkFloat27;
            }
            set
            {
                if (unkFloat27 != value)
                {
                    unkFloat27 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat28
        {
            get
            {
                return unkFloat28;
            }
            set
            {
                if (unkFloat28 != value)
                {
                    unkFloat28 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat29
        {
            get
            {
                return unkFloat29;
            }
            set
            {
                if (unkFloat29 != value)
                {
                    unkFloat29 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat30
        {
            get
            {
                return unkFloat30;
            }
            set
            {
                if (unkFloat30 != value)
                {
                    unkFloat30 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public BindableCollection<PrimitiveWrapperViewModel<Int64>> UnkLongs2 => unkLongs2;

        public BindableCollection<PrimitiveWrapperViewModel<Int64>> UnkLongs3 => unkLongs3;

        [MarkDirty]
        public Single UnkFloat31
        {
            get
            {
                return unkFloat31;
            }
            set
            {
                if (unkFloat31 != value)
                {
                    unkFloat31 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat32
        {
            get
            {
                return unkFloat32;
            }
            set
            {
                if (unkFloat32 != value)
                {
                    unkFloat32 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public BindableCollection<PrimitiveWrapperViewModel<Int64>> UnkLongs4 => unkLongs4;

        public BindableCollection<PrimitiveWrapperViewModel<Int64>> UnkLongs5 => unkLongs5;

        public BindableCollection<PrimitiveWrapperViewModel<Int64>> UnkLongs6 => unkLongs6;

        [MarkDirty]
        public Single UnkFloat33
        {
            get
            {
                return unkFloat33;
            }
            set
            {
                if (unkFloat33 != value)
                {
                    unkFloat33 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat34
        {
            get
            {
                return unkFloat34;
            }
            set
            {
                if (unkFloat34 != value)
                {
                    unkFloat34 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat35
        {
            get
            {
                return unkFloat35;
            }
            set
            {
                if (unkFloat35 != value)
                {
                    unkFloat35 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat36
        {
            get
            {
                return unkFloat36;
            }
            set
            {
                if (unkFloat36 != value)
                {
                    unkFloat36 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public BindableCollection<PrimitiveWrapperViewModel<Int64>> UnkLongs7 => unkLongs7;

        [MarkDirty]
        public Byte UnkByte8
        {
            get
            {
                return unkByte8;
            }
            set
            {
                if (value != unkByte8)
                {
                    unkByte8 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Byte UnkByte9
        {
            get
            {
                return unkByte9;
            }
            set
            {
                if (value != unkByte9)
                {
                    unkByte9 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat37
        {
            get
            {
                return unkFloat37;
            }
            set
            {
                if (unkFloat37 != value)
                {
                    unkFloat37 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public BindableCollection<PrimitiveWrapperViewModel<Int16>> UnkShorts => unkShorts;

        [MarkDirty]
        public Single UnkFloat38
        {
            get
            {
                return unkFloat38;
            }
            set
            {
                if (unkFloat38 != value)
                {
                    unkFloat38 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat39
        {
            get
            {
                return unkFloat39;
            }
            set
            {
                if (unkFloat39 != value)
                {
                    unkFloat39 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat40
        {
            get
            {
                return unkFloat40;
            }
            set
            {
                if (unkFloat40 != value)
                {
                    unkFloat40 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Int32 UnkInt
        {
            get
            {
                return unkInt;
            }
            set
            {
                if (value != unkInt)
                {
                    unkInt = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public Vector4ViewModel UnkVec3
        {
            get => unkVec3;
        }

        public DirtyTracker DirtyTracker => dirtyTracker;
    }
}
