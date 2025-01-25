using UnityEngine;
using UnityEngine.UI;

public class ScoreEnding : MonoBehaviour
{
    private Text scoreText;
    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }
    private void Start()
    {
        scoreText.text = "FINAL \n SCORE \n" + ScoreSystem.instance.currentScore;
    }
}
