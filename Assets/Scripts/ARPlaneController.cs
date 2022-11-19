using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class ARPlaneController : MonoBehaviour
{
    ARPlaneManager planeManager;
    GameObject prefab;

    void Start()
    {
        planeManager = GetComponent<ARPlaneManager>();
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
}
