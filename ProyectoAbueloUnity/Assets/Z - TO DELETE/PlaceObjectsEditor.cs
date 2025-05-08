#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(PlaceObjects))]
public class PlaceObjectsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Dibuja el inspector por defecto
        DrawDefaultInspector();

        // Agrega un botón
        PlaceObjects placeObjects = (PlaceObjects)target;
        if (GUILayout.Button("Place objects"))
        {
            placeObjects.Place();
        }
    }
}
#endif
