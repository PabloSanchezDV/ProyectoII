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
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _actionText;

    [Header("FreeMove")]
    [SerializeField] private TextMeshProUGUI _clockText;
    [SerializeField] private RectTransform _hourHand;
    [SerializeField] private RectTransform _minuteHand;
    
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

    #region Private Methods
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

    private IEnumerator UpdatePicture()
    {
        yield return new WaitForEndOfFrame();
        _picture.sprite = ScreenshotManager.Instance.LastPictureSprite;
    }
    #endregion

    #region Public Methods
    public void UpdatePicture(string name, string action = " ")
    {
        _nameText.text = name;
        _actionText.text = action;
        StartCoroutine(UpdatePicture());
    }

    public void UpdateClock(float daytime)
    {
        float hours = daytime / 60f;
        float minutes = daytime % 60f;

        //Debug.Log($"{Mathf.FloorToInt(hours):D2}:{Mathf.FloorToInt(minutes):D2}");

        _hourHand.localEulerAngles = new Vector3(0f, 0f, -(hours % 12 * 30f));
        _minuteHand.localEulerAngles = new Vector3(0f, 0f, -(minutes * 6f));
    }

    public void TriggerFadeOut()
    {
        _anim.SetTrigger("FadeOut");
    }
    #endregion

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

    public void FadeOutComplete()
    {
        GameManager.Instance.EndDay();
    }
    #endregion
}
