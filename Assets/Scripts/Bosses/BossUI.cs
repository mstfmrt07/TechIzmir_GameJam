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

        UpdateUI(0, 0);
    }

    public void UpdateUI(int deltaHP, int deltaMana)
    {
        nameText.text = boss.Data.bossName;
        hpStat.UpdateUI(boss.CurrentHP.ToString(), deltaHP == 0 ? "" : "-" + deltaHP.ToString());
        manaStat.UpdateUI(boss.CurrentMana.ToString(), deltaMana == 0 ? "" : "-" + deltaMana.ToString());
    }

}
