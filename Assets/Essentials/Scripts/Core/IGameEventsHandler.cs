public interface IGameEventsHandler
{
    void SubscribeGameEvents();
    void OnLevelLoaded();
    void OnLevelStarted();
    void OnLevelFailed();
    void OnLevelSucceeded();
}