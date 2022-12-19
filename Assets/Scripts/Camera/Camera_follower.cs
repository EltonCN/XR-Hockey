using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for following the position of the camera.
/// </summary>
public class Camera_follower : MonoBehaviour
{
    public Transform coordinates_to_be_followed;
    public Vector3 offset;
    
    void Start()
    {
     transform.position = coordinates_to_be_followed.position + offset;   
    }

    void Update()
    {
        Vector3 newposition =  coordinates_to_be_followed.position + offset;
        transform.position = newposition;
        //transform.rotation = coordinates_to_be_followed.rotation;
    }
}
