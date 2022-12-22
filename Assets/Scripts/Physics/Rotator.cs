using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

/// <summary>
/// This class is responsible for rotating the object using an action.
/// </summary>
public class Rotator : MonoBehaviour
{
    [Tooltip("Input action to rotate target. Must be a Vector2 between [-1, -1] and [1, 1].")]
    [SerializeField] InputActionReference rotateAction;

    Vector3 originalAngles;

    void Start()
    {
        originalAngles = transform.rotation.eulerAngles;
    }


    void OnEnable()
    {
        rotateAction.action.performed += this.ReceiveRotateAction;
    }

    void OnDisable()
    {
        rotateAction.action.performed -= this.ReceiveRotateAction;
    }

    /// <summary>
    /// Receives the action and rotates the game object.
    /// </summary>
    /// <param name="context">The CallbackContext of the action. Must have a Vector2 value.</param>
    public void ReceiveRotateAction(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        float angle = Mathf.Atan2(input.y, input.x);

        Vector3 angles = new Vector3(originalAngles.x, angle, originalAngles.z);
        angles *= Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(angles);
    }

}
