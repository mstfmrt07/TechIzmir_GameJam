using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private Player player;
    public StatUI hpStat;
    public StatUI manaStat;
    public StatUI armorStat;
    public StatUI damageStat;

    public void Initialize()
    {
        player = Player.Instance;
        player.OnPlayerUpdated += UpdateUI;

        UpdateUI();
    }

    public void UpdateUI()
    {
        hpStat.UpdateUI(player.CurrentHP.ToString());
        manaStat.UpdateUI(player.CurrentMana.ToString());
        armorStat.UpdateUI(player.CurrentArmor.ToString());
        damageStat.UpdateUI(player.CurrentDamage.ToString());
    }
}
