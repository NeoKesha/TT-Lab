using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TT_Lab.AssetData.Code.Object;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Assets.Factory;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.Xbox.Items.RMX.Code.AgentLab;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code.AgentLab;

namespace TT_Lab.AssetData.Code
{
    public class GameObjectData : AbstractAssetData
    {

        public GameObjectData(ITwinObject gameObject)
        {
            SetTwinItem(gameObject);
        }

        public GameObjectData(String path) => LoadInternal(path);

        [JsonProperty(Required = Required.Always)]
        public ITwinObject.ObjectType Type { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte UnkTypeValue { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte CameraReactJointAmount { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte ExitPointAmount { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte[] SlotsMap { get; set; }
        [JsonProperty(Required = Required.Always)]
        public String Name { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<ObjectTriggerBehaviourData> TriggerBehaviours { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> OGISlots { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> AnimationSlots { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> BehaviourSlots { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> ObjectSlots { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> SoundSlots { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 InstanceStateFlags { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt32> InstFlags { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Single> InstFloats { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt32> InstIntegers { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> RefObjects { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> RefOGIs { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> RefAnimations { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> RefBehaviourCommandsSequences { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> RefBehaviours { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> RefSounds { get; set; }
        [JsonProperty(Required = Required.Always)]
        [JsonConverter(typeof(ScriptPackConverter))]
        public ITwinBehaviourCommandPack BehaviourPack { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            TriggerBehaviours.Clear();
            OGISlots.Clear();
            AnimationSlots.Clear();
            BehaviourSlots.Clear();
            ObjectSlots.Clear();
            SoundSlots.Clear();
            InstFlags.Clear();
            InstFloats.Clear();
            InstIntegers.Clear();
            RefObjects.Clear();
            RefOGIs.Clear();
            RefAnimations.Clear();
            RefBehaviourCommandsSequences.Clear();
            RefBehaviours.Clear();
            RefSounds.Clear();
        }

        public override void Import(LabURI package, String? variant)
        {
            ITwinObject gameObject = GetTwinItem<ITwinObject>();
            Type = gameObject.Type;
            UnkTypeValue = gameObject.UnkTypeValue;
            CameraReactJointAmount = gameObject.ReactJointAmount;
            ExitPointAmount = gameObject.ExitPointAmount;
            SlotsMap = CloneUtils.CloneArray(gameObject.SlotsMap);
            Name = new String(gameObject.Name.ToCharArray());
            TriggerBehaviours = new List<ObjectTriggerBehaviourData>();
            foreach (var e in gameObject.TriggerBehaviours)
            {
                TriggerBehaviours.Add(new ObjectTriggerBehaviourData(package, variant, e));
            }
            OGISlots = new List<LabURI>();
            foreach (var e in gameObject.OGISlots)
            {
                OGISlots.Add((e == 65535) ? LabURI.Empty : AssetManager.Get().GetUri(package, typeof(OGI).Name, null, e));
            }
            AnimationSlots = new List<LabURI>();
            foreach (var e in gameObject.AnimationSlots)
            {
                AnimationSlots.Add((e == 65535) ? LabURI.Empty : AssetManager.Get().GetUri(package, typeof(Animation).Name, null, e));
            }
            BehaviourSlots = new List<LabURI>();
            foreach (var e in gameObject.BehaviourSlots)
            {
                var found = false;
                foreach (var cm in gameObject.RefCodeModels)
                {
                    BehaviourCommandsSequence cmGuid = AssetManager.Get().GetAsset<BehaviourCommandsSequence>(package, typeof(BehaviourCommandsSequence).Name, null, cm);
                    LabURI subGuid = AssetManager.Get().GetUri(package, typeof(BehaviourGraph).Name, cmGuid.ID.ToString(), e);
                    if (!subGuid.Equals(LabURI.Empty))
                    {
                        BehaviourSlots.Add(subGuid);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    BehaviourSlots.Add((e == 65535) ? LabURI.Empty : AssetManager.Get().GetUri(package, typeof(BehaviourStarter).Name, null, e));
                }
            }
            ObjectSlots = new List<LabURI>();
            foreach (var e in gameObject.ObjectSlots)
            {
                ObjectSlots.Add((e == 65535) ? LabURI.Empty : AssetManager.Get().GetUri(package, typeof(GameObject).Name, null, e));
            }
            SoundSlots = new List<LabURI>();
            foreach (var e in gameObject.SoundSlots)
            {
                var list = CollectMulti5Uri(package, null, e);
                if (list.Count != 0)
                {
                    SoundSlots.AddRange(list);
                }
                else
                {
                    SoundSlots.Add((e == 65535) ? LabURI.Empty : AssetManager.Get().GetUri(package, typeof(SoundEffect).Name, null, e));
                }
            }
            RefObjects = new List<LabURI>();
            foreach (var e in gameObject.RefObjects)
            {
                RefObjects.Add(AssetManager.Get().GetUri(package, typeof(GameObject).Name, null, e));
            }
            RefOGIs = new List<LabURI>();
            foreach (var e in gameObject.RefOGIs)
            {
                RefOGIs.Add(AssetManager.Get().GetUri(package, typeof(OGI).Name, null, e));
            }
            RefAnimations = new List<LabURI>();
            foreach (var e in gameObject.RefAnimations)
            {
                RefAnimations.Add(AssetManager.Get().GetUri(package, typeof(Animation).Name, null, e));
            }
            RefBehaviourCommandsSequences = new List<LabURI>();
            foreach (var e in gameObject.RefCodeModels)
            {
                RefBehaviourCommandsSequences.Add(AssetManager.Get().GetUri(package, typeof(BehaviourCommandsSequence).Name, null, e));
            }
            RefBehaviours = new List<LabURI>();
            foreach (var e in gameObject.RefBehaviours)
            {
                // Range reserved for CodeModel(Command sequences) script IDs
                if (e > 500 && e < 616)
                {
                    foreach (var cm in RefBehaviourCommandsSequences)
                    {
                        var cmAsset = AssetManager.Get().GetAsset<BehaviourCommandsSequence>(cm);
                        if (cmAsset.BehaviourGraphLinks.ContainsKey(e))
                        {
                            RefBehaviours.Add(cmAsset.BehaviourGraphLinks[e]);
                            break;
                        }
                    }
                }
                else
                {
                    if (e % 2 == 0)
                    {
                        RefBehaviours.Add(AssetManager.Get().GetUri(package, typeof(BehaviourStarter).Name, null, e));
                    }
                    else
                    {
                        RefBehaviours.Add(AssetManager.Get().GetUri(package, typeof(BehaviourGraph).Name, null, e));
                    }
                }
            }
            RefSounds = new List<LabURI>();
            foreach (var e in gameObject.RefSounds)
            {
                var sndGuid = AssetManager.Get().GetUri(package, typeof(SoundEffect).Name, null, e);
                if (sndGuid == LabURI.Empty)
                {
                    var multi5 = CollectMulti5Uri(package, null, e);
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
            BehaviourPack = gameObject.BehaviourPack;
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            var assetManager = AssetManager.Get();
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            writer.Write((Int32)Type);
            writer.Write(UnkTypeValue);
            writer.Write(CameraReactJointAmount);
            writer.Write(ExitPointAmount);
            writer.Write(SlotsMap);
            writer.Write(Name);

            writer.Write(TriggerBehaviours.Count);
            foreach (var triggerBehaviour in TriggerBehaviours)
            {
                writer.Write((UInt16)assetManager.GetAsset(triggerBehaviour.TriggerBehaviour).ID);
                writer.Write(triggerBehaviour.UnkTriggerValue);
                writer.Write(triggerBehaviour.BehaviourCallerIndex);
            }

            void writeUriList(IList<LabURI> list)
            {
                writer.Write(list.Count);
                foreach (var item in list)
                {
                    writer.Write((UInt16)(item == LabURI.Empty ? 65535 : assetManager.GetAsset(item).ID));
                }
            }
            writeUriList(OGISlots);
            writeUriList(AnimationSlots);
            writeUriList(BehaviourSlots);
            writeUriList(ObjectSlots);
            writeUriList(SoundSlots);

            writer.Write(InstanceStateFlags);

            void writeParamsList<T>(IList<T> list, Action<T> writeFunc)
            {
                writer.Write(list.Count);
                foreach (var item in list)
                {
                    writeFunc(item);
                }
            }
            writeParamsList(InstFlags, writer.Write);
            writeParamsList(InstFloats, writer.Write);
            writeParamsList(InstIntegers, writer.Write);

            writeUriList(RefObjects);
            writeUriList(RefOGIs);
            writeUriList(RefAnimations);
            writeUriList(RefBehaviourCommandsSequences);
            writeUriList(RefBehaviours);

            // Write unknowns/unused object refs
            writer.Write(0);

            writeUriList(RefSounds);

            BehaviourPack.Write(writer);

            ms.Position = 0;
            return factory.GenerateObject(ms);
        }

        private static List<LabURI> CollectMulti5Uri(LabURI package, String? variant, UInt16 id)
        {
            var result = new List<LabURI>();
            var enUri = AssetManager.Get().GetUri(package, typeof(SoundEffectEN).Name, variant, id);
            var frUri = AssetManager.Get().GetUri(package, typeof(SoundEffectFR).Name, variant, id);
            var grUri = AssetManager.Get().GetUri(package, typeof(SoundEffectGR).Name, variant, id);
            var itUri = AssetManager.Get().GetUri(package, typeof(SoundEffectIT).Name, variant, id);
            var spUri = AssetManager.Get().GetUri(package, typeof(SoundEffectSP).Name, variant, id);
            var jpUri = AssetManager.Get().GetUri(package, typeof(SoundEffectJP).Name, variant, id);

            if (!enUri.Equals(LabURI.Empty))
            {
                result.Add(enUri);
            }
            if (!frUri.Equals(LabURI.Empty))
            {
                result.Add(frUri);
            }
            if (!grUri.Equals(LabURI.Empty))
            {
                result.Add(grUri);
            }
            if (!itUri.Equals(LabURI.Empty))
            {
                result.Add(itUri);
            }
            if (!spUri.Equals(LabURI.Empty))
            {
                result.Add(spUri);
            }
            if (!jpUri.Equals(LabURI.Empty))
            {
                result.Add(jpUri);
            }

            return result;
        }
    }
    class ScriptPackConverter : JsonConverter<ITwinBehaviourCommandPack>
    {
        public override ITwinBehaviourCommandPack ReadJson(JsonReader reader, Type objectType, ITwinBehaviourCommandPack? existingValue, Boolean hasExistingValue, JsonSerializer serializer)
        {
            // By default create PS2 behaviour command pack
            existingValue ??= new PS2BehaviourCommandPack();
            String? str = reader.ReadAsString();
            using (var stream = new MemoryStream())
            using (var _writer = new StreamWriter(stream))
            using (var _reader = new StreamReader(stream))
            {
                _writer.Write(str);
                stream.Position = 0;
                if (_reader.BaseStream.Length != 0)
                {
                    var version = _reader.ReadLine()!.Trim();
                    if (version == "@PS2 Pack")
                    {
                        existingValue ??= new PS2BehaviourCommandPack();
                    }
                    else if (version == "@Xbox Pack")
                    {
                        existingValue ??= new XboxBehaviourCommandPack();
                    }
                    _reader.BaseStream.Position = 0;
                    existingValue.ReadText(_reader);
                }
            }
            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, ITwinBehaviourCommandPack? value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value!.ToString());

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
