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
