using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SnapObjectsToTerrain))]
public class SnapObjectsToTerrainEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Dibuja el inspector por defecto
        DrawDefaultInspector();

        // Agrega un botón
        SnapObjectsToTerrain snapObjects = (SnapObjectsToTerrain)target;
        if (GUILayout.Button("Snap objects"))
        {
            snapObjects.SnapObjects();
        }
    }
}
