using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControl : MonoBehaviour
{
    private Animator _anim;
    private Transform _target;
    private float _interpolationValue = 0.0f;
    private Transform _headBone;
    private Quaternion _headRotation;
    private float _headResetIPTime = 5f;
    private float _headResetTimer = 0f;

    public void Initialize(Animator anim)
    {
        _anim = anim;
        _headRotation = _headBone.rotation;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
    
    public void SetHeadResetInterpolationTime(float time)
    {
        _headResetIPTime = time;
    }

    public void SetInterpolationValue(float interpolationValue, bool map = true)
    { 
        if(map) 
            _interpolationValue = Map(interpolationValue); 
        else
            _interpolationValue = interpolationValue;

        Vector3 lookVector = _target.transform.position - transform.position;
        lookVector.Normalize();

        float headX = Mathf.Clamp(lookVector.x, -1f, 1f);
        float headY = Mathf.Clamp(lookVector.y, -1f, 1f);

        _anim.SetFloat("HeadX", headX);
        _anim.SetFloat("HeadY", headY);

        _anim.SetLayerWeight(1, interpolationValue);

        // Code relative to displacing the bone directly

        //Quaternion correction = Quaternion.Euler(90, 0, 0); // Positive-Y is the forward direction, not positive-Z
        //Quaternion targetRotation = Quaternion.LookRotation(lookVector) * correction;
        //_headBone.transform.rotation = targetRotation;
        ////_headBone.rotation = Quaternion.Slerp(_headRotation, targetRotation, _interpolationValue);
    }

    public void SetHeadBone(Transform headBone)
    {
        _headBone = headBone;
    }

    private float Map(float x)
    {
        return (x - 0.5f) / 0.5f;
    }

    public void ResetHead(float interpolationValue)
    {
        _headResetTimer = _headResetIPTime * interpolationValue; // InterpolationValue acts as a percentage of the timer
        StartCoroutine(InterpolateHeadReset());
    }

    IEnumerator InterpolateHeadReset()
    {
        yield return null;
        SetInterpolationValue(_headResetTimer / _headResetIPTime);
        _headResetTimer -= Time.deltaTime;
        if (_headResetTimer > 0)
            StartCoroutine(InterpolateHeadReset());
    }

}
