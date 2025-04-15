using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notebook : FSMTemplateState
{
    public Notebook(FSMTemplateMachine fsmMachine) : base(fsmMachine)
    {
        _fsm = fsmMachine;
    }
}
