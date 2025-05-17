using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animals : Notebook
{
    public Animals(FSMTemplateMachine fsmMachine, InputActions inputActions) : base(fsmMachine, inputActions) { }

    public override void Enter()
    {
        base.Enter();

        if (!_changeInnerPage)
            SetPagePostItParent(NotebookPage.Animals);
        else
        {
            ClearPostItChildsFromNotebookPage();
            _changeInnerPage = false;
        }

        ((InputHandler)_fsm).CurrentNotebookPage = NotebookPage.Animals;
    }
}
