using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

/// <summary>
/// This class raises a Game Event when enabled.
/// </summary>
public class RaiseEvent : MonoBehaviour
{
    [Tooltip("Game Event to raise when enabled.")]
    [SerializeField] GameEvent onEnableEvent;

    void OnEnable()
    {
        onEnableEvent?.Invoke();
    }
}
