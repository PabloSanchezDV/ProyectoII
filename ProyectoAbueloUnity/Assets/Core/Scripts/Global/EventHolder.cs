using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHolder : MonoBehaviour
{
    public static EventHolder Instance;

    [NonSerialized] public UnityEvent onZoomChange = new UnityEvent();
    [NonSerialized] public UnityEvent onPictureTaken = new UnityEvent();
    [NonSerialized] public UnityEvent onPictureShown = new UnityEvent();
    [NonSerialized] public UnityEvent onPageTurned = new UnityEvent();
    [NonSerialized] public UnityEvent onScreenshotTaken = new UnityEvent();
    [NonSerialized] public UnityEvent onPhotoObjectsDetected = new UnityEvent();
    [NonSerialized] public UnityEvent onMammothHeadbutt = new UnityEvent();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
