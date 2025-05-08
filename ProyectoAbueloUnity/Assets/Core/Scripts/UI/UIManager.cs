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
        return new Vector2(108, MapCameraFovToPosition(fov));
    }

    private float MapCameraFovToPosition(float fov)
    {
        return 216f + (fov - _inputHandler.zoomLowerLimit) * ((-112f) - 216f) / (_inputHandler.zoomUpperLimit - _inputHandler.zoomLowerLimit);
    }

    private string GetAnimalName(Animal animal)
    {
        switch (animal)
        {
            case(Animal.Mammoth):
                return "Mamut";
            case (Animal.Elk):
                return "Alce";
            case (Animal.Bird):
                return "Pájaro";
            case (Animal.Ornito):
                return "Ornito";
            case (Animal.None):
                return "";
            default:
                throw new System.Exception("Cannot get animal name. Check animal name is properly set.");
        }
    }

    private string GetAnimalAction(Animal animal, Action action)
    {
        switch (animal)
        {
            case (Animal.Mammoth):
                return GetMammothAction(action);
            case (Animal.Elk):
                return GetElkAction(action);
            case (Animal.Bird):
                return GetBirdAction(action);
            case (Animal.Ornito):
                return GetOrnitoAction(action);
            case (Animal.None):
                return "";
            default:
                throw new System.Exception("Cannot get animal action. Check animal name and action are properly set.");
        }
    }

    #region Animal Actions
    private string GetMammothAction(Action action)
    {
        switch (action)
        {
            case (Action.Walking):
                return "";          // Walking is the default action. It triggers the animal but doesn't trigger any specific state 
            case (Action.Action1):
                return "Comiendo";
            case (Action.Action2):
                return "Durmiendo";
            case (Action.Action3):
                return "Placando árbol";
            case (Action.Action4):
                return "Bañándose";
            case (Action.Other):
                return "";
            default:
                throw new System.Exception("Cannot get Mammoth action. Check Mammoth action is properly set.");
        }
    }

    private string GetElkAction(Action action)
    {
        switch (action)
        {
            case (Action.Action1):
                return "Comiendo";
            case (Action.Action2):
                return "Agitando los adornos";
            case (Action.Action3):
                return "Berreando";
            case (Action.Action4):
                return "Presumiendo";
            case (Action.Other):
                return "";
            default:
                throw new System.Exception("Cannot get Elk action. Check Elk action is properly set.");
        }
    }

    private string GetBirdAction(Action action)
    {
        switch (action)
        {
            case (Action.Action1):
                return "Comiendo";
            case (Action.Action2):
                return "Tragando piedras";
            case (Action.Action3):
                return "Picoteando al mamut";
            case (Action.Action4):
                return "Volando";
            case (Action.Other):
                return "";
            default:
                throw new System.Exception("Cannot get Bird action. Check Bird action is properly set.");
        }
    }

    private string GetOrnitoAction(Action action)
    {
        switch (action)
        {
            case (Action.Action1):
                return "Comiendo";
            case (Action.Action2):
                return "Nadando";
            case (Action.Action3):
                return "Tomando el sol";
            case (Action.Action4):
                return "Protegiendo el nido";
            case (Action.Other):
                return "";
            default:
                throw new System.Exception("Cannot get Ornito action. Check Ornito action is properly set.");
        }
    }
    #endregion

    private IEnumerator UpdatePicture()
    {
        yield return new WaitForEndOfFrame();
        _picture.sprite = ScreenshotManager.Instance.LastPictureSprite;
    }
    #endregion

    #region Public Methods
    public void UpdateAnimalPicture(Animal animal, Action action = Action.Other)
    {
        _animalText.text = GetAnimalName(animal);
        _actionText.text = GetAnimalAction(animal, action);
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
