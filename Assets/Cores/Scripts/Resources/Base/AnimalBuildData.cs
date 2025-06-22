using System;
using Newtonsoft.Json;

[Serializable]
public class AnimalBuildData : IBuildData, IResource
{
    public AnimalData animalData { get; private set; }
    public DateTime time_planted_at { get; private set; }
    public DateTime last_yield_time { get; private set; }
    public int plant_at_index { get; private set; }
    public int yield_count { get; private set; }
    public int current_yield_count { get; private set; }
    public string product_id => animalData.product_id;
    public int amount => current_yield_count;
    [field: JsonIgnore] public event Action OnYieldChange;
    public float time_to_next_yield
    {
        get
        {
            if (time_planted_at == DateTime.MinValue) return 0f;
            var baseTime = yield_count == 0 ? time_planted_at : last_yield_time;
            var elapsed = (float)(DateTime.Now - baseTime).TotalMinutes;
            return Math.Max(0f, animalData.produce_time_per_unit - elapsed);
        }
    }

    public AnimalBuildData(AnimalData data)
    {
        animalData = data;
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

    /// <summary>
    /// Validate the animal can produce the product.
    /// </summary>
    public bool CanYield()
    {
        return time_to_next_yield <= 0f && yield_count < animalData.max_yield &&
               current_yield_count < animalData.max_yield;
    }

    /// <summary>
    /// Ticking time when the animal produces product
    /// </summary>
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
        OnYieldChange?.Invoke();
        return result;
    }

    /// <summary>
    /// Validate the animal has reached the end of its life cycle.
    /// </summary>
    public bool IsDead() => yield_count >= animalData.max_yield;

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

    // public AnimalBuildSaveData ToSaveData()
    // {
    //     return new AnimalBuildSaveData
    //     {
    //         animal_id = Animal_Data.animal_id,
    //         plant_at_index = plant_at_index,
    //         time_planted_ticks = time_planted_at.Ticks,
    //         last_yield_ticks = last_yield_time.Ticks,
    //         yield_count = yield_count
    //     };
    // }
    //
    // public static AnimalBuildData FromSaveData(AnimalBuildSaveData saveData, AnimalData configData)
    // {
    //     return new AnimalBuildData(configData)
    //     {
    //         plant_at_index = saveData.plant_at_index,
    //         time_planted_at = new DateTime(saveData.time_planted_ticks),
    //         last_yield_time = new DateTime(saveData.last_yield_ticks),
    //         yield_count = saveData.yield_count
    //     };
    // }
}

[Serializable]
public struct AnimalBuildSaveData
{
    public string animal_id;
    public int plant_at_index;
    public long time_planted_ticks;
    public long last_yield_ticks;
    public int yield_count;
}