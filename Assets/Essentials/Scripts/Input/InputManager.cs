using UnityEngine;

public class InputManager : MSingleton<InputManager>, IGameEventsHandler
{
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

    public void OnLevelLoaded()
    {
    }

    public void OnLevelStarted()
    {
    }

    public void OnLevelFailed()
    {
    }

    public void OnLevelSucceeded()
    {
    }
}
