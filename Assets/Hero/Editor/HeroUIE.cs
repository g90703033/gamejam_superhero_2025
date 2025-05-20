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

        if (GUILayout.Button("Get Joints InitData"))
        {
            myScript.joints = myScript.GetComponentsInChildren<Joint>();

            myScript.jointLocalPositions = new Vector3[myScript.joints.Length];
            myScript.jointLocalRotations = new Quaternion[myScript.joints.Length];
            myScript.jointRbs = new Rigidbody[myScript.joints.Length];

            for (int i = 0; i < myScript.joints.Length; i++) 
            {
                myScript.jointLocalPositions[i] = myScript.joints[i].transform.localPosition;
                myScript.jointLocalRotations[i] = myScript.joints[i].transform.localRotation;

                myScript.jointRbs[i] = myScript.joints[i].GetComponent<Rigidbody>();
            }

            EditorUtility.SetDirty(myScript);
        }

        if (GUILayout.Button("Set Size"))
        {
            myScript.AddSizeLevel(myScript.sizeL);

            EditorUtility.SetDirty(myScript);
        }
    }
}