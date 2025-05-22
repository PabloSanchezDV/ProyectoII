using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FreeMove : Gameplay
{
    public FreeMove(FSMTemplateMachine fsm, InputActions inputActions) : base(fsm, inputActions) { }

    public override void Enter()
    {
        base.Enter();

        UIManager.Instance.ShowFreeMoveControls();

        timeBetweenSteps = ((InputHandler)_fsm).timeBetweenSteps;
        _inputActions.FreeMove.Interact.started += Interact;
        _inputActions.Gameplay.ToggleMap.started += ToggleMap;

        _inputActions.FreeMove.Enable();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(isCameraToggled)
        {
            isCameraToggled = false;
            _fsm.ChangeState(((InputHandler)_fsm).cameraMode);
        }
    }

    public override void UpdatePhysics()
    {
        Move();
        LookAround();
    }

    public override void Exit()
    {
        base.Exit();

        if(UIManager.Instance != null)
            UIManager.Instance.HideFreeMoveControls();

        _inputActions.FreeMove.Interact.started -= Interact;
        _inputActions.Gameplay.ToggleMap.started -= ToggleMap;

        _inputActions.FreeMove.Disable();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit hitInfo, ((InputHandler)_fsm).interactiveRaycastDistance, ~((InputHandler)_fsm).playerLayerMask))
        {
            if(hitInfo.transform.gameObject.TryGetComponent<IInteractive>(out IInteractive iInteractive))
                iInteractive.Interact();
        }
        DebugManager.Instance.DebugMessage("Interacting");
    }

    protected void ToggleMap(InputAction.CallbackContext context)
    {
        _exitReason = ExitReason.Map;
        _exitCondition = true;
    }
}
