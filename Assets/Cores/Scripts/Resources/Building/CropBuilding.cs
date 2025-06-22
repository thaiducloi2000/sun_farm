using UnityEngine;

public class CropBuilding : BuildingBase<PlantBuildData>
{
    public override void Setup(PlantBuildData data)
    {
        if (data.PlantData == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning("Setup failed: PlantData is null.");
#endif
            return;
        }

        m_data = data;
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

        m_buildingState.SetState(m_data.current_yield_count > 0 ? 1 : 0);
        m_data.PlaceAt(index);
        return true;
    }

    public override IResource CollectResource() => m_data;
}