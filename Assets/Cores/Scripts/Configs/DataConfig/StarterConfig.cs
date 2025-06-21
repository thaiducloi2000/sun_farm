using System;
using System.Collections.Generic;
using System.Linq;
using Score;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Starter Config", menuName = "Configs/StarterConfig")]
public class StarterConfig : BaseConfig<StarterData>
{
    public static Dictionary<ItemType, StarterData> DctStarterData { get; private set; } = new();

    public override void Load()
    {
        DctStarterData.Clear();

        foreach (var data in Data)
        {
            if (DctStarterData.ContainsKey(data.item_type)) continue;
            DctStarterData.Add(data.item_type, data);
        }
#if UNITY_EDITOR
        Debug.Log(DctStarterData.Count);
#endif
    }
}

[Serializable]
public class StarterData
{
    public ItemType item_type;
    public string item_id;
    public int amount;
}