using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis
{
    X = 0,
    Y = 1,
    Z = 2
}

[System.Serializable]
public class RandomVelocitySettings
{
    [SerializeField] public bool randomVelocity;
    [SerializeField] public float randomMagnitudeMin;
    [SerializeField] public float randomMagnitudeMax;
    [SerializeField][Tooltip("Minimum rotation angle, in degrees")] public float randomAngleMin;
    [SerializeField][Tooltip("Maximum rotation angle, in degrees")] public float randomAngleMax;
    [SerializeField] public Axis rotationAxis;
}

[RequireComponent(typeof(Rigidbody))]
public class SetVelocity : MonoBehaviour
{
    [SerializeField] Vector3 velocity;
    [SerializeField] float delay = 1f;
    [SerializeField] RandomVelocitySettings randomVelocitySettings;

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
        
        if(randomVelocitySettings.randomVelocity)
        {
            float magnitude = Random.Range(randomVelocitySettings.randomMagnitudeMin, randomVelocitySettings.randomMagnitudeMax);
            float angle = Random.Range(randomVelocitySettings.randomAngleMin, randomVelocitySettings.randomAngleMax);

            Vector3 rVelocity = new Vector3(magnitude, 0f , 0f);

            Vector3 angles = Vector3.zero;
            angles[(int) randomVelocitySettings.rotationAxis] = angle;

            rVelocity = Quaternion.Euler(angles) * rVelocity;

            this.velocity = rVelocity;
        }
    }

    void Update()
    {
        if(done)
        {
            return;
        }

        if(Time.time - startTime > delay)
        {
            rb.velocity = this.transform.TransformDirection(velocity);
            done = true;

            this.enabled = false;
        }
    }
}
