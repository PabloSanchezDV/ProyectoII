using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class Moving : FollowRoutine
{
    public Moving(FSMTemplateMachine fsm) : base(fsm) { }

    public override void Enter()
    {
        base.Enter();
        ((AnimalFSM)_fsm).AnimalAnimator.SetBool("Moving", true);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (((AnimalFSM)_fsm).IsFollowingRoutine)
        {
            GetState(out _currentState);
            if(_currentState != this)
            {
                Debug.Log("Change state to: " + _currentState);
                _fsm.ChangeState(_currentState);
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        if (((AnimalFSM)_fsm).IsFollowingRoutine)
            UpdatePositionAndRotation();
    }

    public override void Exit()
    {
        base.Exit();
        ((AnimalFSM)_fsm).AnimalAnimator.SetBool("Moving", false);
    }

    private void UpdatePositionAndRotation()
    {
        _fsm.transform.position = ((AnimalFSM)_fsm).AnimalPathFollower.transform.position;
        _fsm.transform.rotation = ((AnimalFSM)_fsm).AnimalPathFollower.transform.rotation;
    }
}
