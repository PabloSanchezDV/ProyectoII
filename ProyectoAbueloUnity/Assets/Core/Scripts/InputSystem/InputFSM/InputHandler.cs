using System;
using System.Collections;
using System.Collections.Generic;
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
    [Header("References")]
    [SerializeField] private Animator _playerAnim;
    [SerializeField] private Animator _armsAnim;
    [SerializeField] private Animator _notebookAnim;
    [SerializeField] private SkinnedMeshRenderer _armsRenderer;
    [SerializeField] private SkinnedMeshRenderer _notebookRenderer;

    [Header("Parameters -  Free Move")]
    [SerializeField] public float moveSpeed;
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

    [Header("References -  Notebook")]
    [SerializeField] private NotebookCT _notebookCT;
    [SerializeField] private PostItCT _postIt;
    [SerializeField] private Transform _upperCoverTransform;
    [SerializeField] private Transform _notebookPageTransform;
    [SerializeField] private Transform _lowerCoverTransform;
    [SerializeField] private Transform[] _pagePostIts;

    [Header("References -  Notebook Pages")]
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

    private InputActions _inputActions;
    private GameObject _player;
    private GameObject _camera;

    private Collider _notebookCollider;
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
    #endregion

    #region Properties
    public GameObject Player {  get { return _player; } }
    public GameObject Camera { get  { return _camera; } set { _camera = value; } }
    public Animator PlayerAnim { get { return _playerAnim; } }
    public Animator ArmsAnim { get { return _armsAnim; } }
    public Animator NotebookAnim { get { return _notebookAnim; } }

    public SkinnedMeshRenderer ArmsRenderer {  get { return _armsRenderer; } }
    public SkinnedMeshRenderer NotebookRenderer { get { return _notebookRenderer; } }

    public Collider NotebookCollider { get { return _notebookCollider; } }
    public NotebookCT NotebookCT {  get { return _notebookCT; } }

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

        DisableArmsRenderer();
        _notebookCollider = _notebookAnim.gameObject.GetComponent<Collider>();
        _notebookCollider.enabled = false;
        
        foreach(Transform postIt in _pagePostIts)
            postIt.gameObject.SetActive(false);

        _notebookCT.postIt = _postIt;

        DebugManager.Instance.DebugGlobalSystemMessage("InputHandler initialized");
    }

    protected override void GetInitialState(out FSMTemplateState state)
    {
        state = freeMove;
        freeMove.Enter();
    }

    public void LookForTargetsOnCamera()
    {
        StartCoroutine(WaitForAddingTargetsToList());
    }

    private IEnumerator WaitForAddingTargetsToList()
    {
        yield return new WaitForSeconds(Time.deltaTime * 10f);
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
    }
}
