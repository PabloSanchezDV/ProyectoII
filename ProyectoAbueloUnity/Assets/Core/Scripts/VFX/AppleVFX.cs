using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _appleParticleSystem;

    void Start()
    {
        EventHolder.Instance.onMammothHeadbutt.AddListener(PlayAppleParticleSystem);
    }

    private void OnDisable()
    {
        EventHolder.Instance.onMammothHeadbutt.RemoveListener(PlayAppleParticleSystem);
    }

    public void PlayAppleParticleSystem()
    {
        _appleParticleSystem.Play();
        EventHolder.Instance.onMammothHeadbutt.RemoveListener(PlayAppleParticleSystem);
    }
}
