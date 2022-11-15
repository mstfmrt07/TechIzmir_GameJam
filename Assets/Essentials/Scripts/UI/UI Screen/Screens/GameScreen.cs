

public class GameScreen : UIScreen
{
    public CardDeckUI cardDeckUI;

    public override void Load()
    {
        base.Load();
        this.Wait(0.5f, () => cardDeckUI.InitializeCards());
    }

    public override void Reset()
    {
        base.Reset();
    }

    public override void Close()
    {
        base.Close();
        cardDeckUI.ClearCards();
    }
}
