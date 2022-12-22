using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class implements a game event listener, a subscriber of a Game Event.
/// </summary>
public class GameEventListener: MonoBehaviour
{
    [Tooltip("Game event to lister.")]
    [SerializeField] GameEvent gameEvent;

    [Tooltip("Actions to do when the Game Event is raised.")]
    [SerializeField] UnityEvent OnGameEvent;

    void OnEnable()
    {
        gameEvent.Register(this);
    }

    void OnDisable()
    {
        gameEvent.Unregister(this);
    }

    /// <summary>
    /// Its called when the Game Event is invoked.
    /// </summary>
    public void OnInvoke()
    {
        if(gameObject.activeInHierarchy)
        {
            OnGameEvent.Invoke();
        }
        
    }

}