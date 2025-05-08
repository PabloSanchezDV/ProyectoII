using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetsDetector : MonoBehaviour
{
    public List<CameraTarget> cameraTargetsList;

    private void Start()
    {
        cameraTargetsList = new List<CameraTarget>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IPhotographable photographable = other.GetComponent<IPhotographable>();
        if (photographable != null)
            cameraTargetsList.Add(photographable.GetCameraTarget());
    }
}
