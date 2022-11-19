using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TranslateY : MonoBehaviour
{
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
