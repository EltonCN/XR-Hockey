using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents a game event that can be called and have listeners.
/// </summary>
/// 
/// <remarks>
/// It implements the Observer pattern. Use with the GameEventListener, 
/// that implements the subscriber behaviour.
/// </remarks>
[CreateAssetMenu(menuName = "ScriptableObjects/GameEvent")]
public class GameEvent : ScriptableObject
{
    List<GameEventListener> listeners = new List<GameEventListener>();

    /// <summary>
    /// Invoke the event, notifying the listeners.
    /// </summary>
    public void Invoke()
    {
        foreach(GameEventListener listener in listeners)
        {
            listener.OnInvoke();
        }
    }

    /// <summary>
    /// Register a listener (subscriber) of the event.
    /// </summary>
    /// <param name="listener">The listener to register.</param>
    public void Register(GameEventListener listener)
    {
        if(!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    /// <summary>
    /// Unregister a listener of the event.
    /// </summary>
    /// <param name="listener">The listener to unregister.</param>
    public void Unregister(GameEventListener listener)
    {
        if(listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}