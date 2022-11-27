using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Computer : MonoBehaviour
{
    [SerializeField] GameObjectVariable diskVariable;
    [SerializeField] float velocity = 0.3f;
    Vector3 localRight;
    Rigidbody rb;
    Vector3 offset = new Vector3(0,0,0);
    public float K = 0.2f;

    /*private readonly float  _cumulativeErrorLimit;
    private float  _cumulativeError;
    private float  _lastError;

    [SerializeField] float _proportional = 1;
    [SerializeField] float _integral = 0.01f;
    [SerializeField] float _derivative = 0.1f;
    [SerializeField] float _time_step = 0.1f;
    [SerializeField] float _max_speed = 20;
    [SerializeField] float _spd = 10000;*/

    void Start()
    {
        localRight = transform.forward;
        
        rb = GetComponent<Rigidbody>();
        
    }

    void FixedUpdate()
    {
        if (diskVariable.value != null)
        {
            
            float rand_x = K * Mathf.Cos(Time.time);
            float rand_y = K * Mathf.Cos(Time.time +  (Mathf.PI/2));
            float rand_z = K * Mathf.Cos(Time.time +  Mathf.PI);
            
            //float rand_x = Random.Range(-0.1f, 0.1f);
            //float rand_y = Random.Range(-0.1f, 0.1f);
            //float rand_z = Random.Range(-0.1f, 0.1f);

            /*Vector3 this2camera = diskVariable.value.transform.position - this.transform.position; //This to camera position vector
            Vector3 movement = Vector3.Dot(this2camera, localRight) * localRight; //Previous vector projected to right direction
            Vector3 targetPosition = this.transform.position + movement; //Target position to match the camera in the right direction

             RaycastHit hit;
            if(!rb.SweepTest(movement, out hit, movement.magnitude)) //Check if will not collide in anything
            {
                 //rb.MovePosition(targetPosition); Doesn't work. Wrong coordinate space?
                 transform.position = targetPosition;
            }*/
            offset.x = rand_x;
            offset.y = rand_y;
            offset.z = rand_z;

            Vector3 this2disk = diskVariable.value.transform.position - this.transform.position;
            this2disk = this2disk + offset;
             
            float distance = Vector3.Dot(this2disk, localRight);

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

    /*private float Calculate_velocity(float deltaTime, float target, float current)
    {
        var error = target - current;
        var errorGradient = (error - _lastError) * deltaTime;
        _cumulativeError += error * deltaTime;
        _cumulativeError = Mathf.Clamp(_cumulativeError, -_cumulativeErrorLimit, _cumulativeErrorLimit);
        _lastError = error;

        return Mathf.Clamp((_proportional * error) + (_integral * _cumulativeError) + (_derivative * errorGradient), -_max_speed, _max_speed);
    }*/
}
