using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsHolder : MonoBehaviour
{
    public static AnimalsHolder Instance;
    
    private List<AnimalDeprecated> animals = new List<AnimalDeprecated>();

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        GameObject[] animalsGO = GameObject.FindGameObjectsWithTag("Animal");

        foreach(GameObject animalGO in animalsGO)
        {
            AnimalDeprecated animal = animalGO.GetComponent<AnimalDeprecated>();
            animals.Add(animal);
        }
    }

    public void UpdateAllAnimals(float time)
    {
        foreach (AnimalDeprecated animal in animals)
        {
            if (animal != null)
            {
                animal.UpdateAnimal(time);
            }
        }
    }

    public void UpdateAllAnimalsAfterDebugTimeChange(float time)
    {
        foreach(AnimalDeprecated animal in animals)
        {
            if(animal != null)
            {
                animal.UpdatePositionAndRotationOnDebugTimeChange(time);
            }
        }
    }

    public void CheckAnimalsOnCamera()
    {
        foreach(AnimalDeprecated animal in animals)
        {
            if(animal.IsOnCamera())
                Debug.Log(animal.ToString() + " captured in camera doing " + animal.Action + ".");
        }
    }
}
