using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab
{
    static public class CloneUtils
    {
        /**
         * Deep clone. Use ONLY when all other Cloning procedures do not apply.
         */
        static public T DeepClone<T>(T original)
        {
            using (var stream = new MemoryStream())
            {
                StreamWriter writer = new StreamWriter(stream);
                StreamReader reader = new StreamReader(stream);
                writer.Write(JsonConvert.SerializeObject(original));
                writer.Flush();
                stream.Position = 0;
                return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            }
        }
        /**
         * List clone. Use when Clone is aplicable for T
         */
        static public List<T> CloneList<T>(List<T> originalList) where T : new()
        {
            List<T> newList = new List<T>(originalList.Count);
            bool isPrimitive = typeof(T).IsPrimitive;
            if (isPrimitive)
            {
                foreach (T e in originalList)
                {
                    newList.Add(e);
                }
            } 
            else
            {
                foreach (T e in originalList)
                {
                    newList.Add(Clone(e));
                }
            }
            return newList;
        }
        /**
         * Array clone. Use when Clone is aplicable for T
         */
        static public T[] CloneArray<T>(T[] originalArray) where T : new()
        {
            T[] newArray = new T[originalArray.Length];
            bool isPrimitive = typeof(T).IsPrimitive;
            if (isPrimitive)
            {
                for (var i = 0; i < originalArray.Length; ++i)
                {
                    newArray[i] = originalArray[i];
                }
            }
            else
            {
                for (var i = 0; i < originalArray.Length; ++i)
                {
                    newArray[i] = Clone(originalArray[i]);
                }
            }
            return newArray;
        }
        /**
         * Unsafe List clone. Use when Clone in not is aplicable for T, but CloneUnsafe is
         */
        static public List<T> CloneListUnsafe<T>(List<T> originalList) where T : new()
        {
            List<T> newList = new List<T>(originalList.Count);
            bool isPrimitive = typeof(T).IsPrimitive;
            if (isPrimitive)
            {
                foreach (T e in originalList)
                {
                    newList.Add(e);
                }
            }
            else
            {
                foreach (T e in originalList)
                {
                    newList.Add(CloneUnsafe(e));
                }
            }
            return newList;
        }
        /**
         * Unsafe Array clone. Use when Clone in not is aplicable for T, but CloneUnsafe is
         */
        static public T[] CloneArrayUnsafe<T>(T[] originalArray) where T : new()
        {
            T[] newArray = new T[originalArray.Length];
            bool isPrimitive = typeof(T).IsPrimitive;
            if (isPrimitive)
            {
                for (var i = 0; i < originalArray.Length; ++i)
                {
                    newArray[i] = originalArray[i];
                }
            }
            else
            {
                for (var i = 0; i < originalArray.Length; ++i)
                {
                    newArray[i] = CloneUnsafe(originalArray[i]);
                }
            }
            return newArray;
        }
        /**
         * Surface clone. Use when cloned object contains only primitives
         */
        static public T Clone<T>(T original) where T : new()
        {
            T newObj = new T();
            Type t = typeof(T);
            var fields = t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                field.SetValue(newObj, field.GetValue(original));
            }
            return newObj;
        }
        /**
         * Unsafe deep clone. Use when cloning element has no loops or collections inside, but got references. IT IS UNSAFE! Better use DeepClone. Plz
         */
        static public T CloneUnsafe<T>(T original) where T : new()
        {
            T newObj = new T();
            Type t = typeof(T);
            var fields = t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                bool isPrimitive = typeof(T).IsPrimitive;
                if (isPrimitive)
                {
                    newObj = original;
                } 
                else
                {
                    object val = typeof(CloneUtils)
                    .GetMethod("CloneUnsafe")
                    .MakeGenericMethod(field.FieldType)
                    .Invoke(null, new Object[] { field.GetValue(original) });
                    field.SetValue(newObj, Convert.ChangeType(val, field.FieldType));
                }
            }
            return newObj;
        }
        //TODO: Go fuck myself
    }
}
