using UnityEngine;

public class AnimalBuilding : BuildingBase<AnimalBuildData>
{
    public override void Setup(AnimalBuildData data)
    {
        if (data.animalData == null)
        {
            Debug.LogWarning("Setup failed: AnimalData is null.");
            return;
        }

        m_data = data;
    }

    public override bool BuildAt(int index)
    {
        if (index < 0)
        {
            Debug.LogWarning("Invalid build index.");
            return false;
        }

        m_data.PlaceAt(index);
        return true;
    }

    public override IResource CollectResource()
    {
        if (m_data.amount <= 0)
        {
            Debug.Log("No animal products to collect.");
            return null;
        }

        m_data.Collect();
        return m_data;
    }
}
