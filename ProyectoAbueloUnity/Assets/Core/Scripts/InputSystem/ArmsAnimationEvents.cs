using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsAnimationEvents : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;

    public void DisableArmsOnAnimationEnd()
    {
        _inputHandler.HideArmsNotebook();
    }

    public void ShowCameraUI()
    {
        UIManager.Instance.PlayCameraModeEnterTransition();
    }

    public void HideArms()
    {
        _inputHandler.HideArmsCameraMode();
    }

    public void EnableFreeMoveInputs()
    {
        _inputHandler.EnableFreeMoveInputs();
    }

    public void EnableCameraModeInputs()
    {
        _inputHandler.EnableCameraModeInputs();
    }
}
