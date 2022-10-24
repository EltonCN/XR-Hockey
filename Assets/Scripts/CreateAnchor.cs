using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class CreateAnchor : MonoBehaviour
{
    [SerializeField] ARAnchorManager anchorManager;
    [SerializeField] GameObject prefabToAttach;
    [SerializeField] GameObject ghostTarget;

    [SerializeField] InputActionReference createPrefabAction;

    [SerializeField] UnityEvent<GameObject> onCreate;

    bool haveReference;

    // Start is called before the first frame update
    void Start()
    {
        haveReference = false;
        
    }

    void OnEnable()
    {
        createPrefabAction.action.performed += this.CreatePrefab;
    }

    void OnDisable()
    {
        createPrefabAction.action.performed -= this.CreatePrefab;
    }

    public void CreateAnchorOnHitPlane(RaycastHit hit)
    {
        ARPlane plane = hit.transform.gameObject.GetComponent<ARPlane>();
        Vector3 position = hit.point;

        if(plane == null)
        {
            return;
        }
        else
        {
            ghostTarget.transform.position = position;
            haveReference = true;
            
        }

    }

    public void CreatePrefab(InputAction.CallbackContext context)
    {
        if(!haveReference)
        {
            return;
        }

        GameObject go = Instantiate(prefabToAttach, ghostTarget.transform.position, Quaternion.identity);
        go.AddComponent<ARAnchor>();

        onCreate.Invoke(go);

    }
}
