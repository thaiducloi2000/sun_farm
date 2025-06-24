using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCommon : PlayerData<CommonData>
{
    protected override string DataKey => "Common";
    protected override void LoadDataDefault()
    {
        Data = new CommonData();
        Data.Gold = 0;
    }
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
