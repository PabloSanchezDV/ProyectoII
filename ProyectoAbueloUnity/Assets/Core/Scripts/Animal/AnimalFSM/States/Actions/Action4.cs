using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action4 : ActionState
{
    public Action4(FSMTemplateMachine fsm) : base(fsm) { }

    public override void Enter()
    {
        base.Enter();

        ((AnimalFSM)_fsm).AnimalAnimator.SetBool("Action4", true);
        ((AnimalFSM)_fsm).CurrentAction = Action.Action4;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        GetState(out _currentState);
        if (_currentState != this)
        {
            _fsm.ChangeState(_currentState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        ((AnimalFSM)_fsm).AnimalAnimator.SetBool("Action4", false);
    }
}
