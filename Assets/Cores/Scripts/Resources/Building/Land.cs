using UnityEngine;
using System;

public struct LandPrefabData
{
    public int LandIndex;
    public Action<int> OnInteractCallback; 
}
public class Land : MonoBehaviour, IInteractObject, IInteract
{
    private LandPrefabData m_data;

    public void Setup(LandPrefabData data)
    {
        m_data = data;
    }
    
    public IInteractObject Interact()
    {
        m_data.OnInteractCallback?.Invoke(m_data.LandIndex);
        return this;
    }

    public void ResetData()
    {
        m_data = default;
    }
}