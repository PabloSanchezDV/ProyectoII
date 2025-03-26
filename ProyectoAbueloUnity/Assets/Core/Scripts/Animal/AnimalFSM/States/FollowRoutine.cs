using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRoutine : FSMTemplateState
{
    protected FSMTemplateState _currentState;
    private float _followRoutineFLV;
    private float _lookAtPlayerFLV;

    public FollowRoutine(FSMTemplateMachine fsm) : base(fsm) { }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _followRoutineFLV = ((AnimalFSM)_fsm).GetFollowRoutineMembershipValue(((AnimalFSM)_fsm).Noise);
        _lookAtPlayerFLV = ((AnimalFSM)_fsm).GetLookAtPlayerMembershipValue(((AnimalFSM)_fsm).Noise);

        if (_followRoutineFLV < _lookAtPlayerFLV)
        {
            Debug.Log("Change state to: " + ((AnimalFSM)_fsm).lookAtPlayer);
            _fsm.ChangeState(((AnimalFSM)_fsm).lookAtPlayer);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        ((AnimalFSM)_fsm).Speed = _followRoutineFLV * ((AnimalFSM)_fsm).OriginalSpeed;
        ((AnimalFSM)_fsm).AnimalAnimator.speed = _followRoutineFLV;
    }

    public void GetStateAtTime(float time, out FSMTemplateState state)
    {
        state = GetState(GetActionAtTime(time));
    }

    private Action GetActionAtTime(float time)
    {
        for (int i = 0; i < ((AnimalFSM)_fsm).Times.Length - 1; i++)
        {
            if (time >= ((AnimalFSM)_fsm).Times[i] && time < ((AnimalFSM)_fsm).Times[i + 1])
            {
                return ((AnimalFSM)_fsm).Actions[i];
            }
        }

        return ((AnimalFSM)_fsm).Actions[((AnimalFSM)_fsm).Times.Length - 1];
    }

    private FSMTemplateState GetState(Action action)
    {
        switch (action)
        {
            case Action.Walk:
                return ((AnimalFSM)_fsm).moving;
            case Action.Action1:
                return ((AnimalFSM)_fsm).action1;
            case Action.Action2:
                return ((AnimalFSM)_fsm).action2;
            case Action.Action3:
                return ((AnimalFSM)_fsm).action3;
            case Action.Action4:
                return ((AnimalFSM)_fsm).action4;
            default:
                throw new Exception("The animal action cannot be processed.");
        }
    }
}
