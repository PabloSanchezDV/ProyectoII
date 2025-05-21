using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenuButtonCT : NotebookCursorTarget
{
    public override void PrimaryPressed(Vector3 pressedWorldPosition)
    {
        base.PrimaryPressed(pressedWorldPosition);

        ScenesController.Instance.GoToMainMenu();
    }
}
