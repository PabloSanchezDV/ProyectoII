using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderButtonCT : NotebookCursorTarget
{
    [SerializeField] private SliderCT _slicerCT;
    [SerializeField] private int _level;

    public override void PrimaryPressed(Vector3 pressedWorldPosition)
    {
        base.PrimaryPressed(pressedWorldPosition);

        _slicerCT.SetLevel(_level);
    }
}
