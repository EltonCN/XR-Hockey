using UnityEngine;

/// <summary>
/// This class is responsible for autonomously controlling the movement of a hitter.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Computer : MonoBehaviour
{
    [SerializeField] GameObjectVariable diskVariable;
    [SerializeField] float velocity = 0.3f;
    Vector3 localRight;
    Rigidbody rb;
    Vector3 offset = new Vector3(0,0,0);
    public float K = 0.2f;

    void Start()
    {
        localRight = transform.forward;
        
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (diskVariable.value != null)
        {
            
            offset.x = K * Mathf.Cos(Time.time);
            offset.y = K * Mathf.Cos(Time.time +  (Mathf.PI/2));
            offset.z = K * Mathf.Cos(Time.time +  Mathf.PI);

            Vector3 this2disk = diskVariable.value.transform.position - this.transform.position;
            this2disk = this2disk + offset;

            float distance = Vector3.Dot(this2disk, localRight);
            var a = new Vector3(0,0,1);
            var b = new Vector3(0,0,-1);

            if(Mathf.Abs(distance) < 0.01f)
            {
                rb.velocity = Vector3.zero;
            }
            else if(distance > 0)
            {
                rb.velocity = velocity*localRight;
            }
            else
            {
                rb.velocity = -velocity*localRight;
            }
        }
    }
}
