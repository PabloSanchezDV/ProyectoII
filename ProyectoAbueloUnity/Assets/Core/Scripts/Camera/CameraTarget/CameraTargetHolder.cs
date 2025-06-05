using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetHolder : MonoBehaviour, IPhotographable
{
    [SerializeField] private CameraTarget cameraTarget;
    [SerializeField] private Transform[] _checkPoints;

    public Transform[] CheckPoints { get { return _checkPoints; } }    
    
    public CameraTarget GetCameraTarget() { return cameraTarget; }
}
