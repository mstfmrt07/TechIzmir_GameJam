using TMPro;
using UnityEngine;

public class CardWorldUI : MonoBehaviour
{
    private Card card;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI damageText;

    public void Initialize(Card card)
    {
        this.card = card;
        card.OnGetDamage += UpdateUI;

        UpdateUI(card, 0);
    }

    public void UpdateUI(Card card, int damage)
    {
        hpText.text = card.CurrentHP.ToString();
        damageText.text = card.Data.damage.ToString();
    }
}
