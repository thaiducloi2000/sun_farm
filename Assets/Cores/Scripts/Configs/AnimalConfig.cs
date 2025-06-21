using System.Collections.Generic;
using UnityEngine;
using Score;
public class AnimalConfig : BaseConfig<AnimalData>
{
    public static Dictionary<string, AnimalData> DctAnimalData { get; private set; } = new();

    public override void Load()
    {
        DctAnimalData.Clear();

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

#if UNITY_EDITOR
        Debug.Log($"[AnimalConfig] Loaded {DctAnimalData.Count} entries.");
#endif
    }

    public static bool Get(string id, out AnimalData result) =>
        DctAnimalData.TryGetValue(id, out result);
}
[System.Serializable]
public class AnimalData
{
    public string animal_id;
    public string animal_name;
    public int produce_time_per_unit;
    public int max_yield;
    public string product_id;
    public string item_type;
}