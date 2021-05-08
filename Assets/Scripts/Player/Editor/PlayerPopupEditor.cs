using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerTextContainer))]
public class PlayerPopupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var script = (PlayerTextContainer)target;
        if (GUILayout.Button("Test pop up"))
            script.Popup("Test", "Popup");
    }
}
