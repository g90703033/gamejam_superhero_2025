using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Victim))]
public class VictimUIE : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Victim myScript = (Victim)target;

        if(GUILayout.Button("UpdateTag"))
        {
            myScript.UpdateTag();
        }
    }
}