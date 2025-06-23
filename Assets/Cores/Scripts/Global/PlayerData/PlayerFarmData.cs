using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFarmData : PlayerData<FarmData>
{
    protected override string DataKey => "FarmData";

    protected override void LoadDataDefault()
    {
        Data = new FarmData()
        {
            Seeds = new List<ResourceData>(),
            Animals = new List<ResourceData>(),
            Starters = new List<ResourceData>(),
            Buildings = new List<ResourceData>(),
            Equipments = new List<ResourceData>(),
        };
        Dictionary<ItemType, StarterData> starterData = DataContainer.StarterConfig.DctStarterData;
        foreach (var data in starterData)
        {
            ResourceData resource = new ResourceData() { id = data.Value.item_id, amount = data.Value.amount };
            Data.AddResource(data.Key, resource);
        }
    }
}

[Serializable]
public class FarmData
{
    public List<ResourceData> Seeds;
    public List<ResourceData> Animals;
    public List<ResourceData> Starters;
    public List<ResourceData> Buildings;
    public List<ResourceData> Equipments;

    public List<ResourceData> GetByType(ItemType type)
    {
        return type switch
        {
            ItemType.Seed => Seeds,
            ItemType.Animal => Animals,
            ItemType.Worker => Starters,
            ItemType.Building => Buildings,
            ItemType.Equipment => Equipments,
            _ => null
        };
    }

    public void AddResource(ItemType type, ResourceData data)
    {
        var list = GetByType(type);
        if (list != null) list.Add(data);
    }
}

[Serializable]
public struct ResourceData
{
    public string id;
    public int amount;
}