using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hitter : MonoBehaviour
{
    [SerializeField] LayerMask hittableLayers;
    [SerializeField] float multiplier = 1f;
    Vector3 previousPosition;
    float lastTime;
    Vector3 velocity;
    Rigidbody rb;

    LayerMask targetsLayers;

    void Start()
    {
        previousPosition = this.transform.position;
        lastTime = Time.time;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        velocity = (this.transform.position - previousPosition) / (Time.time-lastTime);

        lastTime = Time.time;
        previousPosition = this.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            float otherMass = other.attachedRigidbody.mass;
            float mass = rb.mass;
            
            Vector3 otherVelocity = other.attachedRigidbody.velocity;

            Vector3 posVelocity = otherVelocity + ((otherMass/mass)*velocity);
            posVelocity *= multiplier;

            other.attachedRigidbody.velocity = posVelocity;
        }
        
    }
}
