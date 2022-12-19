using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class implements a serializable game event listener.
/// </summary>
public class GameEventListener: MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    [SerializeField] UnityEvent OnGameEvent;

    void OnEnable()
    {
        gameEvent.Register(this);
    }

    void OnDisable()
    {
        gameEvent.Unregister(this);
    }

    public void OnInvoke()
    {
        if(gameObject.activeInHierarchy)
        {
            OnGameEvent.Invoke();
        }
        
    }

}