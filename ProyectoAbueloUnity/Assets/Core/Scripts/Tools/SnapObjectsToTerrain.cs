#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using System.ComponentModel;
using UnityEngine;

public class SnapObjectsToTerrain : MonoBehaviour
{
    [SerializeField] private Transform[] transforms;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _height;

    public void SnapObjects()
    {
        foreach (Transform t in transforms)
        {
            if (Physics.Raycast(t.position, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity, _layerMask))
            {
                Vector3 snappedWorldPos = hitInfo.point + Vector3.up * _height;
                t.position = snappedWorldPos;
            }
            else
            {
                throw new WarningException($"Object {t} has not found terrain under it.");
            }
        
            #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(t);
            #endif
        }

        #if UNITY_EDITOR
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(this.gameObject.scene);
        #endif
    }
}
#endif