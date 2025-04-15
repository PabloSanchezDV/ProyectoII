using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSystem : MonoBehaviour
{
    private PathCreator[] _pathCreators;
    public PathCreator[] PathCreators { set { _pathCreators = value; } }

    private float _speed;
    public float Speed { set { _speed = value; } }

    [SerializeField] private bool _doesFollowPath;
    public bool DoesFollowPath { get { return _doesFollowPath; } set { _doesFollowPath = value; } }

    private EndOfPathInstruction _endOfPathInstruction = EndOfPathInstruction.Stop;
    private float _distanceTravelled;
    private int _currentPathIndex;

    private void Start()
    {
        _doesFollowPath = true;
        _distanceTravelled = 0;
        _currentPathIndex = 0;
    }

    public void UpdatePositionAndRotation()
    {
        if(_doesFollowPath)
        {
            _distanceTravelled += _speed * Time.deltaTime;
            if(_currentPathIndex <= _pathCreators.Length - 1)
            {
                // Check other methods so it can regulate position throw times instead of speed.
                transform.position = _pathCreators[_currentPathIndex].path.GetPointAtDistance(_distanceTravelled, _endOfPathInstruction);
                transform.rotation = _pathCreators[_currentPathIndex].path.GetRotationAtDistance(_distanceTravelled, _endOfPathInstruction);
            }
        }
    }

    public void UpdatePositionAndRotationOnDebugTimeChange(float time)
    {
        _distanceTravelled = _speed * time;

        if (_currentPathIndex <= _pathCreators.Length - 1)
        {
            // Check other methods so it can regulate position throw times instead of speed.
            transform.position = _pathCreators[_currentPathIndex].path.GetPointAtDistance(_distanceTravelled, _endOfPathInstruction);
            transform.rotation = _pathCreators[_currentPathIndex].path.GetRotationAtDistance(_distanceTravelled, _endOfPathInstruction);
        }
    }

    public void SetNextPath()
    {
        _currentPathIndex++;
        _distanceTravelled = 0;
    }
}
