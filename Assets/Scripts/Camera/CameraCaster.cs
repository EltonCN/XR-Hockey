using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

/// <summary>
/// This class is responsible for following the position of the camera.
/// </summary>
public class CameraCaster : MonoBehaviour
{
    [Tooltip("Layers that can be hit by the camera.")]
    [SerializeField] LayerMask layermask;

    [Tooltip("Max distance for hit check.")]
    [SerializeField] float maxDistance = 100;

    [Tooltip("Methods to invoke when a hit occur.")]
    [SerializeField] UnityEvent<RaycastHit> onHit;
    
    [Tooltip("Show the caster array for debugging.")]
    [SerializeField] bool showDebugRay = false;
    [SerializeField] LineRenderer debugLineRenderer;

    void Update()
    {
        Vector3 origin = Camera.main.transform.position;
        Vector3 direction = Camera.main.transform.forward;

        if(showDebugRay)
        {
            Debug.DrawRay(origin, 100*direction, Color.green);
        
            if(debugLineRenderer != null)
            {
                debugLineRenderer.positionCount = 0;
                debugLineRenderer.positionCount = 2;
                debugLineRenderer.SetPosition(0, origin);
                debugLineRenderer.SetPosition(1, 100*direction);
            }
        }
        
        
        RaycastHit hit;
        if(Physics.Raycast(origin, direction, out hit, maxDistance, layermask))
        {
            onHit.Invoke(hit);
        }


    }
}