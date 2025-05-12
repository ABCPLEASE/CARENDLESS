using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;  // Reference to the Game Over UI Panel

    // Called when both players die or the game ends
    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);  // Show Game Over Panel
    }

    // Replay the game (reload the current scene)
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
    }

    // Go back to the main menu (replace "MainMenu" with the actual name of your main menu scene)
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Endless CAR");
    }

    // Quit the game (works in the build, not in the editor)
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting.");
    }
}