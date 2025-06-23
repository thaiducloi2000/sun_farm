using System.Collections.Generic;
using UnityEngine;
using System;
using Score;
using Sirenix.Serialization;

public class ProductConfig : BaseConfig<ProductData>
{
    [NonSerialized, OdinSerialize] public Dictionary<string, ProductData> DctProductData = new();

    public override void Load()
    {
        DctProductData.Clear();

        if (Data == null || Data.Length == 0)
        {
#if UNITY_EDITOR
            Debug.LogWarning("ProductConfig: No data found.");
#endif
            return;
        }

        foreach (var data in Data)
        {
            if (!DctProductData.ContainsKey(data.product_id))
            {
                DctProductData.Add(data.product_id, data);
#if UNITY_EDITOR
                Debug.Log($"[ProductConfig] Added: {data.product_name}");
#endif
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Duplicate key found: {data.product_id}");
            }
#endif
        }

#if UNITY_EDITOR
        Debug.Log($"[ProductConfig] Loaded {DctProductData.Count} entries.");
#endif
    }

    public bool Get(string id, out ProductData result) =>
        DctProductData.TryGetValue(id, out result);
}

[Serializable]
public class ProductData
{
    public string product_id;
    public string product_name;
    public int sell_price_per_unit;
}