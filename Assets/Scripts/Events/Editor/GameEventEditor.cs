using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// Custom editor for GameEvent objects.
/// </summary>
/// 
/// <remarks>
/// Creates a button to invoke the event by the inspector.
/// </remarks>
[CustomEditor(typeof(GameEvent))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameEvent myScript = (GameEvent)target;
        if(GUILayout.Button("Invoke event"))
        {
            myScript.Invoke();
        }
    }
}