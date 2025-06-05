using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderCT : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _buttonsSpriteRenderers;
    [SerializeField] private Color _enabledColor;
    [SerializeField] private Color _disabledColor;
    [SerializeField] protected int _levels;
    [SerializeField] private int _initialLevel;

    private void Start()
    {
        SetLevel(_initialLevel);
    }

    public virtual void SetLevel(int level)
    {
        if (level < 0 || level > _levels)
            throw new System.Exception("Level value is higher than the maximum or lower than the minimum.");

        bool barrierFound = false;
        for (int i = 0; i <= _levels; i++)
        {         
            if(!barrierFound)
                _buttonsSpriteRenderers[i].color = _enabledColor;
            else
                _buttonsSpriteRenderers[i].color = _disabledColor;

            if (i == level)
                barrierFound = true;
        }
    }
}
