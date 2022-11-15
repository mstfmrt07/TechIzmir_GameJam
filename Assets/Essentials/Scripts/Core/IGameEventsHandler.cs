public interface IGameEventsHandler
{
    void SubscribeGameEvents();
    void OnGameLoad();
    void OnGameStarted();
    void OnGameFailed();
    void OnGameRecovered();
}