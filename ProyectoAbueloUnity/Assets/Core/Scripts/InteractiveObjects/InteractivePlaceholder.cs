using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivePlaceholder : MonoBehaviour, IInteractive
{
    public void Interact()
    {
        Debug.Log("Interacting with: " + gameObject.name);
    }
}
