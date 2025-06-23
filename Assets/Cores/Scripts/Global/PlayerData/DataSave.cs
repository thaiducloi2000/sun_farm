using UnityEngine;

public abstract class DataSave : ScriptableObject,ISave
{
    public abstract bool KeyExists(string key);

    public abstract T Load<T>(string key);

    public abstract void Save<T>(string key, T Data);
}
