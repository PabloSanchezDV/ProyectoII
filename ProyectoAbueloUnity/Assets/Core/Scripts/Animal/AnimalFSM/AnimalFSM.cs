using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField] public Animal animal;

    [Header("References")]
    [SerializeField] private Transform _root;
    [SerializeField] private Transform _headBone;
    [SerializeField] private PathCreator[] _pathCreators;
    [SerializeField] private Transform[] _checkPoints;
    [SerializeField] private PathFollower _pathFollower;

    [Header("Atributes")]
    [SerializeField] private float _speed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _fleeSpeed;
    [SerializeField] private float _resetHeadBoneTime;
    [SerializeField] private float _minDistanceToAnimal;
    [SerializeField] private float _maxDistanceToAnimal;
    [SerializeField] private float _soundAttack;
    [SerializeField] private float _soundRelease;
    [SerializeField] private float _despawnTime;
    [SerializeField] private string _navMeshLayerName;
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
    private IKControl _ikControl;
    private Transform _player;
    private Rigidbody _playerRB;
    private NavMeshAgent _navMeshAgent;
    private Plane[] _cameraFrustrum;
    private Vector3 _offset;
    private float _noise;
    private float _timeFromLastNoise;
    private bool _isFollowingRoutine;
    private Action _currentAction;
    #endregion

    #region Properties
    public Animator AnimalAnimator { get { return _animator; } }
    public PathFollower AnimalPathFollower { get { return _pathFollower; } }
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

    public Transform Player {  get { return _player; } }

    public IKControl AnimalIKControl { get { return _ikControl; } }

    public bool IsFollowingRoutine { get { return _isFollowingRoutine; } set { _isFollowingRoutine = value; } }

    public NavMeshAgent AnimalNavMeshAgent { get { return _navMeshAgent; } }
    public float DespawnTime { get { return _despawnTime; } }
    public string NavMeshLayerName { get { return _navMeshLayerName; } }

    public Action CurrentAction { get { return _currentAction; } set { _currentAction = value; } }
    #endregion

    private void Awake()
    {
        idle = new Idle(this);
        moving = new Moving(this);
        lookAtPlayer = new LookAtPlayer(this);
        flee = new Flee(this);
        SetActions();

        InitializeAnimal();
    }

    protected override void GetInitialState(out FSMTemplateState state)
    {
        state = idle;
        state.Enter();
    }

    private void SetActions()
    {
        switch (animal)
        {
            case (Animal.Mammoth):
                action1 = new MammothEat(this); 
                action2 = new MammothSleep(this);
                action3 = new Action3(this);
                action4 = new Action4(this);
                break;
            case (Animal.Bird):
                action1 = new Action1(this);
                action2 = new Action2(this);
                action3 = new Action3(this);
                action4 = new Action4(this);
                break;
            case (Animal.Elk):
                action1 = new Action1(this);
                action2 = new Action2(this);
                action3 = new Action3(this);
                action4 = new Action4(this);
                break;
            case (Animal.Ornito):
                action1 = new Action1(this);
                action2 = new Action2(this);
                action3 = new Action3(this);
                action4 = new Action4(this);
                break;
            default:
                Debug.LogWarning($"Couldn't set custom actions for {gameObject.name}. Default actions have been set for {gameObject.name}. Animal couldn't be filtered properly.");
                action1 = new Action1(this);
                action2 = new Action2(this);
                action3 = new Action3(this);
                action4 = new Action4(this);
                break;
        }

    }

    private void InitializeAnimal()
    {
        if (_actions.Length != _times.Length)
            throw new Exception("The number of actions and times of " + transform.name + " doesn't match. It must have the same number in both of them.");

        _endsNoticingPlayer = _maxValueToLookAtPlayer;
        _startsLookingAtPlayer = _startsNoticingPlayer;
        _endsLookingAtPlayer = _maxValueToFleeFromPlayer;
        _startsFleeingFromPlayer = _maxValueToLookAtPlayer;

        _isFollowingRoutine = true;
        _currentSpeed = _speed;

        _offset = transform.position - _root.transform.position;

        do
        {
            _camera = Camera.main;
            _collider = GetComponent<Collider>();
            _animator = GetComponent<Animator>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _navMeshAgent = GetComponent<NavMeshAgent>();
        } while (_camera == null || _collider == null || _animator == null || _player == null || _navMeshAgent == null);

        _ikControl = (IKControl)gameObject.AddComponent(typeof(IKControl));
        _ikControl.SetHeadBone(_headBone);
        _ikControl.SetTarget(_player);

        _pathFollower.Initialize();
        IntializeAnimalPosition();
        _playerRB = _player.GetComponent<Rigidbody>();

        Debug.Log(transform.name + " is initialized");
    }

    private void IntializeAnimalPosition()
    {
        transform.position = _pathFollower.transform.position;
        transform.rotation = _pathFollower.transform.rotation;
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

    public void DespawnAfterTime()
    {
        StartCoroutine(DespawnAfter());
    }

    IEnumerator DespawnAfter()
    {
        yield return new WaitForSeconds(_despawnTime);
        transform.position = _pathFollower.transform.position;
        Debug.Log("Change state to: " + idle);
        ChangeState(idle);
    }

    #region Noise
    public void CalculateNoise() 
    {
        float noiseValue = CalculateNoiseValue();
        if (noiseValue > 0.1f)
        {
            if (_timeFromLastNoise > _soundRelease)
                _timeFromLastNoise = 0;
            else
            {
                _timeFromLastNoise = Mathf.Lerp(_timeFromLastNoise, _soundAttack, Time.deltaTime * 5f);
                if (Mathf.Abs(_timeFromLastNoise - _soundAttack) < 0.01f)
                {
                    _timeFromLastNoise = _soundAttack;
                }
            }
        }
        if (_timeFromLastNoise < _soundRelease)
        {
            _noise = RideNoiseFunction(noiseValue, _timeFromLastNoise) * 100;
            _timeFromLastNoise += Time.deltaTime;
        }
    }

    private float RideNoiseFunction(float noiseValue, float time)
    {
        if(noiseValue == 0)
            return 0;
        return (noiseValue + NoiseFunction(time, _soundAttack, _soundAttack + _soundRelease / 2, _soundRelease)) / 2;
    }

    private float CalculateNoiseValue()
    {
        float distance = Vector3.Distance(_player.position, transform.position);

        if (distance < _maxDistanceToAnimal)
        {
            float distanceFactor = Mathf.InverseLerp(_maxDistanceToAnimal, _minDistanceToAnimal, distance);
            float speedFactor = Mathf.Clamp01(_playerRB.velocity.magnitude / 5);
            return distanceFactor * speedFactor;
        }
        return 0;
    }

    private float NoiseFunction(float x, float a, float b, float c)
    {
        if (x <= 0) return 0f;
        if (x <= a) return x / a;
        if (x <= b) return 1f;
        if (x <= c) return (x - c) * (x - c) / ((b - c) * (b - c));
        return 0f;
    }
    #endregion

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

    #region Audio
    public void PlayStepSound()
    {
        switch (animal)
        {
            case(Animal.Mammoth):
                AudioManager.Instance.PlayStepsMammothSound(gameObject);
                break;
            case(Animal.Bird):
                //AudioManager.Instance.PlayStepsBirdSound(gameObject);
                break;
            case (Animal.Elk):
                //AudioManager.Instance.PlayStepsElkSound(gameObject);
                break;
            case(Animal.Ornito):
                //AudioManager.Instance.PlayStepsOrnitoSound(gameObject);
                break;
            default:
                throw new Exception($"Couldn't play {gameObject.name} step sound. Animal couldn't be filtered properly.");
        }
    }

    public void PlayFleeSound()
    {
        switch (animal)
        {
            case (Animal.Mammoth):
                AudioManager.Instance.PlayScaredMammothSound(gameObject);
                break;
            case (Animal.Bird):
                //AudioManager.Instance.PlayScaredBirdSound(gameObject);
                break;
            case (Animal.Elk):
                //AudioManager.Instance.PlayScaredElkSound(gameObject);
                break;
            case (Animal.Ornito):
                //AudioManager.Instance.PlayScaredOrnitoSound(gameObject);
                break;
            default:
                throw new Exception($"Couldn't play {gameObject.name} scared sound. Animal couldn't be filtered properly.");
        }
    }
    #endregion
}
