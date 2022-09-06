using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BattleGrid))]
public class BattleGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BattleGrid battleGrid = (BattleGrid)target;

        if (GUILayout.Button("Generate Grid"))
        {
            Debug.Log("Do something");
            battleGrid.GenerateGrid();

        }
    }
}
