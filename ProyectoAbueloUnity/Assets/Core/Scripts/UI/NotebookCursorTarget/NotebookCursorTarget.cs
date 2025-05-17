using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NotebookCursorTarget : MonoBehaviour
{
    public virtual void Hovering() { }

    public virtual void HoveringEnd() { }

    public virtual void PrimaryPressed(Vector3 pressedWorldPosition) { }

    public virtual void SecondaryPressed(Vector3 pressedWorldPosition) { }
}
