using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Events;

/// <summary>
/// This class is reponsible for creating prefabs copies with anchors.
/// </summary>
/// 
/// <remarks>
/// It uses a ghost target position to create the prefab, allowing to change the
/// prefab creation position and visualize it before creation.
/// </remarks>
/// 
/// TODO: Change the class name to something closer to use. "AnchoredPrefabManager"? 
public class CreateAnchor : MonoBehaviour
{
    [Tooltip("The active AR Anchor Manager.")]
    [SerializeField] ARAnchorManager anchorManager;

    [Tooltip("The prefab to create and attach the anchor.")]
    [SerializeField] GameObject prefabToAttach;

    [Tooltip("The visible target when the anchor is not created.")]
    [SerializeField] GameObject ghostTarget;

    [Tooltip("The action that will create the prefab.")]
    [SerializeField] InputActionReference createPrefabAction;

    [Tooltip("Actions to do when the prefab is created with or without plane.")]
    [SerializeField] UnityEvent<GameObject> onCreateBothModes;
    
    [Tooltip("Actions to do only when the prefab is created with a plane reference.")]
    [SerializeField] UnityEvent<GameObject> onCreateWithPlane;

    [Tooltip("Actions to do when the position selection is restoured and uses a plane reference.")]
    [SerializeField] UnityEvent onRestoreWithPlane;

    [Tooltip("If should create the anchor without a plane reference.")]
    [SerializeField] bool withoutPlaneMode;

    [Tooltip("GameObjectVariable to store the created Game Object reference.")]
    [SerializeField] GameObjectVariable tableVariable;

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

    /// <summary>
    /// Changes the ghost target position to the hit intersection with the plane, if have one. 
    /// </summary>
    /// <param name="hit">The raycast hit.</param>
    /// 
    /// TODO: Change name to something closer to use. Ex: "ChangeGhostPositionToPlaneHit".
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

    /// <summary>
    /// Creates the Game Object copying the prefab and using the ghost transform.
    /// </summary>
    /// <param name="context">The action context. Not used.</param>
    public void CreatePrefab(InputAction.CallbackContext context)
    {
        if((!haveReference && !withoutPlaneMode) || this.enabled == false)
        {
            return;
        }

        prefabInstance = Instantiate(prefabToAttach, ghostTarget.transform.position, ghostTarget.transform.rotation);
        prefabInstance.AddComponent<ARAnchor>();

        tableVariable.value = prefabInstance;

        if(!withoutPlaneMode)
            onCreateWithPlane.Invoke(prefabInstance);

        onCreateBothModes.Invoke(prefabInstance);
    }

    /// <summary>
    /// Destroys the previously created object and reactivates the ghost target.
    /// </summary>
    public void RestorePositionSelection()
    {
        Destroy(prefabInstance);

        haveReference = withoutPlaneMode;
        ghostTarget.SetActive(haveReference);

        if(!withoutPlaneMode)
            onRestoreWithPlane.Invoke();
    }
}
