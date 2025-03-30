using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void PlayTutorial()
    {
        SceneManager.LoadScene(1);

    }
    public void PlayLevel1()
    {
        SceneManager.LoadScene(2);

    }
    public void PlayLevel2()
    {
        SceneManager.LoadScene(3);

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
