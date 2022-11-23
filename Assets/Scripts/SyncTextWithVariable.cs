using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SyncTextWithVariable : MonoBehaviour
{
    [SerializeField] FloatVariable variable;
    
    TextMeshProUGUI text;

    float prevValue;

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
        text.text = ((int) variable.value).ToString();
        text.SetAllDirty();
    }
}