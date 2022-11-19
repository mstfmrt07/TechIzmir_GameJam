
public class RoundManager : MSingleton<RoundManager>, IResettable
{
    private IRoundPlayer currentPlayer;

    public void StartRound(IRoundPlayer player, IRoundPlayer enemy)
    {
        player.StartFight(enemy);
        enemy.StartFight(player);

        GiveTurn(player);
    }

    public void GiveTurn(IRoundPlayer roundPlayer)
    {
        currentPlayer = roundPlayer;
        currentPlayer.TakeTurn();
    }

    public void ApplyReset()
    {
        currentPlayer = null;
    }
}
