using System.Collections.Generic;
using UnityEngine;
using Score;
using System;
using Sirenix.Serialization;

public class WorkerConfig : BaseConfig<WorkerData>
{
    [NonSerialized, OdinSerialize] public Dictionary<string, WorkerData> DctWorkerData = new();

    public override void Load()
    {
        DctWorkerData.Clear();

        if (Data == null || Data.Length == 0)
        {
#if UNITY_EDITOR
            Debug.LogWarning("WorkerConfig: No data found.");
#endif
            return;
        }

        foreach (var data in Data)
        {
            if (!DctWorkerData.ContainsKey(data.worker_id))
            {
                DctWorkerData.Add(data.worker_id, data);
#if UNITY_EDITOR
                Debug.Log($"[WorkerConfig] Added: {data.worker_name}");
#endif
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Duplicate key found: {data.worker_id}");
            }
#endif
        }

#if UNITY_EDITOR
        Debug.Log($"[WorkerConfig] Loaded {DctWorkerData.Count} entries.");
#endif
    }

    public bool Get(string id, out WorkerData result) =>
        DctWorkerData.TryGetValue(id, out result);
}
[Serializable]
public class WorkerData
{
    public string worker_id;
    public string worker_name;
    public int hire_cost;
    public int action_time_per_task;
    public string description;
    public string item_type;
}