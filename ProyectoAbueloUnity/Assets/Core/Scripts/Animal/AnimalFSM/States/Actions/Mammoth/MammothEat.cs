using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MammothEat : Action1
{
    private AudioSource _eatingAS;

    public MammothEat(FSMTemplateMachine _fsm) : base(_fsm)
    { }

    public override void Enter()
    {
        base.Enter();
        _eatingAS = AudioManager.Instance.PlayEatingMammothSound(_fsm.gameObject);
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.Instance.StopAudioSource(_eatingAS);
    }
}
