using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMessageOnPathEnd : MonoBehaviour
{
    [SerializeField] private GameObject _animal;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == _animal.name)
        {
            DebugManager.Instance.DebugMessage(_animal.name + " has reached the end of path at time " + (GameManager.Instance.Daytime - (GameManager.Instance.StartHour * 60 + GameManager.Instance.StartMinute)) + ".");
        }
    }
}
