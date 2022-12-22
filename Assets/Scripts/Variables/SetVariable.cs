using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets an ScriptableVariable value on the game object enable.
/// </summary>
/// <typeparam name="VariableType">The specific ScriptableVariable type. Should use the same value type as the ValueType.</typeparam>
/// <typeparam name="ValueType">The value type of the ScriptableVariable.</typeparam>
public class SetVariable<VariableType, ValueType> : MonoBehaviour
where VariableType : ScriptableVariable<ValueType>
{
   [Tooltip("Variable to set the value.")]
   [SerializeField] VariableType variable;

   [Tooltip("Value to be set.")]
   [SerializeField] ValueType onEnableValue;

   void OnEnable()
   {
      variable.value = onEnableValue;
   }

}
