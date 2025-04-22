using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private AnimalFSM _fsm;
    [SerializeField] private bool _applyOffset;

    private float _distanceTravelled;
    private int _currentPathIndex;
    private EndOfPathInstruction _endOfPathInstruction = EndOfPathInstruction.Stop;
    private Vector3 _offset;
    private Action _currentAction;
    public Action CurrentAction { get { return _currentAction; } }

    public void Initialize()
    {
        CalculateOffsetBasedOnAnimal(_fsm.transform);
        _distanceTravelled = 0f;
        _currentPathIndex = 0;
        InitializePosition();
    }

    private void LateUpdate()
    {
        Action newAction = GetActionAtTime(GameManager.Instance.Daytime - (GameManager.Instance.StartHour * 60 + GameManager.Instance.StartMinute));
        if(_currentAction != newAction)
        {
            if(newAction.Equals(Action.Walking))
                SetNextPath();
            _currentAction = newAction;
        }
        if(_currentAction.Equals(Action.Walking))
            UpdatePositionAndRotation();
    }

    public Action GetActionAtTime(float time)
    {
        for (int i = 0; i < _fsm.Times.Length - 1; i++)
        {
            if (time >= _fsm.Times[i] && time < _fsm.Times[i + 1])
            {
                return _fsm.Actions[i];
            }
        }

        return _fsm.Actions[_fsm.Times.Length - 1];
    }

    public void UpdatePositionAndRotation()
    {
        _distanceTravelled += _fsm.OriginalSpeed * Time.deltaTime;
        if (_currentPathIndex <= _fsm.PathCreators.Length - 1)
        {
            transform.position = _fsm.PathCreators[_currentPathIndex].path.GetPointAtDistance(_distanceTravelled, _endOfPathInstruction) + _offset;
            transform.rotation = _fsm.PathCreators[_currentPathIndex].path.GetRotationAtDistance(_distanceTravelled, _endOfPathInstruction);
        }
    }

    private void CalculateOffsetBasedOnAnimal(Transform animal)
    {
        if(_applyOffset)
            _offset = _fsm.Offset.z * transform.forward + _fsm.Offset.y * transform.up;
        else
            _offset = Vector3.zero;
    }

    public void SetNextPath()
    {
        _currentPathIndex++;
        _distanceTravelled = 0;
    }

    private void InitializePosition()
    {
        transform.position = _fsm.PathCreators[0].path.GetPointAtDistance(0, _endOfPathInstruction) + _offset;
        transform.rotation = _fsm.PathCreators[0].path.GetRotationAtDistance(0, _endOfPathInstruction);
    }
}
