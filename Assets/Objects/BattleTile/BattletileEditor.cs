using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BattleGrid))]
public class BattletileEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        /*Battlegrid battleTile = (Battletile)target;
        GUIContent tileStates = new GUIContent("Tile States");
        battleTile.tile*/
    }
}
