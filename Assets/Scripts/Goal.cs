using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] IntVariable poinstsVariable;
    [SerializeField] int points;
    [SerializeField] GameEvent onGoal;

    private void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Disk")
        {
            if (poinstsVariable != null)
                poinstsVariable.value += points;
            onGoal.Invoke();
        }
    }
}
