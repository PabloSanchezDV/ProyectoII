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
            Debug.Log(_animal.name + " has reached the end of path at time " + GameManager.instance.Daytime + ".");
        }
    }
}
