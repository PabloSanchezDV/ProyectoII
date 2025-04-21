using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHolder : MonoBehaviour
{
    public static EventHolder Instance;

    [NonSerialized] public UnityEvent onCameraStateEnter = new UnityEvent();
    [NonSerialized] public UnityEvent onCameraStateExit = new UnityEvent();
    [NonSerialized] public UnityEvent onZoomChange = new UnityEvent();
    [NonSerialized] public UnityEvent onPictureTaken = new UnityEvent();
    [NonSerialized] public UnityEvent onPictureShown = new UnityEvent();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
