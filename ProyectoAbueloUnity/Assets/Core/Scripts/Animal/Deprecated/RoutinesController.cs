using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutinesController : MonoBehaviour
{
    [SerializeField] private Action[] _actions;
    public Action[] Actions { set { _actions = value; } }
    
    [SerializeField] private float[] _times;
    public float[] Times { set { _times = value; } }

    void Start()
    {
        if (_actions.Length != _times.Length)
            throw new Exception("The number of actions and times of " + transform.name + " doesn't match. It must have the same number in both of them.");
    }

    public Action GetActionInTime(float time)
    {
        for(int i = 0; i < _times.Length - 1; i++)
        {
            if(time >= _times[i] && time < _times[i + 1])
            {
                return _actions[i];
            }
        }

        return _actions[_times.Length - 1];
    }
}
