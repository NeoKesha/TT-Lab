using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Twinsanity.Libraries
{
    static public class CloneUtils
    {
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
    }
}
