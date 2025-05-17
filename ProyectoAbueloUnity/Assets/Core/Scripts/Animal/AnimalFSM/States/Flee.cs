using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flee : FSMTemplateState
{
    private float _lookAtPlayerFLV;
    private float _fleeFLV;
    private bool _hasPassedNonReturningPoint;

    public Flee(FSMTemplateMachine fsm) : base(fsm) { }

    public override void Enter()
    {
        base.Enter();
        ((AnimalFSM)_fsm).IsFollowingRoutine = false;
        ((AnimalFSM)_fsm).AnimalAnimator.SetTrigger("Flee");
        ((AnimalFSM)_fsm).AnimalAnimator.SetBool("Moving", true);
        ((AnimalFSM)_fsm).AnimalIKControl.Initialize(((AnimalFSM)_fsm).AnimalAnimator);
        ((AnimalFSM)_fsm).CurrentAction = Action.Other;
        _hasPassedNonReturningPoint = false; 
        SetFleeTarget();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (!_hasPassedNonReturningPoint)
        {
            _fleeFLV = ((AnimalFSM)_fsm).GetFleeMembershipValue(((AnimalFSM)_fsm).Noise);

            if ( _fleeFLV <= Mathf.Epsilon)
            {
                DebugManager.Instance.DebugAnimalStateChangeMessage("Change state to: " + ((AnimalFSM)_fsm).lookAtPlayer);
                ((AnimalFSM)_fsm).ChangeState(((AnimalFSM)_fsm).lookAtPlayer);
            }
            if(_fleeFLV >= 1)
            {
                _hasPassedNonReturningPoint = true;
                ((AnimalFSM)_fsm).DespawnAfterTime(); // Executes a coroutine to return to idle
                ((AnimalFSM)_fsm).PlayFleeSound();
            }            
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        if (!_hasPassedNonReturningPoint)
        {
            ((AnimalFSM)_fsm).CalculateNoise();
            _lookAtPlayerFLV = ((AnimalFSM)_fsm).GetLookAtPlayerMembershipValue(((AnimalFSM)_fsm).Noise); // Despite being a condition in other states here is only used to interpolate head IK
            ((AnimalFSM)_fsm).AnimalIKControl.SetInterpolationValue(_lookAtPlayerFLV);
            ((AnimalFSM)_fsm).FleeSpeed = _fleeFLV * ((AnimalFSM)_fsm).OriginalFleeSpeed;
            ((AnimalFSM)_fsm).AnimalAnimator.speed = _fleeFLV;
        }
        else
        {
            Debug.Log("Non Returning Point passed.");
            ((AnimalFSM)_fsm).FleeSpeed = ((AnimalFSM)_fsm).OriginalFleeSpeed;
        }
    }

    public override void Exit()
    {
        base.Exit();
        ((AnimalFSM)_fsm).AnimalAnimator.SetBool("Moving", false);
        ((AnimalFSM)_fsm).AnimalNavMeshAgent.isStopped = true;
    }

    private void SetFleeTarget()
    {
        Vector3 fleeDirection = (_fsm.transform.position - ((AnimalFSM)_fsm).Player.position).normalized;
        Vector3 fleePoint = _fsm.transform.position + fleeDirection * 200;

        if(CheckPoint(fleePoint))
        {
            ((AnimalFSM)_fsm).AnimalNavMeshAgent.SetDestination(fleePoint);
            ((AnimalFSM)_fsm).AnimalNavMeshAgent.isStopped = false;
        }
        else
        {
            SetRandomPoint();
        }
    }

    private bool CheckPoint(Vector3 fleePoint)
    {
        NavMeshPath path = new NavMeshPath(); 
        ((AnimalFSM)_fsm).AnimalNavMeshAgent.CalculatePath(fleePoint, path);
        if (path.status.Equals(NavMeshPathStatus.PathComplete))
        {
            return true;
        }
        return false;
    }

    private void SetRandomPoint()
    {
        Vector3 randomPoint = _fsm.transform.position + ((Vector3)Random.insideUnitCircle * 200);
        if (CheckPoint(randomPoint))
        {
            ((AnimalFSM)_fsm).AnimalNavMeshAgent.SetDestination(randomPoint);
            ((AnimalFSM)_fsm).AnimalNavMeshAgent.isStopped = false;
        }
        else
            SetRandomPoint();
    }
}
