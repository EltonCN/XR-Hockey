using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InitialForce : MonoBehaviour
{
    [SerializeField] Vector3 force;
    [SerializeField] float delay = 1f;

    Rigidbody rb;
    float startTime;
    bool done;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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