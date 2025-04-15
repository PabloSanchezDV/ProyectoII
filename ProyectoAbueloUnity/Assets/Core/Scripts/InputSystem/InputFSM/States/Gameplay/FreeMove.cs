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

        timeBetweenSteps = ((InputHandler)_fsm).timeBetweenSteps;
        _inputActions.FreeMove.Interact.started += Interact;

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

        _inputActions.FreeMove.Interact.started -= Interact;

        _inputActions.FreeMove.Disable();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if(Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, ((InputHandler)_fsm).interactiveRaycastDistance, ~((InputHandler)_fsm).playerLayerMask))
        {
            if(hitInfo.transform.gameObject.TryGetComponent<IInteractive>(out IInteractive iInteractive))
                iInteractive.Interact();
        }
        Debug.Log("Interacting");
    }
}
