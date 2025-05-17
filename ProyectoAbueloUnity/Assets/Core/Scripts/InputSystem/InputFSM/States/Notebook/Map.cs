using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : Notebook
{
    public Map(FSMTemplateMachine fsmMachine, InputActions inputActions) : base(fsmMachine, inputActions) { }

    public override void Enter()
    {
        base.Enter();

        SetPagePostItParent(NotebookPage.Map);
        ((InputHandler)_fsm).CurrentNotebookPage = NotebookPage.Map;
        ((InputHandler)_fsm).NotebookCT.ShowPins();
        ((InputHandler)_fsm).NotebookCollider.enabled = true;
    }

    public override void Exit()
    {
        base.Exit();

        ((InputHandler)_fsm).NotebookCT.HideMapElements();
        ((InputHandler)_fsm).NotebookCollider.enabled = false;
    }    
}
