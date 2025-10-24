using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    void Start()
    {

        UpdateScoreUI();
    }

    // Called when a new target is spawned
    public void SubscribeToTarget(Subject target)
    {
        target.OnTargetHit += AddScore;
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

    public void Save_Score()
    {
        SaveScore.SavingScore(score); ;
        Debug.Log("Score saved.");
    }

    public void Load_Score()
    {
        score = SaveScore.LoadScore();
        UpdateScoreUI();
        Debug.Log("Score loaded: " + score);
    }
}
