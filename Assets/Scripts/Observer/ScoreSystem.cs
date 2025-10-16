using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public Subject target;

    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    void Start()
    {
        // Subscribe to the event
        target.OnTargetHit += AddScore;

        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score Updated, total score: " + score);

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
}
