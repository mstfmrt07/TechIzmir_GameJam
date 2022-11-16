using System;
using UnityEngine;

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

        GameEvents.OnLevelStarted?.Invoke();
    }

    private void Update()
    {
        if (!isGamePlaying)
            return;
    }

    public void FinishGame()
    {
        if (!isGamePlaying)
            return;

        isGamePlaying = false;

        GameEvents.OnLevelFailed?.Invoke();
    }

    public void RestartGame()
    {
        RecycleBin.Instance.DisposeAll();
        LoadGame();
    }
}
