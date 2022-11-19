using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameController : MonoBehaviour
{
    public static event EventHandler RestartGame;

    [SerializeField] private GameObject GazePointer;

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

    protected virtual void OnRestartEvent(EventArgs e)
    {
        Debug.Log("OnRestartEvent");
        EventHandler handler = RestartGame;
        handler?.Invoke(this, e);
    }

    public void RestartEvent() {
        OnRestartEvent(EventArgs.Empty);
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
