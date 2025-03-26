using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

public class Animal : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform[] _checkPoints;
    [SerializeField] private PathCreator[] _pathCreators;

    [Header("Atributes")]
    [SerializeField] private float _speed;

    [Header("Routine")]
    [SerializeField] private Action[] _actions;
    [SerializeField] private float[] _times;

    private InCameraDetector _inCameraDetector;
    private PathSystem _pathSystem;
    private RoutinesController _routinesController;
    private Action _currentAction;
    public Action Action { get { return _currentAction; } }

    // Start is called before the first frame update
    void Start()
    {
        _inCameraDetector = gameObject.AddComponent<InCameraDetector>();
        _inCameraDetector.CheckPoints = _checkPoints;

        _pathSystem = gameObject.AddComponent<PathSystem>();
        _pathSystem.PathCreators = _pathCreators;
        _pathSystem.Speed = _speed;

        _routinesController = gameObject.AddComponent<RoutinesController>();
        _routinesController.Actions = _actions;
        _routinesController.Times = _times;

        _currentAction = _actions[0];
    }

    public void UpdateAnimal(float time)
    {
        Action action = _routinesController.GetActionInTime(time);

        if (action != _currentAction)
        {
            SetUpNewAction(action);
            _currentAction = action;
        }

        switch(action)
        {
            case Action.Walk:
                UpdatePositionAndRotation();
                break;
            case Action.Action1:
                // Do Action 1
                break;
            case Action.Action2:
                // Do Action 2
                break;
            case Action.Action3:
                // Do Action 3
                break;
            case Action.Action4:
                // Do Action 4
                break;
            default:
                throw new Exception("The animal action cannot be processed.");
        }
    }

    private void SetUpNewAction(Action action)
    {
        switch (action)
        {
            case Action.Walk:
                _pathSystem.SetNextPath();
                KeepFollowingPath();
                break;
            case Action.Action1:
                // Set Up Action 1
                break;
            case Action.Action2:
                // Set Up Action 2
                break;
            case Action.Action3:
                // Set Up Action 3
                break;
            case Action.Action4:
                // Set Up Action 4
                break;
            default:
                throw new Exception("The animal action cannot be processed.");
        }
    }

    #region InCameraDetector Methods
    public bool IsOnCamera()
    {
        return _inCameraDetector.IsOnCamera();
    }
    #endregion

    #region PathSystem Methods
    public void UpdatePositionAndRotationOnDebugTimeChange(float time)
    {
        _pathSystem.UpdatePositionAndRotationOnDebugTimeChange(time);
    }

    private void UpdatePositionAndRotation()
    {
        _pathSystem.UpdatePositionAndRotation();
    }

    private void StopFollowingPath()
    {
        _pathSystem.DoesFollowPath = false;
    }

    private void KeepFollowingPath()
    {
        _pathSystem.DoesFollowPath = true;
    }
    #endregion
}
