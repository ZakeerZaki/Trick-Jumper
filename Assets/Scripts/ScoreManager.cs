using UnityEngine;
using TMPro;  // Make sure to add the TextMeshPro namespace

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component
    private int score = 0;            // Initial score

    void Start()
    {
        UpdateScoreText();            // Initialize the score display
    }

    // Call this method to add points to the score
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // Call this method to reset the score to 0
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    // Update the score text UI
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
