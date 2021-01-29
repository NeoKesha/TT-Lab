using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Assets.Instance;

namespace TT_Lab.Util
{
    public static class GuidManager
    {
        private static HashSet<Type> excludeTypes = new HashSet<Type>()
        {
            typeof(Folder),
            typeof(ChunkFolder),
            typeof(Trigger),
            typeof(Position),
            typeof(Path),
            typeof(Particles),
            typeof(ObjectInstance),
            typeof(Scenery),
            typeof(DynamicScenery),
            typeof(Collision),
            typeof(ChunkLinks),
            typeof(Camera),
            typeof(AiPosition),
            typeof(AiPath)
        };
        private static Type cmSpecial = typeof(CodeModel);
        private static Type sfxSpecial = typeof(SoundEffect);
        public static Dictionary<Guid, IAsset> GuidToAsset { get; set; }
        public static Dictionary<Guid, UInt32> GuidToTwinId { get; set; }
        public static Dictionary<KeyValuePair<Type, UInt32>, Guid> TwinIdToGuid { get; set; }
        public static Dictionary<KeyValuePair<Guid, UInt32>, Guid> CmSubScriptIdToGuid { get; set; }
        public static Dictionary<UInt32, List<Guid>> SfxMulti5List {get;set;}

        public static void InitMappers(Dictionary<Guid, IAsset> Assets)
        {
            GuidToAsset = new Dictionary<Guid, IAsset>();
            GuidToTwinId = new Dictionary<Guid, UInt32>();
            TwinIdToGuid = new Dictionary<KeyValuePair<Type, UInt32>, Guid>();
            CmSubScriptIdToGuid = new Dictionary<KeyValuePair<Guid, uint>, Guid>();
            SfxMulti5List = new Dictionary<uint, List<Guid>>();
            foreach (var key in Assets.Keys)
            {
                IAsset asset = Assets[key];
                try
                {
                    GuidToAsset.Add(asset.UUID, asset);
                    var assetType = asset.GetType();
                    if (!excludeTypes.Contains(assetType))
                    {
                        GuidToTwinId.Add(asset.UUID, asset.ID);
                        TwinIdToGuid.Add(new KeyValuePair<Type, uint>(asset.GetType(), asset.ID), asset.UUID);
                    }
                    if (assetType == cmSpecial)
                    {
                        var cm = (CodeModel)asset;
                        foreach (var e in cm.SubScriptGuids)
                        {
                            GuidToAsset.Add(e.Value, asset);
                            GuidToTwinId.Add(e.Value, e.Key);
                            CmSubScriptIdToGuid.Add(new KeyValuePair<Guid, uint>(asset.UUID, e.Key), e.Value);
                        }
                    } 
                    else if (sfxSpecial.IsAssignableFrom(assetType) && sfxSpecial != assetType)
                    {
                        if (!SfxMulti5List.ContainsKey(asset.ID))
                        {
                            SfxMulti5List.Add(asset.ID, new List<Guid>());
                        }
                        var list = SfxMulti5List[asset.ID];
                        list.Add(asset.UUID);
                    }
                }
                catch(Exception ex)
                {
                    Log.WriteLine($"Error initializing mapper: {ex.Message} for {asset.Type} ID {asset.ID}");
                }
            }
        }
        public static List<Guid> GetGuidListOfMulti5(UInt32 id)
        {
            if (SfxMulti5List.ContainsKey(id))
            {
                return SfxMulti5List[id];
            }
            else
            {
                return null;
            } 
        }
        public static Guid GetGuidByCmSubScriptId(Guid guid, UInt32 id)
        {
            var key = new KeyValuePair<Guid, UInt32>(guid, id);
            return GetGuidByCmSubScriptId(key);
        }
        public static Guid GetGuidByCmSubScriptId(KeyValuePair<Guid, UInt32> key)
        {
            if (CmSubScriptIdToGuid.ContainsKey(key))
            {
                return CmSubScriptIdToGuid[key];
            }
            return Guid.Empty;
        }
        public static IAsset GetAssetByGuid(Guid guid)
        {
            return GuidToAsset[guid];
        }
        public static IAsset GetAssetByGuid(String guid)
        {
            return GuidToAsset[new Guid(guid)];
        }

        public static UInt32 GetTwinIdByGuid(Guid guid)
        {
            return GuidToTwinId[guid];
        }
        public static UInt32 GetTwinIdByGuid(String guid)
        {
            return GuidToTwinId[new Guid(guid)];
        }

        public static Guid GetGuidByTwinId(UInt32 twinId, Type type)
        {
            return GetGuidByTwinId(new KeyValuePair<Type, UInt32>(type, twinId));
        }
        public static Guid GetGuidByTwinId(KeyValuePair<Type, UInt32> key)
        {
            if (!TwinIdToGuid.ContainsKey(key))
            {
                //var trace = new System.Diagnostics.StackTrace();
                //Log.WriteLine($"WARNING: FOUND EMPTY GUID CHECK THAT YOU USED CORRECT TWIN ID AND TYPE");
                return Guid.Empty;
            }
            return TwinIdToGuid[key];
        }

        public static void UpdateTwinId(String guid, Type type, UInt32 newTwinId)
        {
            UpdateTwinId(new Guid(guid), type, newTwinId);
        }
        public static void UpdateTwinId(Guid guid, Type type, UInt32 newTwinId)
        {
            UpdateTwinId(GetTwinIdByGuid(guid), type, newTwinId);
        }
        public static void UpdateTwinId(UInt32 oldTwinId, Type type, UInt32 newTwinId)
        {
            UpdateTwinId(new KeyValuePair<Type, UInt32>(type, oldTwinId), newTwinId);
        }
        public static void UpdateTwinId(KeyValuePair<Type, UInt32> key, UInt32 newTwinId)
        {
            if (TwinIdToGuid.ContainsKey(key))
            {
                Guid guid = TwinIdToGuid[key];
                TwinIdToGuid.Remove(key);
                TwinIdToGuid.Add(new KeyValuePair<Type, uint>(key.Key, newTwinId), guid);
                GuidToTwinId[guid] = newTwinId;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
