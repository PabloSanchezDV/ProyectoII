using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void StartNewGame()
    {
        ScenesController.Instance.NewGame();
    }

    public void ContinueGame()
    {
        ScenesController.Instance.ContinueGame();
    }

    public void Exit()
    {
        ScenesController.Instance.QuitGame();
    }
}
