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
    public AudioSource PlayNightSound(GameObject gameObject)
    {
        _db.nightCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.nightVolume);
        return CreateAudioSource(gameObject, _db.nightAC, _db.nightCurrentVolume, _db.nightPitchSwift, _db.nightMinDistance, _db.nightMaxDistance);
    }

    public AudioSource PlayStepsSound(GameObject gameObject)
    {
        _db.stepsCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.stepsVolume);
        return CreateAudioSource(gameObject, _db.stepsAC, _db.stepsCurrentVolume, _db.stepsPitchSwift, _db.stepsMinDistance, _db.stepsMaxDistance);
    }

    #region Mammoth

    public AudioSource PlayStepsMammothSound(GameObject gameObject)
    {
        _db.stepsMammothCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.stepsMammothVolume);
        return CreateAudioSource(gameObject, _db.stepsMammothAC, _db.stepsMammothCurrentVolume, _db.stepsMammothPitchSwift, _db.stepsMammothMinDistance, _db.stepsMammothMaxDistance);
    }

    public AudioSource PlayEatingMammothSound(GameObject gameObject)
    {
        _db.eatingMammothCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.eatingMammothVolume);
        return CreateAudioSource(gameObject, _db.eatingMammothAC, _db.eatingMammothCurrentVolume, _db.eatingMammothPitchSwift, _db.eatingMammothMinDistance, _db.eatingMammothMaxDistance, true);
    }

    public AudioSource PlayScaredMammothSound(GameObject gameObject)
    {
        _db.scaredMammothCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.scaredMammothVolume);
        return CreateAudioSource(gameObject, _db.scaredMammothAC, _db.scaredMammothCurrentVolume, _db.scaredMammothPitchSwift, _db.scaredMammothMinDistance, _db.scaredMammothMaxDistance);
    }

    public AudioSource PlaySleepingMammothSound(GameObject gameObject)
    {
        _db.sleepingMammothCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.sleepingMammothVolume);
        return CreateAudioSource(gameObject, _db.sleepingMammothAC, _db.sleepingMammothCurrentVolume, _db.sleepingMammothPitchSwift, _db.sleepingMammothMinDistance, _db.sleepingMammothMaxDistance, true);
    }

    public AudioSource PlayHowlMammothSound(GameObject gameObject)
    {
        _db.howlMammothCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.howlMammothVolume);
        return CreateAudioSource(gameObject, _db.howlMammothAC, _db.howlMammothCurrentVolume, _db.howlMammothPitchSwift, _db.howlMammothMinDistance, _db.howlMammothMaxDistance);
    }

    public AudioSource PlayHeadbuttMammothSound(GameObject gameObject)
    {
        _db.headbuttMammothCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.headbuttMammothVolume);
        return CreateAudioSource(gameObject, _db.headbuttMammothAC, _db.headbuttMammothCurrentVolume, _db.headbuttMammothPitchSwift, _db.headbuttMammothMinDistance, _db.headbuttMammothMaxDistance);
    }

    #endregion

    #region Elk
    public AudioSource PlayEatingElkSound(GameObject gameObject)
    {
        _db.eatingElkCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.eatingElkVolume);
        return CreateAudioSource(gameObject, _db.eatingElkAC, _db.eatingElkCurrentVolume, _db.eatingElkPitchSwift, _db.eatingElkMinDistance, _db.eatingElkMaxDistance, true);
    }

    public AudioSource PlayHowlElkSound(GameObject gameObject)
    {
        _db.howlElkCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.howlElkVolume);
        return CreateAudioSource(gameObject, _db.howlElkAC, _db.howlElkCurrentVolume, _db.howlElkPitchSwift, _db.howlElkMinDistance, _db.howlElkMaxDistance);
    }

    public AudioSource PlayScaredElkSound(GameObject gameObject)
    {
        _db.scaredElkCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.scaredElkVolume);
        return CreateAudioSource(gameObject, _db.scaredElkAC, _db.scaredElkCurrentVolume, _db.scaredElkPitchSwift, _db.scaredElkMinDistance, _db.scaredElkMaxDistance);
    }

    public AudioSource PlayStepsElkSound(GameObject gameObject)
    {
        _db.stepsElkCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.stepsElkVolume);
        return CreateAudioSource(gameObject, _db.stepsElkAC, _db.stepsElkCurrentVolume, _db.stepsElkPitchSwift, _db.stepsElkMinDistance, _db.stepsElkMaxDistance);
    }

    #endregion

    #region AmbientSound

    public AudioSource PlayLeavesSound(GameObject gameObject)
    {
        _db.leafsCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.leafsVolume);
        return CreateAudioSource(gameObject, _db.leafsAC, _db.leafsCurrentVolume, _db.leafsPitchSwift, _db.leafsMinDistance, _db.leafsMaxDistance, true);
    }

    public AudioSource PlayWindSound(GameObject gameObject)
    {
        _db.windCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.windVolume);
        return CreateAudioSource(gameObject, _db.windAC, _db.windCurrentVolume, _db.windPitchSwift, _db.windMinDistance, _db.windMaxDistance, true);
    }

    public AudioSource PlayWindLonelinessSound(GameObject gameObject)
    {
        _db.windLonelinessCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.windLonelinessVolume);
        return CreateAudioSource(gameObject, _db.windLonelinessAC, _db.windLonelinessCurrentVolume, _db.windLonelinessPitchSwift, _db.windLonelinessMinDistance, _db.windLonelinessMaxDistance, true);
    }

    public AudioSource PlayBirdsSound(GameObject gameObject)
    {
        _db.birdsCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.birdsVolume);
        return CreateAudioSource(gameObject, _db.birdsAC, _db.birdsCurrentVolume, _db.birdsPitchSwift, _db.birdsMinDistance, _db.birdsMaxDistance, true);
    }

    public AudioSource PlayRiverSound(GameObject gameObject)
    {
        _db.riverCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.riverVolume);
        return CreateAudioSource(gameObject, _db.riverAC, _db.riverCurrentVolume, _db.riverPitchSwift, _db.riverMinDistance, _db.riverMaxDistance, true);
    }

    public AudioSource PlayWaterfallSound(GameObject gameObject)
    {
        _db.waterfallCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.waterfallVolume);
        return CreateAudioSource(gameObject, _db.waterfallAC, _db.waterfallCurrentVolume, _db.waterfallPitchSwift, _db.waterfallMinDistance, _db.waterfallMaxDistance, true);
    }
    #endregion

    #region Camera

    public AudioSource PlayCameraOnSound(GameObject gameObject)
    {
        _db.cameraOnCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.cameraOnVolume);
        return CreateAudioSource(gameObject, _db.cameraOnAC, _db.cameraOnCurrentVolume, _db.cameraOnPitchSwift, _db.cameraOnMinDistance, _db.cameraOnMaxDistance);
    }

    public AudioSource PlayPhotoSound(GameObject gameObject)
    {
        _db.photoCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.photoVolume);
        return CreateAudioSource(gameObject, _db.photoAC, _db.photoCurrentVolume, _db.photoPitchSwift, _db.photoMinDistance, _db.photoMaxDistance);
    }

    public AudioSource PlayNewNotebookEntrySound(GameObject gameObject)
    {
        _db.newNotebookEntryCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.newNotebookEntryVolume);
        return CreateAudioSource(gameObject, _db.newNotebookEntryAC, _db.newNotebookEntryCurrentVolume, _db.newNotebookEntryPitchSwift, _db.newNotebookEntryMinDistance, _db.newNotebookEntryMaxDistance);
    }

    #endregion

    #region Notebook
    public AudioSource PlayOpenNotebookSound(GameObject gameObject)
    {
        _db.openNotebookCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.openNotebookVolume);
        return CreateAudioSource(gameObject, _db.openNotebookAC, _db.openNotebookCurrentVolume, _db.openNotebookPitchSwift, _db.openNotebookMinDistance, _db.openNotebookMaxDistance);
    }

    public AudioSource PlayCloseNotebookSound(GameObject gameObject)
    {
        _db.closeNotebookCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.closeNotebookVolume);
        return CreateAudioSource(gameObject, _db.closeNotebookAC, _db.closeNotebookCurrentVolume, _db.closeNotebookPitchSwift, _db.closeNotebookMinDistance, _db.closeNotebookMaxDistance);
    }

    public AudioSource PlayPageSound(GameObject gameObject)
    {
        _db.pageCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.pageVolume);
        return CreateAudioSource(gameObject, _db.pageAC, _db.pageCurrentVolume, _db.pagePitchSwift, _db.pageMinDistance, _db.pageMaxDistance);
    }

    public AudioSource PlayPushpinSound(GameObject gameObject)
    {
        _db.pushpinCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.pushpinVolume);
        return CreateAudioSource(gameObject, _db.pushpinAC, _db.pushpinCurrentVolume, _db.pushpinPitchSwift, _db.pushpinMinDistance, _db.pushpinMaxDistance);
    }

    #endregion

    #region Arpeggio
    public AudioSource PlayArpeggioAmSound(GameObject gameObject)
    {
        _db.arpeggioAmCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.arpeggioAmVolume);
        return CreateAudioSource(gameObject, _db.arpeggioAmAC, _db.arpeggioAmCurrentVolume, _db.arpeggioAmPitchSwift, _db.arpeggioAmMinDistance, _db.arpeggioAmMaxDistance);
    }

    public AudioSource PlayArpeggioBbmajCSound(GameObject gameObject)
    {
        _db.arpeggioBbmajCCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.arpeggioBbmajCVolume);
        return CreateAudioSource(gameObject, _db.arpeggioBbmajCAC, _db.arpeggioBbmajCCurrentVolume, _db.arpeggioBbmajCPitchSwift, _db.arpeggioBbmajCMinDistance, _db.arpeggioBbmajCMaxDistance);
    }

    public AudioSource PlayArpeggioEmSound(GameObject gameObject)
    {
        _db.arpeggioEmCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.arpeggioEmVolume);
        return CreateAudioSource(gameObject, _db.arpeggioEmAC, _db.arpeggioEmCurrentVolume, _db.arpeggioEmPitchSwift, _db.arpeggioEmMinDistance, _db.arpeggioEmMaxDistance);
    }

    public AudioSource PlayArpeggioGSound(GameObject gameObject)
    {
        _db.arpeggioGCurrentVolume = ChangeSFXVolumeAsPerModifier(_db.arpeggioGVolume);
        return CreateAudioSource(gameObject, _db.arpeggioGAC, _db.arpeggioGCurrentVolume, _db.arpeggioGPitchSwift, _db.arpeggioGMinDistance, _db.arpeggioGMaxDistance);
    }

    #endregion


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
        if(go != null)
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
        return null;
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