using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public Transform cardContainer;
    [SerializeField] private List<Card> cards;

    public void ShuffleCards()
    {
        //Clear current cards
        cards.Clear();
        var drawedCards = CardManager.Instance.DrawCards(GameManager.Instance.maxCardsOnDeck);

        foreach (var cardData in drawedCards)
        {
            var currentCard = Instantiate(CardManager.Instance.cardPrefab, cardContainer);
            currentCard.InitializeCard(cardData);
            AddCard(currentCard);
        }
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public void PlayCard(Card card)
    {
        if (!cards.Contains(card))
            return;

        card.Play();
        cards.Remove(card);
    }

    public void RemoveCard(Card card)
    {
        if (!cards.Contains(card))
            return;

        //TODO
        cards.Remove(card);
        Destroy(card.gameObject);
    }
}
