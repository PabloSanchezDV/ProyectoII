using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _appleParticleSystem;
    [SerializeField] private float _timeToDespawn;

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
        StartCoroutine(DisableAppleVFXAfterTime());
        EventHolder.Instance.onMammothHeadbutt.RemoveListener(PlayAppleParticleSystem);
    }

    IEnumerator DisableAppleVFXAfterTime()
    {
        yield return new WaitUntil(() => GameManager.Instance.Daytime >= _timeToDespawn + GameManager.Instance.StartDaytime);
        _appleParticleSystem.gameObject.SetActive(false);
    }
}
