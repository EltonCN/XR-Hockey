using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This class is responsible for displaying in the UI (TextMeshProUGUI) the value of a IntVariable object.
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class SyncTextWithIntVariable : MonoBehaviour
{
    [SerializeField] IntVariable variable;
    
    TextMeshProUGUI text;

    int prevValue;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        prevValue = variable.value;

        updateText();
    }

    // Update is called once per frame
    void Update()
    {
        if(prevValue != variable.value)
        {
            updateText();
            prevValue = variable.value;
        }
    }

    void updateText()
    {
        text.text = variable.value.ToString();
        text.SetAllDirty();
    }

    void OnValidate()
    {
        text = GetComponent<TextMeshProUGUI>();
        updateText();
    }
}
