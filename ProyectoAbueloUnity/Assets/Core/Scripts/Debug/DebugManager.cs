using UnityEngine;

public class DebugManager : MonoBehaviour 
{
    public static DebugManager Instance;

    [SerializeField] private bool _showAnimalMessages;
    [SerializeField] private bool _showGlobalSystemsMessages;
    [SerializeField] private bool _showAnimalStateChangesMessages;
    [SerializeField] private bool _showInputHandlerStateChangesMessages;
    [SerializeField] private bool _showDebugMessages;
    [SerializeField] private bool _showDebugCameraSystemMessages;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void DebugAnimalMessage(string message)
    {
        if(_showAnimalMessages)
            Debug.Log(message);
    }

    public void DebugGlobalSystemMessage(string message)
    {
        if (_showGlobalSystemsMessages)
            Debug.Log(message);
    }

    public void DebugMessage(string message)
    {
        if (_showDebugMessages)
            Debug.Log(message);
    }

    public void DebugAnimalStateChangeMessage(string message)
    {
        if(_showAnimalStateChangesMessages)
            Debug.Log(message);
    }

    public void DebugInputHandlerStateChangeMessage(string message)
    {
        if(_showInputHandlerStateChangesMessages)
            Debug.Log(message);
    }

    public void DebugCameraSystemMessage(string message)
    {
        if(_showDebugCameraSystemMessages)
            Debug.Log(message);
    }
}
