using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class InputHandler : FSMTemplateMachine
{
    #region States
    [NonSerialized] public FreeMove freeMove;
    [NonSerialized] public CameraMode cameraMode;
    [NonSerialized] public Map mapPage;
    #region Animal Pages
    [NonSerialized] public Mammoth mammothPage;
    [NonSerialized] public Elk elkPage;
    [NonSerialized] public Ornito ornitoPage;
    #endregion
    #region Plants Pages
    [NonSerialized] public Plants1 plantsPage1;
    [NonSerialized] public Plants2 plantsPage2;
    [NonSerialized] public Plants3 plantsPage3;
    #endregion
    [NonSerialized] public Fungus fungusPage;
    [NonSerialized] public Bugs bugsPage;
    #region Gallery Pages
    [NonSerialized] public Gallery1 galleryPage1;
    [NonSerialized] public Gallery2 galleryPage2;
    [NonSerialized] public Gallery3 galleryPage3;
    [NonSerialized] public Gallery4 galleryPage4;
    #endregion
    [NonSerialized] public Settings settingsPage;
    #endregion

    #region Fields

    #region References
    [Header("References")]
    [SerializeField] private Animator _playerAnim;
    [SerializeField] private Animator _armsAnim;
    [SerializeField] private Animator _notebookAnim;
    [SerializeField] private Animator _mapAnim;
    [SerializeField] private SkinnedMeshRenderer _armsRenderer;
    [SerializeField] private SkinnedMeshRenderer _notebookRenderer;
    [SerializeField] private SkinnedMeshRenderer _mapRenderer;
    [SerializeField] private GameObject _clock;
    [SerializeField] private GameObject _minuteHand;
    [SerializeField] private GameObject _hourHand;
    [SerializeField] private GameObject _polaroid;
    [SerializeField] private Sprite _polaroidPictureSprite;
    #endregion

    #region Parameters -  Free Move
    [Header("Parameters -  Free Move")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float maxMoveSpeed;
    [SerializeField] public float maxMoveSpeedCameraMode;
    [SerializeField] public float interactiveRaycastDistance;
    [SerializeField] public float timeBetweenSteps;
    [SerializeField] public LayerMask playerLayerMask;
    #endregion

    #region Parameters -  Camera Mode
    [Header("Parameters -  Camera Mode")]
    [SerializeField] public float zoomModifier;
    [SerializeField] public float zoomUpperLimit;
    [SerializeField] public float zoomLowerLimit;
    [SerializeField] public float focusDistanceChangeSpeedModifier;
    [SerializeField] public float apertureChangeSpeedModifier;
    [SerializeField] public float focalLengthChangeSpeedModifier;
    #endregion

    #region References -  Notebook
    [Header("References -  Notebook")]
    [SerializeField] private MapCT _mapCT;
    [SerializeField] private PostItCT _postIt;
    [SerializeField] private Transform _upperCoverTransform;
    [SerializeField] private Transform _notebookPageTransform;
    [SerializeField] private Transform _lowerCoverTransform;
    [SerializeField] private Transform[] _pagePostIts;
    #endregion

    #region References - Notebook Pages
    [Header("References - Notebook Pages")]
    [SerializeField] private GameObject[] _mammothPageObjects;
    [SerializeField] private GameObject[] _elkPageObjects;
    [SerializeField] private GameObject[] _ornitoPageObjects;
    [SerializeField] private GameObject[] _plantsPage1Objects;
    [SerializeField] private GameObject[] _plantsPage2Objects;
    [SerializeField] private GameObject[] _plantsPage3Objects;
    [SerializeField] private GameObject[] _fungusPageObjects;
    [SerializeField] private GameObject[] _bugsPageObjects;
    [SerializeField] private GameObject[] _galleryPage1Objects;
    [SerializeField] private GameObject[] _galleryPage2Objects;
    [SerializeField] private GameObject[] _galleryPage3Objects;
    [SerializeField] private GameObject[] _galleryPage4Objects;
    [SerializeField] private GameObject[] _settingsPageObjects;
    #endregion

    #region References - Polaroid Pictures
    [Header("References - Polaroid Pictures")]
    [SerializeField] private SpriteRenderer _mammothGlobalPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _mammothEatPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _mammothSleepPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _mammothHeadbuttPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _mammothShakePictureSpriteRenderer;
    [Space]
    [SerializeField] private SpriteRenderer _elkGlobalPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _elkEatPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _elkShakePictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _elkGrowlPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _elkShowOffPictureSpriteRenderer;
    [Space]
    [SerializeField] private SpriteRenderer _ornitoGlobalPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _ornitoEatPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _ornitoSwimPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _ornitoSunbathingPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _ornitoProtectPictureSpriteRenderer;
    [Space]
    [SerializeField] private SpriteRenderer _chestnutTreePictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _birchTreePictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _heatherPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _gorsePictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _hollyPictureSpriteRenderer;
    [Space]
    [SerializeField] private SpriteRenderer _rushPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _dandelionPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _cloverPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _sprucePictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _cypressPictureSpriteRenderer;
    [Space]
    [SerializeField] private SpriteRenderer _fernPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _redChestnutTreePictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _beechTreePictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _willowTreePictureSpriteRenderer;
    [Space]
    [SerializeField] private SpriteRenderer _fairyRingPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _amanitaPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _parasolPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _goldenChanterellePictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _boletusPictureSpriteRenderer;
    [Space]
    [SerializeField] private SpriteRenderer _moonBeatlePictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _fireflyPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _butterflyPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _dragonflyPictureSpriteRenderer;
    [SerializeField] private SpriteRenderer _beePictureSpriteRenderer;

    [SerializeField] private SpriteRenderer[] _galleryPicturesSpriteRenderers = new SpriteRenderer[64];
    #endregion

    #region References - Game Objects to Activate on PictureTaken
    [Header("References - Game Objects to Activate on PictureTaken")]
    [SerializeField] private GameObject[] _mammothGlobalPictureGOs;
    [SerializeField] private GameObject[] _mammothEatPictureGOs;
    [SerializeField] private GameObject[] _mammothSleepPictureGOs;
    [SerializeField] private GameObject[] _mammothHeadbuttPictureGOs;
    [SerializeField] private GameObject[] _mammothShakePictureGOs;
    [Space]
    [SerializeField] private GameObject[] _elkGlobalPictureGOs;
    [SerializeField] private GameObject[] _elkEatPictureGOs;
    [SerializeField] private GameObject[] _elkShakePictureGOs;
    [SerializeField] private GameObject[] _elkGrowlPictureGOs;
    [SerializeField] private GameObject[] _elkShowOffPictureGOs;
    [Space]
    [SerializeField] private GameObject[] _ornitoGlobalPictureGOs;
    [SerializeField] private GameObject[] _ornitoEatPictureGOs;
    [SerializeField] private GameObject[] _ornitoSwimPictureGOs;
    [SerializeField] private GameObject[] _ornitoSunbathingPictureGOs;
    [SerializeField] private GameObject[] _ornitoProtectPictureGOs;
    [Space]
    [SerializeField] private GameObject[] _chestnutTreePictureGOs;
    [SerializeField] private GameObject[] _birchTreePictureGOs;
    [SerializeField] private GameObject[] _heatherPictureGOs;
    [SerializeField] private GameObject[] _gorsePictureGOs;
    [SerializeField] private GameObject[] _hollyPictureGOs;
    [Space]
    [SerializeField] private GameObject[] _rushPictureGOs;
    [SerializeField] private GameObject[] _dandelionPictureGOs;
    [SerializeField] private GameObject[] _cloverPictureGOs;
    [SerializeField] private GameObject[] _sprucePictureGOs;
    [SerializeField] private GameObject[] _cypressPictureGOs;
    [Space]
    [SerializeField] private GameObject[] _fernPictureGOs;
    [SerializeField] private GameObject[] _redChestnutTreePictureGOs;
    [SerializeField] private GameObject[] _beechTreePictureGOs;
    [SerializeField] private GameObject[] _willowTreePictureGOs;
    [Space]
    [SerializeField] private GameObject[] _fairyRingPictureGOs;
    [SerializeField] private GameObject[] _amanitaPictureGOs;
    [SerializeField] private GameObject[] _parasolPictureGOs;
    [SerializeField] private GameObject[] _goldenChanterellePictureGOs;
    [SerializeField] private GameObject[] _boletusPictureGOs;
    [Space]
    [SerializeField] private GameObject[] _moonBeatlePictureGOs;
    [SerializeField] private GameObject[] _fireflyPictureGOs;
    [SerializeField] private GameObject[] _butterflyPictureGOs;
    [SerializeField] private GameObject[] _dragonflyPictureGOs;
    [SerializeField] private GameObject[] _beePictureGOs;

    [SerializeField] private GameObject[] _galleryPicturesGOs = new GameObject[64];
    #endregion

    private InputActions _inputActions;
    private GameObject _player;
    private GameObject _camera;

    private Collider _mapCollider;
    private NotebookPage _currentNotebookPage = NotebookPage.None;
    private AnimalsPage _currentAnimalsPage;
    private AnimalsPage _animalsPageToGo;
    private PlantsPage _currentPlantsPage;
    private PlantsPage _plantsPageToGo;
    private GalleryPage _currentGalleryPage;
    private GalleryPage _galleryPageToGo;

    private GameObject[] _turnOffAfterTurningPageGOs;
    private bool _isTurningNotebookPageToRight;
    private bool _isClosingNotebook;
    private System.Action _resetAction;
    private Target _screenshotTarget = Target.None;
    #endregion

    #region Properties
    public GameObject Player {  get { return _player; } }
    public GameObject Camera { get  { return _camera; } set { _camera = value; } }
    public Animator PlayerAnim { get { return _playerAnim; } }
    public Animator ArmsAnim { get { return _armsAnim; } }
    public Animator NotebookAnim { get { return _notebookAnim; } }
    public Animator MapAnim { get { return _mapAnim; } }

    public SkinnedMeshRenderer ArmsRenderer {  get { return _armsRenderer; } }
    public SkinnedMeshRenderer NotebookRenderer { get { return _notebookRenderer; } }
    public SkinnedMeshRenderer MapRenderer { get { return _mapRenderer; } }

    public GameObject Clock { get { return _clock; } }
    public GameObject MinuteHand { get { return _minuteHand; } }
    public GameObject HourHand { get { return _hourHand; } }
    public GameObject Polaroid { get { return _polaroid; } }
    public Sprite PolaroidPictureSprite { get { return _polaroidPictureSprite; } }

    public Collider MapCollider { get { return _mapCollider; } }
    public MapCT MapCT {  get { return _mapCT; } }

    public Transform UpperCoverTransform {  get { return _upperCoverTransform; } }
    public Transform NotebookPageTransform { get { return _notebookPageTransform; } }
    public Transform LowerCoverTransform { get { return _lowerCoverTransform; } }

    public Transform[] PagePostIts {  get { return _pagePostIts; } }

    public GameObject[] MammothPageGOs { get { return _mammothPageObjects; } }
    public GameObject[] ElkPageGOs { get { return _elkPageObjects; } }
    public GameObject[] OrnitoPageGOs { get { return _ornitoPageObjects; } }   
    
    public GameObject[] PlantsPage1GOs { get { return _plantsPage1Objects; } }    
    public GameObject[] PlantsPage2GOs { get { return _plantsPage2Objects; } }    
    public GameObject[] PlantsPage3GOs { get { return _plantsPage3Objects; } }   
    
    public GameObject[] FungusPageGOs { get { return _fungusPageObjects; } }    
    public GameObject[] BugsPageGOs { get { return _bugsPageObjects; } }    

    public GameObject[] GalleryPage1GOs { get { return _galleryPage1Objects; } }
    public GameObject[] GalleryPage2GOs { get { return _galleryPage2Objects; } }
    public GameObject[] GalleryPage3GOs { get { return _galleryPage3Objects; } }
    public GameObject[] GalleryPage4GOs { get { return _galleryPage4Objects; } }

    public GameObject[] SettingsPageGOs { get { return _settingsPageObjects; } }

    public NotebookPage CurrentNotebookPage {  get { return _currentNotebookPage; } set { _currentNotebookPage = value; } }
    public AnimalsPage CurrentAnimalsPage { get { return _currentAnimalsPage; } set { _currentAnimalsPage = value; } }
    public AnimalsPage AnimalsPageToGo { get { return _animalsPageToGo; } set { _animalsPageToGo = value; } }
    public PlantsPage CurrentPlantsPage { get { return _currentPlantsPage; } set { _currentPlantsPage = value; } }
    public PlantsPage PlantsPageToGo { get { return _plantsPageToGo; } set { _plantsPageToGo = value; } }
    public GalleryPage CurrentGalleryPage { get { return _currentGalleryPage; } set { _currentGalleryPage = value; } }
    public GalleryPage GalleryPageToGo { get { return _galleryPageToGo; } set { _galleryPageToGo = value; } }

    public GameObject[] TurnOffAfterTurningPageGOs { get { return _turnOffAfterTurningPageGOs; } set { _turnOffAfterTurningPageGOs = value; } }
    public bool IsTurningNotebookPageToRight { get { return _isTurningNotebookPageToRight; } set { _isTurningNotebookPageToRight = value; } }

    public bool IsClosingNotebook { get { return _isClosingNotebook; } set { _isClosingNotebook = value; } }

    public System.Action ResetAction { get { return _resetAction; } set { _resetAction = value; } }

    public Target ScreenshotTarget { get { return _screenshotTarget; } set { _screenshotTarget = value; } }

    #region Picture Sprite Renderer

    #region Mammoth
    public SpriteRenderer MammothGlobalPictureSpriteRenderer { get { return _mammothGlobalPictureSpriteRenderer; } }
    public SpriteRenderer MammothEatPictureSpriteRenderer { get { return _mammothEatPictureSpriteRenderer; } }
    public SpriteRenderer MammothSleepPictureSpriteRenderer { get { return _mammothSleepPictureSpriteRenderer; } }
    public SpriteRenderer MammothHeadbuttPictureSpriteRenderer { get { return _mammothHeadbuttPictureSpriteRenderer; } }
    public SpriteRenderer MammothShakePictureSpriteRenderer { get { return _mammothShakePictureSpriteRenderer; } }
    #endregion

    #region Elk
    public SpriteRenderer ElkGlobalPictureSpriteRenderer { get { return _elkGlobalPictureSpriteRenderer; } }
    public SpriteRenderer ElkEatPictureSpriteRenderer { get { return _elkEatPictureSpriteRenderer; } }
    public SpriteRenderer ElkShakePictureSpriteRenderer { get { return _elkShakePictureSpriteRenderer; } }
    public SpriteRenderer ElkGrowlPictureSpriteRenderer { get { return _elkGrowlPictureSpriteRenderer; } }
    public SpriteRenderer ElkShowOffPictureSpriteRenderer { get { return _elkShowOffPictureSpriteRenderer; } }
    #endregion

    #region Ornito
    public SpriteRenderer OrnitoGlobalPictureSpriteRenderer { get { return _ornitoGlobalPictureSpriteRenderer; } }
    public SpriteRenderer OrnitoEatPictureSpriteRenderer { get { return _ornitoEatPictureSpriteRenderer; } }
    public SpriteRenderer OrnitoSwimPictureSpriteRenderer { get { return _ornitoSwimPictureSpriteRenderer; } }
    public SpriteRenderer OrnitoSunbathingPictureSpriteRenderer { get { return _ornitoSunbathingPictureSpriteRenderer; } }
    public SpriteRenderer OrnitoProtectPictureSpriteRenderer { get { return _ornitoProtectPictureSpriteRenderer; } }
    #endregion

    #region Plants
    public SpriteRenderer ChestnutTreePictureSpriteRenderer { get { return _chestnutTreePictureSpriteRenderer; } }
    public SpriteRenderer BirchTreePictureSpriteRenderer { get { return _birchTreePictureSpriteRenderer; } }
    public SpriteRenderer HeatherPictureSpriteRenderer { get { return _heatherPictureSpriteRenderer; } }
    public SpriteRenderer GorsePictureSpriteRenderer { get { return _gorsePictureSpriteRenderer; } }
    public SpriteRenderer HollyPictureSpriteRenderer { get { return _hollyPictureSpriteRenderer; } }
    public SpriteRenderer RushPictureSpriteRenderer { get { return _rushPictureSpriteRenderer; } }
    public SpriteRenderer DandelionPictureSpriteRenderer { get { return _dandelionPictureSpriteRenderer; } }
    public SpriteRenderer CloverPictureSpriteRenderer { get { return _cloverPictureSpriteRenderer; } }
    public SpriteRenderer SprucePictureSpriteRenderer { get { return _sprucePictureSpriteRenderer; } }
    public SpriteRenderer CypressPictureSpriteRenderer { get { return _cypressPictureSpriteRenderer; } }
    public SpriteRenderer FernPictureSpriteRenderer { get { return _fernPictureSpriteRenderer; } }
    public SpriteRenderer RedChestnutTreePictureSpriteRenderer { get { return _redChestnutTreePictureSpriteRenderer; } }
    public SpriteRenderer BeechTreePictureSpriteRenderer { get { return _beechTreePictureSpriteRenderer; } }
    public SpriteRenderer WillowTreePictureSpriteRenderer { get { return _willowTreePictureSpriteRenderer; } }
    public SpriteRenderer FairyRingPictureSpriteRenderer { get { return _fairyRingPictureSpriteRenderer; } }
    public SpriteRenderer AmanitaPictureSpriteRenderer { get { return _amanitaPictureSpriteRenderer; } }
    public SpriteRenderer ParasolPictureSpriteRenderer { get { return _parasolPictureSpriteRenderer; } }
    public SpriteRenderer GoldenChanterellePictureSpriteRenderer { get { return _goldenChanterellePictureSpriteRenderer; } }
    public SpriteRenderer BoletusPictureSpriteRenderer { get { return _boletusPictureSpriteRenderer; } }
    #endregion

    #region Bugs
    public SpriteRenderer MoonBeatlePictureSpriteRenderer { get { return _moonBeatlePictureSpriteRenderer; } }
    public SpriteRenderer FireflyPictureSpriteRenderer { get { return _fireflyPictureSpriteRenderer; } }
    public SpriteRenderer ButterflyPictureSpriteRenderer { get { return _butterflyPictureSpriteRenderer; } }
    public SpriteRenderer DragonflyPictureSpriteRenderer { get { return _dragonflyPictureSpriteRenderer; } }
    public SpriteRenderer BeePictureSpriteRenderer { get { return _beePictureSpriteRenderer; } }
    #endregion

    public SpriteRenderer[] GalleryPicturesSpriteRenderers { get { return _galleryPicturesSpriteRenderers; } }

    #region Game Objects to Activate on PictureTaken

    #region Mammoth
    public GameObject[] MammothGlobalPictureGOs { get { return _mammothGlobalPictureGOs; } }
    public GameObject[] MammothEatPictureGOs { get { return _mammothEatPictureGOs; } }
    public GameObject[] MammothSleepPictureGOs { get { return _mammothSleepPictureGOs; } }
    public GameObject[] MammothHeadbuttPictureGOs { get { return _mammothHeadbuttPictureGOs; } }
    public GameObject[] MammothShakePictureGOs { get { return _mammothShakePictureGOs; } }
    #endregion

    #region Elk
    public GameObject[] ElkGlobalPictureGOs { get { return _elkGlobalPictureGOs; } }
    public GameObject[] ElkEatPictureGOs { get { return _elkEatPictureGOs; } }
    public GameObject[] ElkShakePictureGOs { get { return _elkShakePictureGOs; } }
    public GameObject[] ElkGrowlPictureGOs { get { return _elkGrowlPictureGOs; } }
    public GameObject[] ElkShowOffPictureGOs { get { return _elkShowOffPictureGOs; } }
    #endregion

    #region Ornito
    public GameObject[] OrnitoGlobalPictureGOs { get { return _ornitoGlobalPictureGOs; } }
    public GameObject[] OrnitoEatPictureGOs { get { return _ornitoEatPictureGOs; } }
    public GameObject[] OrnitoSwimPictureGOs { get { return _ornitoSwimPictureGOs; } }
    public GameObject[] OrnitoSunbathingPictureGOs { get { return _ornitoSunbathingPictureGOs; } }
    public GameObject[] OrnitoProtectPictureGOs { get { return _ornitoProtectPictureGOs; } }
    #endregion

    #region Plants
    public GameObject[] ChestnutTreePictureGOs { get { return _chestnutTreePictureGOs; } }
    public GameObject[] BirchTreePictureGOs { get { return _birchTreePictureGOs; } }
    public GameObject[] HeatherPictureGOs { get { return _heatherPictureGOs; } }
    public GameObject[] GorsePictureGOs { get { return _gorsePictureGOs; } }
    public GameObject[] HollyPictureGOs { get { return _hollyPictureGOs; } }

    public GameObject[] RushPictureGOs { get { return _rushPictureGOs; } }
    public GameObject[] DandelionPictureGOs { get { return _dandelionPictureGOs; } }
    public GameObject[] CloverPictureGOs { get { return _cloverPictureGOs; } }
    public GameObject[] SprucePictureGOs { get { return _sprucePictureGOs; } }
    public GameObject[] CypressPictureGOs { get { return _cypressPictureGOs; } }

    public GameObject[] FernPictureGOs { get { return _fernPictureGOs; } }
    public GameObject[] RedChestnutTreePictureGOs { get { return _redChestnutTreePictureGOs; } }
    public GameObject[] BeechTreePictureGOs { get { return _beechTreePictureGOs; } }
    public GameObject[] WillowTreePictureGOs { get { return _willowTreePictureGOs; } }

    public GameObject[] FairyRingPictureGOs { get { return _fairyRingPictureGOs; } }
    public GameObject[] AmanitaPictureGOs { get { return _amanitaPictureGOs; } }
    public GameObject[] ParasolPictureGOs { get { return _parasolPictureGOs; } }
    public GameObject[] GoldenChanterellePictureGOs { get { return _goldenChanterellePictureGOs; } }
    public GameObject[] BoletusPictureGOs { get { return _boletusPictureGOs; } }
    #endregion

    #region Bugs
    public GameObject[] MoonBeatlePictureGOs { get { return _moonBeatlePictureGOs; } }
    public GameObject[] FireflyPictureGOs { get { return _fireflyPictureGOs; } }
    public GameObject[] ButterflyPictureGOs { get { return _butterflyPictureGOs; } }
    public GameObject[] DragonflyPictureGOs { get { return _dragonflyPictureGOs; } }
    public GameObject[] BeePictureGOs { get { return _beePictureGOs; } }
    #endregion
    
    public GameObject[] GalleryPicturesGOs { get { return _galleryPicturesGOs; } }

    #endregion

    #endregion

    #endregion

    private void Awake()
    {
        _inputActions = new InputActions();

        freeMove = new FreeMove(this, _inputActions);
        cameraMode = new CameraMode(this, _inputActions);
        mapPage = new Map(this, _inputActions);

        mammothPage = new Mammoth(this, _inputActions);
        elkPage = new Elk(this, _inputActions);
        ornitoPage = new Ornito(this, _inputActions);

        plantsPage1 = new Plants1(this, _inputActions);
        plantsPage2 = new Plants2(this, _inputActions);
        plantsPage3 = new Plants3(this, _inputActions);

        fungusPage = new Fungus(this, _inputActions);
        bugsPage = new Bugs(this, _inputActions);

        galleryPage1 = new Gallery1(this, _inputActions);
        galleryPage2 = new Gallery2(this, _inputActions);
        galleryPage3 = new Gallery3(this, _inputActions);
        galleryPage4 = new Gallery4(this, _inputActions);

        settingsPage = new Settings(this, _inputActions);

        _player = GameObject.FindGameObjectWithTag("Player");
        _camera = GameObject.FindGameObjectWithTag("MainCamera");

        HideArmsNotebook();
        HideArmsCameraMode();

        _mapCollider = _mapAnim.gameObject.GetComponent<Collider>();
        _mapCollider.enabled = false;
        
        foreach(Transform postIt in _pagePostIts)
            postIt.gameObject.SetActive(false);

        _mapCT.postIt = _postIt;

        EventHolder.Instance.onScreenshotTaken.AddListener(SetPictureToPolaroidPicture);
        InitializeGalleryPictures();

        StartCoroutine(WaitUntilGameManagerHasLoadedToLoadPolariodPictures());

        DebugManager.Instance.DebugGlobalSystemMessage("InputHandler initialized");
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventHolder.Instance.onScreenshotTaken.RemoveListener(SetPictureToPolaroidPicture);
    }

    IEnumerator WaitUntilGameManagerHasLoadedToLoadPolariodPictures()
    {
        yield return new WaitUntil(() => GameManager.Instance.HasLoadedData);
        LoadPolaroidPictures();
    }

    protected override void GetInitialState(out FSMTemplateState state)
    {
        state = freeMove;
        freeMove.Enter();
    }

    public void LookForTargetsOnCamera()
    {
        StartCoroutine(WaitUntilPhotoLayerObjectsDetected());
    }

    // A Coroutine is needed as this code is executed after physics operations.
    // The first frame is used just to enable the collider.
    // The second one is used to detect the collisions.
    // Only then we can check the targets added to the list.
    private IEnumerator WaitUntilPhotoLayerObjectsDetected()
    {
        yield return new WaitForEndOfFrame();
        // CameraCollider detects objects in Photo layer
        yield return new WaitForEndOfFrame();
        cameraMode.CheckTargetsOnCamera();
    }

    public void DisableArmsRenderer()
    {
        _armsRenderer.enabled = false;
        _notebookRenderer.enabled = false;
    }

    public NotebookCursorTarget GetCursorTarget()
    {
        return UIManager.Instance.GetNotebookCursorTarget();
    }

    public void GoToPage(NotebookPage notebookPage)
    {
        switch (_currentNotebookPage)
        {
            case NotebookPage.Map:
                mapPage.GoToPage(notebookPage);
                break;
            case NotebookPage.Animals:
                GoToFromAnimalsPage(notebookPage);
                break;
            case NotebookPage.Plants:
                GoToFromPlantsPage(notebookPage);
                break;
            case NotebookPage.Fungus:
                fungusPage.GoToPage(notebookPage);
                break;
            case NotebookPage.Bugs:
                bugsPage.GoToPage(notebookPage);
                break;
            case NotebookPage.Gallery:
                GoToFromGalleryPage(notebookPage);
                break;
            case NotebookPage.Settings:
                settingsPage.GoToPage(notebookPage);
                break;
            default:
                throw new System.Exception("Unable to filter page state. Make sure each PagePostItCT has properly set up PageToGo field.");
        }
    }

    private void GoToFromAnimalsPage(NotebookPage notebookPage)
    {
        switch (_currentAnimalsPage)
        {
            case AnimalsPage.Mammoth:
                mammothPage.GoToPage(notebookPage);
                break;
            case AnimalsPage.Elk:
                elkPage.GoToPage(notebookPage);
                break;
            case AnimalsPage.Ornito:
                ornitoPage.GoToPage(notebookPage);
                break;
            default:
                throw new System.Exception("Unable to filter animal page state.");
        }
    }

    private void GoToFromPlantsPage(NotebookPage notebookPage)
    {
        switch (_currentPlantsPage)
        {
            case PlantsPage.Plants1:
                plantsPage1.GoToPage(notebookPage);
                break;
            case PlantsPage.Plants2:
                plantsPage2.GoToPage(notebookPage);
                break;
            case PlantsPage.Plants3:
                plantsPage3.GoToPage(notebookPage);
                break;
            default:
                throw new System.Exception("Unable to filter plants page state.");
        }
    }

    private void GoToFromGalleryPage(NotebookPage notebookPage)
    {
        switch (_currentGalleryPage)
        {
            case GalleryPage.Gallery1:
                galleryPage1.GoToPage(notebookPage);
                break;
            case GalleryPage.Gallery2:
                galleryPage2.GoToPage(notebookPage);
                break;
            case GalleryPage.Gallery3:
                galleryPage3.GoToPage(notebookPage);
                break;
            case GalleryPage.Gallery4:
                galleryPage4.GoToPage(notebookPage);
                break;
            default:
                throw new System.Exception("Unable to filter gallery page state.");
        }
    }

    public void GoToAnimalsPage(AnimalsPage animalsPage)
    {
        switch (animalsPage)
        {
            case AnimalsPage.Mammoth:
                _animalsPageToGo = AnimalsPage.Mammoth;
                break;
            case AnimalsPage.Elk:
                _animalsPageToGo = AnimalsPage.Elk;
                break;
            case AnimalsPage.Ornito:
                _animalsPageToGo = AnimalsPage.Ornito;
                break;
            default:
                throw new System.Exception("Unable to filter animals page state. Make sure each NextPageCT has properly set up PageToGo field.");
        }
        GoToPage(NotebookPage.Animals);
    }

    public void GoToPlantsPage(PlantsPage plantsPage)
    {
        switch (plantsPage)
        {
            case PlantsPage.Plants1:
                _plantsPageToGo = PlantsPage.Plants1;
                break;
            case PlantsPage.Plants2:
                _plantsPageToGo = PlantsPage.Plants2;
                break;
            case PlantsPage.Plants3:
                _plantsPageToGo = PlantsPage.Plants3;
                break;
            default:
                throw new System.Exception("Unable to filter plants page state. Make sure each NextPageCT has properly set up PageToGo field.");
        }
        GoToPage(NotebookPage.Plants);
    }

    public void GoToGalleryPage(GalleryPage galleryPage)
    {
        switch (galleryPage)
        {
            case GalleryPage.Gallery1:
                _galleryPageToGo = GalleryPage.Gallery1;
                break;
            case GalleryPage.Gallery2:
                _galleryPageToGo = GalleryPage.Gallery2;
                break;
            case GalleryPage.Gallery3:
                _galleryPageToGo = GalleryPage.Gallery3;
                break;
            case GalleryPage.Gallery4:
                _galleryPageToGo = GalleryPage.Gallery4;
                break;
            default:
                throw new System.Exception("Unable to filter gallery page state. Make sure each NextPageCT has properly set up PageToGo field.");
        }
        GoToPage(NotebookPage.Gallery);
    }

    public void DeactivateObjectsAfterTurningPage()
    {
        if(_turnOffAfterTurningPageGOs != null)
        {
            foreach (GameObject gameObject in _turnOffAfterTurningPageGOs)
                gameObject.SetActive(false);
            _turnOffAfterTurningPageGOs = null;
        }
        else
        {
            if(_mapRenderer != null)
                _mapRenderer.enabled = false;
        }

    }

    public void HideArmsCameraMode()
    {
        _armsRenderer.enabled = false;
        _polaroid.GetComponent<Renderer>().enabled = false;
    }

    public void HideArmsNotebook()
    {
        _armsRenderer.enabled = false;
        _notebookRenderer.enabled = false;
        _mapRenderer.enabled = false;
        _clock.GetComponent<MeshRenderer>().enabled = false;
        _minuteHand.GetComponent<MeshRenderer>().enabled = false;
        _hourHand.GetComponent<MeshRenderer>().enabled = false;
    }

    #region Polaroid Pictures Methods
    private void LoadPolaroidPictures()
    {
        if (GameManager.Instance.MammothGlobalPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.MammothGlobal), Target.MammothGlobal);
        }
        if (GameManager.Instance.MammothEatPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.MammothEat), Target.MammothEat);
        }
        if (GameManager.Instance.MammothSleepPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.MammothSleep), Target.MammothSleep);
        }
        if (GameManager.Instance.MammothHeadbuttPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.MammothHeadbutt), Target.MammothHeadbutt);
        }
        if (GameManager.Instance.MammothShakePictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.MammothShake), Target.MammothShake);
        }

        if (GameManager.Instance.ElkGlobalPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.ElkGlobal), Target.ElkGlobal);
        }
        if (GameManager.Instance.ElkEatPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.ElkEat), Target.ElkEat);
        }
        if (GameManager.Instance.ElkShakePictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.ElkShake), Target.ElkShake);
        }
        if (GameManager.Instance.ElkGrowlPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.ElkGrowl), Target.ElkGrowl);
        }
        if (GameManager.Instance.ElkShowOffPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.ElkShowOff), Target.ElkShowOff);
        }

        if (GameManager.Instance.OrnitoGlobalPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.OrnitoGlobal), Target.OrnitoGlobal);
        }
        if (GameManager.Instance.OrnitoEatPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.OrnitoEat), Target.OrnitoEat);
        }
        if (GameManager.Instance.OrnitoSwimPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.OrnitoSwim), Target.OrnitoSwim);
        }
        if (GameManager.Instance.OrnitoSunbathingPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.OrnitoSunbathing), Target.OrnitoSunbathing);
        }
        if (GameManager.Instance.OrnitoProtectPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.OrnitoProtect), Target.OrnitoProtect);
        }

        if (GameManager.Instance.ChestnutTreePictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.ChestnutTree), Target.ChestnutTree);
        }
        if (GameManager.Instance.BirchTreePictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.BirchTree), Target.BirchTree);
        }
        if (GameManager.Instance.HeatherPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.Heather), Target.Heather);
        }
        if (GameManager.Instance.GorsePictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.Gorse), Target.Gorse);
        }
        if (GameManager.Instance.HollyPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.Holly), Target.Holly);
        }

        if (GameManager.Instance.RushPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.Rush), Target.Rush);
        }
        if (GameManager.Instance.DandelionPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.Dandelion), Target.Dandelion);
        }
        if (GameManager.Instance.CloverPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.Clover), Target.Clover);
        }
        if (GameManager.Instance.SprucePictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.Spruce), Target.Spruce);
        }
        if (GameManager.Instance.CypressPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.Cypress), Target.Cypress);
        }

        if (GameManager.Instance.FernPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.Fern), Target.Fern);
        }
        if (GameManager.Instance.RedChestnutTreePictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.RedChestnutTree), Target.RedChestnutTree);
        }
        if (GameManager.Instance.BeechTreePictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.BeechTree), Target.BeechTree);
        }
        if (GameManager.Instance.WillowTreePictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.WillowTree), Target.WillowTree);
        }

        if (GameManager.Instance.FairyRingPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.FairyRing), Target.FairyRing);
        }
        if (GameManager.Instance.AmanitaPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.Amanita), Target.Amanita);
        }
        if (GameManager.Instance.ParasolPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.Parasol), Target.Parasol);
        }
        if (GameManager.Instance.GoldenChanterellePictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.GoldenChanterelle), Target.GoldenChanterelle);
        }
        if (GameManager.Instance.BoletusPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.Boletus), Target.Boletus);
        }

        if (GameManager.Instance.MoonBeatlePictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.MoonBeatle), Target.MoonBeatle);
        }
        if (GameManager.Instance.FireflyPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.Firefly), Target.Firefly);
        }
        if (GameManager.Instance.ButterflyPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.Butterfly), Target.Butterfly);
        }
        if (GameManager.Instance.DragonflyPictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.Dragonfly), Target.Dragonfly);
        }
        if (GameManager.Instance.BeePictureTaken)
        {
            SetPictureToPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(Target.Bee), Target.Bee);
        }
        for(int i = 0; i < GameManager.Instance.GalleryPicturesTaken.Length; i++)
        {
            if (GameManager.Instance.GalleryPicturesTaken[i])
            {
                SetPictureToGalleryPolaroidPicture(ScreenshotManager.Instance.LoadScreenshot(i), i);
            }
        }
    }

    private void SetPictureToPolaroidPicture()
    {
        switch (_screenshotTarget)
        {
            case Target.MammothGlobal:
                _mammothGlobalPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _mammothGlobalPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _mammothGlobalPictureGOs)
                    go.SetActive(true);
                break;
            case Target.MammothEat:
                _mammothEatPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _mammothEatPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _mammothEatPictureGOs)
                    go.SetActive(true);
                break;
            case Target.MammothSleep:
                _mammothSleepPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _mammothSleepPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _mammothSleepPictureGOs)
                    go.SetActive(true);
                break;
            case Target.MammothHeadbutt:
                _mammothHeadbuttPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _mammothHeadbuttPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _mammothHeadbuttPictureGOs)
                    go.SetActive(true);
                break;
            case Target.MammothShake:
                _mammothShakePictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _mammothShakePictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _mammothShakePictureGOs)
                    go.SetActive(true);
                break;
            case Target.ElkGlobal:
                _elkGlobalPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _elkGlobalPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _elkGlobalPictureGOs)
                    go.SetActive(true);
                break;
            case Target.ElkEat:
                _elkEatPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _elkEatPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _elkEatPictureGOs)
                    go.SetActive(true);
                break;
            case Target.ElkShake:
                _elkShakePictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _elkShakePictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _elkShakePictureGOs)
                    go.SetActive(true);
                break;
            case Target.ElkGrowl:
                _elkGrowlPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _elkGrowlPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _elkGrowlPictureGOs)
                    go.SetActive(true);
                break;
            case Target.ElkShowOff:
                _elkShowOffPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _elkShowOffPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _elkShowOffPictureGOs)
                    go.SetActive(true);
                break;
            case Target.OrnitoGlobal:
                _ornitoGlobalPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _ornitoGlobalPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _ornitoGlobalPictureGOs)
                    go.SetActive(true);
                break;
            case Target.OrnitoEat:
                _ornitoEatPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _ornitoEatPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _ornitoEatPictureGOs)
                    go.SetActive(true);
                break;
            case Target.OrnitoSwim:
                _ornitoSwimPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _ornitoSwimPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _ornitoSwimPictureGOs)
                    go.SetActive(true);
                break;
            case Target.OrnitoSunbathing:
                _ornitoSunbathingPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _ornitoSunbathingPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _ornitoSunbathingPictureGOs)
                    go.SetActive(true);
                break;
            case Target.OrnitoProtect:
                _ornitoProtectPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _ornitoProtectPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _ornitoProtectPictureGOs)
                    go.SetActive(true);
                break;
            case Target.ChestnutTree:
                _chestnutTreePictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _chestnutTreePictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _chestnutTreePictureGOs)
                    go.SetActive(true);
                break;
            case Target.BirchTree:
                _birchTreePictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _birchTreePictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _birchTreePictureGOs)
                    go.SetActive(true);
                break;
            case Target.Heather:
                _heatherPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _heatherPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _heatherPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Gorse:
                _gorsePictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _gorsePictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _gorsePictureGOs)
                    go.SetActive(true);
                break;
            case Target.Holly:
                _hollyPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _hollyPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _hollyPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Rush:
                _rushPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _rushPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _rushPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Dandelion:
                _dandelionPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _dandelionPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _dandelionPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Clover:
                _cloverPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _cloverPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _cloverPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Spruce:
                _sprucePictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _sprucePictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _sprucePictureGOs)
                    go.SetActive(true);
                break;
            case Target.Cypress:
                _cypressPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _cypressPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _cypressPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Fern:
                _fernPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _fernPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _fernPictureGOs)
                    go.SetActive(true);
                break;
            case Target.RedChestnutTree:
                _redChestnutTreePictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _redChestnutTreePictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _redChestnutTreePictureGOs)
                    go.SetActive(true);
                break;
            case Target.BeechTree:
                _beechTreePictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _beechTreePictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _beechTreePictureGOs)
                    go.SetActive(true);
                break;
            case Target.WillowTree:
                _willowTreePictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _willowTreePictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _willowTreePictureGOs)
                    go.SetActive(true);
                break;
            case Target.FairyRing:
                _fairyRingPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _fairyRingPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _fairyRingPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Amanita:
                _amanitaPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _amanitaPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _amanitaPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Parasol:
                _parasolPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _parasolPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _parasolPictureGOs)
                    go.SetActive(true);
                break;
            case Target.GoldenChanterelle:
                _goldenChanterellePictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _goldenChanterellePictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _goldenChanterellePictureGOs)
                    go.SetActive(true);
                break;
            case Target.Boletus:
                _boletusPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _boletusPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _boletusPictureGOs)
                    go.SetActive(true);
                break;
            case Target.MoonBeatle:
                _moonBeatlePictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _moonBeatlePictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _moonBeatlePictureGOs)
                    go.SetActive(true);
                break;
            case Target.Firefly:
                _fireflyPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _fireflyPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _fireflyPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Butterfly:
                _butterflyPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _butterflyPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _butterflyPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Dragonfly:
                _dragonflyPictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _dragonflyPictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _dragonflyPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Bee:
                _beePictureSpriteRenderer.sprite = ScreenshotManager.Instance.LastPictureSprite;
                _beePictureSpriteRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                foreach (GameObject go in _beePictureGOs)
                    go.SetActive(true);
                break;
            case Target.None:
                SetPictureToGalleryPolaroidPicture();
                break;
            default:
                throw new WarningException("Unable to filter Target");
        }
    }
    
    public void SetPictureToPolaroidPicture(Sprite sprite, Target target, int galleryIndex = 0)
    {
        switch (target)
        {
            case Target.MammothGlobal:
                _mammothGlobalPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _mammothGlobalPictureGOs)
                    go.SetActive(true);
                break;
            case Target.MammothEat:
                _mammothEatPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _mammothEatPictureGOs)
                    go.SetActive(true);
                break;
            case Target.MammothSleep:
                _mammothSleepPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _mammothSleepPictureGOs)
                    go.SetActive(true);
                break;
            case Target.MammothHeadbutt:
                _mammothHeadbuttPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _mammothHeadbuttPictureGOs)
                    go.SetActive(true);
                break;
            case Target.MammothShake:
                _mammothShakePictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _mammothShakePictureGOs)
                    go.SetActive(true);
                break;
            case Target.ElkGlobal:
                _elkGlobalPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _elkGlobalPictureGOs)
                    go.SetActive(true);
                break;
            case Target.ElkEat:
                _elkEatPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _elkEatPictureGOs)
                    go.SetActive(true);
                break;
            case Target.ElkShake:
                _elkShakePictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _elkShakePictureGOs)
                    go.SetActive(true);
                break;
            case Target.ElkGrowl:
                _elkGrowlPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _elkGrowlPictureGOs)
                    go.SetActive(true);
                break;
            case Target.ElkShowOff:
                _elkShowOffPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _elkShowOffPictureGOs)
                    go.SetActive(true);
                break;
            case Target.OrnitoGlobal:
                _ornitoGlobalPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _ornitoGlobalPictureGOs)
                    go.SetActive(true);
                break;
            case Target.OrnitoEat:
                _ornitoEatPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _ornitoEatPictureGOs)
                    go.SetActive(true);
                break;
            case Target.OrnitoSwim:
                _ornitoSwimPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _ornitoSwimPictureGOs)
                    go.SetActive(true);
                break;
            case Target.OrnitoSunbathing:
                _ornitoSunbathingPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _ornitoSunbathingPictureGOs)
                    go.SetActive(true);
                break;
            case Target.OrnitoProtect:
                _ornitoProtectPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _ornitoProtectPictureGOs)
                    go.SetActive(true);
                break;
            case Target.ChestnutTree:
                _chestnutTreePictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _chestnutTreePictureGOs)
                    go.SetActive(true);
                break;
            case Target.BirchTree:
                _birchTreePictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _birchTreePictureGOs)
                    go.SetActive(true);
                break;
            case Target.Heather:
                _heatherPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _heatherPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Gorse:
                _gorsePictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _gorsePictureGOs)
                    go.SetActive(true);
                break;
            case Target.Holly:
                _hollyPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _hollyPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Rush:
                _rushPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _rushPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Dandelion:
                _dandelionPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _dandelionPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Clover:
                _cloverPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _cloverPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Spruce:
                _sprucePictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _sprucePictureGOs)
                    go.SetActive(true);
                break;
            case Target.Cypress:
                _cypressPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _cypressPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Fern:
                _fernPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _fernPictureGOs)
                    go.SetActive(true);
                break;
            case Target.RedChestnutTree:
                _redChestnutTreePictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _redChestnutTreePictureGOs)
                    go.SetActive(true);
                break;
            case Target.BeechTree:
                _beechTreePictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _beechTreePictureGOs)
                    go.SetActive(true);
                break;
            case Target.WillowTree:
                _willowTreePictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _willowTreePictureGOs)
                    go.SetActive(true);
                break;
            case Target.FairyRing:
                _fairyRingPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _fairyRingPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Amanita:
                _amanitaPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _amanitaPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Parasol:
                _parasolPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _parasolPictureGOs)
                    go.SetActive(true);
                break;
            case Target.GoldenChanterelle:
                _goldenChanterellePictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _goldenChanterellePictureGOs)
                    go.SetActive(true);
                break;
            case Target.Boletus:
                _boletusPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _boletusPictureGOs)
                    go.SetActive(true);
                break;
            case Target.MoonBeatle:
                _moonBeatlePictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _moonBeatlePictureGOs)
                    go.SetActive(true);
                break;
            case Target.Firefly:
                _fireflyPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _fireflyPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Butterfly:
                _butterflyPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _butterflyPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Dragonfly:
                _dragonflyPictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _dragonflyPictureGOs)
                    go.SetActive(true);
                break;
            case Target.Bee:
                _beePictureSpriteRenderer.sprite = sprite;
                foreach (GameObject go in _beePictureGOs)
                    go.SetActive(true);
                break;

            case Target.None:
                SetPictureToGalleryPolaroidPicture(sprite, galleryIndex);
                break;
            default:
                throw new WarningException("Unable to filter Target");
        }
    }

    private void SetPictureToGalleryPolaroidPicture()
    {
        for (int i = 0; i < _galleryPicturesSpriteRenderers.Length; i++)
        {
            if (_galleryPicturesSpriteRenderers[i].sprite.Equals(_polaroidPictureSprite))
            {
                _galleryPicturesSpriteRenderers[i].sprite = ScreenshotManager.Instance.LastPictureSprite;
                _galleryPicturesSpriteRenderers[i].transform.localScale = new Vector3(0.12f, 0.12f, 1f);
                _galleryPicturesGOs[i].SetActive(true);

                if (SettingsManager.Instance.Database.IsSavingPicturesEnabled)
                {
                    ScreenshotManager.Instance.SaveGalleryScreenshot(i);
                    GameManager.Instance.SetPictureTaken(Target.None, i);
                }
                break;
            }
        }
    }

    private void SetPictureToGalleryPolaroidPicture(Sprite sprite, int i)
    {
        _galleryPicturesSpriteRenderers[i].sprite = sprite;
        _galleryPicturesSpriteRenderers[i].transform.localScale = new Vector3(0.12f, 0.12f, 1f);
        _galleryPicturesGOs[i].SetActive(true);
    }

    public void DeleteGalleryPicture(int i)
    {
        _galleryPicturesSpriteRenderers[i].sprite = _polaroidPictureSprite;
        _galleryPicturesGOs[i].SetActive(false);
    }

    private void InitializeGalleryPictures()
    {
        for (int i = 0; i < _galleryPicturesGOs.Length; i++)
        {
            _galleryPicturesGOs[i].GetComponent<GalleryPictureCT>().galleryPictureIndex = i;
        }
    }
    #endregion
}
