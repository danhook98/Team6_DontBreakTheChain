using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Canvas StartScreen;
    [SerializeField] private Canvas OptionScreen;
    [SerializeField] private Canvas MainScreen;

    [SerializeField] private GameTypeSO gameType;

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
        gameType.opponentIsAi = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartingMultiplayer()
    {
        gameType.opponentIsAi = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
