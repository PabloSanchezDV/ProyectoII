using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayNightSound : MonoBehaviour
{
    private AudioSource _aS;
    private bool _hasTriggered;

    void Start()
    {
        _aS = AudioManager.Instance.PlayNightSound(gameObject);
        _hasTriggered = false;
    }

    private void Update()
    {
        if (_hasTriggered)
            return;
        
        if (_aS != null)
        {
            if (!_aS.isPlaying)
            {
                ScenesController.Instance.GoToFase();
                _hasTriggered = false;
            }
        }
    }
}
