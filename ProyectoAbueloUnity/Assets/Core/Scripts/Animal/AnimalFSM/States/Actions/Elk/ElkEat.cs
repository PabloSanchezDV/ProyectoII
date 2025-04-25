using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElkEat : Action1
{
    private AudioSource _eatingAS;
    
    public ElkEat(FSMTemplateMachine fsm) : base(fsm) { }

    public override void Enter()
    {
        base.Enter();
        _eatingAS = AudioManager.Instance.PlayEatingElkSound(_fsm.gameObject);
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.Instance.StopAudioSource(_eatingAS);
    }
}
