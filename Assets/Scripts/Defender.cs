using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Defender : MonoBehaviour
{
    new Camera camera;
    Vector3 localRight;
    Rigidbody rb;

    void Start()
    {
        localRight = transform.forward;

        print(localRight);
        camera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 this2camera = camera.transform.position - this.transform.position; //This to camera position vector
        Vector3 movement = Vector3.Dot(this2camera, localRight) * localRight; //Previous vector projected to right direction

        print(this2camera.ToString()+ " | " + movement.ToString());

        Vector3 targetPosition = this.transform.position + movement; //Target position to match the camera in the right direction

        Ray ray = new Ray(transform.position, movement);
        RaycastHit hit;

       if(!rb.SweepTest(movement, out hit, movement.magnitude)) //Check if will not collide in anything
        {
            rb.MovePosition(targetPosition);
        }
        
    }
}
