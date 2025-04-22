using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float _daytime;
    [SerializeField] private float _daytimeModifier;
    [SerializeField] private int _startHour;
    [SerializeField] private int _startMinute;
    [SerializeField] private int _endingHour;
    [SerializeField] private int _endingMinute;
    
    private bool _dayEnded = false;

    public float Daytime { get { return _daytime; } set { _daytime = value; } }
    public float StartHour { get { return _startHour; } }
    public float StartMinute { get { return _startMinute; } }

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        _daytime = _startHour * 60 + _startMinute;
    }

    void Update()
    {
        _daytime += _daytimeModifier * Time.deltaTime;
        UIManager.Instance.UpdateClock(_daytime);

        if (!_dayEnded)
        {
            if (_daytime >= _endingHour * 60 + _endingMinute)
            {
                UIManager.Instance.TriggerFadeOut();
                _dayEnded = true;
            }
        }
    }

    public void EndDay()
    {
        Debug.Log("Day ended at " + _daytime + "s");
        SceneManager.LoadScene(0);
    }
}
