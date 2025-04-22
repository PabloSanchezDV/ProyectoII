using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsHolder : MonoBehaviour
{
    public static AnimalsHolder Instance;
    
    private List<AnimalFSM> animals = new List<AnimalFSM>();

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
            AnimalFSM animal = animalGO.GetComponent<AnimalFSM>();
            animals.Add(animal);
        }
    }

    public void CheckAnimalsOnCamera()
    {
        bool isAnimalOnPicture = false;
        foreach(AnimalFSM animal in animals)
        {
            if(animal.IsOnCamera())
            {
                Debug.Log(animal.ToString() + " captured in camera doing " + animal.CurrentAction + ".");
                UIManager.Instance.UpdateAnimalPicture(animal.animal, animal.CurrentAction);
                isAnimalOnPicture = true;
            }
        }
        if(!isAnimalOnPicture)
            UIManager.Instance.UpdateAnimalPicture(Animal.None); // It will leave both fields empty whenever the Animal is set to None
    }
}
