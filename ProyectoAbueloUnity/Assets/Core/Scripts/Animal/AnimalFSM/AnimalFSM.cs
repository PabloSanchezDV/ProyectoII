using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class AnimalFSM : FSMTemplateMachine
{
    #region States
    [NonSerialized] public Idle idle;
    [NonSerialized] public Moving moving;
    [NonSerialized] public Action1 action1;
    [NonSerialized] public Action2 action2;
    [NonSerialized] public Action3 action3;
    [NonSerialized] public Action4 action4;
    [NonSerialized] public LookAtPlayer lookAtPlayer;
    [NonSerialized] public Flee flee;
    #endregion

    #region Fields
    [Header("References")]
    [SerializeField] private Transform _root;
    [SerializeField] private PathCreator[] _pathCreators;
    [SerializeField] private Transform[] _checkPoints;

    [Header("Atributes")]
    [SerializeField] private float _speed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _fleeSpeed;
    private float _currentSpeed;
    private float _currentFleeSpeed;

    [Header("Behaviour")]
    [SerializeField] private float _startsNoticingPlayer;
    private float _endsNoticingPlayer; // = _maxValueToLookAtPlayer

    private float _startsLookingAtPlayer; // = _startsNoticingPlayer
    [SerializeField] private float _maxValueToLookAtPlayer;
    private float _endsLookingAtPlayer; // = _maxValueToFleeFromPlayer
    
    private float _startsFleeingFromPlayer; // = _maxValueToLookAtPlayer
    [SerializeField] private float _maxValueToFleeFromPlayer;


    [Header("Routine")]
    [SerializeField] private Action[] _actions;
    [SerializeField] private float[] _times;

    private Collider _collider;
    private Animator _animator;
    private Camera _camera;
    private Plane[] _cameraFrustrum;
    private Vector3 _offset;
    [SerializeField] private float _noise; //Just for debugging 
    #endregion

    #region Properties
    public Animator AnimalAnimator { get { return _animator; } }
    public Action[] Actions { get { return _actions; } }
    public float[] Times { get { return _times; } }
    public PathCreator[] PathCreators { get { return _pathCreators; } }
    public float OriginalSpeed { get { return _speed; } }
    public float Speed 
    {   get { return _currentSpeed; }
        set 
        {
            if (value < _minSpeed)
                _currentSpeed = _minSpeed;
            else if (value > _speed)
                _currentSpeed = _speed;
            else
                _currentSpeed = value;
        }
    }
    public float OriginalFleeSpeed { get { return _fleeSpeed; } }
    public float FleeSpeed
    {
        get { return _currentFleeSpeed; }
        set
        {
            if (value < _minSpeed)
                _currentFleeSpeed = _minSpeed;
            else if (value > _fleeSpeed)
                _currentFleeSpeed = _fleeSpeed;
            else
                _currentFleeSpeed = value;
        }
    }
    public float Noise { get { return _noise; } }
    public Vector3 Offset { get { return _offset; } }
    #endregion

    private void Awake()
    {
        idle = new Idle(this);
        moving = new Moving(this);
        action1 = new Action1(this);
        action2 = new Action2(this);
        action3 = new Action3(this);
        action4 = new Action4(this);
        lookAtPlayer = new LookAtPlayer(this);
        flee = new Flee(this);

        InitializeAnimal();
    }

    protected override void GetInitialState(out FSMTemplateState state)
    {
        state = idle;
        state.Enter();
    }

    private void InitializeAnimal()
    {
        if (_actions.Length != _times.Length)
            throw new Exception("The number of actions and times of " + transform.name + " doesn't match. It must have the same number in both of them.");

        _endsNoticingPlayer = _maxValueToLookAtPlayer;
        _startsLookingAtPlayer = _startsNoticingPlayer;
        _endsLookingAtPlayer = _maxValueToFleeFromPlayer;
        _startsFleeingFromPlayer = _maxValueToLookAtPlayer;

        _currentSpeed = _speed;

        _offset = transform.position - _root.transform.position;

        do
        {
            _camera = Camera.main;
            _collider = GetComponent<Collider>();
            _animator = GetComponent<Animator>();
        } while (_camera == null || _collider == null || _animator == null);

        Debug.Log(transform.name + " is initialized");
    }

    public void CalculateNoiseValue() 
    {
        //TODO: Do Noise Function
        _noise = Mathf.Clamp(_noise, 0, 100);
    }

    public bool IsOnCamera()
    {
        var bounds = _collider.bounds;
        _cameraFrustrum = GeometryUtility.CalculateFrustumPlanes(_camera);
        if (GeometryUtility.TestPlanesAABB(_cameraFrustrum, bounds))
        {
            return DoesRayHit();
        }
        else
        {
            return false;
        }
    }

    private bool DoesRayHit()
    {
        foreach (Transform checkpoint in _checkPoints)
        {
            Vector3 direction = checkpoint.transform.position - _camera.transform.position;
            if (Physics.Raycast(_camera.transform.position, direction, out RaycastHit hit, Mathf.Infinity))
            {

                if (hit.transform.gameObject.Equals(gameObject))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        throw new Exception("The animal " + transform.name + " doesn't have any checkpoints.");
    }

    #region Fuzzy Logic
    /*
     * Fuzzy logic only applies between FollowPath, LookAtPlayer and Flee
     * 
     * INTERACTIONS BETWEEN STATES:
     *      - FollowPath && LookAtPlayer - MaxValue
     *      - LookAtPlayer && Flee - FleeValue ~ (FleeValue < Mathf.Epsilon)
     */

    public float GetFollowRoutineMembershipValue(float noise)
    {
        return DecreasingSlopeFunction(noise, _startsNoticingPlayer, _endsNoticingPlayer);
    }

    public float GetLookAtPlayerMembershipValue(float noise)
    {
        return TriangularFunction(noise, _startsLookingAtPlayer, _maxValueToLookAtPlayer, _endsLookingAtPlayer);
    }

    public float GetFleeMembershipValue(float noise)
    {
        return IncreasingSlopeFunction(noise, _startsFleeingFromPlayer, _maxValueToFleeFromPlayer);
    }

    private float TriangularFunction(float x, float a, float b, float c)
    {
        if (a != b && b != c)
        {
            if (x <= a || x >= c) return 0f;
            if (x <= b) return (x - a) / (b - a);
            return (c - x) / (c - b); // (c - x) is inverted so it returns the absolute value of the function
        }
        else
            throw new System.Exception("Triangular function is not defined properly.");
    }

    private float DecreasingSlopeFunction(float x, float a, float b)
    {
        if (x <= a) return 1f;
        if (x >= b) return 0f;
        else return (a - x) / (b - a) + 1; // (a - x) is inverted so it returns the absolute value of the function
    }

    private float IncreasingSlopeFunction(float x, float a, float b)
    {
        if (x <= a) return 0f;
        if (x >= b) return 1f;
        else return (x - a) / (b - a);
    }
    #endregion
}
