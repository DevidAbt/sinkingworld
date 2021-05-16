using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public Text scoreText;
    public Text lastScoreText;
    public Text bestScoreText;
    void Start()
    {
        score = 0;
        if (lastScoreText != null)
        {
            lastScoreText.text = PlayerPrefs.GetInt("lastScore", 0).ToString();
            bestScoreText.text = PlayerPrefs.GetInt("maxScore", 0).ToString();
        }
    }

    public void addScore(int plusScore)
    {
        score += plusScore;
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void updateLastScore()
    {
        PlayerPrefs.SetInt("lastScore", score);
        int maxScore = PlayerPrefs.GetInt("maxScore", 0);
        if (score > maxScore)
        {
            PlayerPrefs.SetInt("maxScore", score);
        }
    }
}
