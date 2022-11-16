using UnityEngine;

public class InputManager : MSingleton<InputManager>, IGameEventsHandler, IResettable
{
    public TapDetector tapToStart;

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
        tapToStart.IsActive = true;
        tapToStart.OnTap += GameManager.Instance.StartGame;
    }

    public void OnLevelStarted()
    {
        tapToStart.OnTap -= GameManager.Instance.StartGame;
        tapToStart.IsActive = false;
    }

    public void OnLevelFailed()
    {
    }

    public void OnLevelSucceeded()
    {
    }

    public void ApplyReset()
    {
        tapToStart.OnTap -= GameManager.Instance.StartGame;
        tapToStart.IsActive = false;
    }
}
