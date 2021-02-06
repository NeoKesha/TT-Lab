using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public Byte Type { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte UnkTypeValue { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte UnkOGIArraySize { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte OGIType2ArraySize { get; set; }
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
        public UInt32 InstanceStateFlags { get; set; }
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
        public List<Guid> RefCodemodels { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Guid> RefScripts { get; set; }
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
            Type = gameObject.Type;
            UnkTypeValue = gameObject.UnkTypeValue;
            UnkOGIArraySize = gameObject.UnkOgiArraySize;
            OGIType2ArraySize = gameObject.OgiType2ArraySize;
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
                var found = false;
                foreach (var cm in gameObject.RefCodeModels)
                {
                    Guid cmGuid = GuidManager.GetGuidByTwinId(cm, typeof(CodeModel));
                    Guid subGuid = GuidManager.GetGuidByCmSubScriptId(cmGuid, e);
                    if (!subGuid.Equals(Guid.Empty))
                    {
                        ScriptSlots.Add(subGuid);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    ScriptSlots.Add((e == 65535) ? Guid.Empty : GuidManager.GetGuidByTwinId(e, typeof(HeaderScript)));
                }
            }
            ObjectSlots = new List<Guid>();
            foreach (var e in gameObject.ObjectSlots)
            {
                ObjectSlots.Add((e == 65535) ? Guid.Empty : GuidManager.GetGuidByTwinId(e, typeof(GameObject)));
            }
            SoundSlots = new List<Guid>();
            foreach (var e in gameObject.SoundSlots)
            {
                var list = GuidManager.GetGuidListOfMulti5(e);
                if (list != null)
                {
                    SoundSlots.AddRange(list);
                }
                else
                {
                    SoundSlots.Add((e == 65535) ? Guid.Empty : GuidManager.GetGuidByTwinId(e, typeof(SoundEffect)));
                }
            }
            RefObjects = new List<Guid>();
            foreach (var e in gameObject.RefObjects)
            {
                RefObjects.Add(GuidManager.GetGuidByTwinId(e, typeof(GameObject)));
            }
            RefOGIs = new List<Guid>();
            foreach (var e in gameObject.RefOGIs)
            {
                RefOGIs.Add(GuidManager.GetGuidByTwinId(e, typeof(OGI)));
            }
            RefAnimations = new List<Guid>();
            foreach (var e in gameObject.RefAnimations)
            {
                RefAnimations.Add(GuidManager.GetGuidByTwinId(e, typeof(Animation)));
            }
            RefCodemodels = new List<Guid>();
            foreach (var e in gameObject.RefCodeModels)
            {
                RefCodemodels.Add(GuidManager.GetGuidByTwinId(e, typeof(CodeModel)));
            }
            RefScripts = new List<Guid>();
            foreach (var e in gameObject.RefScripts)
            {
                // Range reserved for CodeModel script IDs
                if (e > 500 && e < 616)
                {
                    foreach (var cm in RefCodemodels)
                    {
                        Guid subGuid = GuidManager.GetGuidByCmSubScriptId(cm, e);
                        if (!subGuid.Equals(Guid.Empty))
                        {
                            RefScripts.Add(subGuid);
                            break;
                        }
                    }
                }
                else
                {
                    if (e % 2 == 0)
                    {
                        RefScripts.Add(GuidManager.GetGuidByTwinId(e, typeof(HeaderScript)));
                    }
                    else
                    {
                        RefScripts.Add(GuidManager.GetGuidByTwinId(e, typeof(MainScript)));
                    }
                }
            }
            RefSounds = new List<Guid>();
            foreach (var e in gameObject.RefSounds)
            {
                var sndGuid = GuidManager.GetGuidByTwinId(e, typeof(SoundEffect));
                if (sndGuid == Guid.Empty)
                {
                    var multi5 = GuidManager.GetGuidListOfMulti5(e);
                    foreach (var snd in multi5)
                    {
                        RefSounds.Add(snd);
                    }
                    continue;
                }
                RefSounds.Add(sndGuid);
            }
            InstanceStateFlags = gameObject.InstanceStateFlags;
            InstFlags = CloneUtils.CloneList(gameObject.InstFlags);
            InstFloats = CloneUtils.CloneList(gameObject.InstFloats);
            InstIntegers = CloneUtils.CloneList(gameObject.InstIntegers);
            ScriptPack = gameObject.ScriptPack;
        }
    }
    class ScriptPackConverter : JsonConverter<ScriptPack>
    {
        public override ScriptPack ReadJson(JsonReader reader, Type objectType, ScriptPack? existingValue, Boolean hasExistingValue, JsonSerializer serializer)
        {
            if (existingValue == null)
            {
                existingValue = new ScriptPack();
            }
            String? str = reader.ReadAsString();
            using (MemoryStream stream = new MemoryStream())
            using (StreamWriter _writer = new StreamWriter(stream))
            using (StreamReader _reader = new StreamReader(stream))
            {
                _writer.Write(str);
                stream.Position = 0;
                if (_reader.BaseStream.Length != 0)
                {
                    existingValue.ReadText(_reader);
                }
            }
            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, ScriptPack? value, JsonSerializer serializer) 
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
