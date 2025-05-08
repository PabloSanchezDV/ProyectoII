using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image fader;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Play()
    {
        StartCoroutine(FadeOut());
    }

    public void Exit()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }

    IEnumerator FadeOut()
    {
        yield return null;
        Color color = fader.color;
        color.a += Time.deltaTime;
        fader.color = color;

        if (color.a < 1)
        {
            StartCoroutine(FadeOut());
        }
        else
            SceneManager.LoadScene(1);
    }
}
