using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private Player player;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI manaText;

    public void Initialize()
    {
        player = Player.Instance;
        player.OnPlayerUpdated += UpdateUI;

        UpdateUI();
    }

    public void UpdateUI()
    {
        hpText.text = player.CurrentHP.ToString();
        hpText.text = player.CurrentMana.ToString();
    }
}
