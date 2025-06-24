using System;
using System.Collections.Generic;
using EventBus;
using Score;
using UnityEngine;
using UnityEngine.Pool;

[Serializable]
public class RequestBuildingAtLandData : IEventData
{
    public int index;
    public Vector3 position;
    public BuildingBase BuildingData;
}

public class BuildingManager : MonoBehaviourEventListener
{
    private Dictionary<string, IObjectPool<BuildingBase>> buildings = new Dictionary<string, IObjectPool<BuildingBase>>();
    private Dictionary<int, BuildingBase> CurrentBuilding = new Dictionary<int, BuildingBase>();
    protected override void RegisterEvents()
    {
        EventBus<GameplayEvent>.AddListener<RequestBuildingAtLandData>(
            (int)EventId_Gameplay.RequestBuildingAtLand,
            OnBuildingBuildCallBack
        );
    }

    protected override void UnregisterEvents()
    {
        EventBus<GameplayEvent>.RemoveListener<RequestBuildingAtLandData>((int)EventId_Gameplay.RequestBuildingAtLand,
            OnBuildingBuildCallBack
        );
    }

    private void OnBuildingBuildCallBack(RequestBuildingAtLandData data)
    {
        if (CurrentBuilding.TryGetValue(data.index, out BuildingBase building))
        {

            Debug.Log($"[BuildingManager] Built {building.name} at {data.position} fail because have other building");
            return;
        }
        string key = data.BuildingData.name;

        if (!buildings.TryGetValue(key, out var pool))
        {
            pool = CreatePool(data.BuildingData);
            buildings.Add(key, pool);
        }

        var instance = pool.Get();
        instance.transform.position = data.position;

        // instance.Setup(data.BuildingData.m_data);
        instance.name = data.BuildingData.name;
        CurrentBuilding.Add(data.index, instance);
        Debug.Log($"[BuildingManager] Built {instance.name} at {data.position}");
    }

    #region Pool

    private IObjectPool<BuildingBase> CreatePool(BuildingBase prefab)
    {
        return new ObjectPool<BuildingBase>(() => CreatePooledItem(prefab), OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject);
    }

    private BuildingBase CreatePooledItem(BuildingBase prefabs)
    {
        BuildingBase obj = Instantiate(prefabs);
        obj.name = prefabs.name;
        return obj;
    }

    private void OnReturnedToPool(BuildingBase obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(BuildingBase obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(BuildingBase obj)
    {
        Destroy(obj.gameObject);
    }
    #endregion
}