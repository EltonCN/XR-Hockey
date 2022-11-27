using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class RaiseEvent : MonoBehaviour
{
    [SerializeField] GameEvent onEnableEvent;

    void OnEnable()
    {
        onEnableEvent?.Invoke();
    }

 

}
