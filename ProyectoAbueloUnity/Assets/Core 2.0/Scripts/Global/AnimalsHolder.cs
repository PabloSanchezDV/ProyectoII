using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsHolder : MonoBehaviour
{
    public static AnimalsHolder Instance;
    
    private List<Animal> animals = new List<Animal>();

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
            Animal animal = animalGO.GetComponent<Animal>();
            animals.Add(animal);
        }
    }

    public void UpdateAllAnimals(float time)
    {
        foreach (Animal animal in animals)
        {
            if (animal != null)
            {
                animal.UpdateAnimal(time);
            }
        }
    }

    public void UpdateAllAnimalsAfterDebugTimeChange(float time)
    {
        foreach(Animal animal in animals)
        {
            if(animal != null)
            {
                animal.UpdatePositionAndRotationOnDebugTimeChange(time);
            }
        }
    }

    public void CheckAnimalsOnCamera()
    {
        foreach(Animal animal in animals)
        {
            if(animal.IsOnCamera())
                Debug.Log(animal.ToString() + " captured in camera doing " + animal.Action + ".");
        }
    }
}
