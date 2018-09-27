using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void GoToGame()
    {
        SceneManager.LoadScene("DevScene");
    }

    public void GoToHowToPlay()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
