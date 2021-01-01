using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private static Type hsSpecial = typeof(HeaderScript);
        public static Dictionary<Guid, IAsset> GuidToAsset { get; set; }
        public static Dictionary<Guid, UInt32> GuidToTwinId { get; set; }
        public static Dictionary<KeyValuePair<Type, UInt32>, Guid> TwinIdToGuid { get; set; }
        public static Dictionary<KeyValuePair<Guid,UInt32>,Guid> CmSubScriptIdToGuid { get; set; }

        public static void InitMappers(Dictionary<Guid, IAsset> Assets)
        {
            GuidToAsset = new Dictionary<Guid, IAsset>(Assets.Count);
            GuidToTwinId = new Dictionary<Guid, UInt32>(Assets.Count);
            TwinIdToGuid = new Dictionary<KeyValuePair<Type, UInt32>, Guid>(Assets.Count);
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
                }
                catch(Exception ex)
                {
                    Log.WriteLine($"Error initializing mapper: {ex.Message} for {asset.Type} ID {asset.ID}");
                    throw ex;
                }
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
            else
            {
                return Guid.Empty;
            }
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
