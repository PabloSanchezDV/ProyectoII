#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RandomizeObjects))]
public class RandomizeObjectsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Dibuja el inspector por defecto
        DrawDefaultInspector();

        // Agrega un bot�n
        RandomizeObjects editTrees = (RandomizeObjects)target;
        if (GUILayout.Button("Randomize objects"))
        {
            editTrees.EditTransforms();
        }
    }
}
#endif