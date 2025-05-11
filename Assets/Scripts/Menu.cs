using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Minigame"); // Замініть "GameScene" на назву вашої сцени з грою
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu() 
    {
        SceneManager.LoadScene("Menu");
    }


}
