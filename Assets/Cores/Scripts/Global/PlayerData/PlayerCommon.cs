using System;
using Score;
using UnityEngine;
using EventBus;

public class PlayerCommon : PlayerData<CommonData>
{
    protected override string DataKey => "Common";

    protected override void LoadDataDefault()
    {
        Data = new CommonData();
        Data.Gold = 0;
        Data.Level = 1;
    }

    #region Gold

    public bool SubGold(int require)
    {
        if (Data.Gold < require)
        {
            Debug.Log($"[PlayerCommon Data] Gold Not Enought");
            return false;
        }

        EventBus<UIEvent>.PostEvent((int)EventId_UI.OnGoldValueChange, new UITextNumberData()
        {
            OldNumber = Data.Gold,
            NewNumber = Data.Gold - require
        });
        Data.Gold -= require;
        SetDirty();
        return true;
    }

    public void AddGold(int amount)
    {
        EventBus<UIEvent>.PostEvent((int)EventId_UI.OnGoldValueChange, new UITextNumberData()
        {
            OldNumber = Data.Gold,
            NewNumber = Data.Gold + amount
        });
        Data.Gold += amount;
        SetDirty();
    }

    #endregion

    #region Utils
    public void PostUIChange(EventId_UI eventId, int oldValue, int newValue)
    {
        EventBus<UIEvent>.PostEvent((int)eventId, new UITextNumberData()
        {
            OldNumber = oldValue,
            NewNumber = newValue
        });
        SetDirty();
    }
    #endregion
}

[Serializable]
public class CommonData
{
    public int Level;
    public int Gold;
    public int Tomato_Crop;
    public int BlueBerry_Crop;
    public int StrawBerry_Crop;
    public int Cow;
    public int BlueBerry;
    public int StrawBerry;
    public int Tomato;
    public int Milk;
}