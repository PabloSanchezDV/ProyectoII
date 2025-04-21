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
    [SerializeField] private TextMeshProUGUI _animalText;
    [SerializeField] private TextMeshProUGUI _actionText;
    
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

        EventHolder.Instance.onCameraStateEnter.AddListener(PlayCameraModeEnterTransition);
        EventHolder.Instance.onCameraStateExit.AddListener(PlayCameraModeExitTransition);
        EventHolder.Instance.onZoomChange.AddListener(MoveZoomArrow);
        EventHolder.Instance.onPictureTaken.AddListener(PlayFlashEffect);
    }

    private void InitializeUI()
    {
        _anim.enabled = true;
    }

    private void PlayCameraModeEnterTransition()
    {
        MoveZoomArrow();
        _anim.SetTrigger("TriggerCamera");
    }

    private void PlayCameraModeExitTransition()
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

    public void UpdateAnimalPicture(string animal, string action)
    {
        _animalText.text = animal;
        _actionText.text = action;
        StartCoroutine(UpdatePicture());
    }

    IEnumerator UpdatePicture()
    {
        yield return new WaitForEndOfFrame();
        _picture.sprite = ScreenshotManager.Instance.LastPictureSprite;
    }

    #region AnimationMethods
    public void SetCameraFOV()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).speed == 1)
            _mainCamera.fieldOfView = _inputHandler.zoomUpperLimit - _inputHandler.zoomLowerLimit;
        else
            _mainCamera.fieldOfView = 60f;
    }

    public void ShowPicture()
    {
        _pictureFrameAnim.SetTrigger("ShowPicture");
    }
    #endregion
}
