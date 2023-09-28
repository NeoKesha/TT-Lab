using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TT_Lab.Util
{
    static public class CloneUtils
    {
        /// <summary>
        /// Deep clone. Use ONLY when all other Cloning procedures do not apply.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
        static public T DeepClone<T>(T original)
        {
            using var stream = new MemoryStream();
            using StreamWriter writer = new StreamWriter(stream);
            using StreamReader reader = new StreamReader(stream);
            writer.Write(JsonConvert.SerializeObject(original));
            writer.Flush();
            stream.Position = 0;
            return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
        }
        /// <summary>
        /// Deep clone. Use ONLY when all other Cloning procedures do not apply.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        static public object DeepClone(object original, Type type)
        {
            using var stream = new MemoryStream();
            using StreamWriter writer = new StreamWriter(stream);
            using StreamReader reader = new StreamReader(stream);
            writer.Write(JsonConvert.SerializeObject(original));
            writer.Flush();
            stream.Position = 0;
            var deserObj = typeof(JsonConvert).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(m => m.Name == "DeserializeObject"
                && m.IsGenericMethod && m.GetParameters().Length == 1);
            return deserObj.MakeGenericMethod(type).Invoke(null, new object[] { reader.ReadToEnd() })!;
        }
        /// <summary>
        /// List clone. Use when Clone is aplicable for T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalList"></param>
        /// <returns></returns>
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
        static public List<T> CloneList<T>(List<T> originalList, Func<T, T> constructor)
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
                    newList.Add(constructor(e));
                }
            }
            return newList;
        }
        /// <summary>
        /// Array clone. Use when Clone is aplicable for T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalArray"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Unsafe List clone. Use when Clone in not is applicable for T, but CloneUnsafe is
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalList"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Unsafe Array clone. Use when Clone in not is aplicable for T, but CloneUnsafe is
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalArray"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Surface clone. Use when cloned object contains only primitives
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Unsafe deep clone. Use when cloning element has no loops or collections inside, but got references. IT IS UNSAFE! Better use DeepClone. Plz
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
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
