using UnityEngine;

/// <summary>
/// Scriptable object that stores values.
/// </summary>
public class ScriptableVariable<T> : ScriptableObject
{
    public T value;
}