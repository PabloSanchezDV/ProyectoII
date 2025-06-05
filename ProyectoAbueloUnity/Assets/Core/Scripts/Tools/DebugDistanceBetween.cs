using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDistanceBetween : MonoBehaviour
{
    [SerializeField] private Transform _objectA;
    [SerializeField] private Transform _objectB;

    // Update is called once per frame
    void Update()
    {
        if (_objectA != null && _objectB != null)
            DebugManager.Instance.DebugMessage($"Distance between {_objectA.name} and {_objectB.name} is {Vector3.Distance(_objectA.position, _objectB.position)}.");
    }
}
