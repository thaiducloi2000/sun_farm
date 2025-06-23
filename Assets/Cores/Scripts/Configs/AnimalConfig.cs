using System.Collections.Generic;
using UnityEngine;
using Score;
using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;

public class AnimalConfig : BaseConfig<AnimalData>
{
    [NonSerialized, OdinSerialize] public Dictionary<string, AnimalData> DctAnimalData = new();
    [NonSerialized, OdinSerialize] public Dictionary<string, AnimalBuilding> DctAnimalDataPrefabs = new();

    public override void Load()
    {
        DctAnimalData.Clear();
        DctAnimalDataPrefabs.Clear();
        if (Data == null || Data.Length == 0)
        {
#if UNITY_EDITOR
            Debug.LogWarning("AnimalConfig: No data found.");
#endif
            return;
        }

        foreach (var data in Data)
        {
            if (!DctAnimalData.ContainsKey(data.animal_id))
            {
                DctAnimalData.Add(data.animal_id, data);
#if UNITY_EDITOR
                Debug.Log($"[AnimalConfig] Added: {data.animal_name}");
#endif
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Duplicate key found: {data.animal_id}");
            }
#endif
        }
        AssetDatabase.Refresh();
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { GameDefine.Path.AnimalPrefabPath});
        Debug.Log($"{guids.Length} Animal prefabs found.");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var prefab = AssetDatabase.LoadAssetAtPath<AnimalBuilding>(path);
            if (prefab == null) continue;

            if (!DctAnimalDataPrefabs.ContainsKey(prefab.name))
            {
                DctAnimalDataPrefabs.Add(prefab.name, prefab);
            }
            else
            {
                Debug.LogWarning($"[AnimalConfig] Duplicate prefab name: {prefab.name}");
            }
        }

        Debug.Log($"[AnimalConfig] Loaded {DctAnimalDataPrefabs.Count} Animal prefabs.");
    }
    public bool Get(string id, out AnimalData result) =>
        DctAnimalData.TryGetValue(id, out result);
}

[Serializable]
public class AnimalData
{
    public string animal_id;
    public string animal_name;
    public int produce_time_per_unit;
    public int max_yield;
    public string product_id;
    public string item_type;
}