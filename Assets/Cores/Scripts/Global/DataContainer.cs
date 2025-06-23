using UnityEngine;

public class DataContainer : MonoBehaviour
{
    public static DataContainer Instance = default;
    [field: SerializeField] private PlantConfig m_PlantConfig;
    [field: SerializeField] private AnimalConfig m_AnimalConfig;
    [field: SerializeField] private ProductConfig m_ProductConfig;
    [field: SerializeField] private BuildConfig m_BuildConfig;
    [field: SerializeField] private BuildingConfig m_BuildingConfig;
    [field: SerializeField] private WorkerConfig m_WorkerConfig;
    [field: SerializeField] private FarmUpgradeConfig m_FarmUpgradeConfig;
    [field: SerializeField] private EquipmentConfig m_EquipmentConfig;
    [field: SerializeField] private StoreConfig m_StoreConfig;
    [field: SerializeField] private StarterConfig m_StarterConfig;

    public static PlantConfig PlantConfig => Instance.m_PlantConfig;
    public static AnimalConfig AnimalConfig => Instance.m_AnimalConfig;
    public static ProductConfig ProductConfig => Instance.m_ProductConfig;
    public static BuildConfig BuildConfig => Instance.m_BuildConfig;
    public static BuildingConfig BuildingConfig => Instance.m_BuildingConfig;
    public static WorkerConfig WorkerConfig => Instance.m_WorkerConfig;
    public static FarmUpgradeConfig FarmUpgradeConfig => Instance.m_FarmUpgradeConfig;
    public static EquipmentConfig EquipmentConfig => Instance.m_EquipmentConfig;
    public static StoreConfig StoreConfig => Instance.m_StoreConfig;
    public static StarterConfig StarterConfig => Instance.m_StarterConfig;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}