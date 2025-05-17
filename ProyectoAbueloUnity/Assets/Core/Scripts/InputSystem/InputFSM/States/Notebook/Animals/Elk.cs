using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elk : Animals
{
    public Elk(FSMTemplateMachine fsmMachine, InputActions inputActions) : base(fsmMachine, inputActions) { }

    public override void Enter()
    {
        base.Enter();
        foreach (GameObject go in ((InputHandler)_fsm).ElkPageGOs)
            go.SetActive(true);
        ((InputHandler)_fsm).CurrentAnimalsPage = AnimalsPage.Elk;

        if (!((InputHandler)_fsm).IsTurningNotebookPageToRight)
        {
            SetRightNotebookPageAsParent(((InputHandler)_fsm).ElkPageGOs[0]);
            SetLowerAsParent(((InputHandler)_fsm).ElkPageGOs[1]);
        }
        else
        {
            SetUpperAsParent(((InputHandler)_fsm).ElkPageGOs[0]);
            SetLeftNotebookPageAsParent(((InputHandler)_fsm).ElkPageGOs[1]);
        }
    }

    public override void Exit()
    {
        base.Exit();
        ((InputHandler)_fsm).TurnOffAfterTurningPageGOs = ((InputHandler)_fsm).ElkPageGOs;
        
        if (((InputHandler)_fsm).ElkPageGOs != null)
        {
            if (!((InputHandler)_fsm).IsTurningNotebookPageToRight)
            {
                SetUpperAsParent(((InputHandler)_fsm).ElkPageGOs[0]);
                SetLeftNotebookPageAsParent(((InputHandler)_fsm).ElkPageGOs[1]);
            }
            else
            {
                SetRightNotebookPageAsParent(((InputHandler)_fsm).ElkPageGOs[0]);
                SetLowerAsParent(((InputHandler)_fsm).ElkPageGOs[1]);
            }
        }
    }
}
