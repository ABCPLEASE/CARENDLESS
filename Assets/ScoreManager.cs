using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text player1ScoreText;
    public Text player2ScoreText;

    private float player1Score = 0f;
    private float player2Score = 0f;

    [Header("Overtime Score Increase")]
    public float overtimeScoreRate = 1f;  // How much score to increase per second
    public float overtimeScoreCap = 100f; // Cap for overtime score increase

    void Start()
    {
        // Initialize the scores if necessary
        player1Score = 0f;
        player2Score = 0f;
        UpdateScoreUI();
    }

    void Update()
    {
        // Increase the score over time for both players
        if (player1Score < overtimeScoreCap)
        {
            player1Score += overtimeScoreRate * Time.deltaTime;
        }

        if (player2Score < overtimeScoreCap)
        {
            player2Score += overtimeScoreRate * Time.deltaTime;
        }

        UpdateScoreUI();
    }

    // Method to update the score display
    void UpdateScoreUI()
    {
        if (player1ScoreText != null)
            player1ScoreText.text = "Player 1 Score: " + Mathf.FloorToInt(player1Score);

        if (player2ScoreText != null)
            player2ScoreText.text = "Player 2 Score: " + Mathf.FloorToInt(player2Score);
    }

    // Method to add score for Player 1
    public void AddScoreForPlayer1(float scoreToAdd)
    {
        player1Score += scoreToAdd;
        UpdateScoreUI();
    }

    // Method to add score for Player 2
    public void AddScoreForPlayer2(float scoreToAdd)
    {
        player2Score += scoreToAdd;
        UpdateScoreUI();
    }
}
