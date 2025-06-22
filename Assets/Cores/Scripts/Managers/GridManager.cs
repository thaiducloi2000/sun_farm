using System;
using System.Collections.Generic;
using EventBus;
using Score;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviourEventListener
{
    [Serializable]
    public struct GridListenerData : IEventData
    {
        public int amount;
    }

    [Header("Grid Settings")] [Tooltip("Number of tiles in X direction (columns)")]
    public int TILE_COUNT_X;

    [Tooltip("Number of tiles in Y direction (rows)")]
    public int TILE_COUNT_Y;

    [Tooltip("Size of each tile")] public float TILE_SPACING_X;
    [Tooltip("Size of each tile")] public float TILE_SPACING_Y;

    [Tooltip("Y-position offset for tiles")]
    public float _PositionY;

    [Header("Prefabs")] [Tooltip("Prefab for interactive tile")]
    public Land prefab;

    private IObjectPool<Land> m_Pool;


    // List of tile scripts for interaction
    [SerializeField] private List<Land> _tiles;

    /// <summary>
    /// Initializes the chess board grid
    /// </summary>
    void Awake()
    {
        if (m_Pool == null)
        {
            CreatePool();
        }
    }

    #region Test

    [Button]
    public void ClickGenerateTiles()
    {
        EventBus<GameplayEvent>.PostEvent((int)EventId_Gameplay.SpawnLand, new GridListenerData());
    }

    #endregion

    private void SpawnLand(GridListenerData Amount)
    {
        InitializeGrid();
    }

    /// <summary>
    /// Creates and positions all tiles on the board
    /// </summary>
    private void InitializeGrid()
    {
        m_Pool = CreatePool();
        ClearGrid();
        _tiles = new List<Land>(TILE_COUNT_X * TILE_COUNT_Y);

        for (int i = 0; i < TILE_COUNT_Y; i++)
        {
            for (int j = 0; j < TILE_COUNT_X; j++)
            {
                CreateTile(i, j);
            }
        }
    }
    private void ClearGrid()
    {
        if (_tiles == null) return;
        foreach (var tile in _tiles)
        {
            m_Pool.Release(tile);
        }
        _tiles.Clear();
    }

    /// <summary>
    /// Creates a single tile at the specified grid position
    /// </summary>
    private void CreateTile(int row, int col)
    {
        Land tile = m_Pool.Get();
        tile.transform.position = new Vector3(col * TILE_SPACING_X, _PositionY, row * TILE_SPACING_Y);
        tile.transform.SetParent(transform);
        _tiles.Add(tile);
        
        tile.Setup(new LandPrefabData()
        {
            LandIndex =  row * TILE_COUNT_X + col,
            OnInteractCallback = OnSelectLand,
        });
    }

    private void OnSelectLand(int index)
    {
        EventBus<GameplayEvent>.PostEvent((int)EventId_Gameplay.RequestBuildAtLand, new RequestBuildAtLand()
        {
            landIndex = index,
            canBuildRespone = CanBuildLand,
            refuseBuildRespone = RefuseBuildLand
        } );
    }

    private void CanBuildLand()
    {
        
    }

    private void RefuseBuildLand()
    {
        
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