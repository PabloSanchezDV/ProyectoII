using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Plants2 : Plants
{
    public Plants2(FSMTemplateMachine fsmMachine, InputActions inputActions) : base(fsmMachine, inputActions) { }

    public override void Enter()
    {
        base.Enter();
        foreach (GameObject go in ((InputHandler)_fsm).PlantsPage2GOs)
            go.SetActive(true);
        ((InputHandler)_fsm).CurrentPlantsPage = PlantsPage.Plants2;

        if (!((InputHandler)_fsm).IsTurningNotebookPageToRight)
        {
            SetRightNotebookPageAsParent(((InputHandler)_fsm).PlantsPage2GOs[0]);
            SetLowerAsParent(((InputHandler)_fsm).PlantsPage2GOs[1]);
        }
        else
        {
            SetUpperAsParent(((InputHandler)_fsm).PlantsPage2GOs[0]);
            SetLeftNotebookPageAsParent(((InputHandler)_fsm).PlantsPage2GOs[1]);
        }
    }

    public override void Exit()
    {
        base.Exit();
        ((InputHandler)_fsm).TurnOffAfterTurningPageGOs = ((InputHandler)_fsm).PlantsPage2GOs;

        if (((InputHandler)_fsm).PlantsPage2GOs != null)
        {
            if (!((InputHandler)_fsm).IsTurningNotebookPageToRight)
            {
                SetUpperAsParent(((InputHandler)_fsm).PlantsPage2GOs[0]);
                SetLeftNotebookPageAsParent(((InputHandler)_fsm).PlantsPage2GOs[1]);
            }
            else
            {
                SetRightNotebookPageAsParent(((InputHandler)_fsm).PlantsPage2GOs[0]);
                SetLowerAsParent(((InputHandler)_fsm).PlantsPage2GOs[1]);
            }
        }
    }
}
