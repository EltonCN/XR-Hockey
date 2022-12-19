using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for detecting goals, analyzing collisions with the disk, incrementing points and invoking the goal event.
/// For it to work, the component must be inserted in the object that represents the goal.
/// </summary>
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
