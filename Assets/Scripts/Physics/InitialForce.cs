using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for assigning an initial force to an object.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class InitialForce : MonoBehaviour
{
    [Tooltip("Force to assign.")]
    [SerializeField] Vector3 force;

    [Tooltip("Time after enable it will wait before assigning the force.")]
    [SerializeField] float delay = 1f;

    Rigidbody rb;
    float startTime;
    bool done;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    void OnEnable()
    {
        startTime = Time.time;
        done = false;
    }

    void Update()
    {
        if(done)
        {
            return;
        }

        if(Time.time - startTime > delay)
        {
            rb.AddForce(force, ForceMode.Impulse);
            done = true;

            this.enabled = false;
        }
    }
}
