using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

public class CameraMode : Gameplay
{
    InputAction _zoom;
    private Camera _mainCamera;
    private MeshCollider _cameraMeshCollider;
    private CameraTargetsDetector _cameraTargetsDetector;
    private PostProcessVolume _postProcessVolume;
    private DepthOfField _depthOfField;
    private CameraSetting _currentSetting;
    private bool _isCameraInitialized = false;
    private bool _isTakingPicture = false;

    public CameraMode(FSMTemplateMachine fsm, InputActions inputActions) : base(fsm, inputActions) { }

    public override void Enter()
    {
        base.Enter();

        if(!_isCameraInitialized)
            InitializeCamera();

        _currentSetting = CameraSetting.FocusDistance;
        timeBetweenSteps = ((InputHandler)_fsm).timeBetweenSteps / 2;

        AudioManager.Instance.PlayCameraOnSound(_camera.gameObject);

        ((InputHandler)_fsm).PlayerAnim.SetTrigger("CameraUp");
        ((InputHandler)_fsm).ArmsAnim.SetTrigger("HoldCamera");

        ((InputHandler)_fsm).Polaroid.GetComponent<MeshRenderer>().enabled = true;
        ((InputHandler)_fsm).ArmsRenderer.enabled = true;

        EventHolder.Instance.onPictureShown.AddListener(ResetIsTakingPicture);
    }

    public override void UpdateLogic()
    {
        if(_isTakingPicture)
            return;

        base.UpdateLogic();
        if (isCameraToggled)
        {
            isCameraToggled = false;
            _fsm.ChangeState(((InputHandler)_fsm).freeMove);
        }
    }

    public override void UpdatePhysics()
    {
        if (_isTakingPicture)
            return;

        Move();
        LookAround();
        Zoom();

        if (_inputActions.CameraMode.ChangeSettingUp.IsPressed())
            SetSettingUp();
        if (_inputActions.CameraMode.ChangeSettingDown.IsPressed())
            SetSettingDown();
    }

    public override void Exit()
    {
        base.Exit();

        _zoom = null;
        _inputActions.CameraMode.TakePicture.started -= TakePicture;
        _inputActions.CameraMode.ChangeSetting.started -= ChangeSetting;
        _inputActions.CameraMode.ResetCamera.started -= ResetCamera;

        _inputActions.CameraMode.Disable();

        EventHolder.Instance.onPictureShown.RemoveListener(ResetIsTakingPicture);

        if(((InputHandler)_fsm).PlayerAnim != null)
            ((InputHandler)_fsm).PlayerAnim.SetTrigger("CameraDown");
        if(UIManager.Instance != null)
            UIManager.Instance.PlayCameraModeExitTransition();
        if(_mainCamera != null)
            _mainCamera.fieldOfView = 60f;

        if(((InputHandler)_fsm).Polaroid != null)
            ((InputHandler)_fsm).Polaroid.GetComponent<MeshRenderer>().enabled = true;
        if (((InputHandler)_fsm).ArmsRenderer != null)
            ((InputHandler)_fsm).ArmsRenderer.enabled = true;
    }


    private void Zoom()
    {
        if (_zoom == null)
            return;

        _mainCamera.fieldOfView -= _zoom.ReadValue<Vector2>().y * ((InputHandler)_fsm).zoomModifier * Time.deltaTime;
        if (_mainCamera.fieldOfView > ((InputHandler)_fsm).zoomUpperLimit)
            _mainCamera.fieldOfView = ((InputHandler)_fsm).zoomUpperLimit;
        else if (_mainCamera.fieldOfView < ((InputHandler)_fsm).zoomLowerLimit)
            _mainCamera.fieldOfView = ((InputHandler)_fsm).zoomLowerLimit;
        EventHolder.Instance.onZoomChange?.Invoke();
    }

    private void ChangeSetting(InputAction.CallbackContext context)
    {
        if (_isTakingPicture)
            return;

        switch (_currentSetting)
        {
            case(CameraSetting.FocusDistance):
                _currentSetting = CameraSetting.Aperture; 
                break;
            case(CameraSetting.Aperture):
                _currentSetting = CameraSetting.FocalLength; 
                break;
            case(CameraSetting.FocalLength):
                _currentSetting = CameraSetting.FocusDistance; 
                break;
            default:
                throw new System.Exception("CameraSetting is not properly set.");            
        }
    }

    private void SetSettingUp()
    {
        switch (_currentSetting)
        {
            case (CameraSetting.FocusDistance):
                _depthOfField.focusDistance.value += ((InputHandler)_fsm).focusDistanceChangeSpeedModifier * Time.deltaTime;
                break;
            case (CameraSetting.Aperture):
                _depthOfField.aperture.value += ((InputHandler)_fsm).apertureChangeSpeedModifier * Time.deltaTime;
                break;
            case (CameraSetting.FocalLength):
                _depthOfField.focalLength.value += ((InputHandler)_fsm).focalLengthChangeSpeedModifier * Time.deltaTime;
                break;
            default:
                throw new System.Exception("CameraSetting is not properly set.");
        }
    }

