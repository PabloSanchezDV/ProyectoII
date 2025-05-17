using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryPageCT : NotebookCursorTarget
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private GalleryPage _pageToGo;

    public override void PrimaryPressed(Vector3 pressedWorldPosition)
    {
        _inputHandler.GoToGalleryPage(_pageToGo);
    }
}
