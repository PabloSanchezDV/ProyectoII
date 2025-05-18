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
        ((InputHandler)_fsm).MapCT.ShowPins();
        ((InputHandler)_fsm).MapCollider.enabled = true;
        ((InputHandler)_fsm).MapRenderer.enabled = true;

    }

    public override void Exit()
    {
        base.Exit();

        if(((InputHandler)_fsm).MapCT != null)
            ((InputHandler)_fsm).MapCT.HideMapElements();
        if (((InputHandler)_fsm).MapCollider != null)
            ((InputHandler)_fsm).MapCollider.enabled = false;
    }    
}
