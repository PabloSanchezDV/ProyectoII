using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudioOnPlay : MonoBehaviour
{
    [SerializeField] private SoundEffect _soundEffect;
    private AudioSource _aS;

    void Start()
    {
        switch (_soundEffect) 
        {
            case (SoundEffect.Wind):
                _aS = AudioManager.Instance.PlayWindSound(gameObject);
                break;
            case (SoundEffect.WindLoneliness):
                _aS = AudioManager.Instance.PlayWindLonelinessSound(gameObject);
                break;
            case (SoundEffect.Leaves):
                _aS = AudioManager.Instance.PlayLeavesSound(gameObject);
                break;
            case (SoundEffect.Birds):
                _aS = AudioManager.Instance.PlayBirdsSound(gameObject);
                break;
            case (SoundEffect.Waterfall):
                _aS = AudioManager.Instance.PlayWaterfallSound(gameObject);
                break;
            case (SoundEffect.River):
                _aS = AudioManager.Instance.PlayRiverSound(gameObject);
                break;
            default:
                throw new System.Exception($"Sound effect in {gameObject.name} not set.");
        }      
    }

    private void UpdateAudioSourceOnSettingsChange()
    {
        //TODO
    }
}
