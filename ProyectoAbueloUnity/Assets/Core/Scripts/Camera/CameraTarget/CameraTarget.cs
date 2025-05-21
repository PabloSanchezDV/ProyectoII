using System;
using UnityEngine;

[CreateAssetMenu(menuName = "PhotographableObjects/BasicPhotographable")]
public class CameraTarget : ScriptableObject
{
    [SerializeField] protected Target _target;
    [NonSerialized] public Transform[] checkPoints;
    protected Transform targetTransform;

    public void InitializeCameraTarget(Transform transform, Transform[] checkPoints)
    {
        targetTransform = transform;
        this.checkPoints = checkPoints;
    }

    public bool DoesRayHit(Camera camera)
    {
        foreach (Transform checkpoint in checkPoints)
        {
            Vector3 direction = checkpoint.transform.position - camera.transform.position;
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

        throw new Exception("The Camera Target " + targetTransform.name + " doesn't have any checkpoints.");
    }

    public virtual Target GetTarget()
    {
        return _target;
    }
}
