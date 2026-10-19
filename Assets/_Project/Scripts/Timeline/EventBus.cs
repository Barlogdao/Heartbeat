using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    private List<EventData> _events = new();
    public static EventBus Instance;

    public event Action<EventData> EventStarted;
    public event Action<EventData> EventEnded;

    private void Awake()
    {
        Instance = this;
    }

    public bool HasEvent(EventData eventData)
    {
        return _events.Contains(eventData);
    }

    public void AddEvent(EventData eventData)
    {
        _events.Add(eventData);
        EventStarted?.Invoke(eventData);
    }

    public void RemoveEvent(EventData eventData)
    {
        EventEnded?.Invoke(eventData);
        _events.Remove(eventData);
    }

    private void CleatBus()
    {
        foreach (var eventData in _events)
        {
            EventEnded?.Invoke (eventData);
        }
    }

    private void OnDestroy()
    {
        CleatBus();
    }

}

