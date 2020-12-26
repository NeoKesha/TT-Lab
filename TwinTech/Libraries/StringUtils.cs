using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinsanity.Libraries
{
    static class StringUtils
    {
        static public string GetStringInBetween(String src, String str1, String str2)
        {
            int pos1 = src.IndexOf(str1) + str1.Length - 1;
            int pos2 = src.IndexOf(str2);
            return src.Substring(pos1 + 1, pos2 - pos1 - 1);
        }
        static public string GetStringAfter(String src, String str)
        {
            int pos = src.IndexOf(str) + str.Length - 1;
            return src.Substring(pos + 1);
        }
        static public string GetStringBefore(String src, String str)
        {
            int pos = src.IndexOf(str) + str.Length - 1;
            return src.Substring(0, pos);
        }
        static public string GetTabulatedString(String src, Int32 tabs)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Clear();
            for (int i = 0; i < tabs; ++i)
            {
                stringBuilder.Append("   ");
            }
            stringBuilder.Append(src);
            return stringBuilder.ToString();
        }
        static public void WriteLineTabulated(StreamWriter writer, String src, Int32 tabs)
        {
            writer.WriteLine(GetTabulatedString(src, tabs));
        }
        static public void WriteTabulated(StreamWriter writer, String src, Int32 tabs)
        {
            writer.Write(GetTabulatedString(src, tabs));
        }
    }
}
