using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "Local Save", menuName = "Configs/Saves/Local Save")]
public class LocalSave :  DataSave
{
    private string GetPath(string key)
    {
        return Path.Combine(Application.persistentDataPath, key + ".json");
    }

    public override void Save<T>(string key, T data)
    {
        try
        {
            string path = GetPath(key);
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[LocalSave] Failed to save {key}: {e.Message}");
        }
    }

    public override T Load<T>(string key)
    {
        try
        {
            string path = GetPath(key);
            if (!File.Exists(path))
                return default;

            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[LocalSave] Failed to load {key}: {e.Message}");
            return default;
        }
    }

    public override bool KeyExists(string key)
    {
        string path = GetPath(key);
        return File.Exists(path);
    }
}
