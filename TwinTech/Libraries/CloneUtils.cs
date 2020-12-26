using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Twinsanity.Libraries
{
    static public class CloneUtils
    {
        static public T Clone<T>(T original) where T : new()
        {
            T newObj = new T();
            Type t = typeof(T);
            var fields = t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var copy = Activator.CreateInstance(t);
            for (int i = 0; i < fields.Length; i++)
            {
                fields[i].SetValue(copy, fields[i].GetValue(original));
            }
            return newObj;
        }
    }
}
