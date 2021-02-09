using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace TT_Lab.Editors.Instance
{
    public static class Util
    {
        public readonly static ObservableCollection<object> Layers;

        static Util()
        {
            Layers = new ObservableCollection<object>(Enum.GetValues(typeof(Enums.Layouts)).Cast<object>());
        }
    }
}
