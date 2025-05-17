using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fungus : Notebook
{
    public Fungus(FSMTemplateMachine fsmMachine, InputActions inputActions) : base(fsmMachine, inputActions) { }

    public override void Enter()
    {
        base.Enter();

        SetPagePostItParent(NotebookPage.Fungus);
        foreach (GameObject go in ((InputHandler)_fsm).FungusPageGOs)
            go.SetActive(true);
        ((InputHandler)_fsm).CurrentNotebookPage = NotebookPage.Fungus;

        if (!((InputHandler)_fsm).IsTurningNotebookPageToRight)
        {
            SetRightNotebookPageAsParent(((InputHandler)_fsm).FungusPageGOs[0]);
            SetLowerAsParent(((InputHandler)_fsm).FungusPageGOs[1]);
        }
        else
        {
            SetUpperAsParent(((InputHandler)_fsm).FungusPageGOs[0]);
            SetLeftNotebookPageAsParent(((InputHandler)_fsm).FungusPageGOs[1]);
        }
    }

    public override void Exit()
    {
        base.Exit();
        ((InputHandler)_fsm).TurnOffAfterTurningPageGOs = ((InputHandler)_fsm).FungusPageGOs;

        if (((InputHandler)_fsm).FungusPageGOs != null)
        {
            if (!((InputHandler)_fsm).IsTurningNotebookPageToRight)
            {
                SetUpperAsParent(((InputHandler)_fsm).FungusPageGOs[0]);
                SetLeftNotebookPageAsParent(((InputHandler)_fsm).FungusPageGOs[1]);
            }
            else
            {
                SetRightNotebookPageAsParent(((InputHandler)_fsm).FungusPageGOs[0]);
                SetLowerAsParent(((InputHandler)_fsm).FungusPageGOs[1]);
            }
        }
    }
}
