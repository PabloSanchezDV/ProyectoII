using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PhotographableObjects/AnimalPhotographable")]
public class AnimalCameraTarget : CameraTarget
{
    [NonSerialized] public Animal animal;
    [NonSerialized] private AnimalFSM animalFSM;
    [NonSerialized] public Transform[] checkPoints;

    public void InitializeAnimalCameraTarget(Transform transform, Animal animal, AnimalFSM animalFSM, Transform[] checkPoints)
    {
        InitializeCameraTarget(transform);

        this.animal = animal;
        this.animalFSM = animalFSM;
        this.checkPoints = checkPoints;
    }

    public override bool DoesRayHit(Camera camera)
    {
        foreach (Transform checkpoint in checkPoints)
        {
            Vector3 direction = checkpoint.transform.position - camera.transform.position;
            if (Physics.Raycast(camera.transform.position, direction, out RaycastHit hit, Mathf.Infinity))
            {

                if (hit.transform.gameObject.Equals(targetTransform.gameObject))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        throw new Exception("The animal " + targetTransform.name + " doesn't have any checkpoints.");
    }

    public string GetAnimalAction()
    {
        switch (animal)
        {
            case (Animal.Mammoth):
                return GetMammothAction(animalFSM.CurrentAction);
            case (Animal.Elk):
                return GetElkAction(animalFSM.CurrentAction);
            case (Animal.Bird):
                return GetBirdAction(animalFSM.CurrentAction);
            case (Animal.Ornito):
                return GetOrnitoAction(animalFSM.CurrentAction);
            case (Animal.None):
                return "";
            default:
                throw new System.Exception("Cannot get animal action. Check animal name and action are properly set.");
        }
    }

    #region Animal Actions
    private string GetMammothAction(Action action)
    {
        switch (action)
        {
            case (Action.Walking):
                return "";          // Walking is the default action. It triggers the animal but doesn't trigger any specific state 
            case (Action.Action1):
                return "Comiendo";
            case (Action.Action2):
                return "Durmiendo";
            case (Action.Action3):
                return "Placando árbol";
            case (Action.Action4):
                return "Bañándose";
            case (Action.Other):
                return "";
            default:
                throw new System.Exception("Cannot get Mammoth action. Check Mammoth action is properly set.");
        }
    }

    private string GetElkAction(Action action)
    {
        switch (action)
        {
            case (Action.Action1):
                return "Comiendo";
            case (Action.Action2):
                return "Agitando los adornos";
            case (Action.Action3):
                return "Berreando";
            case (Action.Action4):
                return "Presumiendo";
            case (Action.Other):
                return "";
            default:
                throw new System.Exception("Cannot get Elk action. Check Elk action is properly set.");
        }
    }

    private string GetBirdAction(Action action)
    {
        switch (action)
        {
            case (Action.Action1):
                return "Comiendo";
            case (Action.Action2):
                return "Tragando piedras";
            case (Action.Action3):
                return "Picoteando al mamut";
            case (Action.Action4):
                return "Volando";
            case (Action.Other):
                return "";
            default:
                throw new System.Exception("Cannot get Bird action. Check Bird action is properly set.");
        }
    }

    private string GetOrnitoAction(Action action)
    {
        switch (action)
        {
            case (Action.Action1):
                return "Comiendo";
            case (Action.Action2):
                return "Nadando";
            case (Action.Action3):
                return "Tomando el sol";
            case (Action.Action4):
                return "Protegiendo el nido";
            case (Action.Other):
                return "";
            default:
                throw new System.Exception("Cannot get Ornito action. Check Ornito action is properly set.");
        }
    }
    #endregion
}
