using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour
{
    // This method is called when the Play button is pressed
    public void PlayGame()
    {
        SceneManager.LoadScene("Endless");
    }

    // This method is called when the Quit button is pressed
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();

        // This will only show in the Editor
        Debug.Log("Quit Game pressed.");
    }
}