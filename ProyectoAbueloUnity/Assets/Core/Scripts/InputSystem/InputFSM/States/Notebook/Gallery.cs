using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gallery : Notebook
{
    public Gallery(FSMTemplateMachine fsmMachine, InputActions inputActions) : base(fsmMachine, inputActions) { }

    public override void Enter()
    {
        base.Enter();

        if(!_changeInnerPage)
            SetPagePostItParent(NotebookPage.Gallery);
        else
        {
            ClearPostItChildsFromNotebookPage();
            _changeInnerPage = false;
        }

        ((InputHandler)_fsm).CurrentNotebookPage = NotebookPage.Gallery;
    }

    public override void Exit()
    {
        base.Exit();
        SetPagePostItParent(NotebookPage.Gallery);
    }
}
