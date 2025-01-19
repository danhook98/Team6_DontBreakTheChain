using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Canvas StartScreen;
    [SerializeField] Canvas OptionScreen;
    [SerializeField] Canvas MainScreen;

    public void OpenStartScreen()
    {
        StartScreen.enabled = true;
        MainScreen.enabled = false;
    }

    public void OpenOptionScreen()
    {
        OptionScreen.enabled = true;
        MainScreen.enabled = false;
    }

    public void BackFromOptions()
    {
        OptionScreen.enabled = false;
        MainScreen.enabled = true;
    }

    public void BackFromStart()
    {
        StartScreen.enabled = false;
        MainScreen.enabled = true;
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void StartSinglePlayer()
    {
        SceneManager.LoadScene(0);
    }

    public void StartingMultiplayer()
    {
        SceneManager.LoadScene(1);
    }
}
