using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public event Action<int> OnScoreChanged;

    public int CurrentScore { get; private set; }
    public int HighScore { get; private set; }

    private const string HighScoreKey = "HighScore";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        HighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    private void Start()
    {
        GameManager.Instance.OnGameStart += ResetScore;
        GameManager.Instance.OnGameOver += SaveHighScore;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.OnGameStart -= ResetScore;
        GameManager.Instance.OnGameOver -= SaveHighScore;
    }

    public void AddScore(int amount)
    {
        CurrentScore += amount;
        OnScoreChanged?.Invoke(CurrentScore);

        if (CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
        }
    }

    private void ResetScore()
    {
        CurrentScore = 0;
        OnScoreChanged?.Invoke(CurrentScore);
    }

    private void SaveHighScore()
    {
        if (CurrentScore <= PlayerPrefs.GetInt(HighScoreKey, 0)) return;

        PlayerPrefs.SetInt(HighScoreKey, CurrentScore);
        PlayerPrefs.Save();
    }
}
