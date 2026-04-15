using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    /// <summary>
    /// Loads the next scene according to the current Scene's buildIndex
    /// </summary>
    public void LoadNextScene()
    {
        Debug.Log($"On Scene Change: Game manager is {GameManager.instance.gameObject.name}");

        // Loads the scene with the next buildIndex based on the buildIndex of the ActiveScene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Loads the End Scene.
    /// </summary>
    public void LoadEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    /// <summary>
    /// Loads the MainMenu Scene.
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Loads the Instructions Scene.
    /// </summary>
    public void LoadInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    /// <summary>
    /// Quits the game application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
