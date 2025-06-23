using System;
using System.Collections.Generic;
using UnityEngine;
using Score;
using Sirenix.Serialization;
using UnityEditor;

public class PlantConfig : BaseConfig<PlantData>
{
    [NonSerialized, OdinSerialize] public Dictionary<string, PlantData> DctPlant = new();

    [NonSerialized, OdinSerialize] public Dictionary<string, CropBuilding> DctCropBuildingsDataPrefabs  = new();
    public override void Load()
    {
        DctPlant.Clear();
        DctCropBuildingsDataPrefabs.Clear();
        foreach (var data in Data)
        {
            if (DctPlant.ContainsKey(data.plant_id)) continue;
            DctPlant.Add(data.plant_id, data);
        }
#if UNITY_EDITOR
        Debug.Log(DctPlant.Count);
#endif
        AssetDatabase.Refresh();
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { GameDefine.Path.CropPrefabPath });
        Debug.Log($"{guids.Length} Crop prefabs found.");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var prefab = AssetDatabase.LoadAssetAtPath<CropBuilding>(path);
            if (prefab == null) continue;

            if (!DctCropBuildingsDataPrefabs.ContainsKey(prefab.name))
            {
                DctCropBuildingsDataPrefabs.Add(prefab.name, prefab);
            }
            else
            {
                Debug.LogWarning($"[AnimalConfig] Duplicate prefab name: {prefab.name}");
            }
        }

        Debug.Log($"[AnimalConfig] Loaded {DctCropBuildingsDataPrefabs.Count} Animal prefabs.");
    }

    public bool GetPlant(string id, out PlantData result) => DctPlant.TryGetValue(id, out result);
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