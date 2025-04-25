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
    [Range(0, 1)] public float sleepingMammothVolume;
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

    public AudioClip howlMammothAC;
    [Range(0, 1)] public float howlMammothVolume;
    public float howlMammothMinDistance;
    public float howlMammothMaxDistance;
    [Range(0, 1)] public float howlMammothPitchSwift;
    [NonSerialized] public float howlMammothCurrentVolume;

    public AudioClip headbuttMammothAC;
    [Range(0, 1)] public float headbuttMammothVolume;
    public float headbuttMammothMinDistance;
    public float headbuttMammothMaxDistance;
    [Range(0, 1)] public float headbuttMammothPitchSwift;
    [NonSerialized] public float headbuttMammothCurrentVolume;


    [Header("Elk")]

    public AudioClip eatingElkAC;
    [Range(0, 1)] public float eatingElkVolume;
    public float eatingElkMinDistance;
    public float eatingElkMaxDistance;
    [Range(0, 1)] public float eatingElkPitchSwift;
    [NonSerialized] public float eatingElkCurrentVolume;

    public AudioClip howlElkAC;
    [Range(0, 1)] public float howlElkVolume;
    public float howlElkMinDistance;
    public float howlElkMaxDistance;
    [Range(0, 1)] public float howlElkPitchSwift;
    [NonSerialized] public float howlElkCurrentVolume;

    public AudioClip scaredElkAC;
    [Range(0, 1)] public float scaredElkVolume;
    public float scaredElkMinDistance;
    public float scaredElkMaxDistance;
    [Range(0, 1)] public float scaredElkPitchSwift;
    [NonSerialized] public float scaredElkCurrentVolume;

    public AudioClip stepsElkAC;
    [Range(0, 1)] public float stepsElkVolume;
    public float stepsElkMinDistance;
    public float stepsElkMaxDistance;
    [Range(0, 1)] public float stepsElkPitchSwift;
    [NonSerialized] public float stepsElkCurrentVolume;


    [Header("Enviromental sounds")]

    public AudioClip windAC;
    [Range(0, 1)] public float windVolume;
    public float windMinDistance;
    public float windMaxDistance;
    [Range(0, 1)] public float windPitchSwift;
    [NonSerialized] public float windCurrentVolume;

    public AudioClip leafsAC;
    [Range(0, 1)] public float leafsVolume;
    public float leafsMinDistance;
    public float leafsMaxDistance;
    [Range(0, 1)] public float leafsPitchSwift;
    [NonSerialized] public float leafsCurrentVolume;

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