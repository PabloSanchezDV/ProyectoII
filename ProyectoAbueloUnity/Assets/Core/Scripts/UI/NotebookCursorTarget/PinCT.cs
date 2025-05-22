using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinCT : NotebookCursorTarget
{
    public PostItCT postIt;
    public Color color;
    public string text;
    public Vector3 localPosition;
    public MapCT mapCT;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.materials[1].color = color;
    }

    public override void Hovering()
    {
        Debug.Log("Hovering over " + transform.name);
        _meshRenderer.materials[1].color = Color.yellow;
    }

    public override void HoveringEnd()
    {
        _meshRenderer.materials[1].color = color;
    }

    public override void PrimaryPressed(Vector3 pressedWorldPosition)
    {
        postIt.activePinCT = this;
        postIt.SetText(text);
        postIt.gameObject.SetActive(true);
    }

    public override void SecondaryPressed(Vector3 pressedWorldPosition)
    {
        mapCT.RemovePinFromList(gameObject);
        Destroy(gameObject);
    }
}