    private void SetSettingDown()
    {
        switch (_currentSetting)
        {
            case (CameraSetting.FocusDistance):
                _depthOfField.focusDistance.value -= ((InputHandler)_fsm).focusDistanceChangeSpeedModifier * Time.deltaTime;
                break;
            case (CameraSetting.Aperture):
                _depthOfField.aperture.value -= ((InputHandler)_fsm).apertureChangeSpeedModifier * Time.deltaTime;
                break;
            case (CameraSetting.FocalLength):
                _depthOfField.focalLength.value -= ((InputHandler)_fsm).focalLengthChangeSpeedModifier * Time.deltaTime;
                break;
            default:
                throw new System.Exception("CameraSetting is not properly set.");
        }
    }

    private void InitializeCamera()
    {
        if (_camera != null)
        {
            do
            {
                _mainCamera = _camera.GetComponent<Camera>();
                _cameraMeshCollider = _mainCamera.GetComponent<MeshCollider>();
                _cameraTargetsDetector = _mainCamera.GetComponent<CameraTargetsDetector>();
                //_postProcessVolume = _camera.GetComponent<PostProcessVolume>();
                //_postProcessVolume.profile.TryGetSettings<DepthOfField>(out _depthOfField);
            } while (_mainCamera == null || _cameraMeshCollider == null || _cameraTargetsDetector == null);

            _isCameraInitialized = true;
        }
    }

    private void ResetCameraValues()
    {
        _mainCamera.fieldOfView = SettingsManager.Instance.Database.FieldOfView;
        _depthOfField.focusDistance.value = 10f;
        _depthOfField.aperture.value = 5.6f;
        _depthOfField.focalLength.value = 50f;
    }

    private void ResetCamera(InputAction.CallbackContext context)
    {
        if (_isTakingPicture)
            return;

        ResetCameraValues();
    }

    private void TakePicture(InputAction.CallbackContext context)
    {
        if (_isTakingPicture)
            return;

        AudioManager.Instance.PlayPhotoSound(_camera.gameObject);

        LookForTargetsOnCamera();

        EventHolder.Instance.onPictureTaken?.Invoke();
        _isTakingPicture = true;
    }

    private void ResetIsTakingPicture()
    {
        _isTakingPicture = false;
    }

    private void LookForTargetsOnCamera()
    {
        CalculateFrustumBounds(out Vector3 center, out Vector3 halfExtents);
        Collider[] collidersInsideBox = GetPhotographableObjectsCollidersInsideBox(center, halfExtents);
        List<Collider> collidersList = GetFilteredCollidersInsideFrustum(collidersInsideBox);
        List<Collider> sortedCollidersList = SortListByScreenCoveragePercentage(collidersList);
        CheckTargetsOnCamera(GetCameraTargetHolders(sortedCollidersList));
    }

    private void CalculateFrustumBounds(out Vector3 center, out Vector3 halfExtents)
    {
        float fov = _mainCamera.fieldOfView * Mathf.Deg2Rad;
        float near = _mainCamera.nearClipPlane;
        float far = _mainCamera.farClipPlane;
        float depth = (far - near) / 2f;

        float heightAtFar = Mathf.Tan(fov / 2f) * far;
        float widthAtFar = heightAtFar * _mainCamera.aspect;

        center = _mainCamera.transform.position + _mainCamera.transform.forward * (near + depth);
        halfExtents = new Vector3(widthAtFar, heightAtFar, depth);
    }

    private Collider[] GetPhotographableObjectsCollidersInsideBox(Vector3 center, Vector3 halfExtents)
    {
        return Physics.OverlapBox(center, halfExtents, _mainCamera.transform.rotation, ((InputHandler)_fsm).photographableObjectsLayerMask);
    }

    private List<Collider> GetFilteredCollidersInsideFrustum(Collider[] collidersInsideBox)
    {
        Plane[] frustrumPlanes = GeometryUtility.CalculateFrustumPlanes(_mainCamera);
        List<Collider> collidersList = new List<Collider>();

        foreach (Collider collider in collidersInsideBox)
        {
            Renderer renderer = collider.GetComponentInChildren<Renderer>();

            if (renderer == null)
                continue;

            if (!GeometryUtility.TestPlanesAABB(frustrumPlanes, renderer.bounds))
                continue;
            // else ( it's inside )

            collidersList.Add(collider);
        }

        return collidersList;
    }

    private List<Collider> SortListByScreenCoveragePercentage(List<Collider> collidersList)
    {
        Dictionary<Collider, float> coverageByCollider = new Dictionary<Collider, float>();

        foreach (Collider collider in collidersList)
        {
            Renderer renderer = collider.GetComponentInChildren<Renderer>();
            float coverage = GetScreenCoveragePercentage(renderer.bounds, _mainCamera);
            coverageByCollider[collider] = coverage;
        }

        var orderedPairs = coverageByCollider.OrderByDescending(pair => pair.Value);
        List<Collider> sortedColliders = new List<Collider>();

        foreach (var pair in orderedPairs)
        {
            sortedColliders.Add(pair.Key);
        }

        return sortedColliders;
    }

