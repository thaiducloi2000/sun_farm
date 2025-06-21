using System.Collections.Generic;
using UnityEngine;
using System;
using Score;

public class BuildingConfig : BaseConfig<BuildingData>
{
    public static Dictionary<string, BuildingData> DctBuildingData { get; private set; } = new();

    public override void Load()
    {
        DctBuildingData.Clear();

        if (Data == null || Data.Length == 0)
        {
#if UNITY_EDITOR
            Debug.LogWarning("BuildingConfig: No data found.");
#endif
            return;
        }

        foreach (var data in Data)
        {
            if (!DctBuildingData.ContainsKey(data.building_id))
            {
                DctBuildingData.Add(data.building_id, data);
#if UNITY_EDITOR
                Debug.Log($"[BuildingConfig] Added: {data.building_name}");
#endif
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Duplicate key found: {data.building_id}");
            }
#endif
        }

#if UNITY_EDITOR
        Debug.Log($"[BuildingConfig] Loaded {DctBuildingData.Count} entries.");
#endif
    }

    public static bool Get(string id, out BuildingData result) =>
        DctBuildingData.TryGetValue(id, out result);
}

[Serializable]
public class BuildingData
{
    public string building_id;
    public string building_name;
    public string item_type;
    public int expansion_cost;
}