using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetHolder : MonoBehaviour, IPhotographable
{
    [SerializeField] private CameraTarget cameraTarget;

    private void Start()
    {
        cameraTarget.InitializeCameraTarget(transform);
    }

    public CameraTarget GetCameraTarget() { return cameraTarget; }
}
