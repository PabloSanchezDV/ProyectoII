using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteUnmuteButtonCT : NotebookCursorTarget
{
    [SerializeField] private Sprite _muteSprite;
    [SerializeField] private Sprite _unmuteSprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public override void PrimaryPressed(Vector3 pressedWorldPosition)
    {
        base.PrimaryPressed(pressedWorldPosition);
        bool buttonStatus = SettingsManager.Instance.ToggleSFX();

        if(buttonStatus)
            _spriteRenderer.sprite = _unmuteSprite;
        else
            _spriteRenderer.sprite = _muteSprite;
    }

}
