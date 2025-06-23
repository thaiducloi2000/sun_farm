using System;
using Score;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

public class BuildConfig : BaseConfig<BuildData>
{
    [NonSerialized, OdinSerialize] public Dictionary<string, Dictionary<int, BuildData>> DctBuildData = new();

    public override void Load()
    {
        DctBuildData.Clear();

        if (Data == null || Data.Length == 0)
        {
            return;
        }

        foreach (var data in Data)
        {
            if (!DctBuildData.TryGetValue(data.building_id, out Dictionary<int, BuildData> buildDatas))
            {
                buildDatas = new Dictionary<int, BuildData>();
                DctBuildData[data.building_id] = buildDatas;
            }

            if (!buildDatas.TryGetValue(data.level, out BuildData buildData))
            {
                buildDatas[data.level] = data;
#if UNITY_EDITOR
                Debug.Log($"[BuildConfig] Added: {data.building_id} - Level {data.level}");
#endif
            }
        }
    }

    public bool Get(string id, int level, out BuildData  result)
    {
        {
            result = default;
            if (DctBuildData.TryGetValue(id, out var levels))
            {
                return levels.TryGetValue(level, out result);
            }
            return false;
        }
    }
}

[System.Serializable]
    public class BuildData
    {
        public string building_id;
        public int level;
        public int max_can_build;
    }