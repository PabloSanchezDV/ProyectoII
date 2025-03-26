using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class Moving : FollowRoutine
{
    private float _distanceTravelled;
    private int _currentPathIndex = 0;
    private EndOfPathInstruction _endOfPathInstruction = EndOfPathInstruction.Stop;
    private Vector3 _offset;

    public Moving(FSMTemplateMachine fsm) : base(fsm) { }

    public override void Enter()
    {
        base.Enter();
        _distanceTravelled = 0;
        ((AnimalFSM)_fsm).AnimalAnimator.SetBool("Moving", true);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        GetStateAtTime(GameManager.instance.Daytime, out _currentState);
        if(_currentState != this)
        {
            Debug.Log("Change state to: " + _currentState);
            _fsm.ChangeState(_currentState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        UpdatePositionAndRotation();
    }

    public override void Exit()
    {
        base.Exit();
        SetNextPath();
        ((AnimalFSM)_fsm).AnimalAnimator.SetBool("Moving", false);
    }

    public void UpdatePositionAndRotation()
    {
        _distanceTravelled += ((AnimalFSM)_fsm).Speed * Time.deltaTime;
        if (_currentPathIndex <= ((AnimalFSM)_fsm).PathCreators.Length - 1)
        {
            CalculateOffsetBasedOnAnimal(_fsm.transform);
            _fsm.transform.position = ((AnimalFSM)_fsm).PathCreators[_currentPathIndex].path.GetPointAtDistance(_distanceTravelled, _endOfPathInstruction) + _offset;
            _fsm.transform.rotation = ((AnimalFSM)_fsm).PathCreators[_currentPathIndex].path.GetRotationAtDistance(_distanceTravelled, _endOfPathInstruction);
        }
    }

    private void CalculateOffsetBasedOnAnimal(Transform animal)
    {
        _offset = ((AnimalFSM)_fsm).Offset.z * _fsm.transform.forward + ((AnimalFSM)_fsm).Offset.y * _fsm.transform.up;
    }

    private void SetNextPath()
    {
        _currentPathIndex++;
    }
}
