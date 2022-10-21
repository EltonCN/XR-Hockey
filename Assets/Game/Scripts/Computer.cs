using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Computer : MonoBehaviour
{
    public float speed = 20;

    private int direction = 1;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(direction, 0, 0) * speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Table")
            direction = -(direction);
    }
}
