using TMPro;
using UnityEngine;

public class Observer: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score Updated, total score: " + score);
    }
}