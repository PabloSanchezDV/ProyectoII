using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetHolder : MonoBehaviour, IPhotographable
{
    [SerializeField] private CameraTarget cameraTarget;
    [SerializeField] private Transform[] _checkPoints;

    private void Start()
    {
        cameraTarget.InitializeCameraTarget(transform, _checkPoints);
    }

    public CameraTarget GetCameraTarget() { return cameraTarget; }
}
