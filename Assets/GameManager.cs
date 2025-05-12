using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool player1Dead = false;
    private bool player2Dead = false;

    public GameOverManager gameOverManager;

    public void PlayerDied(PlayerID id)
    {
        Debug.Log("PlayerDied called: " + id);

        if (id == PlayerID.Player1)
        {
            player1Dead = true;
            Debug.Log("player1Dead = true");
        }
        else if (id == PlayerID.Player2)
        {
            player2Dead = true;
            Debug.Log("player2Dead = true");
        }

        CheckGameOver();
    }

    void CheckGameOver()
    {
        Debug.Log($"Checking Game Over: player1Dead={player1Dead}, player2Dead={player2Dead}");

        if (player1Dead && player2Dead)
        {
            Debug.Log("Both players dead - Showing Game Over UI");
            gameOverManager.ShowGameOver();
        }
    }
}
