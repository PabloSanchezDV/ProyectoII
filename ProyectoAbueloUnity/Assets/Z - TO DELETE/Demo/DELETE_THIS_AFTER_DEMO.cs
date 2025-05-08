using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DELETE_THIS_AFTER_DEMO : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene(0);
        }
    }
}
