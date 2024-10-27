using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCameraDetector : MonoBehaviour
{
    private Transform[] _checkPoints;
    public Transform[] CheckPoints {  set { _checkPoints = value; } }

    private Camera _camera;
    private Plane[] _cameraFrustrum;
    private Collider _collider;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _collider = GetComponent<Collider>();
    }

    /// <summary>
    /// Returns true if the animal is on camera and any checkpoint is on camera view.

    public bool IsOnCamera()
    {
        var bounds = _collider.bounds;
        _cameraFrustrum = GeometryUtility.CalculateFrustumPlanes(_camera);
        if (GeometryUtility.TestPlanesAABB(_cameraFrustrum, bounds))
        {
            return DoesRayHit();
        }
        else
        {
            return false;
        }
    }

    private bool DoesRayHit()
    {
        foreach(Transform checkpoint in _checkPoints)
        {
            Vector3 direction = checkpoint.transform.position - _camera.transform.position;
            if(Physics.Raycast(_camera.transform.position, direction, out RaycastHit hit, Mathf.Infinity))
            {

                if(hit.transform.gameObject.Equals(gameObject))
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

        throw new Exception("The animal " + transform.name + " doesn't have any checkpoints.");
    }
}
