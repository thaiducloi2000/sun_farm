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
        }

        public class EnumDefine
        {
            public const string ItemType = "ItemType";
        }
    }
    [Serializable]
    public struct RequestBuildAtLand : IEventData
    {
        public int landIndex;
        public Action canBuildRespone;
        public Action refuseBuildRespone;
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
        RequestBuildAtLand = 5000,
    }
    #endregion
    #region EventId_UI
    public enum EventId_UI
    {
        None = 0,
        
        // Request Popup
    }
    #endregion
}
