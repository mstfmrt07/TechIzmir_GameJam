using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerDownHandler
{
    private Card card;
    public Image cardImage;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI nameText;

    public void Initialize(Card card)
    {
        this.card = card;
        card.OnGetDamage += UpdateUI;
        card.OnDestroy += OnDestroyUI;
        UpdateUI(card, 0);
    }

    public void UpdateUI(Card card, int damage)
    {
        cardImage.sprite = card.Data.cardIcon;
        nameText.text = card.Data.cardName;
        descriptionText.text = card.Data.description;
        armorText.text = card.CurrentHP.ToString();
        damageText.text = card.Data.damage.ToString();
        manaText.text = card.Data.requiredMana.ToString();
    }

    public void OnDestroyUI()
    {
        card.OnGetDamage -= UpdateUI;
        card.OnDestroy -= OnDestroyUI;
    }

    public void InspectCard()
    {
        CardInspectorUI.Instance.InspectCard(this);
    }

    public void PlayCard()
    {
        //TODO implement play card
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InspectCard();
    }
}
