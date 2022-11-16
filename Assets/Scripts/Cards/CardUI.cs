using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerUpHandler
{
    [Header("References")]
    public DraggableUI cardDragger;
    public GameObject ghostCard;

    private Card card;
    public Image cardImage;
    public StatUI manaStat;
    public StatUI armorStat;
    public StatUI damageStat;

    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI nameText;

    public void Initialize(Card card)
    {
        this.card = card;
        card.OnGetDamage += UpdateUI;
        card.OnDestroy += DestroyUI;
        card.OnCardPlayed += DestroyUI;

        cardDragger.IsActive = true;
        cardDragger.OnDragEnded += PlayCard;
        cardDragger.OnDragStarted += OnDragStarted;
        cardDragger.OnDragCancelled += OnDragCancelled;

        UpdateUI(card, 0);
    }

    public void UpdateUI(Card card, int damage)
    {
        cardImage.sprite = card.Data.cardIcon;
        nameText.text = card.Data.cardName;
        descriptionText.text = card.Data.description;

        armorStat.UpdateUI(card.CurrentHP.ToString());
        damageStat.UpdateUI(card.Data.damage.ToString());
        manaStat.UpdateUI(card.Data.requiredMana.ToString());
    }

    public void DestroyUI()
    {
        card.OnGetDamage -= UpdateUI;
        card.OnDestroy -= DestroyUI;
        card.OnCardPlayed -= DestroyUI;

        cardDragger.IsActive = false;
        cardDragger.OnDragEnded -= PlayCard;
        cardDragger.OnDragStarted -= OnDragStarted;
        cardDragger.OnDragCancelled -= OnDragCancelled;

        Destroy(gameObject);
    }

    public void InspectCard()
    {
        if (cardDragger.ExceededDragTimeThreshold)
            return;

        CardInspectorUI.Instance.InspectCard(this);
    }

    public void PlayCard()
    {
        if (Player.Instance.AttemptPlayCard(card))
        {
            DestroyUI();
        }
        else
        {
            cardDragger.CancelDrag();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //if (cardDragger.IsDragging)
        //    return;

        //InspectCard();
    }

    public void OnDragStarted()
    {
    }

    public void OnDragCancelled()
    {
    }
}
