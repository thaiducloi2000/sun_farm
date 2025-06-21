using System.Collections.Generic;
using UnityEngine;
using Score;
using System;
public class FarmUpgradeConfig : BaseConfig<FarmUpgradeData>
{
    public static Dictionary<int, FarmUpgradeData> DctFarmUpgradeData { get; private set; } = new();

    public override void Load()
    {
        DctFarmUpgradeData.Clear();

        if (Data == null || Data.Length == 0)
        {
#if UNITY_EDITOR
            Debug.LogWarning("FarmUpgradeConfig: No data found.");
#endif
            return;
        }

        foreach (var data in Data)
        {
            if (!DctFarmUpgradeData.ContainsKey(data.level))
            {
                DctFarmUpgradeData.Add(data.level, data);
#if UNITY_EDITOR
                Debug.Log($"[FarmUpgradeConfig] Added: {data.cost}");
#endif
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Duplicate key found: {data.level}");
            }
#endif
        }

#if UNITY_EDITOR
        Debug.Log($"[FarmUpgradeConfig] Loaded {DctFarmUpgradeData.Count} entries.");
#endif
    }

    public static bool Get(int level, out FarmUpgradeData result) =>
        DctFarmUpgradeData.TryGetValue(level, out result);
}
[Serializable]
public class FarmUpgradeData
{
    public int level;
    public int cost;
    public int productivity_bonus;
    public string require_item;
    public int amount;
}