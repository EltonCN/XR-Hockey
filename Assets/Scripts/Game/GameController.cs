using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.InputSystem;
using UnityEngine.Events;

/// <summary>
/// This class is responsible for controlling general game settings, such as menu visibility and resetting the table position.
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject GazePointer;

    [SerializeField] private GameObject floatingMenu;

    [SerializeField] private UnityEvent onRestartPositionSelection;

    [SerializeField] private UnityEvent onRestartGame;

    [SerializeField] private InputActionReference changeFloatingMenu;

    [SerializeField] private GameObjectVariable tableVariable;

    protected virtual bool ResetSessionOriginOnStart => true;

    private bool _isSessionOriginMoved = false;
    private Transform _camera;

    void Start() {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        _camera = Camera.main.transform;

        GazePointer.SetActive(true);
    }

    void Update() {
        if (ResetSessionOriginOnStart && !_isSessionOriginMoved && _camera.position != Vector3.zero) {
            OffsetSessionOrigin();
            _isSessionOriginMoved = true;
        }
    }

    void OnEnable()
    {
        changeFloatingMenu.action.performed += this.ChangeFloatingMenuVisibility;
    }

    void OnDisable()
    {
        changeFloatingMenu.action.performed -= this.ChangeFloatingMenuVisibility;
    }

    private void ChangeFloatingMenuVisibility(InputAction.CallbackContext context)
    {
        if(tableVariable.value != null)
        {
            floatingMenu.SetActive(!floatingMenu.activeSelf);
        }
    }

    public void RestartPositionSelection()
    {
        onRestartPositionSelection.Invoke();
    }

    /* Reset score and disk */
    public void RestartGame() {
        onRestartGame.Invoke();
    }

    public void Quit() {
        Application.Quit();
    }

    protected void OffsetSessionOrigin() {
        ARSessionOrigin sessionOrigin = FindObjectOfType<ARSessionOrigin>();
        sessionOrigin.transform.Rotate(0.0f, -_camera.rotation.eulerAngles.y, 0.0f, Space.World);
        sessionOrigin.transform.position = -_camera.position;
    }

}
