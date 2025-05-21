using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryPictureCT : NotebookCursorTarget
{
    [SerializeField] private InputHandler _inputHandler;
    [NonSerialized] public int galleryPictureIndex;

    public override void SecondaryPressed(Vector3 pressedWorldPosition)
    {
        _inputHandler.DeleteGalleryPicture(galleryPictureIndex);
    }
}
