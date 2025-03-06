using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager _instance;
    public static SettingsManager Instance {  get { return _instance; } }

    private bool _areSoundsEnabled = true;
    private bool _areTinnitusSoundsEnabled = true;
    private bool _isMusicEnabled = true;
    private float _sfxVolumeModifier = 1.0f;
    private float _tinnitusSFXVolumeModifier = 1.0f;
    private float _musicVolumeModifier = 1.0f;

    public bool AreSFXEnabled {  get { return _areSoundsEnabled; } }
    public bool AreTinnitusSFXEnabled {  get { return _areTinnitusSoundsEnabled; } }
    public bool IsMusicEnabled {  get { return _isMusicEnabled; } }
    public float SFXVolumeModifier {  get { return _sfxVolumeModifier; } }
    public float TinnitusSFXVolumeModifier { get { return _tinnitusSFXVolumeModifier; } }
    public float MusicVolumeModifier {  get { return _musicVolumeModifier; } }


    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
}
