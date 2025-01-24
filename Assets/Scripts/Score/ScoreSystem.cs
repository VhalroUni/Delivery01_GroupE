using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private int currentScore;

    public static Action<int> OnScoreUpdated;

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
        currentScore += coin.Value;
        OnScoreUpdated?.Invoke(currentScore);
    }
}
