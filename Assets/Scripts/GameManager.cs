using System;
using UnityEngine;
using AudioManager;

public enum GameState { Menu, Playing, Paused, GameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action OnGameStart;
    public event Action OnGamePaused;
    public event Action OnGameResumed;
    public event Action OnGameOver;

    public GameState State { get; private set; } = GameState.Menu;

    [Header("Game Settings")]
    public float GameSpeed = 5f;

    [Header("Difficulty")]
    [SerializeField] private float speedIncreaseRate = 0.1f;
    [SerializeField] private float maxSpeed = 15f;

    private float initialSpeed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        initialSpeed = GameSpeed;
    }

    private void Update()
    {
        if (State != GameState.Playing) return;

        GameSpeed = Mathf.Min(GameSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);
    }

    public void StartGame()
    {
        if (State != GameState.Menu) return;

        State = GameState.Playing;
        GameSpeed = initialSpeed;
        OnGameStart?.Invoke();
    }

    public void TriggerGameOver()
    {
        if (State != GameState.Playing) return;

        State = GameState.GameOver;
        OnGameOver?.Invoke();
    }

    public void TogglePause()
    {
        if (State == GameState.Playing)
            PauseGame();
        else if (State == GameState.Paused)
            ResumeGame();
    }

    public void PauseGame()
    {
        if (State != GameState.Playing) return;

        State = GameState.Paused;
        Time.timeScale = 0f;
        OnGamePaused?.Invoke();
    }

    public void ResumeGame()
    {
        if (State != GameState.Paused) return;

        State = GameState.Playing;
        Time.timeScale = 1f;
        OnGameResumed?.Invoke();
    }

}
