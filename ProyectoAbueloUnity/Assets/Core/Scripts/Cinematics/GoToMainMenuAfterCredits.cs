using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GoToMainMenuAfterCredits : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;

    void Start()
    {
        _videoPlayer.loopPointReached += GoToMainMenu;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMainMenu(_videoPlayer);
        }
    }

    private void GoToMainMenu(VideoPlayer videoPlayer)
    {
        ScenesController.Instance.GoToMainMenu();
    }
}
