using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Minigame"); // ������ "GameScene" �� ����� ���� ����� � ����
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
