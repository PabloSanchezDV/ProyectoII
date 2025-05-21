using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("References")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private InputHandler _inputHandler;

    [Header("Camera")]
    [SerializeField] private RectTransform _zoomArrow;
    [SerializeField] private Animator _pictureFrameAnim;
    [SerializeField] private Image _picture;
    [SerializeField] private Animator _notificationAnim;

    [Header("Notebook")]
    [SerializeField] private RectTransform _notebookCursor;
    [SerializeField] private float _notebookCursorRaycastDistance;
    [SerializeField] private LayerMask _notebookCursorRaycastLayerMask;
    
    private Animator _anim;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();

        InitializeUI();

        EventHolder.Instance.onZoomChange.AddListener(MoveZoomArrow);
        EventHolder.Instance.onPictureTaken.AddListener(PlayFlashEffect);
        EventHolder.Instance.onScreenshotTaken.AddListener(UpdatePicture);
    }

    #region Private Methods
    private void InitializeUI()
    {
        _anim.enabled = true;
    }

    public void PlayCameraModeEnterTransition()
    {
        _anim.SetTrigger("TriggerCamera");
        MoveZoomArrow();
    }

    public void PlayCameraModeExitTransition()
    {
        _anim.SetTrigger("TriggerCamera");
    }

    private void PlayFlashEffect()
    {
        _anim.SetTrigger("TakePicture");
    }

    private void MoveZoomArrow()
    {
        _zoomArrow.anchoredPosition = GetZoomArrowPosition(_mainCamera.fieldOfView);
    }

    private Vector2 GetZoomArrowPosition(float fov)
    {
        return new Vector2(90, MapCameraFovToPosition(fov));
    }

    private float MapCameraFovToPosition(float fov)
    {
        return 216f + (fov - _inputHandler.zoomLowerLimit) * ((-112f) - 216f) / (_inputHandler.zoomUpperLimit - _inputHandler.zoomLowerLimit);
    }
    #endregion

    #region Public Methods
    public void UpdatePicture()
    {
        _picture.sprite = ScreenshotManager.Instance.LastPictureSprite;
    }

    public void TriggerNotification()
    {
        _notificationAnim.SetTrigger("NewNotebookEntry");
    }

    public void TriggerFadeOut()
    {
        _anim.SetTrigger("FadeOut");
    }

    public Vector3 GetCursorWorldPosition()
    {
        Ray cursorRay = _mainCamera.ScreenPointToRay(_notebookCursor.position);

        if (Physics.Raycast(cursorRay, out RaycastHit hitInfo, _notebookCursorRaycastDistance, _notebookCursorRaycastLayerMask)) 
        {
            return hitInfo.point;
        }

        return Vector3.negativeInfinity;
    }

    public NotebookCursorTarget GetNotebookCursorTarget()
    {
        Ray cursorRay = _mainCamera.ScreenPointToRay(_notebookCursor.position);

        if (Physics.Raycast(cursorRay, out RaycastHit hitInfo, _notebookCursorRaycastDistance, _notebookCursorRaycastLayerMask))
        {
            if(hitInfo.collider.gameObject.GetComponent<NotebookCursorTarget>() != null)
                return hitInfo.collider.gameObject.GetComponent<NotebookCursorTarget>();
        }

        return null;
    }

    public void UpdateCursorPosition(Vector2 cursorPosition)
    {
        cursorPosition = new Vector2(Mathf.Clamp(cursorPosition.x, 0, Screen.width), Mathf.Clamp(cursorPosition.y, 0, Screen.height));
        _notebookCursor.position = cursorPosition;
    }

    public void ShowCursor()
    {
        _notebookCursor.gameObject.SetActive(true);
    }

    public void HideCursor()
    {
        if(_notebookCursor != null)
            _notebookCursor.gameObject.SetActive(false);
    }

    public Vector2 GetCursorPosition()
    {
        return (Vector2)_notebookCursor.position;
    }
    #endregion

    #region AnimationMethods
    public void SetCameraFOV()
    {
        _mainCamera.fieldOfView = _inputHandler.zoomUpperLimit - _inputHandler.zoomLowerLimit;
    }

    public void ShowPicture()
    {
        _pictureFrameAnim.SetTrigger("ShowPicture");
    }

    public void FadeOutComplete()
    {
        GameManager.Instance.EndDay();
    }
    #endregion
}
