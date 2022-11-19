using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
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
    GameObject game;

    // Start is called before the first frame update
    void Start()
    {
        haveReference = false;
        
    }

    void OnEnable()
    {
        createPrefabAction.action.performed += this.CreatePrefab;
        GameController.RestartGame += this.Restart;
    }

    void OnDisable()
    {
        createPrefabAction.action.performed -= this.CreatePrefab;
        GameController.RestartGame -= this.Restart;
    }

    public void Restart(object sender, EventArgs e)
    {
        Debug.Log("CreateAnchor restarting...");
        Destroy(game);
        haveReference = false;
    }

    public void CreateAnchorOnHitPlane(RaycastHit hit)
    {
        ARPlane plane = hit.transform.gameObject.GetComponent<ARPlane>();
        Vector3 position = hit.point;

        if(!haveReference)
        {
            ghostTarget.SetActive(true);
        }

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

        GameObject go = Instantiate(prefabToAttach, ghostTarget.transform.position, ghostTarget.transform.rotation);
        go.AddComponent<ARAnchor>();

        onCreate.Invoke(go);
        game = go;
    }
}
