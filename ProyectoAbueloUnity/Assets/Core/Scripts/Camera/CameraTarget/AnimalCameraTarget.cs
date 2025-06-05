using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PhotographableObjects/AnimalPhotographable")]
public class AnimalCameraTarget : CameraTarget
{
    [NonSerialized] private AnimalFSM animalFSM;
    private Animal _animal;

    public void InitializeAnimalCameraTarget(Animal animal, AnimalFSM animalFSM)
    {
        _animal = animal;
        this.animalFSM = animalFSM;
    }

    public override Target GetTarget()
    {
        switch (_animal) 
        {
            case (Animal.Mammoth):
                return GetMammothActionAsTarget();
            case (Animal.Elk):
                return GetElkActionAsTarget();
            case (Animal.Ornito):
                return GetOrnitoActionAsTarget();
            default:
                throw new System.Exception("Couldn't switch from Animal to Target.");
        }
    }

    #region Animal Actions
    private Target GetMammothActionAsTarget()
    {
        switch (animalFSM.CurrentAction)
        {
            case (Action.Walking):
                return Target.MammothGlobal; // Walking is the default action. It triggers the animal but doesn't trigger any specific state 
            case (Action.Action1):
                return Target.MammothEat;
            case (Action.Action2):
                return Target.MammothSleep;
            case (Action.Action3):
                return Target.MammothHeadbutt;
            case (Action.Action4):
                return Target.MammothShake;
            case (Action.Other):
                return Target.MammothGlobal;
            default:
                throw new System.Exception("Cannot convert Mammoth Action to Target. Check Mammoth action is properly set.");
        }
    }

    private Target GetElkActionAsTarget()
    {
        switch (animalFSM.CurrentAction)
        {
            case (Action.Walking):
                return Target.ElkGlobal;
            case (Action.Action1):
                return Target.ElkEat;
            case (Action.Action2):
                return Target.ElkShake;
            case (Action.Action3):
                return Target.ElkGrowl;
            case (Action.Action4):
                return Target.ElkShowOff;
            case (Action.Other):
                return Target.ElkGlobal;
            default:
                throw new System.Exception("Cannot convert Elk Action to Target. Check Elk action is properly set.");
        }
    }

    //private string GetBirdAction(Action action)
    //{
    //    switch (action)
    //    {
    //        case (Action.Action1):
    //            return "Comiendo";
    //        case (Action.Action2):
    //            return "Tragando piedras";
    //        case (Action.Action3):
    //            return "Picoteando al mamut";
    //        case (Action.Action4):
    //            return "Volando";
    //        case (Action.Other):
    //            return "";
    //        default:
    //            throw new System.Exception("Cannot get Bird action. Check Bird action is properly set.");
    //    }
    //}

    private Target GetOrnitoActionAsTarget()
    {
        switch (animalFSM.CurrentAction)
        {
            case (Action.Walking):
                return Target.OrnitoGlobal;
            case (Action.Action1):
                return Target.OrnitoEat;
            case (Action.Action2):
                return Target.OrnitoSwim;
            case (Action.Action3):
                return Target.OrnitoSunbathing;
            case (Action.Action4):
                return Target.OrnitoProtect;
            case (Action.Other):
                return Target.OrnitoGlobal;
            default:
                throw new System.Exception("Cannot convert Ornito Action to Target. Check Ornito action is properly set.");
        }
    }
    #endregion
}
