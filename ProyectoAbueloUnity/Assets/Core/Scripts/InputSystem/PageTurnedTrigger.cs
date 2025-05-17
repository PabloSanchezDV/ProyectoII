using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageTurnedTrigger : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;

    public void DeactivateObjectsAfterTurningPageToLeft()
    {
        if(!_inputHandler.IsTurningNotebookPageToRight)
            _inputHandler.DeactivateObjectsAfterTurningPage();
    }

    public void DeactivateObjectsAfterTurningPageToRight()
    {
        if (_inputHandler.IsTurningNotebookPageToRight)
            _inputHandler.DeactivateObjectsAfterTurningPage();
    }

    public void ResetNotebook()
    {
        if (_inputHandler.IsClosingNotebook)
        {
            _inputHandler.ResetAction();
        }
    }
}
