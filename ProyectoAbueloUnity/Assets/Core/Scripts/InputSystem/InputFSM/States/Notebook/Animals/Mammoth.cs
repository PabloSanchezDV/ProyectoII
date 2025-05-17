using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mammoth : Animals
{
    public Mammoth(FSMTemplateMachine fsmMachine, InputActions inputActions) : base(fsmMachine, inputActions) { }

    public override void Enter()
    {
        base.Enter();
        foreach (GameObject go in ((InputHandler)_fsm).MammothPageGOs)
            go.SetActive(true);
        ((InputHandler)_fsm).CurrentAnimalsPage = AnimalsPage.Mammoth;

        if (!((InputHandler)_fsm).IsTurningNotebookPageToRight)
        {
            SetRightNotebookPageAsParent(((InputHandler)_fsm).MammothPageGOs[0]);
            SetLowerAsParent(((InputHandler)_fsm).MammothPageGOs[1]);
        }
        else
        {
            SetUpperAsParent(((InputHandler)_fsm).MammothPageGOs[0]);
            SetLeftNotebookPageAsParent(((InputHandler)_fsm).MammothPageGOs[1]);
        }
    }

    public override void Exit() 
    { 
        base.Exit();
        ((InputHandler)_fsm).TurnOffAfterTurningPageGOs = ((InputHandler)_fsm).MammothPageGOs;

        if(((InputHandler)_fsm).MammothPageGOs != null)
        {
            if (!((InputHandler)_fsm).IsTurningNotebookPageToRight)
            {
                SetUpperAsParent(((InputHandler)_fsm).MammothPageGOs[0]);
                SetLeftNotebookPageAsParent(((InputHandler)_fsm).MammothPageGOs[1]);    
            }
            else
            {
                SetRightNotebookPageAsParent(((InputHandler)_fsm).MammothPageGOs[0]);
                SetLowerAsParent(((InputHandler)_fsm).MammothPageGOs[1]);
            }
        }
    }
}
