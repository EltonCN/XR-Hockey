using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

/// <summary>
/// This class is responsible for making a game event serializable.
/// </summary>
public class RaiseEvent : MonoBehaviour
{
    [SerializeField] GameEvent onEnableEvent;

    void OnEnable()
    {
        onEnableEvent?.Invoke();
    }
}
