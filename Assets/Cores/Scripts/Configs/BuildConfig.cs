using Score;
using System.Collections.Generic;
using UnityEngine;

public class BuildConfig : BaseConfig<BuildData>
{
    public static Dictionary<string, BuildData> DctBuildData { get; private set; } = new();

    public override void Load()
    {
        DctBuildData.Clear();

        if (Data == null || Data.Length == 0)
        {
#if UNITY_EDITOR
            Debug.LogWarning("BuildConfig: No data found.");
#endif
            return;
        }

        foreach (var data in Data)
        {
            if (!DctBuildData.ContainsKey(data.building_id))
            {
                DctBuildData.Add(data.building_id, data);
#if UNITY_EDITOR
                Debug.Log($"[BuildConfig] Added: {data.level}");
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
        Debug.Log($"[BuildConfig] Loaded {DctBuildData.Count} entries.");
#endif
    }

    public static bool Get(string id, out BuildData result) =>
        DctBuildData.TryGetValue(id, out result);
}
[System.Serializable]
public class BuildData
{
    public string building_id;
    public int level;
    public int max_can_build;
}