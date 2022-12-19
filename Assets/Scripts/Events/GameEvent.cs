using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls a list of game event listeners from Unity.
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/GameEvent")]
public class GameEvent : ScriptableObject
{
    List<GameEventListener> listeners = new List<GameEventListener>();

    public void Invoke()
    {
        foreach(GameEventListener listener in listeners)
        {
            listener.OnInvoke();
        }
    }

    public void Register(GameEventListener listener)
    {
        if(!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void Unregister(GameEventListener listener)
    {
        if(listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}