using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : Notebook
{
    public Plants(FSMTemplateMachine fsmMachine, InputActions inputActions) : base(fsmMachine, inputActions) { }

    public override void Enter()
    {
        base.Enter();

        if (!_changeInnerPage)
            SetPagePostItParent(NotebookPage.Plants);
        else
        {
            ClearPostItChildsFromNotebookPage();
            _changeInnerPage = false;
        }

        ((InputHandler)_fsm).CurrentNotebookPage = NotebookPage.Plants;
    }
}
