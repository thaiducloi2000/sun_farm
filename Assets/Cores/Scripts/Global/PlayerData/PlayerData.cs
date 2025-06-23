using UnityEngine;

public abstract class PlayerData<T> : MonoBehaviour
{
    [field: SerializeField] public T Data { get; protected set; }
    [field: SerializeField] protected DataSave _saveBase;

    public bool IsDirty { get; private set; }
    protected abstract string DataKey { get; }

    public virtual void SetDirty() => IsDirty = true;
    public virtual void Clear() => IsDirty = false;

    #region Save/LoadData
    public virtual void Save()
    {
        if (Data == null)
        {
            Debug.LogWarning($"{nameof(PlayerData<T>)}: Data is null, skip saving.");
            return;
        }
        if (!IsDirty) return;

        _saveBase.Save(DataKey, Data);
        Clear();
    }

    public virtual void Load()
    {
        if (_saveBase.KeyExists(DataKey))
        {
            Data = _saveBase.Load<T>(DataKey);
        }
        else
        {
            LoadDataDefault();
        }

        Clear();
    }

    protected abstract void LoadDataDefault();

    public string GetJson() => JsonUtility.ToJson(Data);
    #endregion
}