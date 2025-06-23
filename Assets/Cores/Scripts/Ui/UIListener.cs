using System;
using System.Collections.Generic;
using EventBus;
using Score;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ObjectEvent<T> : UnityEvent<T>
{
}

[Serializable]
public struct EventListenerCallBack<T>
{
    public EventId_UI KeyListener;
    public ObjectEvent<T> Callback;
}

public abstract class UIListener<T> : MonoBehaviour, IEventListener where T : IEventUIData
{
    [SerializeField] protected EventListenerCallBack<T>[] EventListenerCallBacks;
    protected Dictionary<EventId_UI, EventCallback<T>> _eventsCallback;


    private void OnEnable() => SetupEventListener();
    private void OnDisable() => RemoveEventListener();

    public virtual void SetupEventListener()
    {
        _eventsCallback = new Dictionary<EventId_UI, EventCallback<T>>();

        foreach (var listener in EventListenerCallBacks)
        {
            EventCallback<T> wrapper = (payload) => listener.Callback.Invoke(payload);
            EventBus<UIEvent>.AddListener((int)listener.KeyListener, wrapper);
            _eventsCallback.Add(listener.KeyListener, wrapper);
        }
    }

    public virtual void RemoveEventListener()
    {
        foreach (var kvp in _eventsCallback)
        {
            EventBus<UIEvent>.RemoveListener((int)kvp.Key, kvp.Value);
        }

        _eventsCallback.Clear();
    }
}