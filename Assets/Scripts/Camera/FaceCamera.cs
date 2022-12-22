using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the Game Object always face the camera.
/// </summary>
public class FaceCamera : MonoBehaviour
{
    [Tooltip("If should ignore the X axis when rotating the object.")]
    [SerializeField] bool ignoreXAxis = false;

    [Tooltip("If should ignore the Y axis when rotating the object.")]
    [SerializeField] bool ignoreYAxis = false;

    [Tooltip("If should ignore the Z axis when rotating the object.")]
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
