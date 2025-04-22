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

    public override void Enter()
    {
        base.Enter();     
        ((AnimalFSM)_fsm).AnimalIKControl.Initialize(((AnimalFSM)_fsm).AnimalAnimator); 
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _followRoutineFLV = ((AnimalFSM)_fsm).GetFollowRoutineMembershipValue(((AnimalFSM)_fsm).Noise);
        _lookAtPlayerFLV = ((AnimalFSM)_fsm).GetLookAtPlayerMembershipValue(((AnimalFSM)_fsm).Noise);

        if (_followRoutineFLV <= _lookAtPlayerFLV)
        {
            Debug.Log("Change state to: " + ((AnimalFSM)_fsm).lookAtPlayer);
            _fsm.ChangeState(((AnimalFSM)_fsm).lookAtPlayer);
        }

        if (Vector3.Distance(((AnimalFSM)_fsm).AnimalPathFollower.transform.position, _fsm.transform.position) <= 0.5f)
            ((AnimalFSM)_fsm).IsFollowingRoutine = true;
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        ((AnimalFSM)_fsm).CalculateNoise();
        ((AnimalFSM)_fsm).AnimalIKControl.SetInterpolationValue(_lookAtPlayerFLV);
        ((AnimalFSM)_fsm).Speed = _followRoutineFLV * ((AnimalFSM)_fsm).OriginalSpeed;
        ((AnimalFSM)_fsm).AnimalAnimator.speed = _followRoutineFLV;

        if (!((AnimalFSM)_fsm).IsFollowingRoutine)
        {
            ((AnimalFSM)_fsm).AnimalNavMeshAgent.SetDestination(((AnimalFSM)_fsm).AnimalPathFollower.transform.position);
            ((AnimalFSM)_fsm).AnimalNavMeshAgent.isStopped = false;
        }
        else
        {
            ((AnimalFSM)_fsm).AnimalNavMeshAgent.isStopped = true;
        }
    }

    public void GetState(out FSMTemplateState state)
    {
        state = GetState(((AnimalFSM)_fsm).AnimalPathFollower.CurrentAction);
    }

    private FSMTemplateState GetState(Action action)
    {
        switch (action)
        {
            case Action.Walking:
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
