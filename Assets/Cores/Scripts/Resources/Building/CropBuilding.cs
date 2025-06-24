using UnityEngine;

public class CropBuilding : BuildingBase
{
    public PlantBuildData PlantBuildData { get; private set; }

    public override IBuildData Data => PlantBuildData;

    public override void Setup(IBuildData data)
    {
        if (data is not PlantBuildData dt) return;
        if (dt.PlantData == null)
        {
            return;
        }

        PlantBuildData = dt;
    }

    public override bool BuildAt(int index)
    {
        if (index < 0)
        {
#if UNITY_EDITOR
            Debug.LogWarning("Invalid build index.");
#endif
            return false;
        }

        m_buildingState.SetState(PlantBuildData.current_yield_count > 0 ? 1 : 0);
        PlantBuildData.PlaceAt(index);
        return true;
    }

    public override IResource CollectResource() => PlantBuildData;
}