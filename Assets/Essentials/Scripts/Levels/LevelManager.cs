using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MSingleton<LevelManager>
{
    public Transform levelContainer;
    public List<Level> levels;

    private Level currentLevel = null;

    private int CurrentLevelIndex => CurrentLevelNumber % levels.Count;
    public int CurrentLevelNumber
    {
        get => SaveManager.Instance.CurrentLevel;
        set => SaveManager.Instance.CurrentLevel = value;
    }

    public Level CurrentLevel => currentLevel;

    private void Start()
    {
        LoadCurrentLevel();
    }

    public void LoadLevel(int levelNumber)
    {
        CurrentLevelNumber = levelNumber;

        if (currentLevel != null)
        {
            ResetCurrentLevel();
        }

        currentLevel = Instantiate(levels[CurrentLevelIndex], levelContainer);
        GameEvents.OnGameLoad?.Invoke();
    }

    public void LoadCurrentLevel()
    {
        LoadLevel(CurrentLevelNumber);
    }

    public void LoadNextLevel()
    {
        LoadLevel(CurrentLevelNumber + 1);
    }

    private void ResetCurrentLevel()
    {
        currentLevel.Dispose();
        RecycleBin.Instance.DisposeAll();
    }
}
