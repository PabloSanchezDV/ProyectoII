using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

public class CameraMode : Gameplay
{
    InputAction _zoom;
    private Camera _mainCamera;
    private PostProcessVolume _postProcessVolume;
    private DepthOfField _depthOfField;
    private CameraSetting _currentSetting;
    private bool _isCameraInitialized = false;
    private bool _isTakingPicture = false;

    public CameraMode(FSMTemplateMachine fsm, InputActions inputActions) : base(fsm, inputActions) { }

    public override void Enter()
    {
        base.Enter();

        _zoom = _inputActions.CameraMode.Zoom;
        _inputActions.CameraMode.TakePicture.started += TakePicture;
        _inputActions.CameraMode.ChangeSetting.started += ChangeSetting;
        _inputActions.CameraMode.ResetCamera.started += ResetCamera;

        _inputActions.CameraMode.Enable();

        if(!_isCameraInitialized)
            InitializeCamera();

        _currentSetting = CameraSetting.FocusDistance;
        timeBetweenSteps = ((InputHandler)_fsm).timeBetweenSteps / 2;

        AudioManager.Instance.PlayCameraOnSound(_camera.gameObject);
        EventHolder.Instance.onCameraStateEnter?.Invoke();
        EventHolder.Instance.onPictureShown.AddListener(ResetIsTakingPicture);
    }

    public override void UpdateLogic()
    {
        if(_isTakingPicture)
            return;

        base.UpdateLogic();
        if (isCameraToggled)
        {
            isCameraToggled = false;
            _fsm.ChangeState(((InputHandler)_fsm).freeMove);
        }
    }

    public override void UpdatePhysics()
    {
        if (_isTakingPicture)
            return;

        Move();
        LookAround();
        Zoom();

        if (_inputActions.CameraMode.ChangeSettingUp.IsPressed())
            SetSettingUp();
        if (_inputActions.CameraMode.ChangeSettingDown.IsPressed())
            SetSettingDown();
    }

    public override void Exit()
    {
        base.Exit();

        _zoom = null;
        _inputActions.CameraMode.TakePicture.started -= TakePicture;
        _inputActions.CameraMode.ChangeSetting.started -= ChangeSetting;
        _inputActions.CameraMode.ResetCamera.started -= ResetCamera;

        _inputActions.CameraMode.Disable();

        EventHolder.Instance.onPictureShown.RemoveListener(ResetIsTakingPicture);
        EventHolder.Instance.onCameraStateExit?.Invoke();
    }

    private void TakePicture(InputAction.CallbackContext context)
    {
        if (_isTakingPicture)
            return;

        AudioManager.Instance.PlayPhotoSound(_camera.gameObject);
            AnimalsHolder.Instance.CheckAnimalsOnCamera();
            EventHolder.Instance.onPictureTaken?.Invoke();
            _isTakingPicture = true;
    }

    private void ResetIsTakingPicture()
    {
        _isTakingPicture = false;
    }

    private void Zoom()
    {
        _mainCamera.fieldOfView -= _zoom.ReadValue<Vector2>().y * ((InputHandler)_fsm).zoomModifier * Time.deltaTime;
        if (_mainCamera.fieldOfView > ((InputHandler)_fsm).zoomUpperLimit)
            _mainCamera.fieldOfView = ((InputHandler)_fsm).zoomUpperLimit;
        else if (_mainCamera.fieldOfView < ((InputHandler)_fsm).zoomLowerLimit)
            _mainCamera.fieldOfView = ((InputHandler)_fsm).zoomLowerLimit;
        EventHolder.Instance.onZoomChange?.Invoke();
    }

    private void ChangeSetting(InputAction.CallbackContext context)
    {
        if (_isTakingPicture)
            return;

        switch (_currentSetting)
        {
            case(CameraSetting.FocusDistance):
                _currentSetting = CameraSetting.Aperture; 
                break;
            case(CameraSetting.Aperture):
                _currentSetting = CameraSetting.FocalLength; 
                break;
            case(CameraSetting.FocalLength):
                _currentSetting = CameraSetting.FocusDistance; 
                break;
            default:
                throw new System.Exception("CameraSetting is not properly set.");            
        }
    }

    private void SetSettingUp()
    {
        switch (_currentSetting)
        {
            case (CameraSetting.FocusDistance):
                _depthOfField.focusDistance.value += ((InputHandler)_fsm).focusDistanceChangeSpeedModifier * Time.deltaTime;
                break;
            case (CameraSetting.Aperture):
                _depthOfField.aperture.value += ((InputHandler)_fsm).apertureChangeSpeedModifier * Time.deltaTime;
                break;
            case (CameraSetting.FocalLength):
                _depthOfField.focalLength.value += ((InputHandler)_fsm).focalLengthChangeSpeedModifier * Time.deltaTime;
                break;
            default:
                throw new System.Exception("CameraSetting is not properly set.");
        }
    }

    private void SetSettingDown()
    {
        switch (_currentSetting)
        {
            case (CameraSetting.FocusDistance):
                _depthOfField.focusDistance.value -= ((InputHandler)_fsm).focusDistanceChangeSpeedModifier * Time.deltaTime;
                break;
            case (CameraSetting.Aperture):
                _depthOfField.aperture.value -= ((InputHandler)_fsm).apertureChangeSpeedModifier * Time.deltaTime;
                break;
            case (CameraSetting.FocalLength):
                _depthOfField.focalLength.value -= ((InputHandler)_fsm).focalLengthChangeSpeedModifier * Time.deltaTime;
                break;
            default:
                throw new System.Exception("CameraSetting is not properly set.");
        }
    }

    private void InitializeCamera()
    {
        if (_camera != null)
        {
            do
            {
                _mainCamera = _camera.GetComponent<Camera>();
                _postProcessVolume = _camera.GetComponent<PostProcessVolume>();
                _postProcessVolume.profile.TryGetSettings<DepthOfField>(out _depthOfField);
            } while (_mainCamera == null || _postProcessVolume == null || _depthOfField == null);

            _isCameraInitialized = true;
        }
    }

    private void ResetCameraValues()
    {
        _mainCamera.fieldOfView = SettingsManager.Instance.Database.FieldOfView;
        _depthOfField.focusDistance.value = 10f;
        _depthOfField.aperture.value = 5.6f;
        _depthOfField.focalLength.value = 50f;
    }

    private void ResetCamera(InputAction.CallbackContext context)
    {
        if (_isTakingPicture)
            return;

        ResetCameraValues();
    }

    private enum CameraSetting { FocusDistance, Aperture, FocalLength }
}
