using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follower : MonoBehaviour
{
    public Transform coordinates_to_be_followed;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
     transform.position = coordinates_to_be_followed.position + offset;   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newposition =  coordinates_to_be_followed.position + offset;
        transform.position = newposition;
        //transform.rotation = coordinates_to_be_followed.rotation;
    }
}
