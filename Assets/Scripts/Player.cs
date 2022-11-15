using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MSingleton<Player>
{
    public int hp;
    public int mana;
    public CardDeck cardDeck;
    public Transform tableCardsContainer;
    private List<Card> cardsOnTable;

    private Boss currentBoss;

    public List<Card> CardsOnTable => cardsOnTable;

    private void Awake()
    {
        cardDeck.ShuffleCards();
    }

    public void StartFight(Boss boss)
    {
        currentBoss = boss;
    }

    public void PlayCard(Card card)
    {
        Debug.Log($"{gameObject.name}, plays the card {card.Data.name}.");
        cardDeck.PlayCard(card);

        cardsOnTable.Add(card);
        card.transform.parent = tableCardsContainer;
    }

    public void GetDamage(Card card, int damage)
    {
        if (card == null)
        {
            hp -= damage;
            Debug.Log($"{gameObject.name}, takes {damage} damage.");
        }
        else
        {
            card.GetDamage(damage);
            Debug.Log($"{card.Data.cardName}, takes {damage} damage.");
        }
    }

    public void Attack(int damage)
    {
        Debug.Log($"{gameObject.name}, deals {damage} damage to {currentBoss.Data.name}.");
        currentBoss.GetDamage(damage);
    }

    public void SpendMana(int amount)
    {
        if (mana - amount >= 0)
        {
            mana -= amount;
        }
    }
}
