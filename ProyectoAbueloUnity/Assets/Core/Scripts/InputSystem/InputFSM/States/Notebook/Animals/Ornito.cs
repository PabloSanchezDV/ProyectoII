using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ornito : Animals
{
    public Ornito(FSMTemplateMachine fsmMachine, InputActions inputActions) : base(fsmMachine, inputActions) { }

    public override void Enter()
    {
        base.Enter();
        foreach (GameObject go in ((InputHandler)_fsm).OrnitoPageGOs)
            go.SetActive(true);
        ((InputHandler)_fsm).CurrentAnimalsPage = AnimalsPage.Ornito;

        if (!((InputHandler)_fsm).IsTurningNotebookPageToRight)
        {
            SetRightNotebookPageAsParent(((InputHandler)_fsm).OrnitoPageGOs[0]);
            SetLowerAsParent(((InputHandler)_fsm).OrnitoPageGOs[1]);
        }
        else
        {
            SetUpperAsParent(((InputHandler)_fsm).OrnitoPageGOs[0]);
            SetLeftNotebookPageAsParent(((InputHandler)_fsm).OrnitoPageGOs[1]);
        }
    }

    public override void Exit()
    {
        base.Exit();
        ((InputHandler)_fsm).TurnOffAfterTurningPageGOs = ((InputHandler)_fsm).OrnitoPageGOs;

        if (((InputHandler)_fsm).OrnitoPageGOs != null)
        {
            if (!((InputHandler)_fsm).IsTurningNotebookPageToRight)
            {
                SetUpperAsParent(((InputHandler)_fsm).OrnitoPageGOs[0]);
                SetLeftNotebookPageAsParent(((InputHandler)_fsm).OrnitoPageGOs[1]);
            }
            else
            {
                SetRightNotebookPageAsParent(((InputHandler)_fsm).OrnitoPageGOs[0]);
                SetLowerAsParent(((InputHandler)_fsm).OrnitoPageGOs[1]);
            }
        }
    }
}
