using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Notebook : FSMTemplateState
{
    protected InputActions _inputActions;
    private InputAction _cursorMovement;
    protected Vector3 _cursorWorldPosition;

    private bool _exitToFreeMove;
    private static bool _changePage;
    protected static bool _changeInnerPage;
    private bool _turnToNextPage;
    private static NotebookPage _pageToGo;
    protected NotebookCursorTarget _cursorTarget;
    private NotebookCursorTarget _lastCursorTarget;

    public Notebook(FSMTemplateMachine fsmMachine, InputActions inputActions) : base(fsmMachine)
    {
        _fsm = fsmMachine;
        _inputActions = inputActions;
    }

    public override void Enter()
    {
        base.Enter();

        if (!_changePage)
        {
            ((InputHandler)_fsm).PlayerAnim.SetTrigger("OpenNotebook");
            ((InputHandler)_fsm).ArmsAnim.SetTrigger("OpenNotebook");
            ((InputHandler)_fsm).NotebookAnim.SetTrigger("OpenNotebook");
            ((InputHandler)_fsm).MapAnim.SetTrigger("OpenNotebook");

            ((InputHandler)_fsm).Clock.GetComponent<MeshRenderer>().enabled = true;
            ((InputHandler)_fsm).MinuteHand.GetComponent<MeshRenderer>().enabled = true;
            ((InputHandler)_fsm).HourHand.GetComponent<MeshRenderer>().enabled = true;

            ((InputHandler)_fsm).ArmsRenderer.enabled = true;
            ((InputHandler)_fsm).NotebookRenderer.enabled = true;

            foreach (Transform postIt in ((InputHandler)_fsm).PagePostIts)
                postIt.gameObject.SetActive(true);
        }
        else
            _changePage = false;

        _inputActions.Notebook.CloseNotebook.performed += ToggleNotebook;
        _inputActions.Notebook.PrimaryCursorTrigger.performed += PrimaryCursorTriggered;
        _inputActions.Notebook.SecondaryCursorTrigger.performed += SecondaryCursorTriggered;
        _cursorMovement = _inputActions.Notebook.CursorMovement;

        UIManager.Instance.ShowCursor();

        _inputActions.Notebook.Enable();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_changePage)
        {
            GetPageState(_pageToGo, out FSMTemplateState state);
            ((InputHandler)_fsm).ChangeState(state);
        }

        if (_exitToFreeMove)
        {
            _fsm.ChangeState(((InputHandler)_fsm).freeMove);
        }
    }


    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        MoveCursor();
        CheckTarget();
        UpdateClock();
    }
    

    public override void Exit() 
    { 
        base.Exit();

        if (_exitToFreeMove) // Leaves the hierarchic level
        {
            if(((InputHandler)_fsm).PlayerAnim != null)
                ((InputHandler)_fsm).PlayerAnim.SetTrigger("CloseNotebook");
            if (((InputHandler)_fsm).ArmsAnim != null)
                ((InputHandler)_fsm).ArmsAnim.SetTrigger("CloseNotebook");
            if (((InputHandler)_fsm).NotebookAnim != null)
                ((InputHandler)_fsm).NotebookAnim.SetTrigger("CloseNotebook");
            if (((InputHandler)_fsm).MapAnim != null)
                ((InputHandler)_fsm).MapAnim.SetTrigger("CloseNotebook");
            ((InputHandler)_fsm).ResetAction = ResetNotebook;
            ((InputHandler)_fsm).IsClosingNotebook = true;
            _exitToFreeMove = false;
        }
        else
        {
            if(_turnToNextPage)
            {
                if (((InputHandler)_fsm).NotebookAnim != null)
                    ((InputHandler)_fsm).NotebookAnim.SetTrigger("NextPage");
                if (((InputHandler)_fsm).MapAnim != null)
                    ((InputHandler)_fsm).MapAnim.SetTrigger("NextPage");
            }
            else
                if (((InputHandler)_fsm).NotebookAnim != null)
                ((InputHandler)_fsm).NotebookAnim.SetTrigger("PreviousPage");
                if (((InputHandler)_fsm).MapAnim != null)
                ((InputHandler)_fsm).MapAnim.SetTrigger("PreviousPage");
        }

        _inputActions.Notebook.CloseNotebook.performed -= ToggleNotebook;
        _inputActions.Notebook.PrimaryCursorTrigger.performed -= PrimaryCursorTriggered;
        _inputActions.Notebook.SecondaryCursorTrigger.performed -= SecondaryCursorTriggered;
        _cursorMovement = null;

        UIManager.Instance.HideCursor();

        _inputActions.Notebook.Disable();

        if (((InputHandler)_fsm).TurnOffAfterTurningPageGOs != null)
        {
            foreach (GameObject gameObject in ((InputHandler)_fsm).TurnOffAfterTurningPageGOs)
                SetLowerAsParent(gameObject);
        }
    }

    private void UpdateClock()
    {
        float hours = GameManager.Instance.Daytime / 60f;
        float minutes = GameManager.Instance.Daytime % 60f;

        ((InputHandler)_fsm).HourHand.transform.localEulerAngles = new Vector3(0f, 0f, -(hours % 12 * 30f));
        ((InputHandler)_fsm).MinuteHand.transform.localEulerAngles = new Vector3(0f, 0f, -(minutes * 6f));
    }

    private void ToggleNotebook(InputAction.CallbackContext context)
    {
        _exitToFreeMove = true;
    }


    private void MoveCursor()
    {
        if (_cursorMovement == null)
            return;

        Vector2 cursorMovement = _cursorMovement.ReadValue<Vector2>();
        if (cursorMovement.magnitude < 0.1f) return;

        if(cursorMovement.normalized.Equals(cursorMovement.magnitude))
            UIManager.Instance.UpdateCursorPosition(UIManager.Instance.GetCursorPosition() + (cursorMovement * Time.deltaTime * SettingsManager.Instance.Database.CursorSensitivity));
        else
            UIManager.Instance.UpdateCursorPosition(UIManager.Instance.GetCursorPosition() + cursorMovement);

    }
    
    public void GoToPage(NotebookPage notebookPage)
    {
        if (((InputHandler)_fsm).CurrentNotebookPage != notebookPage)
        {
            if (((InputHandler)_fsm).CurrentNotebookPage < notebookPage)
                _turnToNextPage = false;
            else
                _turnToNextPage = true;

            _pageToGo = notebookPage;
            _changePage = true;
            _changeInnerPage = false; 
            ((InputHandler)_fsm).IsTurningNotebookPageToRight = _turnToNextPage;
        }
        else 
        {
            if (notebookPage == NotebookPage.Animals) // CurrentNotebookPage == NotebookPage.Animals
            {
                if (((InputHandler)_fsm).CurrentAnimalsPage < ((InputHandler)_fsm).AnimalsPageToGo)
                    _turnToNextPage = false;
                else
                    _turnToNextPage = true;
            }
            else if (notebookPage == NotebookPage.Plants) // CurrentNotebookPage == NotebookPage.Plants
            {
                if (((InputHandler)_fsm).CurrentPlantsPage < ((InputHandler)_fsm).PlantsPageToGo)
                    _turnToNextPage = false;
                else
                    _turnToNextPage = true;
            }
            else if (notebookPage == NotebookPage.Gallery) // CurrentNotebookPage == NotebookPage.Gallery
            {
                if (((InputHandler)_fsm).CurrentGalleryPage < ((InputHandler)_fsm).GalleryPageToGo)
                    _turnToNextPage = false;
                else
                    _turnToNextPage = true;
            }

            _pageToGo = notebookPage;
            _changePage = true;
            _changeInnerPage = true;
            ((InputHandler)_fsm).IsTurningNotebookPageToRight = _turnToNextPage;
        }
    }

    private void GetPageState(NotebookPage notebookPage, out FSMTemplateState state)
    {
        switch (notebookPage)
        {
            case NotebookPage.Map:
                state = ((InputHandler)_fsm).mapPage;
                break;
            case NotebookPage.Animals:
                GetAnimalsPageState(((InputHandler)_fsm).AnimalsPageToGo, out FSMTemplateState animalsPageState);
                state = animalsPageState;
                break;
            case NotebookPage.Plants:
                GetPlantsPageState(((InputHandler)_fsm).PlantsPageToGo, out FSMTemplateState plantsPageState);
                state = plantsPageState;
                break;
            case NotebookPage.Fungus:
                state = ((InputHandler)_fsm).fungusPage;
                break;
            case NotebookPage.Bugs:
                state = ((InputHandler)_fsm).bugsPage;
                break;
            case NotebookPage.Gallery:
                GetGalleryPageState(((InputHandler)_fsm).GalleryPageToGo, out FSMTemplateState galleryPageState);
                state = galleryPageState;
                break;
            case NotebookPage.Settings:
                state = ((InputHandler)_fsm).settingsPage;
                break;
            default:
                throw new System.Exception("Unable to filter page state. Make sure each PagePostItCT has properly set up PageToGo field.");
        }
    }

    private void GetAnimalsPageState(AnimalsPage animalsPageToGo, out FSMTemplateState state)
    {
        switch (animalsPageToGo)
        {
            case AnimalsPage.Mammoth:
                state = ((InputHandler)_fsm).mammothPage;
                break;
            case AnimalsPage.Elk:
                state = ((InputHandler)_fsm).elkPage;
                break;
            case AnimalsPage.Ornito:
                state = ((InputHandler)_fsm).ornitoPage;
                break;
            default:
                throw new System.Exception("Unable to filter animals page state. Make sure each NextPageCT has properly set up PageToGo field.");
        }
    }

    private void GetPlantsPageState(PlantsPage plantsPageToGo, out FSMTemplateState state)
    {
        switch (plantsPageToGo) 
        {
            case PlantsPage.Plants1:
                state = ((InputHandler)_fsm).plantsPage1;
                break;
            case PlantsPage.Plants2:
                state = ((InputHandler)_fsm).plantsPage2;
                break;
            case PlantsPage.Plants3:
                state = ((InputHandler)_fsm).plantsPage3;
                break;
            default: 
                throw new System.Exception("Unable to filter plants page state. Make sure each NextPageCT has properly set up PageToGo field.");
        }
    }

    private void GetGalleryPageState(GalleryPage galleryPageToGo, out FSMTemplateState state)
    {
        switch (galleryPageToGo)
        {
            case GalleryPage.Gallery1:
                state = ((InputHandler)_fsm).galleryPage1;
                break;
            case GalleryPage.Gallery2:
                state = ((InputHandler)_fsm).galleryPage2;
                break;
            case GalleryPage.Gallery3:
                state = ((InputHandler)_fsm).galleryPage3;
                break;
            case GalleryPage.Gallery4:
                state = ((InputHandler)_fsm).galleryPage4;
                break;
            default:
                throw new System.Exception("Unable to filter gallery page state. Make sure each NextPageCT has properly set up PageToGo field.");
        }
    }

    private void CheckTarget()
    {
        _cursorTarget = ((InputHandler)_fsm).GetCursorTarget();
        if(_cursorTarget != null)
        {
            if (!_cursorTarget.Equals(_lastCursorTarget))
            {
                if(_lastCursorTarget != null)
                    _lastCursorTarget.HoveringEnd();
                _lastCursorTarget = _cursorTarget;
            }

            _cursorTarget.Hovering();
        }
    }

    private void PrimaryCursorTriggered(InputAction.CallbackContext context)
    {
        _cursorWorldPosition = UIManager.Instance.GetCursorWorldPosition();
        
        if(_cursorWorldPosition.Equals(Vector3.negativeInfinity)) // Vector3.negativeInfinity is the hardcoded value that reflects an invalid point
            return;

        if (_cursorTarget != null)
            _cursorTarget.PrimaryPressed(_cursorWorldPosition);
    }

    private void SecondaryCursorTriggered(InputAction.CallbackContext context)
    {
        _cursorWorldPosition = UIManager.Instance.GetCursorWorldPosition();

        if (_cursorWorldPosition.Equals(Vector3.negativeInfinity)) // Vector3.negativeInfinity is the hardcoded value that reflects an invalid point
            return;

        if (_cursorTarget != null)
            _cursorTarget.SecondaryPressed(_cursorWorldPosition);
    }

    public void NextPage()
    {
        ((InputHandler)_fsm).NotebookAnim.SetTrigger("NextPage");
        ((InputHandler)_fsm).MapAnim.SetTrigger("NextPage");
    }

    public void PreviousPage()
    {
        ((InputHandler)_fsm).NotebookAnim.SetTrigger("PreviousPage");
        ((InputHandler)_fsm).MapAnim.SetTrigger("PreviousPage");
    }

    protected void SetPagePostItParent(NotebookPage notebookPage)
    {
        int minIndex = Mathf.Min((int)notebookPage, (int)((InputHandler)_fsm).CurrentNotebookPage);
        int maxIndex = Mathf.Max((int)notebookPage, (int)((InputHandler)_fsm).CurrentNotebookPage);

        for (int i = 0; i < ((InputHandler)_fsm).PagePostIts.Length; i++)
        {
            if((i == (int)notebookPage && (notebookPage > ((InputHandler)_fsm).CurrentNotebookPage)) || (i > minIndex && i < maxIndex) || (i == (int)((InputHandler)_fsm).CurrentNotebookPage) && (notebookPage < ((InputHandler)_fsm).CurrentNotebookPage))
            {
                ((InputHandler)_fsm).PagePostIts[i].parent = ((InputHandler)_fsm).NotebookPageTransform;
                ((InputHandler)_fsm).PagePostIts[i].localPosition = ((InputHandler)_fsm).PagePostIts[i].GetComponent<PagePostItCT>().NotebookPagePosition;
            }
            else if(i <= minIndex)
            {
                ((InputHandler)_fsm).PagePostIts[i].parent = ((InputHandler)_fsm).UpperCoverTransform;
                ((InputHandler)_fsm).PagePostIts[i].localPosition = ((InputHandler)_fsm).PagePostIts[i].GetComponent<PagePostItCT>().UpperCoverPosition;
            }
            else
            {
                ((InputHandler)_fsm).PagePostIts[i].parent = ((InputHandler)_fsm).LowerCoverTransform;
                ((InputHandler)_fsm).PagePostIts[i].localPosition = ((InputHandler)_fsm).PagePostIts[i].GetComponent<PagePostItCT>().LoweCoverPosition;
            }
        }
    }

    protected void ClearPostItChildsFromNotebookPage()
    {
        for (int i = 0; i < ((InputHandler)_fsm).PagePostIts.Length; i++)
        {
            if (i <= (int)((InputHandler)_fsm).CurrentNotebookPage)
            {
                ((InputHandler)_fsm).PagePostIts[i].parent = ((InputHandler)_fsm).UpperCoverTransform;
                ((InputHandler)_fsm).PagePostIts[i].localPosition = ((InputHandler)_fsm).PagePostIts[i].GetComponent<PagePostItCT>().UpperCoverPosition;
            }
            else
            {
                ((InputHandler)_fsm).PagePostIts[i].parent = ((InputHandler)_fsm).LowerCoverTransform;
                ((InputHandler)_fsm).PagePostIts[i].localPosition = ((InputHandler)_fsm).PagePostIts[i].GetComponent<PagePostItCT>().LoweCoverPosition;
            }
        }
    }

    protected void SetUpperAsParent(GameObject gameObject) 
    {
        gameObject.transform.parent = ((InputHandler)_fsm).UpperCoverTransform;
        gameObject.transform.localRotation = Quaternion.Euler(new Vector3(180f, 180f, -90f));
    }

    protected void SetLeftNotebookPageAsParent(GameObject gameObject)
    {
        gameObject.transform.parent = ((InputHandler)_fsm).NotebookPageTransform;
        gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
    }

    protected void SetRightNotebookPageAsParent(GameObject gameObject)
    {
        gameObject.transform.parent = ((InputHandler)_fsm).NotebookPageTransform;
        gameObject.transform.localRotation = Quaternion.Euler(new Vector3(180f, 0f, -90f));
    }

    protected void SetLowerAsParent(GameObject gameObject) 
    {
        gameObject.transform.parent = ((InputHandler)_fsm).LowerCoverTransform;
        gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
    }

    private void ResetNotebook()
    {
        AnimatorStateInfo animatorStateInfo = ((InputHandler)_fsm).NotebookAnim.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.IsName("CloseBook"))
        {
            ResetPostIts();
            DisablePostIts();
            ResetPages();

            ((InputHandler)_fsm).CurrentNotebookPage = NotebookPage.Map;
            ((InputHandler)_fsm).CurrentAnimalsPage = AnimalsPage.Mammoth;
            ((InputHandler)_fsm).CurrentPlantsPage = PlantsPage.Plants1;
            ((InputHandler)_fsm).CurrentGalleryPage = GalleryPage.Gallery1;

            ((InputHandler)_fsm).TurnOffAfterTurningPageGOs = null;
        }
    }

    private void ResetPostIts()
    {
        foreach (Transform postIt in ((InputHandler)_fsm).PagePostIts)
            postIt.gameObject.SetActive(true);
    }

    private void DisablePostIts()
    {
        foreach (Transform postIt in ((InputHandler)_fsm).PagePostIts)
            postIt.gameObject.SetActive(false);
    }

    private void ResetPages()
    {
        foreach(GameObject gameObject in ((InputHandler)_fsm).MammothPageGOs)
        {
            gameObject.SetActive(false);
            SetLowerAsParent(gameObject);
        }
        foreach (GameObject gameObject in ((InputHandler)_fsm).ElkPageGOs)
        {
            gameObject.SetActive(false);
            SetLowerAsParent(gameObject);
        }
        foreach (GameObject gameObject in ((InputHandler)_fsm).OrnitoPageGOs)
        {
            gameObject.SetActive(false);
            SetLowerAsParent(gameObject);
        }

        foreach (GameObject gameObject in ((InputHandler)_fsm).PlantsPage1GOs)
        {
            gameObject.SetActive(false);
            SetLowerAsParent(gameObject);
        }
        foreach (GameObject gameObject in ((InputHandler)_fsm).PlantsPage2GOs)
        {
            gameObject.SetActive(false);
            SetLowerAsParent(gameObject);
        }
        foreach (GameObject gameObject in ((InputHandler)_fsm).PlantsPage3GOs)
        {
            gameObject.SetActive(false);
            SetLowerAsParent(gameObject);
        }

        foreach (GameObject gameObject in ((InputHandler)_fsm).FungusPageGOs)
        {
            gameObject.SetActive(false);
            SetLowerAsParent(gameObject);
        }
        foreach (GameObject gameObject in ((InputHandler)_fsm).BugsPageGOs)
        {
            gameObject.SetActive(false);
            SetLowerAsParent(gameObject);
        }

        foreach (GameObject gameObject in ((InputHandler)_fsm).GalleryPage1GOs)
        {
            gameObject.SetActive(false);
            SetLowerAsParent(gameObject);
        }
        foreach (GameObject gameObject in ((InputHandler)_fsm).GalleryPage2GOs)
        {
            gameObject.SetActive(false);
            SetLowerAsParent(gameObject);
        }
        foreach (GameObject gameObject in ((InputHandler)_fsm).GalleryPage3GOs)
        {
            gameObject.SetActive(false);
            SetLowerAsParent(gameObject);
        }
        foreach (GameObject gameObject in ((InputHandler)_fsm).GalleryPage4GOs)
        {
            gameObject.SetActive(false);
            SetLowerAsParent(gameObject);
        }

        foreach (GameObject gameObject in ((InputHandler)_fsm).SettingsPageGOs)
        {
            gameObject.SetActive(false);
            SetLowerAsParent(gameObject);
        }
    }
}
