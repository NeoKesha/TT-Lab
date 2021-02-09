using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Instance.Particles
{
    public class ParticleInstanceViewModel : SavebleViewModel
    {
        private UInt32 version;
        private Vector3ViewModel position = new Vector3ViewModel();
        private Int16 unkShort1;
        private Int16 unkShort2;
        private Int16 unkShort3;
        private Int16 unkShort4;
        private Int16 unkShort5;
        private Int32 unkInt1;
        private String name = "Particle Instance";
        private Int32 unkInt2;
        private Int32 unkInt3;
        private Single unkFloat1;
        private Int16 unkShort6;
        private Int16 unkShort7;
        private Single unkFloat2;
        private Single unkFloat3;
        private Int16 unkShort8;

        public ParticleInstanceViewModel() { }
        public ParticleInstanceViewModel(ParticleInstance pi)
        {
            version = pi.Version;
            name = new String(pi.Name);
            position = new Vector3ViewModel(pi.Position);
            unkShort1 = pi.UnkShort1;
            unkShort2 = pi.UnkShort2;
            unkShort3 = pi.UnkShort3;
            unkShort4 = pi.UnkShort4;
            unkShort5 = pi.UnkShort5;
            unkShort6 = pi.UnkShort6;
            unkShort7 = pi.UnkShort7;
            unkShort8 = pi.UnkShort8;
            unkInt1 = pi.UnkInt1;
            unkInt2 = pi.UnkInt2;
            unkInt3 = pi.UnkInt3;
            unkFloat1 = pi.UnkFloat1;
            unkFloat2 = pi.UnkFloat2;
            unkFloat3 = pi.UnkFloat3;
        }

        public override void Save(Object? o = null)
        {
            var pi = (ParticleInstance)o!;
            pi.Version = Version;
            pi.Name = Name.ToCharArray();
            pi.Position = new Vector3
            {
                X = Position.X,
                Y = Position.Y,
                Z = Position.Z
            };
            pi.UnkShort1 = UnkShort1;
            pi.UnkShort2 = UnkShort2;
            pi.UnkShort3 = UnkShort3;
            pi.UnkShort4 = UnkShort4;
            pi.UnkShort5 = UnkShort5;
            pi.UnkShort6 = UnkShort6;
            pi.UnkShort7 = UnkShort7;
            pi.UnkShort8 = UnkShort8;
            pi.UnkInt1 = UnkInt1;
            pi.UnkInt2 = UnkInt2;
            pi.UnkInt3 = UnkInt3;
            pi.UnkFloat1 = UnkFloat1;
            pi.UnkFloat2 = UnkFloat2;
            pi.UnkFloat3 = UnkFloat3;
        }

        public UInt32 Version
        {
            get
            {
                return version;
            }
            set
            {
                if (value != version)
                {
                    version = value;
                    NotifyChange();
                }
            }
        }
        public Vector3ViewModel Position
        {
            get
            {
                return position;
            }
            set
            {
                if (value != position)
                {
                    position = value;
                    NotifyChange();
                }
            }
        }
        public Int16 UnkShort1
        {
            get
            {
                return unkShort1;
            }
            set
            {
                if (value != unkShort1)
                {
                    unkShort1 = value;
                    NotifyChange();
                }
            }
        }
        public Int16 UnkShort2
        {
            get
            {
                return unkShort2;
            }
            set
            {
                if (value != unkShort2)
                {
                    unkShort2 = value;
                    NotifyChange();
                }
            }
        }
        public Int16 UnkShort3
        {
            get
            {
                return unkShort3;
            }
            set
            {
                if (value != unkShort3)
                {
                    unkShort3 = value;
                    NotifyChange();
                }
            }
        }
        public Int16 UnkShort4
        {
            get
            {
                return unkShort4;
            }
            set
            {
                if (value != unkShort4)
                {
                    unkShort4 = value;
                    NotifyChange();
                }
            }
        }
        public Int16 UnkShort5
        {
            get
            {
                return unkShort5;
            }
            set
            {
                if (value != unkShort5)
                {
                    unkShort5 = value;
                    NotifyChange();
                }
            }
        }
        public Int32 UnkInt1
        {
            get
            {
                return unkInt1;
            }
            set
            {
                if (value != unkInt1)
                {
                    unkInt1 = value;
                    NotifyChange();
                }
            }
        }
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
                    NotifyChange();
                }
            }
        }
        public Int32 UnkInt2
        {
            get
            {
                return unkInt2;
            }
            set
            {
                if (value != unkInt2)
                {
                    unkInt2 = value;
                    NotifyChange();
                }
            }
        }
        public Int32 UnkInt3
        {
            get
            {
                return unkInt3;
            }
            set
            {
                if (value != unkInt3)
                {
                    unkInt3 = value;
                    NotifyChange();
                }
            }
        }
        public Single UnkFloat1
        {
            get
            {
                return unkFloat1;
            }
            set
            {
                if (value != unkFloat1)
                {
                    unkFloat1 = value;
                    NotifyChange();
                }
            }
        }
        public Int16 UnkShort6
        {
            get
            {
                return unkShort6;
            }
            set
            {
                if (value != unkShort6)
                {
                    unkShort6 = value;
                    NotifyChange();
                }
            }
        }
        public Int16 UnkShort7
        {
            get
            {
                return unkShort7;
            }
            set
            {
                if (value != unkShort7)
                {
                    unkShort7 = value;
                    NotifyChange();
                }
            }
        }
        public Single UnkFloat2
        {
            get
            {
                return unkFloat2;
            }
            set
            {
                if (value != unkFloat2)
                {
                    unkFloat2 = value;
                    NotifyChange();
                }
            }
        }
        public Single UnkFloat3
        {
            get
            {
                return unkFloat3;
            }
            set
            {
                if (value != unkFloat3)
                {
                    unkFloat3 = value;
                    NotifyChange();
                }
            }
        }
        public Int16 UnkShort8
        {
            get
            {
                return unkShort8;
            }
            set
            {
                if (value != unkShort8)
                {
                    unkShort8 = value;
                    NotifyChange();
                }
            }
        }
    }
}
