using System;
using Newtonsoft.Json;

[Serializable]
public class PlantBuildData : IBuildData, IResource
{
    public PlantData PlantData;
    public DateTime time_planted_at { get; private set; }
    public DateTime last_yield_time { get; private set; }
    public int plant_at_index { get; private set; }
    public int yield_count { get; private set; }
    public int current_yield_count { get; private set; }
    [field: JsonIgnore] public event Action OnYieldChange;
    public string product_id => PlantData.product_id;
    public int amount => current_yield_count;
    
    public float time_to_next_yield => CalculateTimeToNextYield();

    public PlantBuildData(PlantData data)
    {
        PlantData = data;
        plant_at_index = -1;
        time_planted_at = DateTime.MinValue;
        last_yield_time = DateTime.MinValue;
        yield_count = 0;
        current_yield_count = 0;
    }

    public void PlaceAt(int index)
    {
        plant_at_index = index;
        time_planted_at = DateTime.Now;
        last_yield_time = time_planted_at;
        yield_count = 0;
        current_yield_count = 0;
    }

    private float CalculateTimeToNextYield()
    {
        if (time_planted_at == DateTime.MinValue) return 0f;
        var baseTime = yield_count == 0 ? time_planted_at : last_yield_time;
        double elapsedMinutes = (DateTime.Now - baseTime).TotalMinutes;
        return Math.Max(0f, PlantData.grow_time_per_yield - (float)elapsedMinutes);
    }

    public bool CanYield()
    {
        return time_to_next_yield <= 0f && yield_count < PlantData.max_yield && 
               current_yield_count < PlantData.max_yield;;
    }

    public void OnYield()
    {
        if (!CanYield()) return;

        yield_count++;
        current_yield_count++;
        OnYieldChange?.Invoke();
        last_yield_time = DateTime.Now;
    }

    public int Collect()
    {
        int result = current_yield_count;
        current_yield_count = 0;
        return result;
    }

    public bool IsDead() => yield_count >= PlantData.max_yield;

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

    // public PlantBuildSaveData ToSaveData()
    // {
    //     return new PlantBuildSaveData
    //     {
    //         plant_id = PlantData.plant_id,
    //         plant_at_index = plant_at_index,
    //         time_planted_ticks = time_planted_at.Ticks,
    //         last_yield_ticks = last_yield_time.Ticks,
    //         yield_count = yield_count
    //     };
    // }
    //
    // public static PlantBuildData FromSaveData(PlantBuildSaveData saveData, PlantData configData)
    // {
    //     return new PlantBuildData(configData)
    //     {
    //         plant_at_index = saveData.plant_at_index,
    //         time_planted_at = new DateTime(saveData.time_planted_ticks),
    //         last_yield_time = new DateTime(saveData.last_yield_ticks),
    //         yield_count = saveData.yield_count
    //     };
    // }
}

[Serializable]
public struct PlantBuildSaveData
{
    public string plant_id;
    public int plant_at_index;
    public long time_planted_ticks;
    public long last_yield_ticks;
    public int yield_count;
}