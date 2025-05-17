using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Gameplay : FSMTemplateState
{
    protected InputActions _inputActions;
    private InputAction _movement;
    private InputAction _cameraRotation;

    private Rigidbody _rb;
    protected Transform _camera;

    protected float timeBetweenSteps;
    private float _timeToNextStep;

    protected bool isCameraToggled;
    private bool _exitCondition;

    private bool _isInitialized;
    private Vector3 direction;
    
    private ExitReason _exitReason;

    public Gameplay(FSMTemplateMachine fsm, InputActions inputActions) : base(fsm)
    {
        _fsm = fsm;
        _inputActions = inputActions;
        _isInitialized = false;
    }

    public override void Enter()
    {
        do
        {
            _rb = ((InputHandler)_fsm).Player.GetComponent<Rigidbody>();
            _camera = ((InputHandler)_fsm).Camera.transform;

            if (_rb != null && _camera != null)
                _isInitialized = true;
        } while (!_isInitialized);

        _movement = _inputActions.Gameplay.Movement;
        _cameraRotation = _inputActions.Gameplay.CameraRotation;
        _inputActions.Gameplay.ToggleMap.started += ToggleMap;
        _inputActions.Gameplay.ToggleMenu.started += ToggleNotebook;
        _inputActions.Gameplay.ToggleCamera.started += ToggleCamera;

        _inputActions.Gameplay.Enable();

        Cursor.lockState = CursorLockMode.Locked;
        _exitCondition = false;
    }

    public override void UpdateLogic()
    {
        if (_exitCondition)
        {
            switch (_exitReason)
            {
                case ExitReason.Map:
                    _fsm.ChangeState(((InputHandler)_fsm).mapPage);
                    break;
                case ExitReason.Notebook:
                    //_fsm.ChangeState(((InputHandler)_fsm).notebook);
                    break;
            }
            _exitCondition = false;
        }
    }

    public override void UpdatePhysics()
    {
        Move();
        LookAround();
    }

    public override void Exit()
    {
        _movement = null;
        _cameraRotation = null;
        _inputActions.Gameplay.ToggleMap.started -= ToggleMap;
        _inputActions.Gameplay.ToggleMenu.started -= ToggleNotebook;
        _inputActions.Gameplay.ToggleCamera.started -= ToggleCamera;

        _inputActions.Gameplay.Disable();
    }

    protected void Move()
    {
        direction = ((InputHandler)_fsm).Player.transform.forward * _movement.ReadValue<Vector2>().y + ((InputHandler)_fsm).Player.transform.right * _movement.ReadValue<Vector2>().x;
        direction.Normalize();
        _rb.AddForce(direction * ((InputHandler)_fsm).moveSpeed * Time.deltaTime);

        //Cap max speed
        Vector3 horizontalVelocity = _rb.velocity;
        horizontalVelocity.y = 0f;

        if (horizontalVelocity.sqrMagnitude > ((InputHandler)_fsm).maxMoveSpeed * ((InputHandler)_fsm).maxMoveSpeed)
        {
            _rb.velocity = horizontalVelocity.normalized * ((InputHandler)_fsm).maxMoveSpeed + Vector3.up * _rb.velocity.y;
        }

        if (direction.magnitude > Mathf.Epsilon)
        {
            _timeToNextStep += Time.deltaTime;
            if (_timeToNextStep >= timeBetweenSteps)
            {
                AudioManager.Instance.PlayStepsSound(((InputHandler)_fsm).Player);
                _timeToNextStep = 0f;
            }
        }
        else
            _timeToNextStep = 0f;
    }

    protected void LookAround()
    {
        ((InputHandler)_fsm).Player.transform.Rotate(Vector3.up, SettingsManager.Instance.Database.MouseSensitivity * _cameraRotation.ReadValue<Vector2>().x);
        _camera.Rotate(Vector3.right, SettingsManager.Instance.Database.MouseSensitivity * -_cameraRotation.ReadValue<Vector2>().y);
        float fixedCameraRotation = Mathf.Clamp(_camera.localRotation.eulerAngles.x, -90f, 90f);
        _camera.localRotation = Quaternion.Euler(new Vector3(_camera.localRotation.eulerAngles.x, 0f, 0f));
    }

    protected void ToggleMap(InputAction.CallbackContext context)
    {
        _exitReason = ExitReason.Map;
        _exitCondition = true;
    }

    protected void ToggleNotebook(InputAction.CallbackContext context)
    {
        _exitReason = ExitReason.Notebook;
        _exitCondition = true;
    }

    protected virtual void ToggleCamera(InputAction.CallbackContext context)
    {
        isCameraToggled = true;
    }

    private enum ExitReason { Map, Notebook, Camera }
}
