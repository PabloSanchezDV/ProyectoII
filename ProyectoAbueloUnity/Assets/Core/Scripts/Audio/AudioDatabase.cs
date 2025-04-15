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

    [Header("Player")]
    public AudioClip stepsAC;
    [Range(0, 1)] public float stepsVolume;
    public float stepsMinDistance;
    public float stepsMaxDistance;
    [Range(0, 1)] public float stepsPitchSwift;
    [NonSerialized] public float stepsCurrentVolume;

    [Header("Camera")]
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

    [Header("Animals")]
    [Header("Mammoth")]
    public AudioClip stepsMammothAC;
    [Range(0, 1)] public float stepsMammothVolume;
    public float stepsMammothMinDistance;
    public float stepsMammothMaxDistance;
    [Range(0, 1)] public float stepsMammothPitchSwift;
    [NonSerialized] public float stepsMammothCurrentVolume;

    public AudioClip eatingMammothAC;
    [Range(0, 1)] public float eatingMammothVolume;
    public float eatingMammothMinDistance;
    public float eatingMammothMaxDistance;
    [Range(0, 1)] public float eatingMammothPitchSwift;
    [NonSerialized] public float eatingMammothCurrentVolume;
    
    public AudioClip sleepingMammothAC;
    [Range(0, 1)] public float sleepMammothVolume;
    public float sleepingMammothMinDistance;
    public float sleepingMammothMaxDistance;
    [Range(0, 1)] public float sleepingMammothPitchSwift;
    [NonSerialized] public float sleepingMammothCurrentVolume;

    public AudioClip scaredMammothAC;
    [Range(0, 1)] public float scaredMammothVolume;
    public float scaredMammothMinDistance;
    public float scaredMammothMaxDistance;
    [Range(0, 1)] public float scaredMammothPitchSwift;
    [NonSerialized] public float scaredMammothCurrentVolume;

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