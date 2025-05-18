using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Hero))]
public class HeroUIE : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Hero myScript = (Hero)target;

        if (GUILayout.Button("Get Cols"))
        {
            myScript.heroCols = myScript.GetComponentsInChildren<Collider>();
            EditorUtility.SetDirty(myScript);
        }
    }
}