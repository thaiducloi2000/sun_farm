using System;
using System.Collections.Generic;
using System.Linq;
using Score;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "Starter Config", menuName = "Configs/StarterConfig")]
public class StarterConfig : BaseConfig<StarterData>
{
    [NonSerialized, OdinSerialize] public Dictionary<ItemType, StarterData> DctStarterData = new();

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