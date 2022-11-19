using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SetVelocity : MonoBehaviour
{
    [SerializeField] Vector3 velocity;
    [SerializeField] float delay = 1f;

    Rigidbody rb;
    float startTime;
    bool done;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    void OnEnable()
    {
        startTime = Time.time;
        done = false;
    }

    void Update()
    {
        if(done)
        {
            return;
        }

        if(Time.time - startTime > delay)
        {
            rb.velocity = velocity;
            done = true;

            this.enabled = false;
        }
    }
}
