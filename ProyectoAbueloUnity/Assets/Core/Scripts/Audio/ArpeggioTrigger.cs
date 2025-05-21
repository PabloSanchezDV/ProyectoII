using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArpeggioTrigger : MonoBehaviour
{
    [SerializeField] private Arpeggio _arpeggio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayArpeggio(other.transform.GetChild(0).gameObject);
            gameObject.SetActive(false);
        }
    }

    private void PlayArpeggio(GameObject go)
    {
        switch(_arpeggio)
        {
            case Arpeggio.Bbmaj_C:
                AudioManager.Instance.PlayArpeggioBbmajCSound(go); 
                break;
            case Arpeggio.Am:
                AudioManager.Instance.PlayArpeggioAmSound(go);
                break;
            case Arpeggio.G:
                AudioManager.Instance.PlayArpeggioGSound(go);
                break;
            case Arpeggio.Em:
                AudioManager.Instance.PlayArpeggioEmSound(go);
                break;
            default:
                throw new Exception("Couldn't filter Arpeggio. Make sure the Arpeggio field is properly set");
        }
    }

    private enum Arpeggio { Bbmaj_C, Am, G, Em }
}
