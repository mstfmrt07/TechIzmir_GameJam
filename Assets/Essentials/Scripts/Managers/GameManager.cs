using System;
using UnityEngine;

public class GameManager : MSingleton<GameManager>
{
    private bool isGamePlaying;
    private float gameFlowSpeed = 1f;
    private int score;
    private bool highScoreBeaten;

    public bool IsGamePlaying => isGamePlaying;
    public int Score => score;
    public int HighScore => SaveManager.Instance.HighScore;

    public Action OnHighScoreBeaten;

    private void Start()
    {
        LoadGame();
    }

    private void LoadGame()
    {
        if (isGamePlaying)
            return;

        score = 0;
        highScoreBeaten = false;

        GameEvents.OnGameLoad?.Invoke();
    }

    public void StartGame()
    {
        if (isGamePlaying)
            return;

        gameFlowSpeed = 1f;
        isGamePlaying = true;

        GameEvents.OnGameStarted?.Invoke();
    }

    private void Update()
    {
        if (!isGamePlaying)
            return;

        if (score > HighScore && HighScore > 0 && !highScoreBeaten)
        {
            highScoreBeaten = true;
            OnHighScoreBeaten?.Invoke();
        }
    }

    public void FinishGame()
    {
        if (!isGamePlaying)
            return;

        isGamePlaying = false;

        GameEvents.OnGameFailed?.Invoke();
    }

    public void RecoverGame()
    {
        //Todo implement Recover game
        if (isGamePlaying)
            return;

        isGamePlaying = true;

        GameEvents.OnGameRecovered?.Invoke();
    }

    public void RestartGame()
    {
        isGamePlaying = false;
        gameFlowSpeed = 1f;

        if (score > HighScore)
            SaveManager.Instance.HighScore = score;

        RecycleBin.Instance.DisposeAll();
        LoadGame();
    }
}
