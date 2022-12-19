using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for ensuring that an object's collider is always inactive.
/// </summary>
[RequireComponent(typeof(Collider))]
public class ForceNotTrigger : MonoBehaviour
{
    Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
    }
    
    // Update is called once per frame
    void Update()
    {
        collider.isTrigger = false;
    }
}
