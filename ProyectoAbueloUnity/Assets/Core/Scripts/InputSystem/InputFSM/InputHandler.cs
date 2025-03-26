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
    [SerializeField] public float maxMoveSpeed;
    [SerializeField] public float maxMoveSpeedCameraMode;
    [SerializeField] public float interactiveRaycastDistance;
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
    #endregion

    #region Properties
    public GameObject Player {  get { return _player; } }
    #endregion

    private void Awake()
    {
        _inputActions = new InputActions();

        freeMove = new FreeMove(this, _inputActions);
        cameraMode = new CameraMode(this, _inputActions);

        _player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void GetInitialState(out FSMTemplateState state)
    {
        state = freeMove;
        freeMove.Enter();
    }
}
