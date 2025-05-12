using UnityEngine;

public class Coin : MonoBehaviour
{
    public float scoreValue = 10f;  // How much score the coin gives

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.name);  // Log which object is triggering the coin

        if (other.CompareTag("Player1"))
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScoreForPlayer1(scoreValue);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player2"))
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScoreForPlayer2(scoreValue);
            }
            Destroy(gameObject);
        }
    }
}

