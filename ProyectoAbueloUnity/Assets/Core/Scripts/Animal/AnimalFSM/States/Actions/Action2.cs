using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action2 : ActionState
{
    public Action2(FSMTemplateMachine fsm) : base(fsm) { }

    public override void Enter()
    {
        base.Enter();

        ((AnimalFSM)_fsm).AnimalAnimator.SetBool("Action2", true);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        GetStateAtTime(GameManager.instance.Daytime, out _currentState);
        if (_currentState != this)
        {
            _fsm.ChangeState(_currentState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        ((AnimalFSM)_fsm).AnimalAnimator.SetBool("Action2", false);
    }
}
