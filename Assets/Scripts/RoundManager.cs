
public class RoundManager : MSingleton<RoundManager>
{
    private IRoundPlayer player;
    private IRoundPlayer enemy;

    private IRoundPlayer currentPlayer;

    public void StartRound(IRoundPlayer player, IRoundPlayer enemy)
    {
        this.player = player;
        this.enemy = enemy;

        player.StartFight(enemy);
        enemy.StartFight(player);

        GiveTurn(player);
        WarningMessage.Instance.Show($"Round Started!");
    }

    public void GiveTurn(IRoundPlayer roundPlayer)
    {
        currentPlayer = roundPlayer;
        currentPlayer.TakeTurn();
    }
}
