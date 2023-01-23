using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EditorTools;
using UnityEditor;

[EditorTool("Cistom Snap Move", typeof(GameObject))]

public class CustomSnappingTool : EditorTool
{
    [SerializeField] private Texture2D icon;
    public override GUIContent toolbarIcon
    {
        get
        {
            return new GUIContent
            {
                image = icon,
                text = "Custom Snap Move Tool",
                tooltip = ""
            };
        }
    }

    public override void OnToolGUI(EditorWindow window)
    {
        Transform targetTransform = ((GameObject)target).transform;

        if (EditorGUI.EndChangeCheck()) 
        {
            Undo.RecordObject(targetTransform, "Moved object");
        }
    }

}
