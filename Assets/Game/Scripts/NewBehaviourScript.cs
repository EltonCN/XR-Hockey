using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 15;
    private Rigidbody _rigidbody;
    private Vector3 collisionNormal;
    private Vector3 contactPoint;
    private Vector3 reflectionPoint;



    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
         _rigidbody.velocity =   reflectionPoint * speed * Time.fixedDeltaTime;
     
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Table"  || LayerMask.LayerToName(collision.gameObject.layer) == "Player" || LayerMask.LayerToName(collision.gameObject.layer) == "Hitter" )
        

             

            collisionNormal = collision.contacts[0].normal;
            contactPoint = collision.contacts[0].point;
            reflectionPoint =  Vector3.Reflect(contactPoint, collisionNormal);

            // Draw a different colored ray for every normal in the collision
            foreach (var item in collision.contacts)
            {
                Debug.DrawRay(item.point, item.point * 100, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 10f);
            }
       
       
            
    }

}
