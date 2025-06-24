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
    {
        if (!m_StoreConfig.Get("item_store_02", out StoreData itemData))
        {
            Debug.Log($"[ShopManager] Not Found item: item_store_02");
            return;
        }

        if (m_PlayerDataManager.PlayerCommon.Data.Gold < itemData.price)
        {
            Debug.Log($"[ShopManager] Gold Not Enought for Buy : item_store_02 - Require {itemData.price}");
            return;
        }

        int oldGold = m_PlayerDataManager.PlayerCommon.Data.Gold;
        int newGold = m_PlayerDataManager.PlayerCommon.Data.Gold - itemData.price;
        UITextNumberData goldData = new UITextNumberData()
        {
            OldNumber = oldGold,
            NewNumber = newGold
        };
        EventBus<UIEvent>.PostEvent((int)EventId_UI.OnGoldValueChange, goldData);

        int oldSeed = m_PlayerDataManager.PlayerCommon.Data.BlueBerry_Crop;
        int newSeed = m_PlayerDataManager.PlayerCommon.Data.BlueBerry_Crop + 1;

        UITextNumberData blueBerryCrop = new UITextNumberData()
        {
            OldNumber = oldSeed,
            NewNumber = newSeed
        };
        EventBus<UIEvent>.PostEvent((int)EventId_UI.OnCropChange_BlueBerry, blueBerryCrop);
    }

    private void Buy_seed_tomato()
    {
        if (!m_StoreConfig.Get("item_store_01", out StoreData itemData))
        {
            Debug.Log($"[ShopManager] Not Found item: item_store_01");
            return;
        }

        if (m_PlayerDataManager.PlayerCommon.Data.Gold < itemData.price)
        {
            Debug.Log($"[ShopManager] Gold Not Enought for Buy : item_store_01 - Require {itemData.price}");
            return;
        }

        int oldGold = m_PlayerDataManager.PlayerCommon.Data.Gold;
        int newGold = m_PlayerDataManager.PlayerCommon.Data.Gold - itemData.price;
        UITextNumberData goldData = new UITextNumberData()
        {
            OldNumber = oldGold,
            NewNumber = newGold
        };
        EventBus<UIEvent>.PostEvent((int)EventId_UI.OnGoldValueChange, goldData);

        int oldSeed = m_PlayerDataManager.PlayerCommon.Data.Tomato_Crop;
        int newSeed = m_PlayerDataManager.PlayerCommon.Data.Tomato_Crop + 1;

        UITextNumberData tomatoCrop = new UITextNumberData()
        {
            OldNumber = oldSeed,
            NewNumber = newSeed
        };
        EventBus<UIEvent>.PostEvent((int)EventId_UI.OnCropChange_Tomato, tomatoCrop);
    }

    private void Buy_animal_cow()
    {
        if (!m_StoreConfig.Get("item_store_03", out StoreData itemData))
        {
            Debug.Log($"[ShopManager] Not Found item: item_store_03");
            return;
        }

        if (m_PlayerDataManager.PlayerCommon.Data.Gold < itemData.price)
        {
            Debug.Log($"[ShopManager] Gold Not Enought for Buy : item_store_03 - Require {itemData.price}");
            return;
        }

        int oldGold = m_PlayerDataManager.PlayerCommon.Data.Gold;
        int newGold = m_PlayerDataManager.PlayerCommon.Data.Gold - itemData.price;
        UITextNumberData goldData = new UITextNumberData()
        {
            OldNumber = oldGold,
            NewNumber = newGold
        };
        EventBus<UIEvent>.PostEvent((int)EventId_UI.OnGoldValueChange, goldData);

        int oldCow = m_PlayerDataManager.PlayerCommon.Data.Cow;
        int newCow = m_PlayerDataManager.PlayerCommon.Data.Cow + 1;

        UITextNumberData tomatoCrop = new UITextNumberData()
        {
            OldNumber = oldCow,
            NewNumber = newCow
        };
        EventBus<UIEvent>.PostEvent((int)EventId_UI.OnAmountCowChange, tomatoCrop);
    }

    private void Buy_land()
    {
        if (!m_StoreConfig.Get("item_store_04", out StoreData itemData))
        {
            Debug.Log($"[ShopManager] Not Found item: item_store_04");
            return;
        }

        if (m_PlayerDataManager.PlayerCommon.Data.Gold < itemData.price)
        {
            Debug.Log($"[ShopManager] Gold Not Enought for Buy : item_store_04 - Require {itemData.price}");
            return;
        }

        int oldGold = m_PlayerDataManager.PlayerCommon.Data.Gold;
        int newGold = m_PlayerDataManager.PlayerCommon.Data.Gold - itemData.price;
        UITextNumberData goldData = new UITextNumberData()
        {
            OldNumber = oldGold,
            NewNumber = newGold
        };
        EventBus<UIEvent>.PostEvent((int)EventId_UI.OnGoldValueChange, goldData);
        EventBus<GameplayEvent>.PostEvent((int)EventId_Gameplay.SpawnLand, new GridListenerData() { amount = 1 });
    }
}