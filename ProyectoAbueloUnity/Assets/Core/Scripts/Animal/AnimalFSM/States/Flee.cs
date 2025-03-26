using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : FSMTemplateState
{

    private float _fleeFLV;

    public Flee(FSMTemplateMachine fsm) : base(fsm) { }

    public override void Enter()
    {
        base.Enter();
        ((AnimalFSM)_fsm).AnimalAnimator.SetTrigger("Flee");
        ((AnimalFSM)_fsm).AnimalAnimator.SetBool("Moving", true);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _fleeFLV = ((AnimalFSM)_fsm).GetFleeMembershipValue(((AnimalFSM)_fsm).Noise);

        if( _fleeFLV <= Mathf.Epsilon)
        {
            Debug.Log("Change state to: " + ((AnimalFSM)_fsm).lookAtPlayer);
            ((AnimalFSM)_fsm).ChangeState(((AnimalFSM)_fsm).lookAtPlayer);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        ((AnimalFSM)_fsm).FleeSpeed = _fleeFLV * ((AnimalFSM)_fsm).OriginalFleeSpeed;
        ((AnimalFSM)_fsm).AnimalAnimator.speed = _fleeFLV;
        Debug.Log(((AnimalFSM)_fsm).FleeSpeed);
        _fsm.transform.Translate(new Vector3(1f, 0f, 1f) * ((AnimalFSM)_fsm).FleeSpeed * Time.deltaTime);
    }

    public override void Exit()
    {
        base.Exit();
        ((AnimalFSM)_fsm).AnimalAnimator.SetBool("Moving", false);
    }
}
