using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] bool ignoreXAxis = false;
    [SerializeField] bool ignoreYAxis = false;
    [SerializeField] bool ignoreZAxis = false;

    Vector3 originalAngles;

    // Start is called before the first frame update
    void Start()
    {
        originalAngles = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraAngles = Camera.main.transform.rotation.eulerAngles;

        if(ignoreXAxis)
        {
            cameraAngles.x = originalAngles.x;
        }
        if(ignoreYAxis)
        {
            cameraAngles.y = originalAngles.y;
        }
        if(ignoreZAxis)
        {
            cameraAngles.z = originalAngles.z;
        }

        transform.rotation = Quaternion.Euler(cameraAngles*Mathf.Rad2Deg);
    }
}
