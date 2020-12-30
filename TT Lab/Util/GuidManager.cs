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
        public static Guid NullGuid = new Guid("00000000-0000-0000-0000-000000000000");
        public static Dictionary<Guid, IAsset> GuidToAsset { get; set; } = new Dictionary<Guid, IAsset>();
        public static Dictionary<Guid, UInt32> GuidToTwinId { get; set; } = new Dictionary<Guid, UInt32>();
        public static Dictionary<KeyValuePair<Type, UInt32>, Guid> TwinIdToGuid { get; set; } = new Dictionary<KeyValuePair<Type, UInt32>, Guid>();

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
            if (TwinIdToGuid.ContainsKey(key))
            {
                return TwinIdToGuid[key];
            } 
            else
            {
                return NullGuid;
            }
            
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
