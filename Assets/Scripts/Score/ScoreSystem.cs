using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public int currentScore { get; private set; }
    public static ScoreSystem instance;

    public static Action<int> OnScoreUpdated;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        Coins.OnCoinCollected += UpdateScore;
    }

    private void OnDisable()
    {
        Coins.OnCoinCollected -= UpdateScore;
    }

    private void UpdateScore(Coins coin)
    {
        currentScore += coin.value;
        OnScoreUpdated?.Invoke(currentScore);
    }
    public void ResetScore()
    {
        currentScore = 0;
    }
}
