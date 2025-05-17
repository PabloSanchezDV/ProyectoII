using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsDisabler : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;

    public void DisableArmsOnAnimationEnd()
    {
        _inputHandler.DisableArmsRenderer();
    }
}
