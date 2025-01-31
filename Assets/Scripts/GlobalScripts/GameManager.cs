using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int special_load = 0;
    private bool GameStarted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddSpecialPoints(int value)
    {
        special_load += value;
    }

    public int ReturnSpecialData()
    {
        return special_load;
    }
    
    public delegate void EventHandler(object data);
    private Dictionary<string, EventHandler> events = new Dictionary<string, EventHandler>();
    public void RegisterEvent(string eventName, EventHandler handler)
    {
        if (!events.ContainsKey(eventName))
        {
            events[eventName] = handler;
        }
        else
        {
            events[eventName] += handler;
        }
    }

    public void UnregisterEvent(string eventName, EventHandler handler)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName] -= handler;
        }
    }

    public void TriggerEvent(string eventName, object data = null)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName]?.Invoke(data);
        }
    }

    public bool CanPlay()
    {
        return GameStarted;
    }

    public void ChangeState(bool value)
    {
        GameStarted = value;
    }
}

