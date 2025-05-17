using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PagePostItCT : NotebookCursorTarget
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private NotebookPage _pageToGo = NotebookPage.None;

    [SerializeField] private Vector3 _upperCoverPosition;
    [SerializeField] private Vector3 _notebookPagePosition;
    [SerializeField] private Vector3 _lowerCoverPosition;

    public Vector3 UpperCoverPosition { get { return _upperCoverPosition; } }
    public Vector3 NotebookPagePosition {  get { return _notebookPagePosition; } }
    public Vector3 LoweCoverPosition {  get { return _lowerCoverPosition; } }

    public override void PrimaryPressed(Vector3 pressedWorldPosition)
    {
        _inputHandler.GoToPage(_pageToGo);
    }
}
