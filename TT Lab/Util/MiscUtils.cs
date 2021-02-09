using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace TT_Lab.Util
{
    public static class MiscUtils
    {
        private static Bitmap _boatguy;

        public static object ConvertEnum(Type t, object o)
        {
            return Enum.Parse(t, o.ToString());
        }

        public static T ConvertEnum<T>(object o)
        {
            return (T)ConvertEnum(typeof(T), o);
        }

        public static Bitmap GetBoatGuy()
        {
            if (_boatguy == null)
            {
                _boatguy = new Bitmap(ManifestResourceLoader.GetPathInExe("Images\\boat_guy.png"));
            }
            return _boatguy;
        }

        // Ah yes, WinForms being garbage as usual
        public static object GetDragDropData(this System.Windows.Forms.IDataObject dataObject)
        {
            var info = dataObject.GetType().GetField("innerData", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var obj = info.GetValue(dataObject);
            info = obj.GetType().GetField("innerData", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            obj = info.GetValue(obj);
            info = obj.GetType().GetField("_innerData", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            obj = info.GetValue(obj);
            info = obj.GetType().GetField("_data", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var table = info.GetValue(obj) as System.Collections.Hashtable;
            var values = (object[])table.Cast<System.Collections.DictionaryEntry>().First().Value;
            var value = values[0];
            info = value.GetType().GetField("_data", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return info.GetValue(value);
        }

        public static string GetFileFromDialogue(string filter, string initial_directory = "")
        {
            using (System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog
            {
                InitialDirectory = initial_directory,
                Filter = filter
            })
            {
                if (System.Windows.Forms.DialogResult.OK == ofd.ShowDialog())
                {
                    return ofd.FileName;
                }
            }
            return string.Empty;
        }

        public static Enums.InstanceState ChangeFlag(this Enums.InstanceState state, Enums.InstanceState flags, Boolean set)
        {
            if (!set)
            {
                return state.UnsetFlag(flags);
            }
            return state.SetFlag(flags);
        }
        public static Enums.InstanceState SetFlag(this Enums.InstanceState state, Enums.InstanceState flags)
        {
            state |= flags;
            return state;
        }
        public static Enums.InstanceState UnsetFlag(this Enums.InstanceState state, Enums.InstanceState flags)
        {
            state &= ~flags;
            return state;
        }

        public static Enums.TriggerActivatorObjects ChangeFlag(this Enums.TriggerActivatorObjects state, Enums.TriggerActivatorObjects flags, Boolean set)
        {
            if (!set)
            {
                return state.UnsetFlag(flags);
            }
            return state.SetFlag(flags);
        }
        public static Enums.TriggerActivatorObjects SetFlag(this Enums.TriggerActivatorObjects state, Enums.TriggerActivatorObjects flags)
        {
            state |= flags;
            return state;
        }
        public static Enums.TriggerActivatorObjects UnsetFlag(this Enums.TriggerActivatorObjects state, Enums.TriggerActivatorObjects flags)
        {
            state &= ~flags;
            return state;
        }
    }
}
