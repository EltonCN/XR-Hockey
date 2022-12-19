using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for reflecting the object's direction after a collision.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class CollisionReflect : MonoBehaviour
{
    Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 collisionNormal = collision.contacts[0].normal;

        Vector3 newVelocity =  Vector3.Reflect(_rigidbody.velocity, collisionNormal);

        _rigidbody.velocity = newVelocity;
    }

}
