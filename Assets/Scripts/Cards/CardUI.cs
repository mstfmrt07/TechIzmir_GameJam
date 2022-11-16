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
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI manaText;
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
        armorText.text = card.CurrentHP.ToString();
        damageText.text = card.Data.damage.ToString();
        manaText.text = card.Data.requiredMana.ToString();
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
