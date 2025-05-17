using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bugs : Notebook
{
    public Bugs(FSMTemplateMachine fsmMachine, InputActions inputActions) : base(fsmMachine, inputActions) { }

    public override void Enter()
    {
        base.Enter();

        SetPagePostItParent(NotebookPage.Bugs);
        foreach (GameObject go in ((InputHandler)_fsm).BugsPageGOs)
            go.SetActive(true);
        ((InputHandler)_fsm).CurrentNotebookPage = NotebookPage.Bugs;

        if (!((InputHandler)_fsm).IsTurningNotebookPageToRight)
        {
            SetRightNotebookPageAsParent(((InputHandler)_fsm).BugsPageGOs[0]);
            SetLowerAsParent(((InputHandler)_fsm).BugsPageGOs[1]);
        }
        else
        {
            SetUpperAsParent(((InputHandler)_fsm).BugsPageGOs[0]);
            SetLeftNotebookPageAsParent(((InputHandler)_fsm).BugsPageGOs[1]);
        }
    }

    public override void Exit()
    {
        base.Exit();
        ((InputHandler)_fsm).TurnOffAfterTurningPageGOs = ((InputHandler)_fsm).BugsPageGOs;

        if (((InputHandler)_fsm).BugsPageGOs != null)
        {
            if (!((InputHandler)_fsm).IsTurningNotebookPageToRight)
            {
                SetUpperAsParent(((InputHandler)_fsm).BugsPageGOs[0]);
                SetLeftNotebookPageAsParent(((InputHandler)_fsm).BugsPageGOs[1]);
            }
            else
            {
                SetRightNotebookPageAsParent(((InputHandler)_fsm).BugsPageGOs[0]);
                SetLowerAsParent(((InputHandler)_fsm).BugsPageGOs[1]);
            }
        }
    }
}
