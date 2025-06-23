using System.Collections.Generic;
using UnityEngine;
using Score;
using System;
using Sirenix.Serialization;

public class StoreConfig : BaseConfig<StoreData>
{
    [NonSerialized, OdinSerialize] public Dictionary<string, StoreData> DctStoreData = new();

    public override void Load()
    {
        DctStoreData.Clear();

        if (Data == null || Data.Length == 0)
        {
#if UNITY_EDITOR
            Debug.LogWarning("StoreConfig: No data found.");
#endif
            return;
        }

        foreach (var data in Data)
        {
            if (!DctStoreData.ContainsKey(data.item_store_id))
            {
                DctStoreData.Add(data.item_store_id, data);
#if UNITY_EDITOR
                Debug.Log($"[StoreConfig] Added: {data.item_type}");
#endif
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Duplicate key found: {data.item_store_id}");
            }
#endif
        }

#if UNITY_EDITOR
        Debug.Log($"[StoreConfig] Loaded {DctStoreData.Count} entries.");
#endif
    }

    public bool Get(string id, out StoreData result) =>
        DctStoreData.TryGetValue(id, out result);
}
[Serializable]
public class StoreData
{
    public string item_store_id;
    public string item_type;
    public string item_id;
    public int price;
    public int bulk_size;
}