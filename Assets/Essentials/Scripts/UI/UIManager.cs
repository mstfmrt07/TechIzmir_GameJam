using System.Collections;
using UnityEngine;

public class UIManager : MSingleton<UIManager>, IGameEventsHandler
{
    [Header("References")]
    public StartScreen startScreen;
    public GameScreen gameScreen;
    public LoseScreen loseScreen;

    [Header("Values")]
    public float loseScreenDelay;

    private UIScreen currentScreen;

    private void Awake()
    {
        SubscribeGameEvents();
    }

    public void SubscribeGameEvents()
    {
        GameEvents.OnLevelLoaded += OnLevelLoaded;
        GameEvents.OnLevelStarted += OnLevelStarted;
        GameEvents.OnLevelFailed += OnLevelFailed;
        GameEvents.OnLevelSucceeded += OnLevelSucceeded;
    }

    public void SwitchScreen(UIScreen Screen)
    {
        if (currentScreen != null)
            CloseScreen(currentScreen);

        LoadScreen(Screen);
        currentScreen = Screen;
    }

    public void LoadScreen(UIScreen Screen)
    {
        Screen.Load();
    }

    public void ResetScreen(UIScreen Screen)
    {
        Screen.Reset();
    }

    public void CloseScreen(UIScreen Screen)
    {
        if (Screen == currentScreen)
            currentScreen = null;

        Screen.Close();
    }

    public void ResetAllScreens()
    {
        var screens = FindObjectsOfType<UIScreen>();

        foreach (var screen in screens)
            ResetScreen(screen);
    }

    public void CloseAllScreens()
    {
        var screens = FindObjectsOfType<UIScreen>();

        foreach (var screen in screens)
            CloseScreen(screen);
    }

    public IEnumerator SwitchScreenAfterSeconds(UIScreen screen, float duration)
    {
        yield return new WaitForSeconds(duration);
        SwitchScreen(screen);
    }

    public void OnLevelLoaded()
    {
        ResetAllScreens();
        CloseAllScreens();

        SwitchScreen(startScreen);
    }

    public void OnLevelStarted()
    {
        SwitchScreen(gameScreen);
    }

    public void OnLevelFailed()
    {
        StartCoroutine(SwitchScreenAfterSeconds(loseScreen, loseScreenDelay));
    }

    public void OnLevelSucceeded()
    {
        SwitchScreen(gameScreen);
    }
}