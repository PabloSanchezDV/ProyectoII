using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : FSMTemplateState
{
    public LookAtPlayer(FSMTemplateMachine fsm) : base(fsm) { }

    private float _followRoutineFLV;
    private float _lookAtPlayerFLV;
    private float _fleeFLV;

    public override void Enter()
    {
        base.Enter();
        ((AnimalFSM)_fsm).IsFollowingRoutine = false;
        ((AnimalFSM)_fsm).AnimalAnimator.SetTrigger("LookingAtPlayer");
        ((AnimalFSM)_fsm).AnimalIKControl.Initialize(((AnimalFSM)_fsm).AnimalAnimator);
        ((AnimalFSM)_fsm).CurrentAction = Action.Other;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _followRoutineFLV = ((AnimalFSM)_fsm).GetFollowRoutineMembershipValue(((AnimalFSM)_fsm).Noise);
        _lookAtPlayerFLV = ((AnimalFSM)_fsm).GetLookAtPlayerMembershipValue(((AnimalFSM)_fsm).Noise);
        _fleeFLV = ((AnimalFSM)_fsm).GetFleeMembershipValue(((AnimalFSM)_fsm).Noise);

        if(_lookAtPlayerFLV < _followRoutineFLV)
            _fsm.ChangeState(((AnimalFSM)_fsm).idle);

        if(_fleeFLV > Mathf.Epsilon)
        {
            Debug.Log("Change state to: " + ((AnimalFSM)_fsm).flee);
            _fsm.ChangeState(((AnimalFSM)_fsm).flee);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        ((AnimalFSM)_fsm).CalculateNoise();
        ((AnimalFSM)_fsm).AnimalIKControl.SetInterpolationValue(_lookAtPlayerFLV);
    }

    public override void Exit()
    {
        base.Exit();
        ((AnimalFSM)_fsm).AnimalIKControl.ResetHead();
    }
}
