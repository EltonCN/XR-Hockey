using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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