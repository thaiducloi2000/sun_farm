using nano.vs2;
using UnityEngine;

public abstract class BuildingBase : MonoBehaviour, IInteractObject, IInteract, IBuilding
{
    [SerializeField] protected VisualState m_buildingState;
    public abstract IBuildData Data { get; }

    public abstract void Setup(IBuildData data);
    public abstract bool BuildAt(int index);
    public abstract IResource CollectResource();
    public IInteractObject Interact()
    {
        return this;
    }
}