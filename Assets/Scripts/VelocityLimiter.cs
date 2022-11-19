using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VelocityLimiter : MonoBehaviour
{
    [SerializeField] float maxVelocity;
    Rigidbody rb;
   
    float maxVelocitySQ;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 

        maxVelocitySQ = maxVelocity*maxVelocity;
    }

    void FixedUpdate()
    {
        if(rb.velocity.sqrMagnitude > maxVelocitySQ)
        {
            Vector3 velocity = rb.velocity.normalized;
            velocity *= maxVelocity;
            rb.velocity = velocity;
        }
    }
}
