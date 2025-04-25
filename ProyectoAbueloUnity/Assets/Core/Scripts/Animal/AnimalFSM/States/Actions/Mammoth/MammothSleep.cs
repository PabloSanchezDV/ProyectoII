using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MammothSleep : Action2
{
    private AudioSource _sleepingAS;

    public MammothSleep(FSMTemplateMachine _fsm) : base(_fsm)
    { }

    public override void Enter()
    {
        base.Enter();
        _sleepingAS = AudioManager.Instance.PlaySleepingMammothSound(_fsm.gameObject);
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.Instance.StopAudioSource(_sleepingAS);
    }
}
