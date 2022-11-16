using System;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public Transform cardContainer;
    [SerializeField] private List<Card> cards;

    public List<Card> Cards => cards;

    public Action OnDeckUpdated;

    public void DrawCard()
    {
        Debug.Log("Player draws card...");

        //Clear current cards
        foreach (var card in cards)
        {
            card.DestroyCard();
        }
        cards.Clear();

        var drawedCards = CardManager.Instance.DrawCards(GameManager.Instance.maxCardsOnDeck);

        foreach (var cardData in drawedCards)
        {
            var currentCard = Instantiate(CardManager.Instance.cardPrefab, cardContainer);
            currentCard.InitializeCard(cardData);
            AddCard(currentCard);
        }

        OnDeckUpdated?.Invoke();
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
