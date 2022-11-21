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

    [SerializeField] UnityEvent<GameObject> onCreateBothModes;
    
    [SerializeField] UnityEvent<GameObject> onCreateWithPlane;

    [SerializeField] UnityEvent onRestoreWithPlane;

    [SerializeField] bool withoutPlaneMode;

    bool haveReference;

    GameObject prefabInstance;

    // Start is called before the first frame update
    void Start()
    {
        haveReference = withoutPlaneMode;
    }

    void OnEnable()
    {
        createPrefabAction.action.performed += this.CreatePrefab;
    }

    void OnDisable()
    {
        createPrefabAction.action.performed -= this.CreatePrefab;
    }

    void Update()
    {
        if(haveReference)
            ghostTarget.SetActive(true);
        else
            ghostTarget.SetActive(false);
    }

    public void CreateAnchorOnHitPlane(RaycastHit hit)
    {
        if(withoutPlaneMode)
        {
            return;
        }

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
        if((!haveReference && !withoutPlaneMode) || this.enabled == false)
        {
            return;
        }

        prefabInstance = Instantiate(prefabToAttach, ghostTarget.transform.position, ghostTarget.transform.rotation);
        prefabInstance.AddComponent<ARAnchor>();

        if(!withoutPlaneMode)
            onCreateWithPlane.Invoke(prefabInstance);

        onCreateBothModes.Invoke(prefabInstance);
    }

    public void RestorePositionSelection()
    {
        Destroy(prefabInstance);

        haveReference = withoutPlaneMode;
        ghostTarget.SetActive(haveReference);

        if(!withoutPlaneMode)
            onRestoreWithPlane.Invoke();
    }
}
