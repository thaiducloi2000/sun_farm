using EventBus;
using Score;
using UnityEngine;

public class UIBuildButtonGroup : MonoBehaviourEventListener
{
    [SerializeField] private UIButtonBuild[] m_buttons;
    private OnSelectBuildAtLand m_data;

    protected override void RegisterEvents()
    {
        EventBus<UIEvent>.AddListener<OnSelectBuildAtLand>((int)EventId_UI.OnShowUiBuild,
            RequestBuildingAtLandDataHandler);

        ShowButtons(false);
    }

    protected override void UnregisterEvents()
    {
        EventBus<UIEvent>.RemoveListener<OnSelectBuildAtLand>((int)EventId_UI.OnShowUiBuild,
            RequestBuildingAtLandDataHandler);
    }

    private void RequestBuildingAtLandDataHandler(OnSelectBuildAtLand data)
    {
        m_data = data;
        ShowButtons(true);
    }

    private void ShowButtons(bool IsShow)
    {
        foreach (var button in m_buttons)
        {
            button.gameObject.SetActive(IsShow);
            if (IsShow)
            {
                button.Setup(OnClickBuildCallBack);
            }
        }
    }

    private void OnClickBuildCallBack(BuildingBase building)
    {
        m_data.OnBuildingBuildCallBack?.Invoke(building);
        ShowButtons(false);
    }
}