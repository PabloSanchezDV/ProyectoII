using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : FollowRoutine
{
    public Idle(FSMTemplateMachine fsm) : base(fsm) { }

    // Checks for logic on Enter instead of UpdateLogic because it only serves as an entry point to the hierarchy level
    // It always will transition to the proper state given the fact that GetStateAtTime cannot return idle state
    public override void Enter()
    {
        GetState(out _currentState);
        DebugManager.Instance.DebugAnimalStateChangeMessage("Change state to: " + _currentState);
        _fsm.ChangeState(_currentState);
    }
}
