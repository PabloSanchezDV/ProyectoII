using System;
using System.Drawing;
using UnityEngine;

[CreateAssetMenu(menuName = "PhotographableObjects/BasicPhotographable")]
public class CameraTarget : ScriptableObject
{
    [SerializeField] protected Target _target;
    [SerializeField] protected LayerMask _mask;
    [SerializeField] protected float _raycastDistance;

    public bool DoesRayHit(Camera camera, Transform[] checkPoints, Transform targetTransform)
    {
        if(checkPoints.Length == 0)
            throw new Exception("The Camera Target " + targetTransform.name + " doesn't have any checkpoints.");

        foreach (Transform checkpoint in checkPoints)
        {
            if(!CheckPointIsInsideFrustrum(checkpoint, camera))
                continue;

            Vector3 direction = checkpoint.transform.position - camera.transform.position;
            if (Physics.Raycast(camera.transform.position, direction, out RaycastHit hit, _raycastDistance, _mask))
            {

                if (hit.transform.gameObject.Equals(targetTransform.gameObject))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool CheckPointIsInsideFrustrum(Transform checkPoint, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(checkPoint.position) < 0)
                return false;
        }
        return true;
    }

    public virtual Target GetTarget()
    {
        return _target;
    }
}
