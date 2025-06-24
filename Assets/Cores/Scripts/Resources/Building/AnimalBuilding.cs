using UnityEngine;

public class AnimalBuilding : BuildingBase
{
    private AnimalBuildData AnimalBuildData;

    public override IBuildData Data => AnimalBuildData;

    public override void Setup(IBuildData data)
    {
        if (data is not AnimalBuildData dt) return;
        if (dt.animalData == null)
        {
            Debug.LogWarning("Setup failed: AnimalData is null.");
            return;
        }
        AnimalBuildData = dt;
    }

    public override bool BuildAt(int index)
    {
        if (index < 0)
        {
            Debug.LogWarning("Invalid build index.");
            return false;
        }

        AnimalBuildData.PlaceAt(index);
        return true;
    }

    public override IResource CollectResource()
    {
        if (AnimalBuildData.amount <= 0)
        {
            Debug.Log("No animal products to collect.");
            return null;
        }

        AnimalBuildData.Collect();
        return AnimalBuildData;
    }
}
