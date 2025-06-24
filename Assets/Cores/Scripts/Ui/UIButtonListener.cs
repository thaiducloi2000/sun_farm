using System;
using EventBus;
using Score;
using UnityEngine;

[Serializable]
public struct ButtonClickData : IEventData
{
    public EventId_UI Key;
    public EventId_Gameplay ButtonName;
}

public class UIButtonListener : MonoBehaviour
{
    [SerializeField] private ButtonClickData Data;

    public void Click()
    {
        EventBus<UIEvent>.PostEvent((int)Data.Key, Data);
    }
}