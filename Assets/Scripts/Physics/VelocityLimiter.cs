using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for assigning a maximum and minimum limit to the speed of an object that has a rigidbody component.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class VelocityLimiter : MonoBehaviour
{
    [Tooltip("The minimum object velocity.")]
    [SerializeField] float minVelocity;

    [Tooltip("The maximum object velocity.")]
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
