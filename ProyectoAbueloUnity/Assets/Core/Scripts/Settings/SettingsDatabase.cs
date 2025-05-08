using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/SettingsDatabase")]
public class SettingsDatabase : ScriptableObject
{

    [SerializeField] private bool _areSoundsEnabled = true;
    [SerializeField] private bool _areTinnitusSoundsEnabled = true;
    [SerializeField] private bool _isMusicEnabled = true;
    [SerializeField] private float _sfxVolumeModifier = 1.0f;
    [SerializeField] private float _tinnitusSFXVolumeModifier = 1.0f;
    [SerializeField] private float _musicVolumeModifier = 1.0f;

    [SerializeField] private float _mouseSensitivity = 1.0f;
    [SerializeField] private float _fov = 60f;

    [SerializeField] private bool _isSavingPicturesEnabled = true;

    public bool AreSFXEnabled { get { return _areSoundsEnabled; } }
    public bool AreTinnitusSFXEnabled { get { return _areTinnitusSoundsEnabled; } }
    public bool IsMusicEnabled { get { return _isMusicEnabled; } }
    public float SFXVolumeModifier { get { return _sfxVolumeModifier; } }
    public float TinnitusSFXVolumeModifier { get { return _tinnitusSFXVolumeModifier; } }
    public float MusicVolumeModifier { get { return _musicVolumeModifier; } }

    public float MouseSensitivity { get { return _mouseSensitivity; } set { _mouseSensitivity = value; } }
    public float FieldOfView { get { return _fov; } }

    public bool IsSavingPicturesEnabled { get { return _isSavingPicturesEnabled; } }
}
