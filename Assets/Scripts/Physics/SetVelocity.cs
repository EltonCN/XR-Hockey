using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum to store axis names, for better inspector.
/// </summary>
public enum Axis
{
    X = 0,
    Y = 1,
    Z = 2
}

/// <summary>
/// Stores all the settings to create a random velocity.
/// </summary>
[System.Serializable]
public class RandomVelocitySettings
{
    [Tooltip("If should use a random velocity.")]
    [SerializeField] public bool randomVelocity;

    [Tooltip("The minimum magnitude of the velocity.")]
    [SerializeField] public float randomMagnitudeMin;

    [Tooltip("The maximum magnitude of the velocity.")]
    [SerializeField] public float randomMagnitudeMax;

    [Tooltip("The minimum rotation angle, in degrees")]
    [SerializeField] public float randomAngleMin;

    [Tooltip("The maximum rotation angle, in degrees")]
    [SerializeField] public float randomAngleMax;

    [Tooltip("Around which axis should the velocity be rotated")]
    [SerializeField] public Axis rotationAxis;
}

/// <summary>
/// This class is responsible for assigning a velocity to an object.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class SetVelocity : MonoBehaviour
{
    [Tooltip("The velocity to assign (if not random).")]
    [SerializeField] Vector3 velocity;

    [Tooltip("Time after the enable to wait before assiging the velocity.")]
    [SerializeField] float delay = 1f;

    [Tooltip("Settings for assiging a random velocity.")]
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
