using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BattleTile))]
public class BattleTileEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
