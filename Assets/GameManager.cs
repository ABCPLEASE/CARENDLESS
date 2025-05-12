using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool player1Dead = false;
    private bool player2Dead = false;

    public void PlayerDied(PlayerID id)
    {
        if (id == PlayerID.Player1)
            player1Dead = true;
        else
            player2Dead = true;

        CheckGameOver();
    }

    void CheckGameOver()
    {
        if (player1Dead && !player2Dead)
            Debug.Log("Player 2 wins!");
        else if (player2Dead && !player1Dead)
            Debug.Log("Player 1 wins!");
        else if (player1Dead && player2Dead)
            Debug.Log("Draw!");
    }
}
