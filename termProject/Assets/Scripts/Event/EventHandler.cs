﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EventHandler : MonoBehaviour
{
    public delegate void EventDelegate();
    public Dictionary<string, List<EventDelegate>> events;

    private static EventHandler _instance;
    public string testField;

    public static EventHandler Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;

        
        events = new Dictionary<string, List<EventDelegate>>();
    }

    public void Subscribe(string eventName, EventDelegate handler)
    {
       
        if (events.ContainsKey(eventName))
        {
            List<EventDelegate> dels = events[eventName];
            if (dels == null) events[eventName] = new List<EventDelegate>();
            dels.Add(handler);
        }
        else
        {
            events.Add(eventName, new List<EventDelegate>());
            events[eventName].Add(handler);
        }
    }
    
    public void Emit(string eventName)
    {
        List<EventDelegate> dels = events[eventName];

        foreach(EventDelegate del in dels)
        {
            del();
        }
    }
}
