using System;
using UnityEngine;

public class GameManager : MSingleton<GameManager>
{
    public int maxCardsOnDeck;
    private bool isGamePlaying;
    public Boss sampleBoss;

    public bool IsGamePlaying => isGamePlaying;

    private void Start()
    {
        LoadGame();
    }

    private void LoadGame()
    {
        if (isGamePlaying)
            return;

        //TODO find a way to start fight
        Player.Instance.StartFight(sampleBoss);
        sampleBoss.StartFight(Player.Instance);

        GameEvents.OnLevelLoaded?.Invoke();
    }

    public void StartGame()
    {
        if (isGamePlaying)
            return;

        isGamePlaying = true;

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
