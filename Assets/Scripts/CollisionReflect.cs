using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        /*if (LayerMask.LayerToName(collision.gameObject.layer) == "Table"  || LayerMask.LayerToName(collision.gameObject.layer) == "Player" || LayerMask.LayerToName(collision.gameObject.layer) == "Hitter" )
        {*/
            Vector3 collisionNormal = collision.contacts[0].normal;

            Vector3 newVelocity =  Vector3.Reflect(_rigidbody.velocity, collisionNormal);

            _rigidbody.velocity = newVelocity;
       
        //}
    }

}