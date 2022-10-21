using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
