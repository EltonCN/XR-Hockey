using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class ARPlaneController : MonoBehaviour
{
    ARPlaneManager planeManager;
    ARSession session;
    GameObject prefab;

    void Start()
    {
        planeManager = GetComponent<ARPlaneManager>();
        session = FindObjectOfType<ARSession>();
    }


    public void RemoveManagerPrefab()
    {
        prefab = planeManager.planePrefab;
        planeManager.planePrefab = null;
    }

    public void RestaureManagerPrefab()
    {
        planeManager.planePrefab = prefab;
    }

    public void DisableAllPlaneObjects()
    {
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }

    public void EnableAllPlaneObjects()
    {
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(true);
        }
    }

    public void ResetPlaneDetection()
    {
        session.Reset();

        foreach (var plane in planeManager.trackables)
        {
            Destroy(plane.gameObject);
        }
    }
}
