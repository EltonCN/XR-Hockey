using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

/// <summary>
/// This class is responsible for changing the position on the y axis when an input action is triggered.
/// </summary>
public class TranslateY : MonoBehaviour
{
    [Tooltip("The action that sets the y position. Should have a Vector2 value, the magnitude is the y value.")]
    [SerializeField] InputActionReference rotateAction;

    float originalY;

    void Start()
    {
        originalY = transform.position.y;
    }


    void OnEnable()
    {
        rotateAction.action.performed += this.ReceiveTranslateAction;
    }

    void OnDisable()
    {
        rotateAction.action.performed -= this.ReceiveTranslateAction;
    }

    /// <summary>
    /// Assigns a y position proportional to the InputAction magnitude value.
    /// </summary>
    /// <param name="context">The input action CallbackContext. Must have a Vector2 value.</param>
    public void ReceiveTranslateAction(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        float magnitude = Mathf.Clamp(input.magnitude, 0, 1);
        magnitude = Mathf.Lerp(-1, 1, magnitude);

        float y = originalY + magnitude;

        Vector3 position = new Vector3(transform.position.x, y, transform.position.z);
        transform.position = position;
    }

}
