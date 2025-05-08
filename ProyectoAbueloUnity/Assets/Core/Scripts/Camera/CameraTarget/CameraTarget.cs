using System;
using UnityEngine;

[CreateAssetMenu(menuName = "PhotographableObjects/BasicPhotographable")]
public class CameraTarget : ScriptableObject
{
    public string targetName;
    protected Transform targetTransform;

    public void InitializeCameraTarget(Transform transform)
    {
        targetTransform = transform;
    }

    public virtual bool DoesRayHit(Camera camera)
    {
        Vector3 direction = targetTransform.position - camera.transform.position;
        if (Physics.Raycast(camera.transform.position, direction, out RaycastHit hit, Mathf.Infinity))
        {

            if (hit.transform.gameObject.Equals(targetTransform.gameObject))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
