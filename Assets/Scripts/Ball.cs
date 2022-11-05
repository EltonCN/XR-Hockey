using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Transform))]

public class Ball : MonoBehaviour
{

    public float speed = 200;
    
    private Rigidbody _rigidbody;
    private Vector3 collisionNormal;
    private  Transform _transform;
    private  Vector3 origin_position;
    private  Vector3 zero_velocity = new Vector3(0,0,0);
    private  Vector3 previus_velocity;
    private int collision_type;
   

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        origin_position = _transform.position;
         
    }

    void FixedUpdate()
    {
        if(collision_type == 1)
        {
            _rigidbody.velocity =   collisionNormal * speed * Time.fixedDeltaTime;
            collision_type = 0;
        }
        else if(collision_type == 2)
        {
            _rigidbody.velocity = zero_velocity;
            _transform.position =  origin_position;
            collision_type = 0;
        }
        else
        {
             _rigidbody.velocity =  previus_velocity;
        }

      
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Table"  || LayerMask.LayerToName(collision.gameObject.layer) == "Player" || LayerMask.LayerToName(collision.gameObject.layer) == "Hitter" )
        {
            collision_type = 1;
            // Print how many points are colliding with this transform
            Debug.Log("Points colliding: " + collision.contacts.Length);

             // Print the normal of the first point in the collision.
            Debug.Log("Normal of the first point: " + collision.contacts[0].normal);

             // Draw a different colored ray for every normal in the collision
            foreach (var item in collision.contacts)
            {
                Debug.DrawRay(item.point, item.normal * 100, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 10f);
            }

            collisionNormal = collision.contacts[0].normal;
        }
        else if (LayerMask.LayerToName(collision.gameObject.layer) == "Goal")
        {
            collision_type = 2;
            
        }
            
    }


    
    
}
