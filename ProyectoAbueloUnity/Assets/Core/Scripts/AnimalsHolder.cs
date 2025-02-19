using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsHolder : MonoBehaviour
{
    public static AnimalsHolder instance;
    
    private List<Animal> animals = new List<Animal>();

    private void Awake()
    {
        if(instance == null)
            instance = this;
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

    public void ChekAnimlasInCamera()
    {
        foreach (Animal animal in animals)
        {
            if (animal != null)
            {
                animal.ShowIfItsOnCamera();
            }
        }
    }
}
