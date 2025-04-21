using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPictureEndNotifier : MonoBehaviour
{
    public void InvokeOnPictureShownMethod()
    {
        EventHolder.Instance.onPictureShown?.Invoke();
    }
}
