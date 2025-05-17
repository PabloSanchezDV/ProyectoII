using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : Notebook
{
    public Settings(FSMTemplateMachine fsmMachine, InputActions inputActions) : base(fsmMachine, inputActions) { }

    public override void Enter()
    {
        base.Enter();

        SetPagePostItParent(NotebookPage.Settings);
        foreach (GameObject go in ((InputHandler)_fsm).SettingsPageGOs)
            go.SetActive(true);
        ((InputHandler)_fsm).CurrentNotebookPage = NotebookPage.Settings;

        if (!((InputHandler)_fsm).IsTurningNotebookPageToRight)
        {
            SetRightNotebookPageAsParent(((InputHandler)_fsm).SettingsPageGOs[0]);
            SetLowerAsParent(((InputHandler)_fsm).SettingsPageGOs[1]);
        }
        else
        {
            SetUpperAsParent(((InputHandler)_fsm).SettingsPageGOs[0]);
            SetLeftNotebookPageAsParent(((InputHandler)_fsm).SettingsPageGOs[1]);
        }
    }

    public override void Exit()
    {
        base.Exit();
        ((InputHandler)_fsm).TurnOffAfterTurningPageGOs = ((InputHandler)_fsm).SettingsPageGOs;

        if (((InputHandler)_fsm).SettingsPageGOs != null)
        {
            if (!((InputHandler)_fsm).IsTurningNotebookPageToRight)
            {
                SetUpperAsParent(((InputHandler)_fsm).SettingsPageGOs[0]);
                SetLeftNotebookPageAsParent(((InputHandler)_fsm).SettingsPageGOs[1]);
            }
            else
            {
                SetRightNotebookPageAsParent(((InputHandler)_fsm).SettingsPageGOs[0]);
                SetLowerAsParent(((InputHandler)_fsm).SettingsPageGOs[1]);
            }
        }
    }
}
