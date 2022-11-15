public interface IScreen
{
    bool IsVisible { get; }

    void Load();

    void Reset();

    void Close();
}
