using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Util
{
    public static class MiscUtils
    {
        public static T ConvertEnum<T>(object o)
        {
            return (T)Enum.Parse(typeof(T), o.ToString());
        }
    }
}
