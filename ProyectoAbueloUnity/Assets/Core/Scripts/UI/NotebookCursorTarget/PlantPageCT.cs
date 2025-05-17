using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPageCT : NotebookCursorTarget
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private PlantsPage _pageToGo;

    public override void PrimaryPressed(Vector3 pressedWorldPosition)
    {
        _inputHandler.GoToPlantsPage(_pageToGo);
    }
}
