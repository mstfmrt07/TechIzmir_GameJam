using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossUI : MonoBehaviour
{
    public Boss boss;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI nameText;

    public void Initialize(Boss boss)
    {
        this.boss = boss;
        boss.OnGetDamage += UpdateUI;

        UpdateUI(0);
    }

    public void UpdateUI(int damage)
    {
        nameText.text = boss.Data.bossName;
        hpText.text = boss.CurrentHP.ToString();
    }

}
