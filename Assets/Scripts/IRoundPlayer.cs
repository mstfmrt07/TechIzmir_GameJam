public interface IRoundPlayer
{
    public void StartFight(IRoundPlayer enemy);
    public void TakeTurn();
    public void GiveTurn();
}