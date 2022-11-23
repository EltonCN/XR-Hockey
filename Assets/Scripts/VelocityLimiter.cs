using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VelocityLimiter : MonoBehaviour
{
    [SerializeField] float minVelocity;
    [SerializeField] float maxVelocity;
    Rigidbody rb;
    
    float minVelocitySQ;
    float maxVelocitySQ;

    bool moving;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    
        minVelocitySQ = minVelocity*minVelocity;
        maxVelocitySQ = maxVelocity*maxVelocity;
        moving = false;
    }

    void FixedUpdate()
    {
        if(!moving && rb.velocity.sqrMagnitude > 0.01f)
        {
            moving = true;
        }

        if(moving)
        {
            if(rb.velocity.sqrMagnitude < minVelocitySQ)
            {
                Vector3 velocity = rb.velocity.normalized;
                velocity *= minVelocity;
                rb.velocity = velocity;
            }
        }

        if(rb.velocity.sqrMagnitude > maxVelocitySQ)
        {
            Vector3 velocity = rb.velocity.normalized;
            velocity *= maxVelocity;
            rb.velocity = velocity;
        }
    }
}
