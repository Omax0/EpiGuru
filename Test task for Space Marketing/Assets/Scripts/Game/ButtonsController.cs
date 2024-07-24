using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PauseGame()
    {
        GameManager.isGamePaused = true;
    }

    public void CloseGame()
    {
        Debug.Log("Close Game");
        Application.Quit();
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
