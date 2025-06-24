using System;
using System.Collections.Generic;
using EventBus;
using Score;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class GridManager : MonoBehaviourEventListener
{
    [Tooltip("Size of each tile")] public float TILE_SPACING_X;
    [Tooltip("Size of each tile")] public float TILE_SPACING_Y;

    [Tooltip("Y-position offset for tiles")]
    public float _PositionY;

    [Header("Prefabs")] [Tooltip("Prefab for interactive tile")]
    public Land prefab;

    private IObjectPool<Land> m_Pool;
    private BuildConfig m_BuildConfig;

    // List of tile scripts for interaction
    [FormerlySerializedAs("_tiles")] [SerializeField]
    private List<Land> m_lands;

    private int row;
    private int col;
    private int currentSelectedLand = -1;

    /// <summary>
    /// Initializes the chess board grid
    /// </summary>
    void Awake()
    {
        if (m_Pool == null)
        {
            CreatePool();
        }

        m_BuildConfig = DataContainer.BuildConfig;
    }

    #region Test

    [Button]
    public void ClickGenerateTiles()
    {
        EventBus<GameplayEvent>.PostEvent((int)EventId_Gameplay.SpawnLand, new GridListenerData() { amount = 1 });
    }

    #endregion

    private void SpawnLand(GridListenerData data)
    {
        if (!m_BuildConfig.Get(GameDefine.ConfigValue.LandId, 1, out BuildData landBuildData)) return;
        if (m_lands.Count >= landBuildData.max_can_build)
        {
            Debug.Log("Can Not Build More");
            return;
        }

        m_Pool ??= CreatePool();
        int totalSpawned = m_lands.Count;
        row = totalSpawned / GameDefine.ConfigValue.MaxTilesPerRow;
        col = totalSpawned % GameDefine.ConfigValue.MaxTilesPerRow;
        EventBus<GameplayEvent>.PostEvent((int)EventId_Gameplay.UnClockLand, new LandManager.UnclockLandRequest
        {
            LandIndex = row * GameDefine.ConfigValue.MaxTilesPerRow + col,
            OnUnclockResult = OnCreateCallBack,
        });
    }

    private void ClearGrid()
    {
        if (m_lands == null) return;
        foreach (var tile in m_lands)
        {
            m_Pool.Release(tile);
        }

        m_lands.Clear();
    }

    private void OnCreateCallBack(bool IsSuccess)
    {
        if (IsSuccess)
        {
            CreateLand(row, col);
        }

        row = col = -1;
    }

    /// <summary>
    /// Creates a single tile at the specified grid position
    /// </summary>
    private void CreateLand(int row, int col)
    {
        Land land = m_Pool.Get();
        land.transform.position = new Vector3(col * TILE_SPACING_X, _PositionY, row * TILE_SPACING_Y);
        land.transform.SetParent(transform);
        m_lands.Add(land);

        land.Setup(new LandPrefabData()
        {
            LandIndex = row * GameDefine.ConfigValue.MaxTilesPerRow + col,
            OnInteractCallback = OnSelectLand,
        });
    }

    private void OnSelectLand(int index)
    {
        currentSelectedLand = index;
        EventBus<GameplayEvent>.PostEvent((int)EventId_Gameplay.RequestBuildAtLand, new RequestBuildAtLand()
        {
            landIndex = index,
            canBuildRespone = CanBuildLand,
            refuseBuildRespone = RefuseBuildLand
        });
    }

    private void CanBuildLand()
    {
        if (currentSelectedLand < 0) return;
        EventBus<UIEvent>.PostEvent((int)EventId_UI.OnShowUiBuild, new OnSelectBuildAtLand()
        {
            OnBuildingBuildCallBack = OnBuildingBuildCallBack
        });
    }

    private void OnBuildingBuildCallBack(BuildingBase data)
    {
        if (currentSelectedLand < 0) return;
        EventBus<GameplayEvent>.PostEvent((int)EventId_Gameplay.RequestBuildingAtLand, new RequestBuildingAtLandData()
        {
            index = currentSelectedLand,
            BuildingData = data,
            position = m_lands[currentSelectedLand].transform.position,
        });
        currentSelectedLand = -1;
    }

    private void RefuseBuildLand()
    {
        Debug.Log("Can't Build Land");
    }

    #region Pool

    private IObjectPool<Land> CreatePool()
    {
        return new ObjectPool<Land>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
            OnDestroyPoolObject);
    }

    private Land CreatePooledItem()
    {
        return CreatePooledItem(prefab);
    }

    private Land CreatePooledItem(Land prefabs)
    {
        Land fx = Instantiate(prefabs);
        fx.name = prefabs.name;
        return fx;
    }

    private void OnReturnedToPool(Land fx)
    {
        fx.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(Land fx)
    {
        fx.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(Land fx)
    {
        Destroy(fx.gameObject);
    }

    #endregion

    protected override void RegisterEvents()
    {
        EventBus<GameplayEvent>.AddListener<GridListenerData>((int)EventId_Gameplay.SpawnLand, SpawnLand);
    }

    protected override void UnregisterEvents()
    {
        EventBus<GameplayEvent>.RemoveListener<GridListenerData>((int)EventId_Gameplay.SpawnLand, SpawnLand);
    }
}