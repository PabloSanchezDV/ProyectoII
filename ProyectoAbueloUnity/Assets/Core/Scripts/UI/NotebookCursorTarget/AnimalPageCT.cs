using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPageCT : NotebookCursorTarget
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private AnimalsPage _pageToGo;

    public override void PrimaryPressed(Vector3 pressedWorldPosition)
    {
        _inputHandler.GoToAnimalsPage(_pageToGo);
    }
}
