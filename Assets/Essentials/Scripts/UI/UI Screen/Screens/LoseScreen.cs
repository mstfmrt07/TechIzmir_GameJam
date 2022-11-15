
public class LoseScreen : UIScreen
{
    public override void Load()
    {
        base.Load();
    }

    public override void Reset()
    {
        base.Reset();
    }

    public override void Close()
    {
        base.Close();
    }

    public void RestartAction()
    {
        GameManager.Instance.RestartGame();
    }
}
