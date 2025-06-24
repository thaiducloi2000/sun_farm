using System;
using EventBus;

namespace Score
{
    public static class GameDefine
    {

        public class Path
        {
            public const string GENERATE_PATH = "Cores/Scripts/Generated/";
            public const string ItemType_Path = GENERATE_PATH + "ItemType.cs";
            public const string CropPrefabPath = "Assets/Cores/Prefabs/Crops";
            public const string AnimalPrefabPath = "Assets/Cores/Prefabs/Animals";
        }

        public class EnumDefine
        {
            public const string ItemType = "ItemType";
        }

        public class ConfigValue
        {
            public const int MaxTilesPerRow = 10;
            public const string LandId = "building_01";
        }
    }
    [Serializable]
    public struct RequestBuildAtLand : IEventData
    {
        public int landIndex;
        public Action canBuildRespone;
        public Action refuseBuildRespone;
    }

    public struct OnSelectBuildAtLand : IEventData
    {
        public Action<BuildingBase> OnBuildingBuildCallBack;
    }
    
    [Serializable]
    public struct GridListenerData : IEventData
    {
        public int amount;
    }
    #region Marker Class for Event bus System 
    public class GameplayEvent : IEventChannel { }
    public class UIEvent : IEventChannel { }
    #endregion

    #region EventId_Gameplay
    public enum EventId_Gameplay
    {
        None = 0,
        
        //Grid
        SpawnLand = 1000,
        UnClockLand,
        RequestBuildingAtLand ,
        RequestBuildAtLand = 5000,
        
        Click_Buy_Crop_Tomato,
        Click_Buy_Crop_BlueBerry,
        Click_Buy_Crop_StrawBerry,
        Click_Buy_Land,
        Click_Buy_Cow,
        
        Click_Build_Tomato = 6000,
        Click_Build_BlueBerry,
        Click_Build_StrawBerry,
        Click_Build_Cow,
    }
    #endregion
    #region EventId_UI
    public enum EventId_UI
    {
        None = 0,
        
        OnShowUiBuild = 400,
        OnHideUIBuild = 500,
        // OnValue Change
        OnGoldValueChange = 1000,
        OnLevelChange,
        OnCropChange_Tomato,
        OnCropChange_BlueBerry,
        OnCropChange_StrawBerry,
        OnAmountCowChange,
        OnTomatoChange,
        OnBlueBerryChange,
        OnStrawBerryChange,
        OnMilkChange,
        OnLandChange,
        
        // UI Click
        Click_Buy = 5000,
        Click_Build = 6000,
    }
    #endregion
}
