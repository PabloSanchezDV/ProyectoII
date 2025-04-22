using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using PathCreation;
using UnityEngine;

public class PathModifier : MonoBehaviour
{
    private PathCreator _pathCreator;
    [SerializeField] private float _height;
    [SerializeField] private LayerMask _layerMask;

    public void SnapPathToTerrain()
    {
        _pathCreator = GetComponent<PathCreator>();
        int points = _pathCreator.bezierPath.NumPoints;

        for (int i = 0; i < points; i++)
        {
            Vector3 worldPoint = transform.TransformPoint(_pathCreator.bezierPath.GetPoint(i));

            Debug.DrawRay(worldPoint, Vector3.down * 100, Color.red, 2f);

            if (Physics.Raycast(worldPoint, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity, _layerMask))
            {
                Vector3 snappedWorldPos = hitInfo.point + Vector3.up * _height;
                Vector3 localSnappedPos = transform.InverseTransformPoint(snappedWorldPos);
                _pathCreator.bezierPath.SetPoint(i, localSnappedPos);
            }
            else
            {
                throw new WarningException($"Point {i} has not found terrain under it.");
            }
        }

        _pathCreator.TriggerPathUpdate();

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(_pathCreator);
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(_pathCreator.gameObject.scene);
#endif

        Debug.Log("Path snapped to terrain");
    }

}
