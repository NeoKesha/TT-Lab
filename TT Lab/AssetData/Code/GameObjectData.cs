using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.AssetData.Code
{
    public class GameObjectData : AbstractAssetData
    {
        public GameObjectData()
        {
        }

        public GameObjectData(PS2AnyObject gameObject) : this()
        {
            Bitfield = gameObject.Bitfield;
            SlotsMap = (Byte[])gameObject.SlotsMap.Clone();
            Name = gameObject.Name;
            UInt32Slots = new List<UInt32>(gameObject.UInt32Slots);
            OGISlots = new List<UInt16>(gameObject.OGISlots);
            AnimationSlots = new List<UInt16>(gameObject.AnimationSlots);
            ScriptSlots = new List<UInt16>(gameObject.ScriptSlots);
            ObjectSlots = new List<UInt16>(gameObject.ObjectSlots);
            SoundSlots = new List<UInt16>(gameObject.SoundSlots);
            InstancePropsHeader = gameObject.InstancePropsHeader;
            UnkUInt = gameObject.UnkUInt;
            InstFlags = new List<UInt32>(gameObject.InstFlags);
            InstFloats = new List<Single>(gameObject.InstFloats);
            InstIntegers = new List<UInt32>(gameObject.InstIntegers);
            RefObjects = new List<UInt16>(gameObject.RefObjects);
            RefOGIs = new List<UInt16>(gameObject.RefOGIs);
            RefAnimations = new List<UInt16>(gameObject.RefAnimations);
            RefCodeModels = new List<UInt16>(gameObject.RefCodeModels);
            RefScripts = new List<UInt16>(gameObject.RefScripts);
            RefUnknowns = new List<UInt16>(gameObject.RefUnknowns);
            RefSounds = new List<UInt16>(gameObject.RefSounds);
            ScriptPack = gameObject.ScriptPack;
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 Bitfield { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte[] SlotsMap { get; set; }
        [JsonProperty(Required = Required.Always)]
        public String Name { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt32> UInt32Slots { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> OGISlots { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> AnimationSlots { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> ScriptSlots { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> ObjectSlots { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> SoundSlots { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 InstancePropsHeader { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkUInt { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt32> InstFlags { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Single> InstFloats { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt32> InstIntegers { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> RefObjects { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> RefOGIs { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> RefAnimations { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> RefCodeModels { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> RefScripts { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> RefUnknowns { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> RefSounds { get; set; }
        [JsonProperty(Required = Required.Always)]
        [JsonConverter(typeof(ScriptPackConverter))]
        public ScriptPack ScriptPack;

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
    class ScriptPackConverter : JsonConverter<ScriptPack>
    {
        public override ScriptPack ReadJson(JsonReader reader, Type objectType, ScriptPack existingValue, Boolean hasExistingValue, JsonSerializer serializer)
        {
            if (existingValue == null)
            {
                existingValue = new ScriptPack();
            }
            String str = reader.ReadAsString();
            using (MemoryStream stream = new MemoryStream())
            using (StreamWriter _writer = new StreamWriter(stream))
            using (StreamReader _reader = new StreamReader(stream))
            {
                _writer.Write(str);
                stream.Position = 0;
                existingValue.ReadText(_reader);
            }
            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, ScriptPack value, JsonSerializer serializer) 
        {
            JToken t = JToken.FromObject(value.ToString());

            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                JObject o = (JObject)t;
                IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();

                o.AddFirst(new JProperty("Keys", new JArray(propertyNames)));

                o.WriteTo(writer);
            }
        }
    }

}