    private float GetScreenCoveragePercentage(Bounds bounds, Camera camera)
    {
        Vector3[] corners = new Vector3[8];
        Vector3 center = bounds.center;
        Vector3 extents = bounds.extents;

        // Building Bounding Box corners
        corners[0] = _mainCamera.WorldToScreenPoint(center + new Vector3(extents.x, extents.y, extents.z));
        corners[1] = _mainCamera.WorldToScreenPoint(center + new Vector3(extents.x, extents.y, -extents.z));
        corners[2] = _mainCamera.WorldToScreenPoint(center + new Vector3(extents.x, -extents.y, extents.z));
        corners[3] = _mainCamera.WorldToScreenPoint(center + new Vector3(extents.x, -extents.y, -extents.z));
        corners[4] = _mainCamera.WorldToScreenPoint(center + new Vector3(-extents.x, extents.y, extents.z));
        corners[5] = _mainCamera.WorldToScreenPoint(center + new Vector3(-extents.x, extents.y, -extents.z));
        corners[6] = _mainCamera.WorldToScreenPoint(center + new Vector3(-extents.x, -extents.y, extents.z));
        corners[7] = _mainCamera.WorldToScreenPoint(center + new Vector3(-extents.x, -extents.y, -extents.z));

        float minX = float.MaxValue;
        float minY = float.MaxValue;
        float maxX = float.MinValue;
        float maxY = float.MinValue;

        foreach (var corner in corners)
        {
            minX = Mathf.Min(minX, corner.x);
            minY = Mathf.Min(minY, corner.y);
            maxX = Mathf.Max(maxX, corner.x);
            maxY = Mathf.Max(maxY, corner.y);
        }

        float visibleMinX = Mathf.Clamp(minX, 0, Screen.width);
        float visibleMaxX = Mathf.Clamp(maxX, 0, Screen.width);
        float visibleMinY = Mathf.Clamp(minY, 0, Screen.height);
        float visibleMaxY = Mathf.Clamp(maxY, 0, Screen.height);

        float visibleWidth = Mathf.Max(0, visibleMaxX - visibleMinX);
        float visibleHeight = Mathf.Max(0, visibleMaxY - visibleMinY);
        float visibleArea = visibleWidth * visibleHeight;

        float screenArea = Screen.width * Screen.height;

        return visibleArea / screenArea;
    }

    private List<CameraTargetHolder> GetCameraTargetHolders(List<Collider> collidersList)
    {
        List<CameraTargetHolder> cameraTargetHoldersList = new List<CameraTargetHolder>();

        foreach (Collider collider in collidersList)
        {
            IPhotographable photographable = collider.GetComponent<IPhotographable>();
            if (photographable != null)
                cameraTargetHoldersList.Add((CameraTargetHolder)photographable);
        }

        return cameraTargetHoldersList;
    }

    public void CheckTargetsOnCamera(List<CameraTargetHolder> cameraTargetHoldersList)
    {
        //_cameraMeshCollider.enabled = false;

        Target target = Target.None;

        foreach(CameraTargetHolder cameraTargetHolder in cameraTargetHoldersList)
        {
            if(cameraTargetHolder.GetCameraTarget().DoesRayHit(_mainCamera, cameraTargetHolder.CheckPoints, cameraTargetHolder.transform))
            {
                DebugManager.Instance.DebugCameraSystemMessage(cameraTargetHolder.GetCameraTarget().GetTarget().ToString() + " captured in camera.");
                target = cameraTargetHolder.GetCameraTarget().GetTarget();
                break;
            }            
        }

        ((InputHandler)_fsm).ScreenshotTarget = target;
        ScreenshotManager.Instance.ScreenshotTarget = target;

        //if(!target.Equals(Target.None))
        //    GameManager.Instance.SetPictureTaken(target);

        //_cameraTargetsDetector.cameraTargetsList.Clear();
        EventHolder.Instance.onPhotoObjectsDetected?.Invoke();
    }

#if UNITY_EDITOR
    public void DrawCameraOverlappingBoxGizmo()
    {
        if (_mainCamera == null)
            return;

        CalculateFrustumBounds(out Vector3 center, out Vector3 halfExtents);

        Gizmos.color = new Color(0f, 1f, 0f, 0.25f); // Verde translúcido
        Gizmos.matrix = Matrix4x4.TRS(center, _mainCamera.transform.rotation, Vector3.one);
        Gizmos.DrawCube(Vector3.zero, halfExtents * 2f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2f);
    }
#endif

    public override void EnableInputs()
    {
        base.EnableInputs();

        _zoom = _inputActions.CameraMode.Zoom;
        _inputActions.CameraMode.TakePicture.started += TakePicture;
        _inputActions.CameraMode.ChangeSetting.started += ChangeSetting;
        _inputActions.CameraMode.ResetCamera.started += ResetCamera;

        _inputActions.CameraMode.Enable();
    }

    private enum CameraSetting { FocusDistance, Aperture, FocalLength }
}
