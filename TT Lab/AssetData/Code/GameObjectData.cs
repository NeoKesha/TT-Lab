using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets.Code;
using TT_Lab.Util;
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
            twinRef = gameObject;
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
        public List<Guid> OGISlots { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Guid> AnimationSlots { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Guid> ScriptSlots { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Guid> ObjectSlots { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Guid> SoundSlots { get; set; }
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
        public List<Guid> RefObjects { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Guid> RefOGIs { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Guid> RefAnimations { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Guid> RefCodeModels { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Guid> RefScripts { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> RefUnknowns { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Guid> RefSounds { get; set; }
        [JsonProperty(Required = Required.Always)]
        [JsonConverter(typeof(ScriptPackConverter))]
        public ScriptPack ScriptPack { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import()
        {
            PS2AnyObject gameObject = (PS2AnyObject)twinRef;
            Bitfield = gameObject.Bitfield;
            SlotsMap = CloneUtils.CloneArray(gameObject.SlotsMap);
            Name = new String(gameObject.Name.ToCharArray());
            UInt32Slots = CloneUtils.CloneList(gameObject.UInt32Slots);
            OGISlots = new List<Guid>();
            foreach (var e in gameObject.OGISlots)
            {
                OGISlots.Add((e == 65535)?Guid.Empty:GuidManager.GetGuidByTwinId(e, typeof(OGI)));
            }
            AnimationSlots = new List<Guid>();
            foreach (var e in gameObject.AnimationSlots)
            {
                AnimationSlots.Add((e == 65535) ? Guid.Empty : GuidManager.GetGuidByTwinId(e, typeof(Animation)));
            }
            ScriptSlots = new List<Guid>();
            foreach (var e in gameObject.ScriptSlots)
            {
                
                ScriptSlots.Add((e == 65535) ? Guid.Empty : GuidManager.GetGuidByTwinId(e, typeof(HeaderScript)));
            }
            ObjectSlots = new List<Guid>();
            foreach (var e in gameObject.ObjectSlots)
            {
                ObjectSlots.Add((e == 65535) ? Guid.Empty : GuidManager.GetGuidByTwinId(e, typeof(GameObject)));
            }
            SoundSlots = new List<Guid>();
            foreach (var e in gameObject.SoundSlots)
            {
                SoundSlots.Add((e == 65535) ? Guid.Empty : GuidManager.GetGuidByTwinId(e, typeof(SoundEffect)));
            }
            InstancePropsHeader = gameObject.InstancePropsHeader;
            UnkUInt = gameObject.UnkUInt;
            InstFlags = CloneUtils.CloneList(gameObject.InstFlags);
            InstFloats = CloneUtils.CloneList(gameObject.InstFloats);
            InstIntegers = CloneUtils.CloneList(gameObject.InstIntegers);
            RefObjects = new List<Guid>();
            foreach (var e in gameObject.RefObjects)
            {
                RefObjects.Add((e == 65535) ? Guid.Empty : GuidManager.GetGuidByTwinId(e, typeof(GameObject)));
            }
            RefOGIs = new List<Guid>();
            foreach (var e in gameObject.RefOGIs)
            {
                RefOGIs.Add((e == 65535) ? Guid.Empty : GuidManager.GetGuidByTwinId(e, typeof(OGI)));
            }
            RefAnimations = new List<Guid>();
            foreach (var e in gameObject.RefAnimations)
            {
                RefAnimations.Add((e == 65535) ? Guid.Empty : GuidManager.GetGuidByTwinId(e, typeof(Animation)));
            }
            RefCodeModels = new List<Guid>();
            foreach (var e in gameObject.RefCodeModels)
            {
                RefCodeModels.Add((e == 65535) ? Guid.Empty : GuidManager.GetGuidByTwinId(e, typeof(CodeModel)));
            }
            RefScripts = new List<Guid>();
            foreach (var e in gameObject.RefScripts)
            {
                RefScripts.Add((e == 65535) ? Guid.Empty : GuidManager.GetGuidByTwinId(e, typeof(HeaderScript)));
            }
            RefUnknowns = CloneUtils.CloneList(gameObject.RefUnknowns);
            RefSounds = new List<Guid>();
            foreach (var e in gameObject.RefSounds)
            {
                RefSounds.Add((e == 65535) ? Guid.Empty : GuidManager.GetGuidByTwinId(e, typeof(SoundEffect)));
            }
            ScriptPack = gameObject.ScriptPack;
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
