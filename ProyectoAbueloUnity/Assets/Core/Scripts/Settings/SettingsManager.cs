using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}