using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    //References
    [SerializeField] private AudioDatabase _db;

    //Private Variables
    private List<AudioSource> _aSList = new List<AudioSource>();

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    #region Play Methods
    #region SFX
    public AudioSource PlayPlacerholderSound(GameObject gameObject)
    {
        _db.placeholderCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.placeholderVolume);
        return CreateAudioSource(gameObject, _db.placeholderAC, _db.placeholderCurrentVolume, _db.placeholderPitchSwift, _db.placeholderMinDistance, _db.placeholderMaxDistance);
    }
    #endregion

    #region Tinnitus SFX
    // Insert here each SFX that must be disabled in order to be accesible for people with hearing problems
    public AudioSource PlayPlacerholderTinnitusSound(GameObject gameObject)
    {
        _db.placeholderTinnitusCurrentVolume = ChangeTinnitusSFXVolumeAsPerModifier(_db.placeholderTinnitusVolume);
        return CreateAudioSource(gameObject, _db.placeholderTinnitusAC, _db.placeholderTinnitusCurrentVolume, _db.placeholderTinnitusPitchSwift, _db.placeholderTinnitusMinDistance, _db.placeholderTinnitusMaxDistance);
    }
    #endregion

    #region Music
    public AudioSource PlayPlacerholderMusic(GameObject gameObject)
    {
        _db.placeholderMusicCurrentVolume = ChangeMusicVolumeAsPerModifier(_db.placeholderMusicVolume);
        return CreateMusicAudioSource(_db.placeholderMusicAC, _db.placeholderMusicCurrentVolume);
    }
    #endregion
    #endregion

    #region Stop Methods
    public void StopAudioSource(AudioSource aS)
    {
        if (aS != null)
        {
            aS.Stop();
            Destroy(aS);
        }
    }

    public void DestroyAllAudioSourcesOnSceneChange()
    {
        foreach (AudioSource aS in _aSList)
        {
            Destroy(aS);
        }
    }
    #endregion

    #region Private Methods
    private AudioSource CreateAudioSource(GameObject go, AudioClip audioClip, float volume, float pitchSwift = 0, float minDistance = 1, float maxDistance = 500, bool loop = false)
    {
        AudioSource aS = go.AddComponent<AudioSource>();
        aS.clip = audioClip;
        aS.volume = volume;
        aS.pitch = GetPitch(pitchSwift);
        aS.minDistance = minDistance;
        aS.maxDistance = maxDistance;
        aS.loop = loop;
        aS.spatialBlend = 1;

        aS.Play();
        _aSList.Add(aS);

        if (!loop)
        {
            StartCoroutine(DestroyAudioSourceWhenFinish(aS));
        }

        return aS;
    }

    private AudioSource CreateMusicAudioSource(AudioClip audioclip, float volume)
    {
        AudioSource aS = gameObject.AddComponent<AudioSource>();
        aS.clip = audioclip;
        aS.ignoreListenerPause = true;
        aS.volume = volume;
        aS.loop = true;
        aS.Play();
        return aS;
    }

    IEnumerator DestroyAudioSourceWhenFinish(AudioSource audioSource)
    {
        bool isPlaying = true;
        while (isPlaying)
        {
            if (audioSource != null)
            {
                if (!audioSource.isPlaying)
                {
                    isPlaying = false;
                }
            }

            yield return null;
        }
        try
        {
            _aSList.Remove(audioSource);
            Destroy(audioSource);
        }
        catch (Exception e)
        {
            Debug.Log(audioSource.name + " cannot be found and cannot be destroyed. " + e.Message);
        }
    }

    private float GetPitch(float pitchSwift)
    {
        float minPitch = 1 - pitchSwift;
        float maxPitch = 1 + pitchSwift;

        return UnityEngine.Random.Range(minPitch, maxPitch);
    }

    private float ChangeSFXVolumeAsPerModifier(float originalVolume)
    {
        if (SettingsManager.Instance.Database.AreSFXEnabled)
            return originalVolume * SettingsManager.Instance.Database.SFXVolumeModifier;
        else
            return 0f;
    }

    private float ChangeTinnitusSFXVolumeAsPerModifier(float originalVolume)
    {
        if (SettingsManager.Instance.Database.AreTinnitusSFXEnabled)
            return originalVolume * SettingsManager.Instance.Database.TinnitusSFXVolumeModifier;
        else
            return 0f;
    }

    private float ChangeMusicVolumeAsPerModifier(float originalVolume)
    {
        if (SettingsManager.Instance.Database.IsMusicEnabled)
            return originalVolume * SettingsManager.Instance.Database.MusicVolumeModifier;
        else
            return 0f;
    }
    #endregion
}