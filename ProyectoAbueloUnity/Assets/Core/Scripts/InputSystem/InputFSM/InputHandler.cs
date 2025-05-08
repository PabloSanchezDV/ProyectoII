using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : FSMTemplateMachine
{
    #region States
    [NonSerialized] public FreeMove freeMove;
    [NonSerialized] public CameraMode cameraMode;
    #endregion

    #region Fields
    [Header("Parameters -  Free Move")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float stopSpeed;
    [SerializeField] public float maxMoveSpeed;
    [SerializeField] public float maxMoveSpeedCameraMode;
    [SerializeField] public float interactiveRaycastDistance;
    [SerializeField] public float timeBetweenSteps;
    [SerializeField] public LayerMask playerLayerMask;

    [Header("Parameters -  Camera Mode")]
    [SerializeField] public float zoomModifier;
    [SerializeField] public float zoomUpperLimit;
    [SerializeField] public float zoomLowerLimit;
    [SerializeField] public float focusDistanceChangeSpeedModifier;
    [SerializeField] public float apertureChangeSpeedModifier;
    [SerializeField] public float focalLengthChangeSpeedModifier;

    private InputActions _inputActions;
    private GameObject _player;
    private GameObject _camera;
    #endregion

    #region Properties
    public GameObject Player {  get { return _player; } }
    public GameObject Camera { get  { return _camera; } set { _camera = value; } }
    #endregion

    private void Awake()
    {
        _inputActions = new InputActions();

        freeMove = new FreeMove(this, _inputActions);
        cameraMode = new CameraMode(this, _inputActions);

        _player = GameObject.FindGameObjectWithTag("Player");
        _camera = GameObject.FindGameObjectWithTag("MainCamera");

        Debug.Log("InputHandler initialized");
    }

    protected override void GetInitialState(out FSMTemplateState state)
    {
        state = freeMove;
        freeMove.Enter();
    }
}
