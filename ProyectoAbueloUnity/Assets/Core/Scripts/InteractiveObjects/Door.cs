using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractive
{
    public void Interact()
    {
        // UIManager calls to GameManager.EndDay after FadeOut animation is complete
        UIManager.Instance.TriggerFadeOut();
    }
}
