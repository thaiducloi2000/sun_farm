using UnityEngine;

public abstract class BuildingBase<T> : MonoBehaviour, IInteractObject, IInteract, IBuilding where T : IBuildData
{
    protected T m_data;
    public IBuildData Data => m_data;

    public abstract void Setup(T data);
    public abstract bool BuildAt(int index);
    public abstract IResource CollectResource();
    public virtual IInteractObject Interact()
    {
        return this;
    }
}