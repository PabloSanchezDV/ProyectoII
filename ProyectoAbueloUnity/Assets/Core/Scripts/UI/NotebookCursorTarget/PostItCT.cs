using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PostItCT : NotebookCursorTarget
{
    [SerializeField] private TMP_InputField _inputField;
    [NonSerialized] public PinCT activePinCT;

    public override void PrimaryPressed(Vector3 pressedWorldPosition)
    {
        _inputField.ActivateInputField();
        _inputField.onEndEdit.AddListener(SaveTextOnEndEdit);
    }

    private void SaveTextOnEndEdit(string s)
    {
        SaveText();
        _inputField.onEndEdit.RemoveListener(SaveTextOnEndEdit);
    }

    public void SaveText()
    {
        if(activePinCT != null)
            activePinCT.text = _inputField.text;
    }

    public void SetText(string text)
    {
        _inputField.text = text;
    }
}
