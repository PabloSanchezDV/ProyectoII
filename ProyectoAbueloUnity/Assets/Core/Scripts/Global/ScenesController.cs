using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    public static ScenesController Instance;

    [SerializeField] private float _progressToGoToFase2;
    [SerializeField] private float _progressToGoToFase3;
    [SerializeField] private float _progressToEnd;

    private AsyncOperation _sceneLoader;
    private bool _isCheckingForSceneLoaded;
    private bool _doesLoad;
    private int _faseIndex = 3;

    public bool DoesLoad { get { return _doesLoad; } set { _doesLoad = value; } }
    public int FaseSceneIndex { get { return _faseIndex; } }

    // Scenes Indexes
    //  0 - Logos
    //  1 - MainMenu
    //  2 - Cinematic
    //  3 - Fase1
    //  4 - Fase2
    //  5 - Fase3
    //  6 - Credits
    //  7 - Night

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(!_isCheckingForSceneLoaded)
            return;

        if(_sceneLoader != null)
        {
            if(_sceneLoader.isDone)
            {
                _sceneLoader.allowSceneActivation = true; 
                _isCheckingForSceneLoaded = false;
            }
        }
    }

    public void NewGame()
    {
        _faseIndex = 3; // Fase1
        LoadScene(2, false); // Cinematic
    }

    public void ContinueGame()
    {
        LoadScene(SaveSystem.GetCurrentFaseSceneIndex());
    }

    public void EndDay()
    {
        if(GameManager.Instance != null)
        {
            float progress = GameManager.Instance.GetProgress();

            if(progress >= _progressToEnd)
                LoadScene(6); // Credits
            else if(progress > _progressToGoToFase3)
                _faseIndex = 5; // Fase3
            else if (progress > _progressToGoToFase2)
                _faseIndex = 4; // Fase2

            LoadScene(7); // Night
        }
    }

    public void GoToFase(bool doesLoad)
    {
        LoadScene(_faseIndex, doesLoad);
    }

    public void GoToMainMenu()
    {
        LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LoadScene(int sceneIndex, bool doesLoad = true)
    {
        _sceneLoader = SceneManager.LoadSceneAsync(sceneIndex);
        _sceneLoader.allowSceneActivation = true;
        _doesLoad = doesLoad;
        _isCheckingForSceneLoaded = true;
    }
}
