using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathModifier))]
public class PathModifierEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Dibuja el inspector por defecto
        DrawDefaultInspector();

        // Agrega un bot�n
        PathModifier pathModifier = (PathModifier)target;
        if (GUILayout.Button("Snap Path to Terrain"))
        {
            pathModifier.SnapPathToTerrain();
        }
    }
}
