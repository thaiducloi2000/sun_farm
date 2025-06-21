using System;
using System.Collections.Generic;
using UnityEngine;
using Score;

public class PlantConfig : BaseConfig<PlantData>
{
    public static Dictionary<string, PlantData> DctPlant { get; private set; } = new();

    public override void Load()
    {
        DctPlant.Clear();

        foreach (var data in Data)
        {
            if (DctPlant.ContainsKey(data.plant_id)) continue;
            DctPlant.Add(data.plant_id, data);
        }
#if UNITY_EDITOR
        Debug.Log(DctPlant.Count);
#endif
    }

    public static bool GetPlant(string id, out PlantData result) => DctPlant.TryGetValue(id, out result);
}

[Serializable]
public class PlantData
{
    public string plant_id;
    public string plant_name;
    public int grow_time_per_yield;
    public int yield_per_cycle;
    public int max_yield;
    public string product_id;
    public ItemType item_type;
}