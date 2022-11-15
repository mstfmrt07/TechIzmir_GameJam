using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MSingleton<Player>
{
    public Stat hp;
    public Stat armor;
    public Stat mana;
    public Stat damage;
    public CardDeck cardDeck;

    private void Awake()
    {
        cardDeck.ShuffleCards();
    }

    public void PlayCard(Card card)
    {
        cardDeck.PlayCard(card);
    }
}
