using TMPro;
using UnityEngine;

public class BossUI : MonoBehaviour
{
    public Boss boss;

    public StatUI hpStat;
    public StatUI manaStat;
    public TextMeshProUGUI nameText;

    public void Initialize(Boss boss)
    {
        this.boss = boss;
        boss.OnBossUpdated += UpdateUI;

        UpdateUI();
    }

    public void UpdateUI()
    {
        nameText.text = boss.Data.bossName;
        hpStat.UpdateUI(boss.CurrentHP.ToString());
        manaStat.UpdateUI(boss.CurrentMana.ToString());
    }

}
