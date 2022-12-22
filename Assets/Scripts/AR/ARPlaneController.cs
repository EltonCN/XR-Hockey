using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// This class is responsible for controlling the AR plane manager, disabling and enabling plane detection.
/// </summary>
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

    /// <summary>
    /// Removes the prefab from the Plane Manager
    /// </summary>
    public void RemoveManagerPrefab()
    {
        prefab = planeManager.planePrefab;
        planeManager.planePrefab = null;
    }

    /// <summary>
    /// Restaures the original prefab of the Plane Manager
    /// </summary>
    public void RestaureManagerPrefab()
    {
        planeManager.planePrefab = prefab;
    }

    /// <summary>
    /// Disable all previous detected planes game objects
    /// </summary>
    public void DisableAllPlaneObjects()
    {
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Enable all previou detected planes game objects
    /// </summary>
    public void EnableAllPlaneObjects()
    {
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Reset the plane detection. DOES NOT WORK.
    /// </summary>
    public void ResetPlaneDetection()
    {
        session.Reset();

        foreach (var plane in planeManager.trackables)
        {
            Destroy(plane.gameObject);
        }
    }
}
