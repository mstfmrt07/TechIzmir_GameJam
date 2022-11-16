using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MSingleton<CardManager>
{
    public Card cardPrefab;
    public List<CardData> cardDatas;

    private List<CardData> drawedCards = new List<CardData>();

    public List<CardData> DrawCards(int count)
    {
        List<CardData> cards = new List<CardData>();

        for (int i = 0; i < count; i++)
        {
            var currentCard = cardDatas[Random.Range(0, cardDatas.Count)];
            cards.Add(currentCard);
        }

        return cards;
    }
}
