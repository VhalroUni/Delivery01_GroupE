using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public int currentScore { get; private set; }
    private int targetScore = 0;
    private float timeBetweenPoints = 0;
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
        targetScore = currentScore + coin.value;
    }

    private void Update()
    {
        if (currentScore < targetScore) 
        {
            timeBetweenPoints += Time.deltaTime;

            if (timeBetweenPoints >= 0.20f) 
            {
                currentScore += 1;
                OnScoreUpdated?.Invoke(currentScore);
                timeBetweenPoints = 0;
            }
        }
    }
    public void ResetScore()
    {
        currentScore = 0;
        targetScore = 0;
        OnScoreUpdated?.Invoke(currentScore);
    }
}
