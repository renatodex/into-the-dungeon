using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Battlegrid))]
public class BattlegridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Battlegrid battleGrid = (Battlegrid)target;

        if (GUILayout.Button("Build Object"))
        {
            Debug.Log("Do something");
            battleGrid.GenerateGrid();

        }
    }
}
