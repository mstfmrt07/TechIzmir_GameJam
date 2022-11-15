using UnityEngine;

public class InputManager : MSingleton<InputManager>, IGameEventsHandler
{
    private void Awake()
    {
        SubscribeGameEvents();
    }

    public void SubscribeGameEvents()
    {
        GameEvents.OnGameLoad += OnGameLoad;
        GameEvents.OnGameStarted += OnGameStarted;
        GameEvents.OnGameFailed += OnGameFailed;
        GameEvents.OnGameRecovered += OnGameRecovered;
    }

    public void OnGameLoad()
    {
    }

    public void OnGameStarted()
    {
    }

    public void OnGameFailed()
    {
    }

    public void OnGameRecovered()
    {
    }
}
