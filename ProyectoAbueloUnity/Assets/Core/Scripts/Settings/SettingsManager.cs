using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager _instance;
    public static SettingsManager Instance { get { return _instance; } }

    [SerializeField] private SettingsDatabase _db;

    public SettingsDatabase Database { get { return _db; } }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public bool ToggleSFX()
    {
        if(_db != null)
        {
            if (_db.AreSFXEnabled)
            {
                _db.AreSFXEnabled = false;
                return false;
            }
            else
            {
                _db.AreSFXEnabled = true;
            }
        }

        throw new System.Exception("Couldn't find SettingsDatabase");
    }


    public void SetSFXVolume(float volume)
    {
        if (_db != null)
        {
            _db.SFXVolumeModifier = volume;
        }
        throw new System.Exception("Couldn't find SettingsDatabase");
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        if (_db != null)
        {
            _db.MouseSensitivity = sensitivity;
        }
        throw new System.Exception("Couldn't find SettingsDatabase");
    }
}