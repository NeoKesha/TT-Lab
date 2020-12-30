using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets;

namespace TT_Lab.Util
{
    public static class GuidManager
    {
        private static Dictionary<Guid, IAsset> GuidToAsset { get; set; }
        private static Dictionary<Guid, UInt32> GuidToTwinId { get; set; }
        private static Dictionary<KeyValuePair<Type, UInt32>, Guid> TwinIdToGuid { get; set; }

        public static void InitMappers(Dictionary<Guid, IAsset> Assets)
        {
            GuidToAsset.Clear();
            GuidToTwinId.Clear();
            TwinIdToGuid.Clear();
            foreach (var key in Assets.Keys)
            {
                IAsset asset = Assets[key];
                GuidToAsset.Add(asset.GetGuid(), asset);
                GuidToTwinId.Add(asset.GetGuid(), asset.ID);
                TwinIdToGuid.Add(new KeyValuePair<Type, uint>(asset.GetType(), asset.ID), asset.GetGuid());
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
            return TwinIdToGuid[new KeyValuePair<Type, UInt32>(type, twinId)];
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
            Guid guid = TwinIdToGuid[key];
            TwinIdToGuid.Remove(key);
            TwinIdToGuid.Add(new KeyValuePair<Type, uint>(key.Key, newTwinId), guid);
        }
    }
}
