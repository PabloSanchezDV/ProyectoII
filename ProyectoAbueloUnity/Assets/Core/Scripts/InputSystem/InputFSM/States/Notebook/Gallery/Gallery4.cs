using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gallery4 : Gallery
{
    public Gallery4(FSMTemplateMachine fsmMachine, InputActions inputActions) : base(fsmMachine, inputActions) { }

    public override void Enter()
    {
        base.Enter();
        foreach (GameObject go in ((InputHandler)_fsm).GalleryPage4GOs)
            go.SetActive(true);
        ((InputHandler)_fsm).CurrentGalleryPage = GalleryPage.Gallery4;

        if (!((InputHandler)_fsm).IsTurningNotebookPageToRight)
        {
            SetRightNotebookPageAsParent(((InputHandler)_fsm).GalleryPage4GOs[0]);
            SetLowerAsParent(((InputHandler)_fsm).GalleryPage4GOs[1]);
        }
        else
        {
            SetUpperAsParent(((InputHandler)_fsm).GalleryPage4GOs[0]);
            SetLeftNotebookPageAsParent(((InputHandler)_fsm).GalleryPage4GOs[1]);
        }
    }

    public override void Exit()
    {
        base.Exit();
        ((InputHandler)_fsm).TurnOffAfterTurningPageGOs = ((InputHandler)_fsm).GalleryPage4GOs;

        if (((InputHandler)_fsm).GalleryPage4GOs != null)
        {
            if (!((InputHandler)_fsm).IsTurningNotebookPageToRight)
            {
                SetUpperAsParent(((InputHandler)_fsm).GalleryPage4GOs[0]);
                SetLeftNotebookPageAsParent(((InputHandler)_fsm).GalleryPage4GOs[1]);
            }
            else
            {
                SetRightNotebookPageAsParent(((InputHandler)_fsm).GalleryPage4GOs[0]);
                SetLowerAsParent(((InputHandler)_fsm).GalleryPage4GOs[1]);
            }
        }
    }
}
