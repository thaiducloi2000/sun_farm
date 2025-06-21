using System.Collections.Generic;
using UnityEngine;
using Score;
using System;

public class EquipmentConfig : BaseConfig<EquipmentData>
{
    public static Dictionary<string, EquipmentData> DctEquipmentData { get; private set; } = new();

    public override void Load()
    {
        DctEquipmentData.Clear();

        if (Data == null || Data.Length == 0)
        {
#if UNITY_EDITOR
            Debug.LogWarning("EquipmentConfig: No data found.");
#endif
            return;
        }

        foreach (var data in Data)
        {
            if (!DctEquipmentData.ContainsKey(data.equipment_id))
            {
                DctEquipmentData.Add(data.equipment_id, data);
#if UNITY_EDITOR
                Debug.Log($"[EquipmentConfig] Added: {data.equipment_name}");
#endif
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Duplicate key found: {data.equipment_id}");
            }
#endif
        }

#if UNITY_EDITOR
        Debug.Log($"[EquipmentConfig] Loaded {DctEquipmentData.Count} entries.");
#endif
    }

    public static bool Get(string id, out EquipmentData result) =>
        DctEquipmentData.TryGetValue(id, out result);
}
[System.Serializable]
public class EquipmentData
{
    public string equipment_id;
    public string equipment_name;
    public string item_type;
}