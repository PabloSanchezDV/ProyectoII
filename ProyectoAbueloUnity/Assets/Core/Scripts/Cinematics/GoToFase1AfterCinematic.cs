using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GoToFase1AfterCinematic : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;

    void Start()
    {
        _videoPlayer.loopPointReached += GoToFase1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToFase1(_videoPlayer);
        }
    }

    private void GoToFase1(VideoPlayer videoPlayer)
    {
        ScenesController.Instance.GoToFase(false);
    }
}
