using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVariable<VariableType, ValueType> : MonoBehaviour
where VariableType : ScriptableVariable<ValueType>
{
   [SerializeField] VariableType variable;
   [SerializeField] ValueType onEnableValue;

   void OnEnable()
   {
      variable.value = onEnableValue;
   }

}
