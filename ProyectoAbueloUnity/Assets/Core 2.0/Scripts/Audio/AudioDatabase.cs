using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/AudioDatabase")]
public class AudioDatabase : ScriptableObject
{
    [Header("Placerholder SFX")]
    public AudioClip placeholderAC;
    [Range(0, 1)] public float placeholderVolume;
    public float placeholderMinDistance;
    public float placeholderMaxDistance;
    [Range(0, 1)] public float placeholderPitchSwift;
    [NonSerialized] public float placeholderCurrentVolume;

    [Header("Placerholder SFX Prototype")]
    public AudioClip stepsAC;
    [Range(0, 1)] public float stepsVolume;
    public float stepsMinDistance;
    public float stepsMaxDistance;
    [Range(0, 1)] public float stepsPitchSwift;
    [NonSerialized] public float stepsCurrentVolume;

    public AudioClip stepsMamutAC;
    [Range(0, 1)] public float stepsMamutVolume;
    public float stepsMamutMinDistance;
    public float stepsMamutMaxDistance;
    [Range(0, 1)] public float stepsMamutPitchSwift;
    [NonSerialized] public float stepsMamutCurrentVolume;

    public AudioClip eatingMamutAC;
    [Range(0, 1)] public float eatingMamutVolume;
    public float eatingMamutMinDistance;
    public float eatingMamutMaxDistance;
    [Range(0, 1)] public float eatingMamutPitchSwift;
    [NonSerialized] public float eatingMamutCurrentVolume;

    public AudioClip scaredMamutAC;
    [Range(0, 1)] public float scaredMamutVolume;
    public float scaredMamutMinDistance;
    public float scaredMamutMaxDistance;
    [Range(0, 1)] public float scaredMamutPitchSwift;
    [NonSerialized] public float scaredMamutCurrentVolume;

    public AudioClip cameraOnAC;
    [Range(0, 1)] public float cameraOnVolume;
    public float cameraOnMinDistance;
    public float cameraOnMaxDistance;
    [Range(0, 1)] public float cameraOnPitchSwift;
    [NonSerialized] public float cameraOnCurrentVolume;

    public AudioClip photoAC;
    [Range(0, 1)] public float photoVolume;
    public float photoMinDistance;
    public float photoMaxDistance;
    [Range(0, 1)] public float photoPitchSwift;
    [NonSerialized] public float photoCurrentVolume;

    [Header("Placerholder Tinnitus SFX")]
    public AudioClip placeholderTinnitusAC;
    [Range(0, 1)] public float placeholderTinnitusVolume;
    public float placeholderTinnitusMinDistance;
    public float placeholderTinnitusMaxDistance;
    [Range(0, 1)] public float placeholderTinnitusPitchSwift;
    [NonSerialized] public float placeholderTinnitusCurrentVolume;

    [Header("Placeholder Music")] // Note that music doesn't have as much variables as SFX
    public AudioClip placeholderMusicAC;
    [Range(0, 1)] public float placeholderMusicVolume;
    [NonSerialized] public float placeholderMusicCurrentVolume;
}