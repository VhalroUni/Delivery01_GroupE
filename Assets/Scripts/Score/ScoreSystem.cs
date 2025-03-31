using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public int currentScore { get; private set; }
    private int targetScore = 0;
    private float timeBetweenPoints = 0;
    public static ScoreSystem instance;
    private float minTimeBetweenPoints = 0.10f; // Tiempo mínimo entre puntos
    private float maxTimeBetweenPoints = 0.35f;

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
            float progress = (float)currentScore / targetScore; // Progreso entre 0 y 1
            float dynamicTimeBetweenPoints = Mathf.Lerp(minTimeBetweenPoints, maxTimeBetweenPoints, 1 - progress);

            timeBetweenPoints += Time.deltaTime;

            if (timeBetweenPoints >= dynamicTimeBetweenPoints)
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
