using System;
using UnityEngine;

public class UIButtonBuild : MonoBehaviour
{
    [SerializeField] private BuildingBase m_building;
    private Action<BuildingBase> ClickBuildCallback;

    public void Click()
    {
        ClickBuildCallback?.Invoke(m_building);
    }

    public void Setup(Action<BuildingBase> onBuildingBuildCallBack)
    {
        ClickBuildCallback = onBuildingBuildCallBack;
    }
}