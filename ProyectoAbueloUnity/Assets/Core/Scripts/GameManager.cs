using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private float _daytime;
    public float Daytime { get { return _daytime; } set { _daytime = value; } }

    [SerializeField] private float _daytimeModifier;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        _daytime = 0;
    }

    void Update()
    {
        _daytime += _daytimeModifier * Time.deltaTime;
        //Debug.Log(_daytime);
        AnimalsHolder.instance.UpdateAllAnimals(_daytime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AnimalsHolder.instance.ChekAnimlasInCamera();
        }
    }
}
