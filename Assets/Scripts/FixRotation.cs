using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FixRotation : MonoBehaviour
{
    [SerializeField] bool[] axis = {false, false, false};

    Vector3 originalAngles;

    // Start is called before the first frame update
    void Start()
    {
        originalAngles = transform.rotation.eulerAngles;
    }

    void FixedUpdate()
    {
        Vector3 angles = transform.rotation.eulerAngles;
        
        for(int i = 0; i<3; i++)
        {
            if(axis[i])
            {
                angles[i] = originalAngles[i];
            }
        }

        transform.rotation = Quaternion.Euler(angles);
    }

}
