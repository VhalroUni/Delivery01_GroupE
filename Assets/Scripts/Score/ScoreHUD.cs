using UnityEngine;
using UnityEngine.UI;

public class ScoreHUD : MonoBehaviour
{
    private Text scoreText;
    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }
    private void OnEnable()
    {
        ScoreSystem.OnScoreUpdated += UpdateScoreText;
    }
    private void OnDisable()
    {
        ScoreSystem.OnScoreUpdated -= UpdateScoreText;
    }
    private void UpdateScoreText(int score)
    {
        scoreText.text = "SCORE: " + score.ToString();
    }


}
