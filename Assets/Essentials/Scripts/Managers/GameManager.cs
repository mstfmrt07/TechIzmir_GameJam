using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MSingleton<GameManager>
{
    public int maxCardsOnDeck;
    private bool isGamePlaying;

    public bool IsGamePlaying => isGamePlaying;

    private void Start()
    {
        LoadGame();
    }

    private void LoadGame()
    {
        if (isGamePlaying)
            return;

        LevelManager.Instance.LoadCurrentLevel();

        GameEvents.OnLevelLoaded?.Invoke();
    }

    public void StartGame()
    {
        if (isGamePlaying)
            return;

        isGamePlaying = true;
        RoundManager.Instance.StartRound(Player.Instance, LevelManager.Instance.CurrentLevel.boss);
        Player.Instance.OnDestroy += FailGame;
        LevelManager.Instance.CurrentLevel.boss.OnDestroy += WinGame;

        GameEvents.OnLevelStarted?.Invoke();
    }

    private void Update()
    {
        if (!isGamePlaying)
            return;
    }

    public void FailGame()
    {
        if (!isGamePlaying)
            return;

        isGamePlaying = false;

        GameEvents.OnLevelFailed?.Invoke();
    }

    public void WinGame()
    {
        if (!isGamePlaying)
            return;

        isGamePlaying = false;

        GameEvents.OnLevelSucceeded?.Invoke();
    }

    public void RestartGame()
    {
        RecycleBin.Instance.DisposeAll();
        //TODO Implement restart game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
