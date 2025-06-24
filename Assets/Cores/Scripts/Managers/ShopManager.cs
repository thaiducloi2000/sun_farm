using EventBus;
using Score;
using UnityEngine;

public class ShopManager : MonoBehaviourEventListener
{
    private PlayerDataManager m_PlayerDataManager;
    private StoreConfig m_StoreConfig;

    protected override void RegisterEvents()
    {
        EventBus<UIEvent>.AddListener<ButtonClickData>((int)EventId_UI.Click_Buy, OnClickButton);
    }

    protected override void UnregisterEvents()
    {
        EventBus<UIEvent>.RemoveListener<ButtonClickData>((int)EventId_UI.Click_Buy, OnClickButton);
    }

    private void OnClickButton(ButtonClickData data)
    {
        if (m_StoreConfig == null)
        {
            m_StoreConfig = DataContainer.StoreConfig;
        }

        if (m_PlayerDataManager == null)
        {
            m_PlayerDataManager = PlayerDataManager.Instance;
        }

        switch (data.ButtonName)
        {
            case EventId_Gameplay.Click_Buy_Crop_BlueBerry:
                Buy_seed_blueberry();
                break;
            case EventId_Gameplay.Click_Buy_Crop_Tomato:
                Buy_seed_tomato();
                break;
            case EventId_Gameplay.Click_Buy_Cow:
                Buy_animal_cow();
                break;

            case EventId_Gameplay.Click_Buy_Land:
                Buy_land();
                break;
        }
    }

    private void Buy_seed_blueberry()
    {        if (!BuyItem("item_store_02", out var itemData)) return;
        if (!m_PlayerDataManager.PlayerCommon.SubGold(itemData.price)) return;

        int oldCow = m_PlayerDataManager.PlayerCommon.Data.BlueBerry_Crop;
        int newCow = m_PlayerDataManager.PlayerCommon.Data.BlueBerry_Crop + 1;
        m_PlayerDataManager.PlayerCommon.PostUIChange(EventId_UI.OnCropChange_BlueBerry, oldCow, newCow);
        m_PlayerDataManager.PlayerCommon.Data.BlueBerry_Crop += 1;
    }

    private void Buy_seed_tomato()
    {
        if (!BuyItem("item_store_01", out var itemData)) return;
        if (!m_PlayerDataManager.PlayerCommon.SubGold(itemData.price)) return;

        int oldCow = m_PlayerDataManager.PlayerCommon.Data.Tomato_Crop;
        int newCow = m_PlayerDataManager.PlayerCommon.Data.Tomato_Crop + 1;
        m_PlayerDataManager.PlayerCommon.PostUIChange(EventId_UI.OnCropChange_Tomato, oldCow, newCow);
        m_PlayerDataManager.PlayerCommon.Data.Tomato_Crop += 1;
    }

    private void Buy_animal_cow()
    {
        if (!BuyItem("item_store_03", out var itemData)) return;
        if (!m_PlayerDataManager.PlayerCommon.SubGold(itemData.price)) return;

        int oldCow = m_PlayerDataManager.PlayerCommon.Data.Cow;
        int newCow = m_PlayerDataManager.PlayerCommon.Data.Cow + 1;
        m_PlayerDataManager.PlayerCommon.PostUIChange(EventId_UI.OnAmountCowChange, oldCow, newCow);
        m_PlayerDataManager.PlayerCommon.Data.Cow += 1;
    }

    private void Buy_land()
    {
        string itemStoreId = "item_store_04";
        if (!m_StoreConfig.Get(itemStoreId, out var item))
        {
            Debug.Log($"[ShopManager] Not Found item: {itemStoreId}");
            return;
        }

        var buildings = m_PlayerDataManager.PlayerFarmData.Data.Buildings;
        var landIndex = buildings.FindIndex(x => x.id == item.item_id && x.amount > 0);

        if (landIndex >= 0)
        {
            var resource = buildings[landIndex];
            EventBus<UIEvent>.PostEvent((int)EventId_UI.OnLandChange, new UITextNumberData()
            {
                OldNumber = resource.amount,
                NewNumber = resource.amount - 1
            });
            resource.amount -= 1;
            buildings[landIndex] = resource;

            Debug.Log($"[ShopManager] Dùng 1 land từ kho có sẵn. Còn lại: {resource.amount}");

            EventBus<GameplayEvent>.PostEvent((int)EventId_Gameplay.SpawnLand, new GridListenerData() { amount = 1 });
            return;
        }

        Debug.Log(1);
        if (!BuyItem(itemStoreId, out var itemData)) return;
        EventBus<GameplayEvent>.PostEvent((int)EventId_Gameplay.SpawnLand, new GridListenerData() { amount = 1 });
    }

    private bool BuyItem(string storeId, out StoreData itemData)
    {
        itemData = default;

        if (!m_StoreConfig.Get(storeId, out itemData))
        {
            Debug.Log($"[ShopManager] Not Found item: {storeId}");
            return false;
        }

        if (!m_PlayerDataManager.PlayerCommon.SubGold(itemData.price))
        {
            Debug.Log($"[ShopManager] Not enough gold to buy: {storeId}");
            return false;
        }

        return true;
    }
}